using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : Enemy
{
    public void Awake()
    {
        health = 4f;
        movementSpeed = 2f;
        damage = 2f;
        cooldown = 0.2f;
        xpGiven = 2;

        enemySound = Resources.Load<AudioClip>("Audio/enemySound");
        enemyDeath = Resources.Load<AudioClip>("Audio/enemyDeath");
    }
}