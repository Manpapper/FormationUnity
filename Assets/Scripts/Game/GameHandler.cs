using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameHandler : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        if (enemy != null) { GameObject.Instantiate(enemy); }
    }

    private void Init()
    {
        GameObject pathFinder = Resources.Load<GameObject>("Prefabs/PathFinder/A_");
        if (pathFinder) { GameObject.Instantiate(pathFinder); }

        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        if (player) {
            player = GameObject.Instantiate(player);
            Camera.main.GetComponent<CameraFollow>().PlayerTransform = player.transform;
        }

        GameObject healthBar = Resources.Load<GameObject>("Prefabs/UI/HealthBar");
        if (healthBar != null) { GameObject.Instantiate(healthBar); }
    }
}
