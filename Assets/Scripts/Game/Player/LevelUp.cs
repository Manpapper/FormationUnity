using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    private GameObject choice;
    private GameObject levelUpCanvas;
    private GameObject bodyWindow;
    private CC2D playerController;

    enum UpgradeType
    {
        maxHealth, attackSpeed, mouvementSpeed, healthRegen
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        choice = Resources.Load<GameObject>("Prefabs/UI/LevelUp/Choice");
        playerController = gameObject.GetComponent<CC2D>();
        InitCanvas();
    }

    private void InitCanvas()
    {
        levelUpCanvas = Resources.Load<GameObject>("Prefabs/UI/LevelUp/LevelUpCanvas");
        levelUpCanvas = GameObject.Instantiate(levelUpCanvas);
        bodyWindow = GameObject.Find("BodyWindow");
        levelUpCanvas.SetActive(false);
    }

    public void SetActive()
    {
        levelUpCanvas.SetActive(true);
        bodyWindow = GameObject.Find("BodyWindow");
        Time.timeScale = 0f;

        List<String> upgrades = new List<String>()
        {
            "maxHealth",
            "attackSpeed",
            "mouvementSpeed",
            "healthRegen"
        };

        foreach (String upgrade in upgrades)
        {
            if (upgrade.Equals("maxHealth"))
            {
                InitChoiceButton("Max Health + 10%", UpgradeType.maxHealth);
            }
            else if (upgrade.Equals("attackSpeed"))
            {
                InitChoiceButton("Attack Speed + 20%", UpgradeType.attackSpeed);
            }
            else if (upgrade.Equals("mouvementSpeed"))
            {
                InitChoiceButton("Mouvement Speed + 5%", UpgradeType.mouvementSpeed);
            }
        }
    }

    private void InitChoiceButton(string text, UpgradeType upgradeType)
    {
        GameObject choiceButton = GameObject.Instantiate(choice);
        choiceButton.transform.SetParent(bodyWindow.transform);
        Button button = choiceButton.GetComponentInChildren<Button>();
        choiceButton.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = text;

        button.onClick.AddListener(delegate { Upgrade(upgradeType); });
    }

    void Upgrade(UpgradeType upgradeType)
    {
        if(upgradeType == UpgradeType.maxHealth)
        {
            playerController.pStats.playerMaxHealth *= 1.1f;
        }
        else if (upgradeType == UpgradeType.attackSpeed)
        {
            playerController.pStats.attackSpeed *= 1.2f;
            playerController.fireRate /= 1.2f;
        }
        else if (upgradeType == UpgradeType.mouvementSpeed)
        {
            playerController.pStats.attackSpeed *= 1.05f;
        }
        Time.timeScale = 1f;
        Destroy(levelUpCanvas);
        InitCanvas();
    }
}
