using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Audio;

public class GameHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathCanvas;
    public GameObject quitWarning;
    public GameObject counterCanvas;
    PauseMenu pauseMenuScript;

    public GameObject enemy;
    public GameObject enemy2;
    private GameObject enemies;
    public GameObject[] spawners;
    private float _spawnCdPacman = 1.5f;
    private float _spawnCdHand = 3f;
    public bool canSpawn = true;

	public float spawnCdPacman { get => _spawnCdPacman; set => _spawnCdPacman = value; }
	public float spawnCdHand { get => _spawnCdHand; set => _spawnCdHand = value; }

	private void Awake()
    {
        Init();
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
            float random = Random.Range(0, 100);
            if( random < 50)
			{
                StartCoroutine(SpawnLogic(enemy, _spawnCdPacman));
			}
			else
			{
                StartCoroutine(SpawnLogic(enemy2, _spawnCdHand));
			}
        }
	}

    IEnumerator SpawnLogic(GameObject foe, float spawnCoolDown)
	{
        canSpawn = false;
        foe.transform.position = spawners[Random.Range(0, spawners.Length)].transform.position;
        GameObject.Instantiate(foe).transform.SetParent(enemies.transform);
        yield return new WaitForSeconds(spawnCoolDown);
        canSpawn = true;
    }

	private void Init()
    {
        enemies = new GameObject("Enemies");

        spawners = GameObject.FindGameObjectsWithTag("Spawner");

        counterCanvas = Resources.Load<GameObject>("Prefabs/UI/Counters");
        counterCanvas = GameObject.Instantiate(counterCanvas);

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

        GameObject healthBar = Resources.Load<GameObject>("Prefabs/UI/Bar/HealthBar");
        GameObject.Instantiate(healthBar);

        GameObject XpBar = Resources.Load<GameObject>("Prefabs/UI/Bar/XpBar");
        GameObject.Instantiate(XpBar);


        enemy = Resources.Load<GameObject>("Prefabs/Enemies/Pacman");
        enemy2 = Resources.Load<GameObject>("Prefabs/Enemies/HandFoe");

        pauseMenu.SetActive(false);
        quitWarning.SetActive(false);
        deathCanvas.SetActive(false);
    }

    
}
