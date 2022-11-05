using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool minigame;
    public bool cameraCentral;

    public Canvas generator; //this is the canvas of the Maze Generator
    public Canvas overlay; //this is the canvas with the Home button and end screen

   //public GameObject MazeGenerator;
   //public Vector2Int mazeSize;

    public void Start()
    {
        minigame = false;
        cameraCentral = false;
    }

    //This function activates the Minigame state
    public void Minigame()
    {
        if(minigame == false)
        {
            minigame = true;
        }

        if(minigame == true)
        {
            generator.GetComponent<Canvas>().enabled = false; //disable the Maze Generator UI
            overlay.GetComponent<Canvas>().enabled = true; //enable the Minigame UI

            cameraCentral = true;
        }
    }

    public void Home()
    {
        generator.GetComponent<Canvas>().enabled = true; //enable the Maze Generator UI
        overlay.GetComponent<Canvas>().enabled = false; //disable the Minigame UI

        cameraCentral = false;
    }
}
