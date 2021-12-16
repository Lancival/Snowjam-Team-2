using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerControl : MonoBehaviour
{
    public int breadHeld { get; set; }
    public int butterHeld { get; set; }
    public bool presentHeld { get; set; }
    public List<int> jamHeld { get; set; }
    
    [SerializeField] private int jumpHeight;
    [SerializeField] private float speed;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject UIObject;
    [SerializeField] public List<Sprite> sprites;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 inputVec = Vector2.zero;
    private bool grounded;
    
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
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

    /* public void UpdateDisplay()
    {
        for(int i = 0; i < breadHeld; i++){
            GameObject nextUIObject = Instantiate(UIObject, canvas.transform);
            nextUIObject.transform.position = nextUIObject.transform.position + new Vector3(i*150,0,0);
            nextUIObject.GetComponent<Image>().image = sprites[0].texture;
        }
        for(int i = 0; i < butterHeld; i++){
            Instantiate(UIObject, canvas.transform);
        }
        foreach(var jam in jamHeld){
            for(int i = 0; i < jam; i++){
                Instantiate(UIObject, canvas.transform);
            }
        }
    } */
}
