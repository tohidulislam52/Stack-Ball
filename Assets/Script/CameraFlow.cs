using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    private Vector3 flowcamara;
    private Transform player,win;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void LateUpdate()
    {
        if(win == null)
           win = GameObject.FindWithTag("Done").GetComponent<Transform>();
        
        if(transform.position.y -2 > player.position.y && transform.position.y > win.position.y +4)
        {
            flowcamara = new Vector3(transform.position.x,player.position.y,transform.position.z);
            
            transform.position = new Vector3(transform.position.x,flowcamara.y +2 ,-6.5f);
        //     transform.position += new Vector3(0,2,0);
        }
        
        
        // transform.position = Vector3.SmoothDamp(transform.position,player.position, ref valocity,speed);
    }
}
