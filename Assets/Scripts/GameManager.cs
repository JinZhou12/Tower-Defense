using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public List<GameObject> enemies;
    public int money = 100;
    public GameObject moneyUI;
    public GameObject shopUI;
    // Start is called before the first frame update
    private void Awake() {
        current = this;
    }

    private void Start() {
        // Temp
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        // Position UI components
        // Money display
        moneyUI.GetComponent<RectTransform>().localPosition = new Vector3(-(Screen.width / 2), Screen.height / 2 - 50, 0);

        // Shop display
        shopUI.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width * 4/10, 0, 0);
        RectTransform shop = shopUI.GetComponent<RectTransform>();
        shop.sizeDelta = new Vector2(Screen.width * 1/5, Screen.height);
    }

    public void ChangeMoney(int amount){
        money += amount;
        moneyUI.transform.Find("MoneyText").GetComponent<TextMeshProUGUI>().text = money.ToString();
    }

    public void AddEnemy(GameObject enemy){
        enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy){
        enemies.Remove(enemy);
    }
}
