using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{

    public GameManager manager = null;


    // Start is called before the first frame update
    void Start()
    {
        manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            manager.GameCompleted();
        }
    }

}
