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

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CC2D playerController;

    public float Health { get => health; set => health = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }

    private void Start()
    {
        Init();
    }

    private void Init()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CC2D>();
        GetComponentInParent<AIDestinationSetter>().target = player.transform;
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
            Debug.Log(knockbackForce);
        }
        else if (!facingRight)
        {
            enemyRB.AddForce(new Vector2(-knockbackForce * knockbackMultiplier, 0), ForceMode2D.Force);
            Debug.Log(knockbackForce);
        }

        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
               
        wasAttacked = true;
        yield return new WaitForSeconds(1/ playerController.AtkAnimSpeed);
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
