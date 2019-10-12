using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private WhichPlayer multiplayerScript;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1.2f;
    private Vector2 movement;
    private bool facingRight = true;
    private bool walking;

    private Rigidbody2D rb;

    public float margin = 0;

    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        multiplayerScript = GetComponent<WhichPlayer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!SceneManagerScript.Instance.isPaused)
        {
            GetInput();
            HandleDirection();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void GetInput()
    {
        float x = Input.GetAxis("Horizontal" + multiplayerScript.idPlayer);
        float y = Input.GetAxis("Vertical" + multiplayerScript.idPlayer);

        if (x + margin < 0 || (x - margin > 0))
        {
            movement.x = x;
        }
        else
        {
            movement.x = 0;
        }

        if (y + margin < 0 || y - margin > 0){
            movement.y = y;
        }
        else
        {
            movement.y = 0;
        }

    }

    void HandleDirection()
    {
        if (movement.y < 0) {
            animator.SetTrigger("front");
            walking = true;
        }
        else if (movement.y > 0)
        {
            animator.SetTrigger("back");
            walking = true;
        }

        else
        {
            if (movement.x < 0 || movement.x > 0)
            {
                flipPlayer();
                animator.SetTrigger("side");
                walking = true;
            }

            else
            {
                walking = false;
            }
        }

        if (walking && !animator.GetBool("walking"))
        {
            animator.SetBool("walking", true);
        }
        else if (!walking && animator.GetBool("walking"))
        {
            animator.SetBool("walking", false);
        }
    }

    void flipPlayer()
    {
        if(facingRight && movement.x <= 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = !facingRight;
        }
        else if (!facingRight && movement.x >= 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = !facingRight;
        }
    }
}
