using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    private float movementSpeed;
    private float damage;
    private float cooldown;

    private bool haveAttacked = false;
    [SerializeField]
    private Rigidbody2D enemyRB;
    private AIPath _AIPath;
    private int knockbackMultiplier = 100; 
    private float knockbackForce = 3f;
    private bool wasAttacked = false;

    private AudioSource enemyAs;
    private AudioClip enemySound;
    private AudioClip enemyDeath;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CC2D playerController;

    public float Health { get => health; set => health = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
	public AudioClip EnemySound { get => enemySound; set => enemySound = value; }
	public AudioClip EnemyDeath { get => enemyDeath; set => enemyDeath = value; }
	public AudioSource EnemyAs { get => enemyAs; set => enemyAs = value; }

	private void Start()
    {
        Init();
    }

    private void Init()
	{
        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            playerController = player.GetComponent<CC2D>();
            GetComponentInParent<AIDestinationSetter>().target = player.transform;
        }

        enemyAs = GetComponentInParent<AudioSource>();


        _AIPath = GetComponentInParent<AIPath>();
        _AIPath.maxSpeed *= movementSpeed;
        enemyRB = GetComponentInParent<Rigidbody2D>();
    }

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (player)
		{
            if (!haveAttacked && col == player.GetComponent<BoxCollider2D>())
            {
                StartCoroutine(AttackLogic());
            } else if (!wasAttacked && col == player.GetComponent<CapsuleCollider2D>())
		    {
                StartCoroutine(takeDmgLogic(playerController.damage, playerController.FacingRight));
		    }
		}
    }

    private void OnTriggerStay2D(Collider2D col)
    {
		if (player)
		{
            if (!haveAttacked && col == player.GetComponent<BoxCollider2D>())
            {
                StartCoroutine(AttackLogic());
            }
		}
    }

    IEnumerator takeDmgLogic(float damage, bool facingRight)
	{
        _AIPath.canMove = false;
        if (facingRight)
        {
            enemyRB.AddForce(new Vector2(knockbackForce * knockbackMultiplier, 0), ForceMode2D.Force);
        }
        else if (!facingRight)
        {
            enemyRB.AddForce(new Vector2(-knockbackForce * knockbackMultiplier, 0), ForceMode2D.Force);
        }

        health -= damage;
        if (health <= 0)
        {
            playerController.addXp(100);
            AudioSource.PlayClipAtPoint(enemyDeath, transform.position, .1f);
            Destroy(gameObject.transform.parent.gameObject);
        }
               
        wasAttacked = true;
        yield return new WaitForSeconds(1/ playerController.pStats.AttackSpeed);
        enemyRB.velocity = Vector2.zero;
        _AIPath.canMove = true;
        wasAttacked = false;
	}

    IEnumerator AttackLogic()
    {
        haveAttacked = true;
        playerController.TakeDamage(Damage);
        yield return new WaitForSeconds(Cooldown);
        haveAttacked = false;
    }
}
