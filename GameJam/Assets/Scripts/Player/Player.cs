using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    #region Declaration
    public static Player Instance { get; private set; }

    [SerializeField] private float speed;
    public InputRef inputRef;
    private Vector3 movement;
    public Vector2 movementDirection;
    public PlayerData data;
    public Rigidbody rb;
    private Animator animator;

    private bool isGrounded;
    private bool isFalling;
    private bool isJumping;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheckPoint; 
    [SerializeField] private float _groundCheckSize = 0.2f; 
    [SerializeField] private LayerMask groundLayer;

    private float LastOnGroundTime; 


    [Header("Segurador")]
    public Transform itemHolder;
    public bool estaSegurando => itemHolder.childCount > 0;
    public Transform itemSegurado => itemHolder.childCount > 0 ? itemHolder.GetChild(0) : null;

    #endregion

    void Awake()
    {
        if (Instance == null) Instance = this;
        else {
            Destroy(gameObject);
            return;
        }


        inputRef.MoveEvent += Move;
        inputRef.JumpEvent += Jump;
        inputRef.JumpEvent -= Jump;


        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ; 
    }

    void Start()
    {
        SetGravityScale(data.gravityScale);
    }

    void Update()
    {
        Movement();
        GroundCheck();

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * data.jumpForce, ForceMode.Impulse);
        }

        // if (isGrounded && rb.linearVelocity.y <= 0)
        // {
        //     isJumping = false;
        //     isFalling = false;
        //     animator.SetBool("isJumpingAnim", false);
        // }
        // else if (!isGrounded && rb.linearVelocity.y < 0)
        // {
        //     isFalling = true;
        // }

        // if (!isGrounded && rb.linearVelocity.y > 0)
        // {
        //     animator.SetBool("isJumpingAnim", true);
        //     isJumping = true;
        // }

        // if (!isGrounded && rb.linearVelocity.y < 0)
        // {
        //     SetGravityScale(data.gravityScale * data.fallGravityMult);
        // }
        // else
        // {
        //     SetGravityScale(data.gravityScale);
        // }

        // LastOnGroundTime -= Time.deltaTime;
        // animator.SetFloat("inputY", rb.linearVelocity.y);
    }

    private void Movement()
    {
        // if (movementDirection.magnitude > 0.1f)
        // {
        //     Vector3 move = new Vector3(movement.x, 0, 0); // Movimento no eixo X
        //     transform.Translate(move * speed * Time.deltaTime);
        //     animator.SetFloat("inputX", movement.x);
        // }
        // else
        // {
        //     animator.SetFloat("inputX", 0);
        // }

            Vector3 move = new Vector3(movement.x, 0, 0); // Movimento no eixo X
            transform.Translate(move * speed * Time.deltaTime);
    }

    private void Move(Vector2 dir)
    {
        movementDirection = dir;
        movement = new Vector3(movementDirection.x, 0, 0);
    }

    private void Jump()
    {
        if ((isGrounded || LastOnGroundTime > 0f) && !isJumping)
        {
            float force = data.jumpForce;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            isJumping = true;
            LastOnGroundTime = 0f;
            animator.SetBool("isJumpingAnim", isJumping);
        }
        else
        {
            animator.SetBool("isJumpingAnim", !isJumping);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(_groundCheckPoint.position, _groundCheckSize, groundLayer);

        if (isGrounded)
        {
            LastOnGroundTime = 0.2f; 
        }
    }

    public void SetGravityScale(float scale)
    {
        Physics.gravity = new Vector3(0, -9.81f * scale, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckSize);
    }
    

    #region Segurando coisas

    public void SegurarItem(Transform item) {
        if (item == null) return;

        item.SetParent(itemHolder);
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;
    }

    #endregion
}