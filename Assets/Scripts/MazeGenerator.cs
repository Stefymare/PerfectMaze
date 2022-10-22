using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    Vector2Int mazeSize; //Variable for the size (x and y) of the maze

    //Inputs from the user interface
    [SerializeField] InputField width; 
    [SerializeField] InputField height;
    
    public GameObject[] oldNodes; //Created to keep track of all the old nodes before destroying them 

    public Text myText;
    
    //Function to call the generation of the maze from user interface
    public void callFunction()
    {
        DeleteOldMaze(); //Call function to delete the old maze before generating a new one
        
        //Attribute the value for size of the maze based on user inputs
        mazeSize.x = int.Parse(width.text);
        mazeSize.y = int.Parse(height.text);

        //GenerateMazeInstant(mazeSize);
        StartCoroutine(GenerateMaze(mazeSize));
    }
    private void Start()
    {
        myText.enabled = false;
        //GenerateMazeInstant(mazeSize); //use this one for the instant one
        //StartCoroutine(GenerateMaze(mazeSize)); // use this one for the delayed version
    }

    //Function search for all the existing game objects with the tag "MazeNode"
    //then destroys each one of them
    //used to destroy the old maze when a new one is generated
    public void DeleteOldMaze()
    {
        oldNodes = GameObject.FindGameObjectsWithTag("MazeNode");

        foreach (GameObject MazeNode in oldNodes)
        {
            Destroy(MazeNode);
        }

    }

    
    public void GenerateMazeInstant(Vector2Int size)
    {
        
        myText.enabled = false; //Disable the text that should pop-up when the maze is not generated
        List<MazeNode> nodes = new List<MazeNode>(); //Created the list with all nodes

        //The number of cells should be less than 250x250, but more or equal to 10x10
        if ((size.x < 250) && (size.y < 250) && (size.x >= 10) && (size.y >=10)) 
        {
            //Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f)); //Centralise the position of the nodes
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);

            }
        }

        List<MazeNode> currentPath = new List<MazeNode>(); //List of the current nodes in the path
        List<MazeNode> completedNodes = new List<MazeNode>(); //List with nodes that don't have any other neighbours to explore

        //Choose starting node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);


            while (completedNodes.Count < nodes.Count)
            {
                // Check nodes next to the current node
                List<int> possibleNextNodes = new List<int>();
                List<int> possibleDirections = new List<int>();

                int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
                int currentNodeX = currentNodeIndex / size.y;
                int currentNodeY = currentNodeIndex % size.y;

                if (currentNodeY < size.y - 1)
                {
                    //Check node above the current node (North)
                    if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                       !currentPath.Contains(nodes[currentNodeIndex + 1]))
                    {
                        possibleDirections.Add(1);
                        possibleNextNodes.Add(currentNodeIndex + 1);
                    }
                }

                if (currentNodeX < size.x - 1)
                {
                    //Check node to the right of the current node (East)
                    if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                       !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                    {
                        possibleDirections.Add(2);
                        possibleNextNodes.Add(currentNodeIndex + size.y);
                    }
                }

                if (currentNodeY > 0)
                {
                    //Check node below the current node (South)
                    if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                       !currentPath.Contains(nodes[currentNodeIndex - 1]))
                    {
                        possibleDirections.Add(3);
                        possibleNextNodes.Add(currentNodeIndex - 1);
                    }
                }

                if (currentNodeX > 0)
                {
                    //Check node to the left of the current node (West)
                    if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                       !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                    {
                        possibleDirections.Add(4);
                        possibleNextNodes.Add(currentNodeIndex - size.y);
                    }
                }

                //Choose next node
                if (possibleDirections.Count > 0)
                {
                    int chosenDirection = Random.Range(0, possibleDirections.Count);
                    MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                    switch (possibleDirections[chosenDirection])
                    {
                        case 1: //North wall for the current and South wall for chosen
                            chosenNode.RemoveWall(2);
                            currentPath[currentPath.Count - 1].RemoveWall(0);
                            break;
                        case 2: //East wall for current and West wall for chosen
                            chosenNode.RemoveWall(3);
                            currentPath[currentPath.Count - 1].RemoveWall(1);
                            break;
                        case 3: //South wall for current and North for chosen
                            chosenNode.RemoveWall(0);
                            currentPath[currentPath.Count - 1].RemoveWall(2);
                            break;
                        case 4://West wall for current and East for chosen
                            chosenNode.RemoveWall(1);
                            currentPath[currentPath.Count - 1].RemoveWall(3);
                            break;
                    }

                    currentPath.Add(chosenNode);


                }
                //If there is no more explorable neighbourse and there are still unexplored nodes
                //the script is backtracking till it finds a new path
                else 
                {
                    completedNodes.Add(currentPath[currentPath.Count - 1]);

                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
        }
        else
        {
            myText.text = "This maze can not be generated. The maze has to be between 10x10 and 250x250";
            myText.enabled = true;
        }
    }


    //this script is almost the same one with the previous one
    //but the difference is that is delayed because
    //of the use of a WaitForSeconds function within a yield return
    //therefore this version of the generator is used if the user wants to visualise how the maze is generated
    public IEnumerator GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        if ((size.x < 250) && (size.y < 250) && (size.x >= 10) && (size.y >= 10))
        {
            myText.enabled = false;//disable the text that should pop-up
            //Create nodes
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
                    MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                    nodes.Add(newNode);

                }
            }

            List<MazeNode> currentPath = new List<MazeNode>();
            List<MazeNode> completedNodes = new List<MazeNode>();

            //Choose starting node
            currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
            currentPath[0].SetState(NodeState.Current);

            while (completedNodes.Count < nodes.Count)
            {
                // Check nodes next to the current node
                List<int> possibleNextNodes = new List<int>();
                List<int> possibleDirections = new List<int>();

                int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
                int currentNodeX = currentNodeIndex / size.y;
                int currentNodeY = currentNodeIndex % size.y;

                if (currentNodeY < size.y - 1)
                {
                    //Check node above the current node (North)
                    if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                       !currentPath.Contains(nodes[currentNodeIndex + 1]))
                    {
                        possibleDirections.Add(1);
                        possibleNextNodes.Add(currentNodeIndex + 1);
                    }
                }

                if (currentNodeX < size.x - 1)
                {
                    //Check node to the right of the current node (East)
                    if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                       !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                    {
                        possibleDirections.Add(2);
                        possibleNextNodes.Add(currentNodeIndex + size.y);
                    }
                }

                if (currentNodeY > 0)
                {
                    //Check node below the current node (South)
                    if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                       !currentPath.Contains(nodes[currentNodeIndex - 1]))
                    {
                        possibleDirections.Add(3);
                        possibleNextNodes.Add(currentNodeIndex - 1);
                    }
                }

                if (currentNodeX > 0)
                {
                    //Check node to the left of the current node (West)
                    if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                       !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                    {
                        possibleDirections.Add(4);
                        possibleNextNodes.Add(currentNodeIndex - size.y);
                    }
                }

                //Choose next node
                if (possibleDirections.Count > 0)
                {
                    int chosenDirection = Random.Range(0, possibleDirections.Count);
                    MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                    switch (possibleDirections[chosenDirection])
                    {
                        case 1: //North wall for the current and South wall for chosen
                            chosenNode.RemoveWall(2);
                            currentPath[currentPath.Count - 1].RemoveWall(0);
                            break;
                        case 2: //East wall for current and West wall for chosen
                            chosenNode.RemoveWall(3);
                            currentPath[currentPath.Count - 1].RemoveWall(1);
                            break;
                        case 3: //South wall for current and North for chosen
                            chosenNode.RemoveWall(0);
                            currentPath[currentPath.Count - 1].RemoveWall(2);
                            break;
                        case 4://West wall for current and East for chosen
                            chosenNode.RemoveWall(1);
                            currentPath[currentPath.Count - 1].RemoveWall(3);
                            break;
                    }

                    currentPath.Add(chosenNode);
                    chosenNode.SetState(NodeState.Current);

                }
                else
                {
                    completedNodes.Add(currentPath[currentPath.Count - 1]);

                    currentPath[currentPath.Count - 1].SetState(NodeState.Completed);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
                yield return new WaitForSeconds(0.05f);
            }

        }
        else
        {
            myText.text = "This maze can not be generated. The maze has to be between 10x10 and 250x250";
            myText.enabled = true;
        }
    }
}
