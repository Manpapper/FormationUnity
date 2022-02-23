using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packman : Enemy
{
    public void Start()
    {
        Health = 200;
        MovementSpeed = 10;
        Damage = 1;
        Cooldown = 0.5f;
    }
}
