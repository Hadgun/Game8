using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float speed = 2f;
    public float distance = 3f;           // Jarak patrol dari posisi awal

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private Vector3 startPosition;
    private bool movingRight = true;
    private float currentSpeed;

    void Start()
    {
        startPosition = transform.position;

        // Ambil komponen otomatis jika belum di-assign
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();

        // Saran: Set Rigidbody2D Body Type = Kinematic di Inspector
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        Patrol();

        // Update animasi Speed (kecepatan aktual)
        currentSpeed = Mathf.Abs(rb != null ? rb.velocity.x : speed);
        if (anim != null)
            anim.SetFloat("Speed", currentSpeed);
    }

    void Patrol()
    {
        if (movingRight)
        {
            // Gerak ke kanan
            if (rb != null)
                rb.velocity = new Vector2(speed, rb.velocity.y);
            else
                transform.Translate(Vector2.right * speed * Time.deltaTime);

            // Cek batas kanan
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            // Gerak ke kiri
            if (rb != null)
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            else
                transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Cek batas kiri
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        // Flip sprite dengan mengubah localScale (cara paling aman untuk 2D)
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;

        // Kalau mau pakai SpriteRenderer.flip (alternatif):
        // SpriteRenderer sr = GetComponent<SpriteRenderer>();
        // if (sr != null) sr.flipX = !sr.flipX;
    }

    // Optional: Reset posisi patrol (bisa dipanggil dari luar)
    public void ResetPatrol()
    {
        transform.position = startPosition;
        movingRight = true;
        Flip(); // pastikan menghadap kanan lagi
    }
}
