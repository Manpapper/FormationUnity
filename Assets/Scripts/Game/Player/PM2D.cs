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

        float horizontalMoveSpeed = Mathf.Abs(horizontalMove * runSpeed);
        float verticalMoveSpeed = Mathf.Abs(verticalMove * runSpeed);

        if(horizontalMoveSpeed > 0 && verticalMoveSpeed > 0)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed / 1.5f, verticalMove * Time.fixedDeltaTime * runSpeed / 1.5f);
        }
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed, verticalMove * Time.fixedDeltaTime * runSpeed);
        }

        playerAnim.SetFloat("Speed", horizontalMoveSpeed + verticalMoveSpeed);
    }

	private void FixedUpdate()
	{
	}
}
