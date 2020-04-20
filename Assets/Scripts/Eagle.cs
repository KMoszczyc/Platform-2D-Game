using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] transforms;
    [SerializeField] private float speed;

    private SpriteRenderer renderer;
    [SerializeField] private float health = 4f;

    private int targetPosIndex = 1;

    // Start is called before the first frame update
    void Start()
    {

        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (transform.position == transforms[targetPosIndex].position)
            targetPosIndex++;

        if (targetPosIndex >= transforms.Length)
            targetPosIndex = 0;


        if (transform.position.x < transforms[targetPosIndex].position.x)
            renderer.flipX = true;
        if (transform.position.x > transforms[targetPosIndex].position.x)
            renderer.flipX = false;

        transform.position = Vector3.MoveTowards(transform.position, transforms[targetPosIndex].position, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        for(int i=0;i<transforms.Length;i++)
            if(i+1 >=transforms.Length)
                Gizmos.DrawLine(transforms[i].position, transforms[0].position);
            else
                Gizmos.DrawLine(transforms[i].position, transforms[i+1].position);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Missile")
        {
            gameManager.PlayHitSound();
            Destroy(collider.gameObject);
            health -= 0.5f;
            StartCoroutine(TurnRedWhenHit());
        }
    }

    IEnumerator TurnRedWhenHit()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);

        if (health < 0)
            Destroy(gameObject);
        renderer.color = Color.white;
    }

}
