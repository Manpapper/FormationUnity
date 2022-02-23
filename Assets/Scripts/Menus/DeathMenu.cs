using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public void MenuBtn()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitBtn()
	{
		Application.Quit();
	}
}
