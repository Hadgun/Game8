using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bentenk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Tambahkan otomatis kalau belum ada
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        if (col == null)
            col = gameObject.AddComponent<BoxCollider2D>();

        // Supaya tidak jatuh tapi tetap punya massa
        rb.bodyType = RigidbodyType2D.Static;

        // Atur massa (walau static, tetap bisa diset)
        rb.mass = 100f;

        // Pastikan collider aktif
        col.isTrigger = false;
    }
}
