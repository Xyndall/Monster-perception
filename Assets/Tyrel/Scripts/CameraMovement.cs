using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    float rotationX = 0f;

    public GameObject Player = null;
    public GameObject fpsCam = null;

    void Start()
    {
        Cursor.lockState =  CursorLockMode.Locked;
        Cursor.visible = false;

    }


    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            {

                if(hit.collider.tag == "KeyCard")
                {
                    
                    
                    GameObject keyCard = hit.collider.gameObject;

                    Instantiate(keyCard,  transform.position + transform.forward * 1.5f - Vector3.up, transform.rotation, fpsCam.transform);
                    Destroy(hit.collider.gameObject);
                }
            }
        }

    }

}
