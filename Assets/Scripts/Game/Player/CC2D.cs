using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CC2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public float _damage = 2f;

	public PlayerStats pStats = new PlayerStats();
	public float xpNeededToLvlUp = 10;
	private float xpNeedMultiplier = 1.5f;

	private HealthBar healthBar;
	private XpBar xpBar;
	private Animator playerAnim;
	private LevelUp levelUp;

	private bool isAttacking = false;
	private bool finishedAttacking = true;
	private bool didFlip = true;

	private float _fireRate = 1f;

	private CapsuleCollider2D weaponCollider;

	private AudioSource playerAs;

	public bool facingRight { get => m_FacingRight;}
    public float fireRate { get => _fireRate; set => _fireRate = value; }

    private void Awake()
	{
		Init();
	}

    private void Start()
    {
		healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
		levelUp = this.gameObject.GetComponent<LevelUp>();
		healthBar.SetActive(false);
		xpBar = GameObject.Find("XpBar").GetComponent<XpBar>();
	}

    private void FixedUpdate()
	{
	}

	private void Update()
	{
		playerAttack();
	}

    private void Init()
    {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		weaponCollider = this.GetComponent<CapsuleCollider2D>();
		playerAnim = this.gameObject.GetComponent<Animator>();
		playerAnim.SetFloat("atkAnimSpeed", pStats.attackSpeed);
		playerAs = this.GetComponent<AudioSource>();
		PlayerPrefs.SetInt("EnemiesKilled", 0);
		UpdateCounterCanvas();
	}

    public void Move(float moveX, float moveY)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveX * 10f * pStats.movementSpeed, moveY * 10f * pStats.movementSpeed);
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (moveX > 0 && !facingRight || !didFlip)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (moveX < 0 && facingRight || !didFlip)
		{
			// ... flip the player.
			Flip();
		}
	}

	void playerAttack()
	{
		if (finishedAttacking)
		{
			StartCoroutine(playerAttackLogic());
		}
	}

	IEnumerator playerAttackLogic()
	{
		startAtk();
		yield return new WaitForSeconds(1 / pStats.attackSpeed);
		stopAtk();
		yield return new WaitForSeconds(_fireRate);
		finishedAttacking = true;
	}

	private void startAtk()
	{
		playerAs.PlayOneShot(playerAs.clip, .3f);
		finishedAttacking = false;
		playerAnim.SetBool("isAttacking", true);
		isAttacking = true;
		weaponCollider.enabled = true;
	}

	private void stopAtk()
	{
		tryLvlUp();
		weaponCollider.enabled = false;
		playerAnim.SetBool("isAttacking", false);
		isAttacking = false;
	}

	private void tryLvlUp()
	{
		if (pStats.playerXp >= xpNeededToLvlUp)
		{
			LvlUp();
		}
	}

	private void LvlUp()
	{
		do
		{
			pStats.playerXp -= xpNeededToLvlUp;
			pStats.playerLvl++;
			xpNeededToLvlUp *= xpNeedMultiplier;
			levelUp.SetActive();
		} while (pStats.playerXp > xpNeededToLvlUp);

		xpBar.SetMaxValue(xpNeededToLvlUp);
		xpBar.SetValue(pStats.playerXp);

		playerAnim.SetFloat("atkAnimSpeed", pStats.attackSpeed);
		UpdateCounterCanvas();
	}

	private void Flip()
	{
		didFlip = false;
		if (!isAttacking)
		{
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;
			// Multiply the player's x local scale by -1.
			didFlip = true;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

	public void TakeDamage(float _damage)
    {
		pStats.playerHealth -= _damage;
		UpdateHealthBar(pStats.playerHealth);
		if(pStats.playerHealth <= 0)
		{
			if(PlayerPrefs.GetInt("EnemiesKilled") > PlayerPrefs.GetInt("BestScore"))
			{
				PlayerPrefs.SetInt("BestScore", PlayerPrefs.GetInt("EnemiesKilled"));
				UpdateCounterCanvas();
			}

			GameObject.Find("GameHandler").GetComponent<GameHandler>().deathCanvas.SetActive(true);
			Destroy(this.gameObject);
			Destroy(healthBar.gameObject);
		}
	}

	public void UpdateCounterCanvas()
	{

		GameObject.Find("foesKilled").GetComponent<Text>().text = $"Kills : {PlayerPrefs.GetInt("EnemiesKilled")} (Best: {PlayerPrefs.GetInt("BestScore")})";
		GameObject.Find("levelDisplay").GetComponent<Text>().text = $"Level {pStats.playerLvl}";
	}

	private void UpdateHealthBar(float value)
    {
        if (!healthBar) { healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>(); };
		healthBar.SetValue(value);
	}

	public void addXp(int xpAdded)
	{
		pStats.playerXp += xpAdded;
		xpBar.SetValue(pStats.playerXp);
	}
}