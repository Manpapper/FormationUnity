using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : Enemy
{
    public void Awake()
    {
        Health = 200;
        MovementSpeed = 3.5f;
        Damage = 2f;
        Cooldown = 0.2f;
    }
}
