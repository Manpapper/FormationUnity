using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    private int movementSpeed;
    private float damage;
    private float cooldown;

    private bool haveAttacked = false;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CC2D playerController;

    public int Health { get => health; set => health = value; }
    public int MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<CC2D>();
        GetComponentInParent<AIDestinationSetter>().target = player.transform;
    }

	private void OnTriggerEnter2D(Collider2D col)
	{
        if (!haveAttacked && col == player.GetComponent<CapsuleCollider2D>())
        {
            StartCoroutine(AttackLogic());
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!haveAttacked && col == player.GetComponent<CapsuleCollider2D>())
        {
            StartCoroutine(AttackLogic());
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
