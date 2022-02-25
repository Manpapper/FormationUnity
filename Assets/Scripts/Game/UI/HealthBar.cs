using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public CC2D playerController;

    private float timer = 4f;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CC2D>();

        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerController.pStats.PlayerMaxHealth;
        healthBar.value = playerController.pStats.PlayerHealth;
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
        StartCoroutine(ShowBarOnDamageTaken());
    }

    IEnumerator ShowBarOnDamageTaken()
    {
        yield return new WaitForSeconds(timer);
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        healthBar.gameObject.SetActive(active);
    }

}
