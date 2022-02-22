using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseCanvas;
    private GameObject toMenuWarning;


    void Start()
    {
        GetItems();
    }

    void GetItems()
	{
        pauseCanvas = GameObject.Find("PauseCanvas");

        toMenuWarning = Resources.Load<GameObject>("Prefabs/ToMenuWarning");
        toMenuWarning = GameObject.Instantiate(toMenuWarning);
        toMenuWarning.SetActive(false);
    }

    public void Resume()
	{
        //Resume game
	}

    public void QuitToMenu()
	{
        toMenuWarning.SetActive(true);
	}
}
