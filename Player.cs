using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 8f;

    [Header("Input")]
    [SerializeField] private InputActionAsset inputAsset; // Drag InputGame.inputactions ke sini

    private InputAction moveAction;
    private InputAction jumpAction;

    private Rigidbody2D rb;
    private bool isGrounded;

    private Vector2 moveInput;

    void Awake()
    {
        // Setup Input
        if (inputAsset != null)
        {
            var playerMap = inputAsset.FindActionMap("Player");
            moveAction = playerMap.FindAction("Move");
            jumpAction = playerMap.FindAction("Jump");

            moveAction.Enable();
            jumpAction.Enable();
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Baca input gerak
        if (moveAction != null)
            moveInput = moveAction.ReadValue<Vector2>();

        // Gerak horizontal
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        // Lompat
        if (jumpAction != null && jumpAction.WasPressedThisFrame() && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Deteksi tanah (tetap pakai collision seperti sebelumnya)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void OnEnable()
    {
        moveAction?.Enable();
        jumpAction?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
        jumpAction?.Disable();
    }
}   
