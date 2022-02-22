using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseQuitWarning : MonoBehaviour
{
    private GameObject pauseCanvas;
    private GameObject toMenuWarning;


    void Start()
    {
        GetItems();
    }

    void GetItems()
    {
        pauseCanvas = Resources.Load<GameObject>("Prefabs/PauseCanvas");
        toMenuWarning = Resources.Load<GameObject>("Prefabs/ToMenuWarning");
    }

    public void YesQuit()
	{
        SceneManager.LoadScene("MainMenu");
	}

    public void NoStay()
	{
        pauseCanvas.SetActive(true);
        toMenuWarning.SetActive(false);
	}
}
