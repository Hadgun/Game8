using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAi : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase }

    [Header("Referensi")]
    [SerializeField] private Transform player;

    [Header("Speed")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;

    [Header("Detection")]
    [SerializeField] private float chaseRange = 4f;
    [SerializeField] private float patrolTime = 2f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float patrolTimer;
    private EnemyState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = EnemyState.Patrol;
        ChooseRandomDirection();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (currentState == EnemyState.Patrol)
            Patrol(distance);
        else if (currentState == EnemyState.Chase)
            Chase(distance);

        RotateToMovement();
    }

    private void FixedUpdate()
    {
        float speed = currentState == EnemyState.Patrol ? patrolSpeed : chaseSpeed;
        rb.velocity = direction * speed;
    }

    // hanya ada SATU Patrol, tidak ada duplikat
    private void Patrol(float distance)
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

    private void Chase(float distance)
    {
        direction = (player.position - transform.position).normalized;

        if (distance > chaseRange)
        {
            currentState = EnemyState.Patrol;
            ChooseRandomDirection();
        }
    }

    private void RotateToMovement()
    {
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }
    }

    private void ChooseRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        direction = new Vector2(x, y).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState == EnemyState.Patrol)
        {
            ChooseRandomDirection();
        }
    }
}
