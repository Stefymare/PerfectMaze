using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script allign the camera so the entire maze can be visible
public class CameraAllignment : MonoBehaviour
{
    //Inputs from the user interface
    [SerializeField] InputField width;
    [SerializeField] InputField height;

    //Variables to store the new location of the Camera
    public float cameraY;
    public float cameraX;

    Vector2Int mazeSize; //Variable for the size (x and y) of the maze

    public void CameraAllign()
    {
        //Attribute the value for size of the maze based on user inputs
        mazeSize.x = int.Parse(width.text);
        mazeSize.y = int.Parse(height.text);

        //Sets the camera's Y position based on the biggest value of the maze
        if (mazeSize.y >= mazeSize.x)
        {
            cameraY = mazeSize.y;
        }
        else
        {
            cameraY = mazeSize.x;
        }

        //Calculate camera's X position to centralise the maze on the right side of the screen

        cameraX = -(40 * cameraY / 100);
        cameraX = Mathf.RoundToInt(cameraX);
       
        //Set the new camera position
        gameObject.transform.position = new Vector3(cameraX, cameraY, -0.5f);
    }
}
