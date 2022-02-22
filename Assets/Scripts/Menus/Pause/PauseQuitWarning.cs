using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseQuitWarning : MonoBehaviour
{

    void Start()
    {
        GetItems();
    }

    void GetItems()
    {
    }

    public void YesQuit()
	{
        SceneManager.LoadScene("MainMenu");
	}

    public void NoStay()
	{
        Debug.Log("NoStay");
        this.gameObject.SetActive(false);
	}
}
