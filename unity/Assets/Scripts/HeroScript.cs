using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    private float moveHorizontal;
    private float moveVertical;
    private float moveSpeed = 16f;
    private float jumpingPower = 47f;
    private bool isFacingRight = true;
    private bool alive;
    Color deadColor;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        alive = true;
        deadColor = new Color(109, 0, 0);
    }

    void Update()
    {
        if (alive)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");


            if ((Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) || (Input.GetButtonDown("Jump") && IsGrounded()))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if ((Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f) || (Input.GetButtonUp("Jump") && rb.velocity.y > 0f))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            Flip();
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spriteRenderer.color = deadColor;
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && moveHorizontal < 0f || !isFacingRight && moveHorizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Spikes" || collision.gameObject.tag == "DeadZone")
        {
            alive = false;   
        }
    }

}