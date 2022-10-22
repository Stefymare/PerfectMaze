using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed
}
public class MazeNode : MonoBehaviour
{
    [SerializeField] GameObject[] walls; //initialize the list of the walls
    [SerializeField] MeshRenderer floor; //initialize the mesh rendered for the floor (to change color)

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
    }

    //This void is used to change the state color of the floor
    //to make it easier to visualise how the script work
    //base on the state of the node, it will have a certain color
    //white is for Available, yellow for current path nodes
    //and green is for completed noted = nodes with no additional path
    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Available:
                floor.material.color = Color.white;
                break;
            case NodeState.Current:
                floor.material.color = Color.yellow;
                break;
            case NodeState.Completed:
                floor.material.color = Color.green;
                break;
        }
    }
}
