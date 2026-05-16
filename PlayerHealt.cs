using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    [SerializeField] private Scrollbar sb;
    private bool isGameOver = false;

    private void Start()
    {
        if (sb != null)
            sb.size = 1f;
        else
            Debug.LogError("Scrollbar belum di-assign di PlayerHealt!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (sb != null)
            {
                sb.size -= 0.4f;
                Debug.Log("Player terkena enemy! Health tersisa: " + sb.size);
                
                if (sb.size <= 0f && !isGameOver)
                {
                    isGameOver = true;
                    Debug.Log("========================================");
                    Debug.Log("============ GAME OVER! ===============");
                    Debug.Log("========================================");
                }
            }
        }
    }

    public float GetCurrentHealth()
    {
        return sb != null ? sb.size : 0f;
    }
}