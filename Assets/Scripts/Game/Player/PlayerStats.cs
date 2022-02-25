using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
	int _playerLvl = 0;
	float _movementSpeed = 1f;
	float _playerHealth = 100f;
	float _playerMaxHealth = 100f;
	float _playerXp = 0;
	float _attackSpeed = 2f;

	public float movementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
	public float playerHealth { get => _playerHealth; set => _playerHealth = value; }
	public float playerMaxHealth { get => _playerMaxHealth; set => _playerMaxHealth = value; }
	public int playerLvl { get => _playerLvl; set => _playerLvl = value; }
	public float playerXp { get => _playerXp; set => _playerXp = value; }
	public float attackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
}
