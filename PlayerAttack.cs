using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float jarak = 0.4f;

    [Header("Sabit Attack")]
    public bool useScythe = true;
    public Transform scythePoint;
    public float scytheRange = 0.8f;
    public int scytheDamage = 1;
    public LayerMask enemyLayer;
    public float attackCooldown = 0.5f;
    private float lastAttackTime;
    public GameObject scytheSlashEffect;

    void Shoot()
    {
        if (!useScythe)
        {
            Vector3 posisiMenembak = transform.position + transform.up * jarak;
            Instantiate(bullet, posisiMenembak, transform.rotation);
        }
        else
        {
            ScytheAttack();
        }
    }

    void ScytheAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        
        lastAttackTime = Time.time;

        Vector3 attackPosition = scythePoint != null ? scythePoint.position : 
                                 transform.position + (transform.localScale.x > 0 ? Vector3.right : Vector3.left) * scytheRange;
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, scytheRange, enemyLayer);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            Destroy(enemy.gameObject);
            Debug.Log("Sabit mengenai: " + enemy.name);
        }
        
        if (scytheSlashEffect != null)
        {
            GameObject effect = Instantiate(scytheSlashEffect, attackPosition, Quaternion.identity);
            Destroy(effect, 0.2f);
        }
        
        Debug.Log("Serangan sabit!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (useScythe)
        {
            Vector3 attackPosition = scythePoint != null ? scythePoint.position : 
                                     transform.position + (transform.localScale.x > 0 ? Vector3.right : Vector3.left) * scytheRange;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPosition, scytheRange);
        }
    }
}