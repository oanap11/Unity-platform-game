using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    void Start()
    {
        //registers the (number of) diamonds every time we run the game
        Door.instance.RegisterDiamond();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(TagManager.PLAYER_TAG))
        {
            Door.instance.DiamondCollected();
            gameObject.SetActive(false);
        }

    }
}
