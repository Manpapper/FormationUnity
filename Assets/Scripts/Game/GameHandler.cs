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

    public GameObject enemy;
    public GameObject[] spawners;
    public float spawnCd = 1.5f;
    public bool canSpawn = true;

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

		if (canSpawn)
		{
            StartCoroutine(spawnLogic());
		}
	}

    IEnumerator spawnLogic()
	{
        canSpawn = false;
        enemy.transform.position = spawners[Random.Range(0, spawners.Length)].transform.position;
        GameObject.Instantiate(enemy);
        yield return new WaitForSeconds(spawnCd);
        canSpawn = true;

    }

	private void Init()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");

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

        enemy = Resources.Load<GameObject>("Prefabs/Enemies/Pacman");

        pauseMenu.SetActive(false);
        quitWarning.SetActive(false);
        deathCanvas.SetActive(false);
    }

    
}
