using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    public void Awake()
    {
        Health = 100;
        MovementSpeed = 10;
        Damage = 10;
    }
}
