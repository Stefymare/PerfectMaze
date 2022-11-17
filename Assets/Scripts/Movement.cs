using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is responsable for everything related to the movement of the player's character
public class Movement : MonoBehaviour
{
	private Vector3 playerInput = Vector3.zero;
	public Vector3 nextPosition;

	[SerializeField] GameObject model;

	[SerializeField] WallChecker UpChecker;
	[SerializeField] WallChecker RightChecker;
	[SerializeField] WallChecker DownChecker;
	[SerializeField] WallChecker LeftChecker;


	private void Start()
	{
		// we don't want the player to go anywhere when the scene loads.
		nextPosition = transform.position;
	}

	private void Update()
	{
		LegacyMove();
	}

	private void Up() //this function sets the direction for going Up & rotate the player UP
	{
		playerInput.z = 1;

		if (model.transform.eulerAngles != new Vector3(0, 0, 0))
		{
			Vector3 toRotate = new Vector3(0, 0, 0);
			model.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, toRotate, 1f);
		}
	}
	private void Down() //this function sets the direction for going Down & rotate the player towards Down
	{
		playerInput.z = -1;

		if (model.transform.eulerAngles != new Vector3(0, 180, 0))
		{
			Vector3 toRotate = new Vector3(0, 180, 0);
			model.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, toRotate, 1f);
		}
	}
	private void Right() //this function sets the direction for going Right & rotate the player towards Right
	{
		playerInput.x = 1;

		if (model.transform.eulerAngles != new Vector3(0, 90, 0))
		{
			Vector3 toRotate = new Vector3(0, 90, 0);
			model.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, toRotate, 1f);
		}

	}
	private void Left() //this function sets the direction for going Left & rotate the player towards Left
	{
		playerInput.x = -1;

		if (model.transform.eulerAngles != new Vector3(0, -90 , 0))
		{
			Vector3 toRotate = new Vector3(0, - 90, 0);
			model.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, toRotate, 1f);
		}
	}

	//This function checks if the player can go into each direction
	//based on input and moves the player with one square
	//for this section I took inspiration from another person's code and adapted it for my case
	//https://gist.github.com/grimmdev/aea429fc9cca292a2f0e
	private void LegacyMove()
	{
		if (playerInput != Vector3.zero)
		{
			nextPosition = transform.position;
			if (downCheck() && playerInput.z < 0)
			{
				playerInput.z = 0;
			}
			if (upCheck() && playerInput.z > 0)
			{
				playerInput.z = 0;

			}
			if (leftCheck() && playerInput.x < 0)
			{
				playerInput.x = 0;
			}
			if (rightCheck() && playerInput.x > 0)
			{
				playerInput.x = 0;
			}
			nextPosition = transform.position + playerInput;
			playerInput = Vector3.zero;
		}
		if (Vector3.Distance(transform.position, nextPosition) > 0.1f)
		{
			transform.position = Vector3.Lerp(transform.position, nextPosition, 0.1f);
			
		}
		else
		{
			transform.position = nextPosition;
		}
	}

	bool upCheck()
	{
		bool wallUp;
		wallUp = UpChecker.wall;

		return wallUp;
	}

	bool rightCheck()
	{
		bool wallRight;
		wallRight = RightChecker.wall;

		return wallRight;
	}

	bool downCheck()
	{
		bool wallDown;
		wallDown = DownChecker.wall;

		return wallDown;
	}
	bool leftCheck()
	{
		bool wallLeft;
		wallLeft = LeftChecker.wall;

		return wallLeft;
	}
}

