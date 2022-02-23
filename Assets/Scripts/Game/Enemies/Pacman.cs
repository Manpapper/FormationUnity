using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : Enemy
{
    public void Start()
    {
        Health = 200;
        MovementSpeed = 10;
        Damage = 2f;
        Cooldown = 0.2f;
    }
}
