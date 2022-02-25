using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
	int playerLvl = 0;
	float movementSpeed = 1f;
	float playerHealth = 100f;
	float playerMaxHealth = 100f;
	float playerXp = 0;
	float attackSpeed = 2f;

	public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
	public float PlayerHealth { get => playerHealth; set => playerHealth = value; }
	public float PlayerMaxHealth { get => playerMaxHealth; set => playerMaxHealth = value; }
	public int PlayerLvl { get => playerLvl; set => playerLvl = value; }
	public float PlayerXp { get => playerXp; set => playerXp = value; }
	public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
}
