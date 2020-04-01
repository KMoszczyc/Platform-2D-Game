using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
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


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startPos = transform.position;
        renderer = GetComponent<SpriteRenderer>();
        bottomLine = GameObject.Find("BottomLine").transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
      
        if(!gameManager.disablePlayerInput)
            direction = Input.GetAxisRaw("Horizontal");
        if (gameManager.disablePlayerInput)
            direction = 0;

        if (direction < 0)
            renderer.flipX = true;
        if (direction >0)
            renderer.flipX = false;

        Vector3 movement = new Vector3(direction, 0,0) * Time.deltaTime * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(movement.x));
        animator.SetBool("IsJumping", !isGrounded);

        transform.position += movement;
        Jump();

        if (IsBelowBottomLine())
            GoBackToStart();

        

        Debug.Log(health);
    }

    private void GoBackToStart()
    {
        transform.position = startPos;
    }

    private bool IsBelowBottomLine()
    {
        return transform.position.y < bottomLine;
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
}
