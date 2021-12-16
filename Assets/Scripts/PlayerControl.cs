using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public int breadHeld { get; set; }
    public int butterHeld { get; set; }
    public List<int> jamHeld { get; set; }
    private bool grounded;
    [SerializeField] private int jumpHeight;
    [SerializeField] private float speed;

    private Animator animator;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jamHeld = new List<int>(3) {0, 0, 0};
        breadHeld = 0;
        butterHeld = 0;
        animator = GetComponent<Animator>();
        animator.Play("Idle_Anim");
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        //jump if grounded, don't jump if not
        inputVec.y = grounded ? inputVec.y : 0;
        rb.velocity = inputVec * speed;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        /* when player presses input button */
        /* set rigidbody velocity to some dir */
    }
}
