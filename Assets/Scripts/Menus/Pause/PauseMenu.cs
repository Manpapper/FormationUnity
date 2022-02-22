using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private GameObject toMenuWarning;


    // Start is called before the first frame update
    void Start()
    {
        GetItems();
    }

    void GetItems()
	{
        pauseCanvas = Resources.Load<GameObject>("Prefabs/PauseCanvas");
        toMenuWarning = Resources.Load<GameObject>("Prefabs/ToMenuWarning");
    }

    void Resume()
	{
        //Resume game
	}

    void QuitToMenu()
	{
        //
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
