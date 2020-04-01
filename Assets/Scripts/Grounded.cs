using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Ground")
        {
            playerController.isGrounded = true;
        }
        if (collider.tag == "Platform")
        {
            playerController.isGrounded = true;
            player.transform.parent = collider.gameObject.transform;
        }
        if(collider.tag == "Spikes")
        {
            playerController.isGrounded = false;
            Vector3 temp = player.transform.position;
            temp.y += 2;
            player.transform.position = temp;
            playerController.ApplyForce(new Vector2(0, 15));
            playerController.LoseHealth();
        }
        if (collider.tag == "Gem")
        {
            Destroy(collider.gameObject);
            playerController.AddGem();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {

        playerController.isGrounded = false;

        if (collider.tag == "Platform")
        {
            player.transform.parent = null;
        }
    }
}
