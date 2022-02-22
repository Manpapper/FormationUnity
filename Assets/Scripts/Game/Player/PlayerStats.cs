using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
	float movementSpeed = 1f;
	float playerHealth = 100f;
	int playerXp = 0;

	public float GetMovementSpeed()
	{
		return movementSpeed;
	}

	public float GetPlayerHealth()
	{
		return playerHealth;
	}

	public void SetPlayerHealth(float value)
	{
		playerHealth = value;
	}

	public float GetPlayerXp()
	{
		return playerXp;
	}

	public void SetPlayerXp(int value)
	{
		playerXp = value;
	}
}
