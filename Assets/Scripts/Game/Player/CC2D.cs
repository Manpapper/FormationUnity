using UnityEngine;
using UnityEngine.Events;

public class CC2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	public PlayerStats pStats = new PlayerStats();

	private HealthBar healthBar;

	private Animator playerAnim;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		playerAnim = this.gameObject.GetComponent<Animator>();
		//Physics2D.IgnoreLayerCollision(3, 7);
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
	}

	public void Move(float moveX, float moveY)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveX * 10f * pStats.MovementSpeed, moveY * 10f * pStats.MovementSpeed);
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (moveX > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (moveX < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void TakeDamage(float damage)
    {
		pStats.PlayerHealth -= damage;
		UpdateHealthBar(pStats.PlayerHealth);
		if(pStats.PlayerHealth <= 0)
		{
			GameObject.Find("GameHandler").GetComponent<GameHandler>().deathCanvas.SetActive(true);
			Destroy(this.gameObject);
		}
	}

	private void UpdateHealthBar(float value)
    {
        if (!healthBar) { healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>(); };

		healthBar.SetHealth(value);
    }
}