using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public Animator animator;

    float bottomLine;
    private Vector3 startPos;
    public Rigidbody2D rb;
    public SpriteRenderer renderer;

    public float runSpeed = 1;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    private float direction = 1;

    private int gemsCollected = 0;
    private int health = 3;

    private bool gameWon = false;

    public Sprite goUp;
    public Sprite goDown;

    public GameObject missilePrefab;

    [SerializeField] private Camera cam;

    void Start()
    {
        startPos = transform.position;
        renderer = GetComponent<SpriteRenderer>();
        bottomLine = GameObject.Find("BottomLine").transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("throw missile!!");
            ThrowMissile(Input.mousePosition);
        }

      
        if(!gameManager.disablePlayerInput)
            direction = Input.GetAxisRaw("Horizontal");
        if (gameManager.disablePlayerInput)
            direction = 0;

        if (direction < 0)
            renderer.flipX = true;
        if (direction >0)
            renderer.flipX = false;


        Jump();
        AnimatorController();
        if (IsBelowBottomLine())
            GoBackToStart();
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(direction, 0, 0) * Time.deltaTime * runSpeed;

        transform.position += movement;

        AnimatorController();
    }
    private void GoBackToStart()
    {
        transform.position = startPos;
    }

    private bool IsBelowBottomLine()
    {
        return transform.position.y < bottomLine;
    }

    private void AnimatorController()
    {
        if(rb.velocity.y>0.01)
            animator.SetBool("IsJumping", true);

        if (rb.velocity.y < -0.01)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }

        if (rb.velocity.y ==0)
            animator.SetBool("IsFalling", false);

        if (direction != 0)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);

    }


    private void Jump()
    {
  
        if (!gameManager.disablePlayerInput && Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        
    }
    public void ApplyForce(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

   

    public void AddGem()
    {
        gemsCollected++;
        gameManager.UpdateGems(gemsCollected);
    }
    public void LoseHealth()
    {
        health--;
        gameManager.UpdateHearts(health);
    }

    private void OnValidate()
    {
        if (runSpeed *3 > jumpForce)
        {
            jumpForce = runSpeed * 3;
        }
    }

    private void ThrowMissile(Vector2 mousePos)
    {
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        Debug.Log(point);
        GameObject missile = (GameObject)Instantiate(missilePrefab);
        
        Vector2 direction = (new Vector2(point.x,point.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        missile.transform.position = transform.position + new Vector3(direction.x, direction.y, 0)/2;

        missile.GetComponent<Rigidbody2D>().AddForce(direction * 10, ForceMode2D.Impulse);
    }

    public void PlayHitSound()
    {
        gameManager.PlayHitSound();
    }

    public void PlayGemSound()
    {
        gameManager.PlayGemSound();
    }

}
