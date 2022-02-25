using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Bar
{
    protected override void Init()
    {
        bar.maxValue = playerController.pStats.playerMaxHealth;
        bar.value = playerController.pStats.playerHealth;

        hideOnChange = true;
        timer = 4f;
    }
}
