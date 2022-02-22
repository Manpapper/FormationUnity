using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM2D : MonoBehaviour
{

    public CC2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

	private void FixedUpdate()
	{
        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed, verticalMove * Time.fixedDeltaTime * runSpeed);
	}
}
