using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private int movementSpeed;
    private int damage;

    public int Health { get => health; set => health = value; }
    public int MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public int Damage { get => damage; set => damage = value; }

    private void Start()
    {
        GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
