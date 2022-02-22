using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packman : Enemy
{
    public void Awake()
    {
        Health = 200;
        MovementSpeed = 10;
        Damage = 1;
    }
}
