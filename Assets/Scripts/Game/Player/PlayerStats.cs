using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
	int playerLvl = 0;
	float movementSpeed = 1f;
	float playerHealth = 100f;
	int playerXp = 0;

	public int PlayerLvl { get => playerLvl; set => playerLvl = value; }
	public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
	public float PlayerHealth { get => playerHealth; set => playerHealth = value; }
	public int PlayerXp { get => playerXp; set => playerXp = value; }
}
