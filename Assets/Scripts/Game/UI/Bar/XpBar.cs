using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : Bar
{    
    protected override void Init()
    {
        bar.maxValue = playerController.xpNeededToLvlUp;
        bar.value = playerController.pStats.playerXp;
    }
}
