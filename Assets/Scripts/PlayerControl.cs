using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private int breadHeld { get; set; }
    private int butterHeld { get; set; }
    private List<int> jamHeld { get; set; }
    private bool grounded;
    [SerializeField] private int jumpHeight;
    [SerializeField] private float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        inputVec.y = 0;
        rb.velocity = inputVec * speed;

        if (grounded == true)
        {
            // jump
        }
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
