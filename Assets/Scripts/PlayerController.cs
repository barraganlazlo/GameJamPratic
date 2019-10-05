using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private WhichPlayer multiplayerScript;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1.2f;
    private Vector2 movement;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        multiplayerScript = GetComponent<WhichPlayer>();
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

    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal"+multiplayerScript.idPlayer);
        movement.y = Input.GetAxisRaw("Vertical" + multiplayerScript.idPlayer);
    }
}
