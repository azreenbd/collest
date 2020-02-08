using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float mousePosX, mouseLastPosX, speed;
    private Vector3 offset;
    GameObject player;

    void LateUpdate()
    {
        // camera follow local player avatar
        if(player)
        {
            this.transform.position = player.transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // find local player avatar
        player = GameObject.Find("Local");

        // on mouse left click down
        if (Input.GetMouseButtonDown(1))
        {
            // get mouse position
            mousePosX = Input.mousePosition.x;
        }

        // on mouse left click hold
        if (Input.GetMouseButton(1))
        {
            // if user stop moving mouse, reset initial position
            if (mouseLastPosX == Input.mousePosition.x)
            {
                mousePosX = mouseLastPosX;
            }
            else
            {
                // if mouse position on x axis move
                if (mousePosX < Input.mousePosition.x)
                {
                    speed = Input.mousePosition.x - mousePosX;
                    // rotate right
                    transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
                }
                else if (mousePosX > Input.mousePosition.x)
                {
                    speed = mousePosX - Input.mousePosition.x;
                    // rotate left
                    transform.Rotate(Vector3.down, speed * Time.deltaTime, Space.World);
                }
            }
            
            // get previous mouse position
            mouseLastPosX = Input.mousePosition.x;
        }
    }
}
