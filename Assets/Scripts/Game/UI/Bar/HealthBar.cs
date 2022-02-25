using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Bar
{
    protected override void Init()
    {
        bar.maxValue = playerController.pStats.PlayerMaxHealth;
        bar.value = playerController.pStats.PlayerHealth;

        hideOnChange = true;
        timer = 4f;
    }
}
