using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5.0f;
    private Vector2 movement;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal1");
        movement.y = Input.GetAxisRaw("Vertical1");
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
