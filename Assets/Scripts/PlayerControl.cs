using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public int breadHeld { get; set; }
    public int butterHeld { get; set; }
    public List<int> jamHeld { get; set; }
    private bool grounded;
    
    [SerializeField] private int jumpHeight;
    [SerializeField] private float speed;

    private Vector2 inputVec = Vector2.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jamHeld = new List<int>(3) {0, 0, 0};
        breadHeld = 0;
        butterHeld = 0;
        animator = GetComponent<Animator>();
        //animator.Play("Idle_Anim");
    }

    public void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    void FixedUpdate()
    {
        // Set x-velocity without changing y-velocity
        rb.velocity = new Vector2(inputVec.x * speed, rb.velocity.y);

        // Flip duck direction
        if (inputVec.x > 0)
            transform.localScale = Vector3.one;
        else if (inputVec.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Jump only if grounded
        if (inputVec.y > 0 && grounded)
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }
}
