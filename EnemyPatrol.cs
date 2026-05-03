using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float speed = 2f;
    public float distance = 3f; // jarak patrol dari titik awal

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private Vector2 startPosition;
    private bool movingRight = true;
    private float currentSpeed;

    void Start()
    {
        startPosition = transform.position;

        // Ambil komponen otomatis jika belum di-assign
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();

        // Pastikan Rigidbody2D ada
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        // Set sebagai Kinematic biar stabil
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Update()
    {
        // Update animasi
        currentSpeed = Mathf.Abs(speed);
        if (anim != null)
            anim.SetFloat("Speed", currentSpeed);
    }

    void Patrol()
    {
        float moveDir = movingRight ? 1f : -1f;

        Vector2 newPos = rb.position + new Vector2(moveDir * speed * Time.fixedDeltaTime, 0);

        // Gerak pakai physics biar gak tembus
        rb.MovePosition(newPos);

        // Batas patrol kanan
        if (movingRight && transform.position.x >= startPosition.x + distance)
        {
            movingRight = false;
            Flip();
        }
        // Batas patrol kiri
        else if (!movingRight && transform.position.x <= startPosition.x - distance)
        {
            movingRight = true;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    // ⛔ INI BAGIAN PENTING BIAR GAK TEMBUS BENTENK
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bentenk"))
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    // Optional reset
    public void ResetPatrol()
    {
        transform.position = startPosition;
        movingRight = true;

        // pastikan arah benar
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
