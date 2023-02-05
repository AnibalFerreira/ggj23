using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    private bool isFacingRight = true;
    private bool movingRight = true;
    private float moveHorizontal;
    private float moveSpeed = 4f;
    private bool isJumping = false;
    private bool alive;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveHorizontal = 1f;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (movingRight) moveHorizontal = 1f;
        else moveHorizontal = -1f;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }

        if (collision.gameObject.tag == "PatrolArea" || collision.gameObject.tag == "Enemy")
        {
            isFacingRight = !isFacingRight;
            movingRight = !movingRight;
            if (moveHorizontal == -1f) moveHorizontal = 1f;
            else moveHorizontal = -1f;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
