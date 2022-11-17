using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is used for every function related to the game state of the project
public class GameManager : MonoBehaviour
{
    [SerializeField] MazeGenerator MazeGenerator;

    [SerializeField] GameObject player;

    [SerializeField] GameObject cheese;

    [SerializeField] Movement Movement;

    public Text myText; //Variable for the error message

    //Checkers for collision with the walls
    [SerializeField] WallChecker UpChecker;
    [SerializeField] WallChecker RightChecker;
    [SerializeField] WallChecker DownChecker;
    [SerializeField] WallChecker LeftChecker;

    public bool minigame;
    public bool cameraCentral;

    public Canvas generator; //this is the canvas of the Maze Generator
    public Canvas overlay; //this is the canvas with the Home button and controller
    public Canvas winScreen; //this is the canvas of the Level Complete screen

    public Vector3 next;

   //public GameObject MazeGenerator;
   //public Vector2Int mazeSize;

    public void Start()
    {
        minigame = false;
        cameraCentral = false;
    }

    public void Update()
    {
        //You can close the application by pressing Escape
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    //This function activates the Minigame state
    public void Minigame()
    {
        Vector2Int mazeSize;

        mazeSize = MazeGenerator.mazeSize;

        if (minigame == false)
        {
            if ((mazeSize.x <= 30) && (mazeSize.y <= 30))
            {
                minigame = true;
            }
            else
            {
                myText.text = "The maze should be 30x30 or below for the minigame";
                myText.enabled = true;
            }
        
        }

        if(minigame == true)
        {
            generator.GetComponent<Canvas>().enabled = false; //disable the Maze Generator UI
            overlay.GetComponent<Canvas>().enabled = true; //enable the Minigame UI
            winScreen.GetComponent<Canvas>().enabled = false; //disable the Level Complete screen


            cameraCentral = true;

            PositionReset();
            CheeseReset();
            player.SetActive(true);
        }
    }

    public void Home()
    {
        generator.GetComponent<Canvas>().enabled = true; //enable the Maze Generator UI
        overlay.GetComponent<Canvas>().enabled = false; //disable the Minigame UI

        cameraCentral = false;
        
    }
    //This script resets the position of the palyer to a random location within the maze
    public void PositionReset()
    {
        Vector2Int mazeSize;

        mazeSize = MazeGenerator.mazeSize;

        float xPlayer; //variable for x position of the player
        float zPlayer; //variable for z position of the player

        xPlayer = Random.Range(-mazeSize.x/2, mazeSize.x/2); //the maze (0,0) is centered in the middle of the maze so half of the maze
                                                             //is in the left and half in the right
        xPlayer = Mathf.RoundToInt(xPlayer);

        zPlayer = Random.Range(-mazeSize.y/2, mazeSize.y/2); //the same thing applies to upper and lower half of the maze
        zPlayer = Mathf.RoundToInt(zPlayer);

        if (mazeSize.x % 2 == 1)
        {
            xPlayer = xPlayer + 0.5f;

            next = new Vector3(xPlayer, -0.4f, zPlayer); //added this variable as a pointer for the next position
        }
        else
        {
           if (mazeSize.y % 2 == 1)
            {
                zPlayer = zPlayer + 0.5f;
              //  player.transform.position = new Vector3(xPlayer, -0.4f, zPlayer);
                next = new Vector3(xPlayer, -0.4f, zPlayer);
            }
            else
            {
              //  player.transform.position = new Vector3(xPlayer + 0.5f, -0.4f, zPlayer + 0.5f); // this is the new position centered in a cell
                next = new Vector3(xPlayer, -0.4f, zPlayer);
            }
        }
        player.transform.position = next;
        //Debug.Log(player.transform.position);
        Movement.nextPosition = next;
        CheckerReset();
    }

    //This function resets the position of the cheese/ objective of the minigame
    //And uses the same format with the PositionReset function for the player
    public void CheeseReset()
    {
        Vector2Int mazeSize;

        mazeSize = MazeGenerator.mazeSize;

        float xCheese; //variable for x position of the cheese
        float zCheese; //variable for z position of the cheese

        xCheese = Random.Range(-mazeSize.x / 2, mazeSize.x / 2); //the maze (0,0) is centered in the middle of the maze so half of the maze
                                                                 //is in the left and half in the right
        xCheese = Mathf.RoundToInt(xCheese);

        zCheese = Random.Range(-mazeSize.y / 2, mazeSize.y / 2); //the same thing applies to upper and lower half of the maze
        zCheese = Mathf.RoundToInt(zCheese);

        if (mazeSize.x % 2 == 1)
        {
            xCheese = xCheese + 0.5f;

            next = new Vector3(xCheese, -0.1f, zCheese); //added this variable as a pointer for the next position
        }
        else
        {
            if (mazeSize.y % 2 == 1)
            {
                zCheese = zCheese + 0.5f;
                //  player.transform.position = new Vector3(xCheese, -0.4f, zCheese);
                next = new Vector3(xCheese, -0.1f, zCheese);
            }
            else
            {
                //  player.transform.position = new Vector3(xCheese + 0.5f, -0.4f, zCheese + 0.5f); // this is the new position centered in a cell
                next = new Vector3(xCheese, -0.1f, zCheese);
            }
        }
        cheese.SetActive(true);
        cheese.transform.position = next;
        
    }

    //This function resets the checkers
    //and it's called after every position reset
    //due to a bug that blocked the player in certain situations
    public void CheckerReset()
    {
        UpChecker.wall = false;
        RightChecker.wall = false;
        DownChecker.wall = false;
        LeftChecker.wall = false;
    }
}
