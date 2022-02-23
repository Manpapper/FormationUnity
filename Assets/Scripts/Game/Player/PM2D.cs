using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM2D : MonoBehaviour
{
    private Animator playerAnim;
    private CC2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = this.gameObject.GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CC2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed, verticalMove * Time.fixedDeltaTime * runSpeed);
        playerAnim.SetFloat("Speed", Mathf.Abs(horizontalMove * runSpeed) + Mathf.Abs(verticalMove * runSpeed));
    }

	private void FixedUpdate()
	{
	}
}
