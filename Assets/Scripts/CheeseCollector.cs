using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseCollector : MonoBehaviour
{
    public Canvas winScreen; //this is the canvas of the Level Complete screen
    public Canvas overlay; //this is the canvas with the Home button and end screen

    //When the player is touching the collider of the objective (cheese) it enables the WinScreen
    //and disable the controllers (which is on the Overlay Canvas)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cheese")
        {
            overlay.GetComponent<Canvas>().enabled = false; //disable the Minigame UI
            winScreen.GetComponent<Canvas>().enabled = true; //enable the Level Complete screen

            gameObject.SetActive(false);
        }
    }

}
