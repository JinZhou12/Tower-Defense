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
    public int life = 3;
    public GameObject moneyUI;
    public GameObject lifeUI;
    public GameObject shopUI;
    public ShopItem selectedTower;
    public Transform startPoint;
    public Transform[] path; 
    // Start is called before the first frame update
    private void Awake() {
        current = this;
    }

    private void Start() {
        // Temp
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        // Position UI components

        // Shop display
        shopUI.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width * 4/10, 0, 0);
        RectTransform shop = shopUI.GetComponent<RectTransform>();
        shop.sizeDelta = new Vector2(Screen.width * 1.11f/4, Screen.height);
        // Money display
        Vector3 moneyPos = moneyUI.GetComponent<RectTransform>().localPosition;
        moneyUI.GetComponent<RectTransform>().localPosition = new Vector3(moneyPos.x + 100, Screen.height / 2 - 50, 0);
        moneyUI.transform.Find("MoneyText").GetComponent<TextMeshProUGUI>().text = money.ToString();
        // Life display
        Vector3 lifePos = lifeUI.GetComponent<RectTransform>().localPosition;
        lifeUI.GetComponent<RectTransform>().localPosition = new Vector3(lifePos.x - 250, Screen.height / 2 - 50, 0);
        lifeUI.transform.Find("LifeText").GetComponent<TextMeshProUGUI>().text = life.ToString();
    }

    public void ChangeMoney(int amount){
        money += amount;
        moneyUI.transform.Find("MoneyText").GetComponent<TextMeshProUGUI>().text = money.ToString();
    }

    public void ReduceLife(int amount){
        life -= amount;
        lifeUI.transform.Find("LifeText").GetComponent<TextMeshProUGUI>().text = life.ToString();
    }

    public void SetBuyTower(ShopItem shopItem){
        selectedTower = shopItem;
    }

    public void AddEnemy(GameObject enemy){
        enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy){
        enemies.Remove(enemy);
    }
}
