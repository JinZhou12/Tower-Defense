using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public bool friendly;
    public int health;
    public int damage;
    public float speed;

    public void SetHealth(int number){
        health = number;
    }

    public void Damage(int damage){
        // Projectile has durability instead of health
        if (gameObject.tag == "Projectile") health -= 1;
        else health -= damage;
        if (health <= 0) Die();
    }

    public void Die(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Stats otherStat = other.GetComponent<Stats>();
        if (friendly != otherStat.friendly){
            this.Damage(otherStat.damage);
            other.GetComponent<Stats>().Damage(damage);
        }
    }
}
