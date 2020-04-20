using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0.1f;
    private PlayerController playerMovement;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        Vector3 temp = transform.position;

        temp.x = player.transform.position.x;
        //if (Mathf.Abs(player.transform.position.y - transform.position.y) > 1)
        //    temp.y = Vector3.MoveTowards(transform.position, player.transform.position,10* smoothSpeed * Time.deltaTime).y;
        //else
        //    temp.y = Vector3.MoveTowards(transform.position, player.transform.position, smoothSpeed * Time.deltaTime).y;
        float speedDueDist = Mathf.Pow(player.transform.position.y - transform.position.y, 2);
        temp.y = Vector3.MoveTowards(transform.position, player.transform.position, speedDueDist * smoothSpeed * Time.deltaTime).y;

        transform.position = temp;
        
    }
    
}
