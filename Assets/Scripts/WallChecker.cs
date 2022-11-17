using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public bool wall = false;

    
    private void OnTriggerStay(Collider other)
    {
        //if the player stay next to a wall, that means hit is true
        if (other.gameObject.tag == "Wall")
        {
            wall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //when the wall is no longer in the trigger area, the variable turns false
        if (other.gameObject.tag == "Wall")
        {
            wall = false;
        }
    }
}
