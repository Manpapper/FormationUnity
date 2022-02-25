using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFoe : Enemy
{
    public void Awake()
    {
        health = 6f;
        movementSpeed = 0.7f;
        damage = 4f;
        cooldown = 0.2f;
        xpGiven = 4;

        enemySound = Resources.Load<AudioClip>("Audio/EnemySound2");
        enemyDeath = Resources.Load<AudioClip>("Audio/EnemyDeath2");
        Debug.Log("son mort: " + enemyDeath);
    }
}
