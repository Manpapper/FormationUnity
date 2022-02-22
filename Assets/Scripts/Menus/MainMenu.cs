using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject mainMenuCanvas;
    private GameObject settingsMenuCanvas;

    void Start()
    {
        GetItems();
    }

    void GetItems()
    {        
        settingsMenuCanvas = Resources.Load<GameObject>("Prefabs/SettingsMenu");
        
        settingsMenuCanvas = GameObject.Instantiate(settingsMenuCanvas);
        settingsMenuCanvas.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowSettings()
    {
        settingsMenuCanvas.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
