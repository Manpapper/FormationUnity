using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        GameObject player = Resources.Load<GameObject>("Prefabs/Player");
        if (player) {
            player = GameObject.Instantiate(player);
            Camera.main.GetComponent<CameraFollow>().PlayerTransform = player.transform;
        }

        GameObject healthBar = Resources.Load<GameObject>("Prefabs/UI/HealthBar");
        if (healthBar != null) { GameObject.Instantiate(healthBar); }


    }

}
