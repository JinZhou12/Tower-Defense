using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public List<GameObject> enemies;
    public int money = 0;
    // Start is called before the first frame update
    private void Awake() {
        current = this;
    }

    private void Start() {
        // Temp
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public void AddEnemy(GameObject enemy){
        enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy){
        enemies.Remove(enemy);
    }
}
