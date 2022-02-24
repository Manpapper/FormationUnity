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
    private GameObject player;
    [SerializeField]
    private CC2D playerController;

    public float Health { get => health; set => health = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CC2D>();
        GetComponentInParent<AIDestinationSetter>().target = player.transform;
        GetComponentInParent<AIPath>().maxSpeed *= movementSpeed;
    }

	private void OnTriggerEnter2D(Collider2D col)
	{
        if (!haveAttacked && col == player.GetComponent<BoxCollider2D>())
        {
            StartCoroutine(AttackLogic());
        } else if ( col == player.GetComponent<CapsuleCollider2D>())
		{
            TakeDamage(player.GetComponent<CC2D>().damage);
		}
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!haveAttacked && col == player.GetComponent<BoxCollider2D>())
        {
            StartCoroutine(AttackLogic());
        }
    }

	public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    IEnumerator AttackLogic()
    {
        haveAttacked = true;
        playerController.TakeDamage(Damage);
        yield return new WaitForSeconds(Cooldown);
        haveAttacked = false;
    }
}
