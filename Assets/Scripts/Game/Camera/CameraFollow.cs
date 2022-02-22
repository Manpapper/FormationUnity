using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
    private Transform player;

    public Transform PlayerTransform { get => player; set => player = value; }

    // Update is called once per frame
    void Update()
    {
		if (player)
		{
            transform.position = new Vector3(player.position.x, player.position.y, -10f);
		}
    }
}
