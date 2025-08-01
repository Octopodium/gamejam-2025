using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IResetavel
{
    #region Declaration
    public static Player Instance { get; private set; }

    [SerializeField] private float speed;
    public InputRef inputRef;
    private Vector3 movement;
    public Vector2 movementDirection;
    public Vector2 direction;
    public PlayerData data;
    public Rigidbody rb;
    [SerializeField] private Animator animator;

    private bool isGrounded;
    private bool isFalling;
    private bool isJumping;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheckPoint; 
    [SerializeField] private float _groundCheckSize = 0.2f; 
    [SerializeField] private LayerMask groundLayer;

    private float LastOnGroundTime; 

    // Interator
    Interator interator;
    public System.Action OnInteragirPress;

    [Header("Segurador")]
    public Transform itemHolder;
    public bool estaSegurando => itemHolder.childCount > 0;
    public Transform itemSegurado => itemHolder.childCount > 0 ? itemHolder.GetChild(0) : null;

    [Header("Arremessavel")]
    public float arremessoForce = 10f;
    public float arremessoAltura = 2f;

    #endregion

    #region Animation Tags:

    private int airbourne = Animator.StringToHash("Airbourne");
    private int walking = Animator.StringToHash("Walking");
    private int carrying = Animator.StringToHash("Carrying");

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
        inputRef.InteractEvent += Interagir;


        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        choes = new Collider[8];

        interator = GetComponent<Interator>();
    }

    void Start()
    {
        SetGravityScale(data.gravityScale);
    }

    void Update()
    {
        Movement();
        GroundCheck();

        

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            isJumping = false;
            isFalling = false;
            animator.SetBool(airbourne, false);
        }
        else if (!isGrounded && rb.linearVelocity.y < 0)
        {
            isFalling = true;
        }

        if (!isGrounded && rb.linearVelocity.y > 0)
        {
            animator.SetBool(airbourne, true);
            isJumping = true;
        }

        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            SetGravityScale(data.gravityScale * data.fallGravityMult);
        }
        else
        {
            SetGravityScale(data.gravityScale);
        }

        LastOnGroundTime -= Time.deltaTime;
        //animator.SetFloat("inputY", rb.linearVelocity.y);
    }

    private void Movement()
    {
        if (movementDirection.magnitude > 0.1f)
        {
            Vector3 move = new Vector3(movement.x, 0, 0); // Movimento no eixo X
            transform.Translate(move * speed * Time.deltaTime);

            if(move.x > 0){
                animator.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if( move.x < 0){
                animator.transform.localScale = new Vector3(1, 1, 1);
            }

            animator.SetBool(walking, true);
        }
        else
        {
            animator.SetBool(walking, false);
        }
    }

    private void Move(Vector2 dir)
    {
        movementDirection = dir;

        if (movementDirection.magnitude > 0f) direction = movementDirection.normalized;

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
            animator.SetBool(airbourne, isJumping);
        }
        else
        {
            animator.SetBool(airbourne, !isJumping);
        }

        if (isGrounded && !isJumping)
        {
            rb.AddForce(Vector3.up * data.jumpForce, ForceMode.Impulse);
        }
    }

    Collider[] choes;
    ChaoGrudavel chaoGrudado;

    private void GroundCheck()
    {
        int choesCount = Physics.OverlapSphereNonAlloc(_groundCheckPoint.position, _groundCheckSize, choes, groundLayer);
        isGrounded = choesCount > 0;

        if (isGrounded) {
            LastOnGroundTime = 0.2f; 
        }

        if (choesCount > 0) {
            ChaoGrudavel chaoGrudavel = choes[0].GetComponent<ChaoGrudavel>();
            if (chaoGrudavel != null) {
                if (chaoGrudado != chaoGrudavel && chaoGrudado != null) {
                    chaoGrudado.Desgrudar(transform);
                }

                chaoGrudavel.Grudar(transform);
                chaoGrudado = chaoGrudavel;
            } else if (chaoGrudado != null) {
                chaoGrudado.Desgrudar(transform);
                chaoGrudado = null;
            }
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
    
    public void Interagir() {
        if (!interator.Interagir() && estaSegurando) {
            ArremessarItem();
        }
    }

    #region Segurando coisas

    public void SegurarItem(Transform item) {
        if (item == null) return;

        item.SetParent(itemHolder);
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;

        Arremessavel arremesso = item.GetComponent<Arremessavel>();
        if (arremesso != null) {
            arremesso.OnHold();
        }
    }

    #endregion

    public void Resetar() {
        foreach (Transform child in itemHolder) {
            Destroy(child.gameObject);
        }

        rb.linearVelocity = Vector3.zero;
    }

    #region Arremessavel

    public void ArremessarItem() {
        Transform item = itemSegurado;
        if (item == null) return;

        item.SetParent(null);

        Arremessavel arremesso = item.GetComponent<Arremessavel>();
        if (arremesso != null) {
            Vector3 direcao = transform.right * direction.x;
            direcao.y = arremessoAltura;
            arremesso.OnRelease();
            arremesso.rb.AddForce(direcao * arremessoForce, ForceMode.Impulse);
        }
    }

    #endregion
}