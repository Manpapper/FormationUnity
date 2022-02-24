using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathCanvas;
    public GameObject quitWarning;
    PauseMenu pauseMenuScript;

    private void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuScript.IsWarningActive == false)
		{
			if (pauseMenuScript.isPaused)
			{
                pauseMenuScript.Resume();
			} else
			{
                pauseMenuScript.Pause();
            }
		}
	}

	private void Init()
    {
        deathCanvas = Resources.Load<GameObject>("Prefabs/UI/DeathCanvas");
        deathCanvas = GameObject.Instantiate(deathCanvas);

        pauseMenu = Resources.Load<GameObject>("Prefabs/UI/Pause/PauseCanvas");
        pauseMenu = GameObject.Instantiate(pauseMenu);
        pauseMenuScript = pauseMenu.GetComponent<PauseMenu>();

        quitWarning = Resources.Load<GameObject>("Prefabs/UI/Pause/ToMenuWarning");
        quitWarning = GameObject.Instantiate(quitWarning);

        GameObject pathFinder = Resources.Load<GameObject>("Prefabs/PathFinder/A_");
        GameObject.Instantiate(pathFinder);

        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        player = GameObject.Instantiate(player);
        Camera.main.GetComponent<CameraFollow>().PlayerTransform = player.transform;
        GameObject healthBar = Resources.Load<GameObject>("Prefabs/UI/HealthBar");
        GameObject.Instantiate(healthBar);

        GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemies/Pacman");
        GameObject.Instantiate(enemy);

        pauseMenu.SetActive(false);
        quitWarning.SetActive(false);
        deathCanvas.SetActive(false);
    }
}
