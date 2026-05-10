using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemySmart : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase }

    [Header("Referensi")]
    public Transform player;

    [Header("Speed")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Detection")]
    public float chaseRange = 5f;
    public float patrolTime = 2f;

    [Header("Obstacle Check")]
    public LayerMask obstacleLayer;
    public float obstacleCheckDistance = 1f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float patrolTimer;
    private EnemyState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        currentState = EnemyState.Patrol;
        ChooseRandomDirection();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol(distance);
                break;
            case EnemyState.Chase:
                Chase(distance);
                break;
        }

        FlipSprite();
    }

    private void FixedUpdate()
    {
        float speed = currentState == EnemyState.Patrol ? patrolSpeed : chaseSpeed;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleCheckDistance, obstacleLayer);
        if (hit.collider != null)
        {
            ChooseRandomDirection();
        }

        rb.velocity = direction * speed;
    }

    void Patrol(float distance)
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolTime)
        {
            ChooseRandomDirection();
            patrolTimer = 0f;
        }

        if (distance <= chaseRange)
        {
            currentState = EnemyState.Chase;
        }
    }

    void Chase(float distance)
    {
        direction = (player.position - transform.position).normalized;

        if (distance > chaseRange)
        {
            currentState = EnemyState.Patrol;
            ChooseRandomDirection();
        }
    }

    void FlipSprite()
    {
        if (direction.x != 0)
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
    }

    void ChooseRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        direction = new Vector2(x, y).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bentenk"))
        {
            ChooseRandomDirection();
        }
    }
}
