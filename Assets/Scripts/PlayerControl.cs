using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Yarn.Unity;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
[DisallowMultipleComponent]

public class PlayerControl : MonoBehaviour
{
    public int breadHeld { get; set; }
    public int butterHeld { get; set; }
    public bool presentHeld { get; set; }
    public List<int> jamHeld { get; set; }

    [Header("Movement Parameters")]
        [Tooltip("How high the player can jump.")]
        [SerializeField] private int jumpHeight;

        [Tooltip("How quickly the player moves horizontally.")]
        [SerializeField] private float speed;

        [Tooltip("How far back the player will bounce back after colliding with an enemy.")]
        [SerializeField] private int bounceDist;

    [Header("Connected Game Objects")]
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject UIObject;
        [SerializeField] public List<Sprite> sprites;
        [SerializeField] public TMP_Text timerText;
        [SerializeField] public GameObject mainCamera;

    private float timer; // Time in seconds
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 inputVec = Vector2.zero;
    private bool grounded;
    private bool controllable;
    
    void Awake()
    {
        timer = 600;
        rb = GetComponent<Rigidbody2D>();
        jamHeld = new List<int>(3) {0, 0, 0};
        breadHeld = 0;
        butterHeld = 0;
        animator = GetComponent<Animator>();
        controllable = true;
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
            if(!controllable) controllable = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            rb.AddForce(new Vector2(-1, 1) * bounceDist, ForceMode2D.Impulse);
            controllable = false;
        }
    }

    void FixedUpdate()
    {
        // Set x-velocity without changing y-velocity
        if(controllable) rb.velocity = new Vector2(inputVec.x * speed, rb.velocity.y);

        // Flip duck direction and update animator state
        if (inputVec.x == 0)
            animator.SetBool("IsRunning", false);
        else
        {
            animator.SetBool("IsRunning", true);

            if (inputVec.x > 0)
                transform.localScale = Vector3.one;
            else if (inputVec.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        // Jump only if grounded
        if (inputVec.y > 0 && grounded){
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
        }

        // Update position of camera
        mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, mainCamera.transform.position.z);
    }

    void Update()
    {    
        int minutes = (int)timer/60;
        int seconds = (int)timer%60;
        timerText.text = seconds < 10 ? $"Time: {minutes}:0{seconds}" : $"Time: {minutes}:{seconds}";
        timer -= Time.deltaTime;
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
