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
        private Transform mainCamera;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 inputVec = Vector2.zero;
    [SerializeField] private bool grounded;
    private bool controllable;
    private Vector3 scaleFactor;
    private float bufferTime = .2f;
    [SerializeField] private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main.transform;
        scaleFactor = transform.localScale;
        jamHeld = new List<int>(3) {0, 0, 0};
        breadHeld = 0;
        butterHeld = 0;
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
            animator.SetBool("IsJumping", false);
            timer = 0;
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
        else if (other.gameObject.tag == "Ground")
        {
            animator.SetBool("IsJumping", false);
        }
    }

    void FixedUpdate()
    {
        // Set x-velocity without changing y-velocity
        if(controllable) rb.velocity = new Vector2(inputVec.x * speed, rb.velocity.y);
        if(!grounded) timer += Time.fixedDeltaTime;
        // Flip duck direction and update animator state
        if (inputVec.x == 0)
            animator.SetBool("IsRunning", false);
        else
        {
            animator.SetBool("IsRunning", true);

            if (inputVec.x > 0)
                transform.localScale = scaleFactor;
            else if (inputVec.x < 0)
                transform.localScale = new Vector3(-1 * scaleFactor.x, scaleFactor.y, scaleFactor.z);
        }

        // Jump only if groundedd
        if (inputVec.y > 0 && (grounded || timer < bufferTime)){
            rb.velocity = new Vector2(rb.velocity.x,0f);
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
            timer = bufferTime;
            animator.SetBool("IsJumping", true);
        }

        // Update position of camera
        float xcord = this.transform.position.x;
        float ycord = this.transform.position.y;
        if(xcord <= 0.7) {
            xcord = mainCamera.position.x;
        }
        if(ycord <= -3 || ycord >= 10){
            ycord = mainCamera.position.y;
        }
        mainCamera.position = new Vector3(xcord, ycord, mainCamera.position.z);
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
