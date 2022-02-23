using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	private bool isWarningActive = false;
    public bool isPaused = false;

    public bool IsWarningActive { get => isWarningActive; set => isWarningActive = value; }

	void Awake()
    {
    }

    public void Pause()
	{   
        Time.timeScale = 0;
        isPaused = true;
        this.gameObject.SetActive(true);
    }

    public void Resume()
	{
        Time.timeScale = 1;
        isPaused = false;
        this.gameObject.SetActive(false);
    }

    public void QuitToMenu()
	{
        GameObject.Find("GameHandler").GetComponent<GameHandler>().quitWarning.SetActive(true);
        IsWarningActive = true;
    }
}
