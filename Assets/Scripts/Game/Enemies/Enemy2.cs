using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    public void Start()
    {
        Health = 100;
        MovementSpeed = 10;
        Damage = 0.5f;
        Cooldown = 0.5f;
    }
}
