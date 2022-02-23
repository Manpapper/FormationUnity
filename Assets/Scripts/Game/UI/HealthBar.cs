using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public CC2D playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CC2D>();

        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerController.pStats.PlayerMaxHealth;
        healthBar.value = playerController.pStats.PlayerHealth;
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }

}
