using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CC2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public float damage = 2f;

	public PlayerStats pStats = new PlayerStats();
	private HealthBar healthBar;
	private Animator playerAnim;

	private bool isAttacking = false;
	private bool finishedAttacking = true;
	private bool didFlip = true;

	private float fireRate = 1f;
	private float atkAnimSpeed = 2f;

	private CapsuleCollider2D weaponCollider;

	private AudioSource playerAs;

	public bool FacingRight { get => m_FacingRight;}
	public float AtkAnimSpeed { get => atkAnimSpeed;}

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		weaponCollider = this.GetComponent<CapsuleCollider2D>();
		playerAnim = this.gameObject.GetComponent<Animator>();
		playerAnim.SetFloat("atkAnimSpeed", AtkAnimSpeed);
		playerAs = this.GetComponent<AudioSource>();
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
		playerAttack();
	}


	public void Move(float moveX, float moveY)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveX * 10f * pStats.MovementSpeed, moveY * 10f * pStats.MovementSpeed);
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (moveX > 0 && !FacingRight || !didFlip)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (moveX < 0 && FacingRight || !didFlip)
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
		yield return new WaitForSeconds(1 / atkAnimSpeed);
		stopAtk();
		yield return new WaitForSeconds(fireRate);
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
		weaponCollider.enabled = false;
		playerAnim.SetBool("isAttacking", false);
		isAttacking = false;
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

	public void TakeDamage(float damage)
    {
		pStats.PlayerHealth -= damage;
		UpdateHealthBar(pStats.PlayerHealth);
		if(pStats.PlayerHealth <= 0)
		{
			GameObject.Find("GameHandler").GetComponent<GameHandler>().deathCanvas.SetActive(true);
			Destroy(this.gameObject);
			Destroy(healthBar.gameObject);
		}
	}

	private void UpdateHealthBar(float value)
    {
        if (!healthBar) { healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>(); };

		healthBar.SetHealth(value);
		
	}
}