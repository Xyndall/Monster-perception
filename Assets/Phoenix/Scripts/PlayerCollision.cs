using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Colliding with a game object tagged 'Monster' activates another code
        if (collision.gameObject.tag == "Monster")
        {
            FindObjectOfType<GameOver>().EndofGame();
        }
    }
}
