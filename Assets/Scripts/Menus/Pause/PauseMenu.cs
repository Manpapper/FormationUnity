using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject toMenuWarning;


    void Start()
    {
        GetItems();
    }

    void GetItems()
	{
        //pauseCanvas = Resources.Load<GameObject>("Prefabs/PauseCanvas");
        //toMenuWarning = Resources.Load<GameObject>("Prefabs/ToMenuWarning");
    }

    public void Resume()
	{
        //Resume game
	}

    public void QuitToMenu()
	{
        toMenuWarning.SetActive(true);
        pauseCanvas.SetActive(false);
	}
}
