using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _health;
    private float _movementSpeed;
    private float _damage;
    private float _cooldown;
    private int _xpGiven;

    private bool haveAttacked = false;
    [SerializeField]
    private Rigidbody2D enemyRB;
    private AIPath _AIPath;
    private int knockbackMultiplier = 100; 
    private float knockbackForce = 3f;
    private bool wasAttacked = false;

    private AudioSource _enemyAs;
    private AudioClip _enemySound;
    private AudioClip _enemyDeath;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CC2D playerController;

    public float health { get => _health; set => _health = value; }
    public float movementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
    public float damage { get => _damage; set => _damage = value; }
    public float cooldown { get => _cooldown; set => _cooldown = value; }
	public AudioClip enemySound { get => _enemySound; set => _enemySound = value; }
	public AudioClip enemyDeath { get => _enemyDeath; set => _enemyDeath = value; }
	public AudioSource enemyAs { get => _enemyAs; set => _enemyAs = value; }
	public int xpGiven { get => _xpGiven; set => _xpGiven = value; }

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

        _enemyAs = GetComponentInParent<AudioSource>();


        _AIPath = GetComponentInParent<AIPath>();
        _AIPath.maxSpeed *= _movementSpeed;
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
                StartCoroutine(takeDmgLogic(playerController._damage, playerController.facingRight));
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

    IEnumerator takeDmgLogic(float _damage, bool facingRight)
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

        _health -= _damage;
        if (_health <= 0)
        {
            playerController.addXp(_xpGiven);
            AudioSource.PlayClipAtPoint(_enemyDeath, transform.position, .1f);
            Destroy(gameObject.transform.parent.gameObject);
        }
               
        wasAttacked = true;
        yield return new WaitForSeconds(1/ playerController.pStats.attackSpeed);
        enemyRB.velocity = Vector2.zero;
        _AIPath.canMove = true;
        wasAttacked = false;
	}

    IEnumerator AttackLogic()
    {
        haveAttacked = true;
        playerController.TakeDamage(_damage);
        yield return new WaitForSeconds(_cooldown);
        haveAttacked = false;
    }
}
