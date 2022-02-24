using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : Enemy
{
    public void Awake()
    {
        Health = 4f;
        MovementSpeed = 2f;
        Damage = 2f;
        Cooldown = 0.2f;
    }
}