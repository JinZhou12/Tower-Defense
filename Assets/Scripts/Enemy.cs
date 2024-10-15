using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.current.path[pathIndex];
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == GameManager.current.path.Length)
            {
                Destroy(gameObject);
                GameManager.current.ReduceLife(stats.damage);
                return;
            }
            else
            {
                target = GameManager.current.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }
}
