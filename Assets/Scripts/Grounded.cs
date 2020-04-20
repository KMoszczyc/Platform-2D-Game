using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    [SerializeField] GameObject player;
    private PlayerController playerController;
    private bool beingSpiked = false;
    private SpriteRenderer renderer;

    private bool beingHitByEagle = false;


    // Start is called before the first frame update
    void Start()
    {

        playerController = player.GetComponent<PlayerController>();
        renderer = player.GetComponent<SpriteRenderer>();

    }
  
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Ground" || collider.tag == "Missile")
        {
            playerController.isGrounded = true;
        }
        if (collider.tag == "Platform")
        {
            playerController.isGrounded = true;
            player.transform.parent = collider.gameObject.transform;
        }
        if(collider.tag == "Spikes" && !beingSpiked)
        {
            playerController.PlayHitSound();
            playerController.isGrounded = false;
            Vector3 temp = player.transform.position;
            player.transform.position = temp;
            playerController.ApplyForce(new Vector2(0, 20));
            playerController.LoseHealth();
            
            beingSpiked = true;
            StartCoroutine(BeingHitBySpikes());
        }
        if (collider.tag == "Gem")
        {
            Destroy(collider.gameObject);
            playerController.AddGem();
            playerController.PlayGemSound();
        }
        if (collider.tag == "Eagle" && !beingHitByEagle)
        {
            playerController.PlayHitSound();
            beingHitByEagle = true;
            playerController.LoseHealth();
            StartCoroutine(BeingHitByEagle());
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.tag == "Ground" || collider.tag == "Missile")
        {
            playerController.isGrounded = false;
        }

        if (collider.tag == "Platform")
        {
            player.transform.parent = null;
        }
        if (collider.tag == "Eagle")
        {
            beingHitByEagle = false;
        }
    }

    IEnumerator BeingHitBySpikes()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        renderer.color = Color.white;
        beingSpiked = false;
    }

    IEnumerator BeingHitByEagle()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        renderer.color = Color.white;

    }
}
