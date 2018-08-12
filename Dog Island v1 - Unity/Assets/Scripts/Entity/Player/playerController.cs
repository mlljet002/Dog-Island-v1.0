﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float moveSpeed;
	public float gravityScale;
	public float jumpForce;
	public CharacterController controller;
	public Rigidbody rigidBody;

	private Vector3 moveDirection;
	private bool canAirJump = true;

	public Animator anim;
	public Transform camera;
	public Transform characterModel;
	public float rotateSpeed;

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
		canAirJump = true;

		Cursor.lockState = CursorLockMode.Locked;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//moveDirection = new Vector3 (Input.GetAxis ("Horizontal") * moveSpeed, 
	//		moveDirection.y, Input.GetAxis ("Vertical") * moveSpeed);

		moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed)
						+ (transform.right * Input.GetAxis("Horizontal") * moveSpeed);


		if(!controller.isGrounded)
		{
			//moveDirection = new Vector3 (Input.GetAxis ("Horizontal") * moveSpeed *0.8f, 
			//	controller.velocity.y, Input.GetAxis ("Vertical") * moveSpeed/0.8f);

			moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed * 0.8f)
							+ (transform.right * Input.GetAxis("Horizontal") * moveSpeed * 0.8f) 
							+ new Vector3(0, controller.velocity.y, 0);				
		}
			
		if (Input.GetButtonDown ("Jump")) 
		{
			if (controller.isGrounded) 
			{
				canAirJump = true;
				moveDirection.y = jumpForce;
			}
			else if(canAirJump)
			{
				canAirJump = false;
				moveDirection.y = jumpForce;
			}
		}

		if(Input.GetKeyDown("escape"))
		{
			Cursor.lockState = CursorLockMode.None;
		}			

		moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
		controller.Move(moveDirection * Time.deltaTime);

		// Move the player in different directions

		if(Input.GetAxisRaw("Horizontal") > 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y + 90f, 0f);
		}
		else if(Input.GetAxisRaw("Horizontal") < 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y - 90f, 0f);
		}
		else if(Input.GetAxisRaw("Vertical") < 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y + 180f, 0f);
		}
		else if(Input.GetAxisRaw("Vertical") > 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y, 0f);
		}

		if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y + 45f, 0f);
		}
		else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y + 135f, 0f);
		}
		else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y - 45f, 0f);
		}
		else if(Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y - 135f, 0f);
		}

		if(Input.GetButtonDown("Fire1"))
		{
			characterModel.rotation  = Quaternion.Euler(0f, camera.rotation.eulerAngles.y, 0f);
		}

		anim.SetBool("isGrounded", controller.isGrounded);
		anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
	}
}
