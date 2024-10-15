using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float timer = 0f;
    private int projHealth = 1;
    // References
    private Stats stats;
    private GameObject target;
    [SerializeField] GameObject projectilePrefab;

    void Start()
    {
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        timer += Time.deltaTime;
        if (timer > stats.cooldown){
            Shoot();
            timer = 0f;
        }
    }

    private void OnDestroy() {
        // TODO: for adding on destroy effects
    }

    private void FindTarget(){
        if (GameManager.current.enemies.Count == 0) target = null;

        float distance = Mathf.Infinity;
        Vector3 currPos = transform.position;
        // Compare distance to all enemy objects
        foreach(GameObject enemy in GameManager.current.enemies){
            Vector3 diff = enemy.transform.position - currPos;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                // Set as target if has less distance to turret
                target = enemy;
                distance = curDistance;
            }
        }
    }

    private void Shoot(){
        if (target == null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        // Adjust projectile direction and speed
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * stats.projectileSpeed; // Set the projectile's final velocity

        Stats projStat = projectile.GetComponent<Stats>();
        projStat.damage = stats.projectileDamage;
        projStat.speed = stats.projectileSpeed;
        projStat.health = projHealth;

        // Play Shooting Sound
        // m_playerShootAudio.Play();
    }
}
