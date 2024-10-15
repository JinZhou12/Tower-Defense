using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public int cost;
    private bool selected = false;
    private bool buying = false;
    // References
    public GameObject TowerPrefab;
    public TextMeshProUGUI costText;
    public GameObject SelectMask;
    public GameObject PurchaseMask;
    private SpriteRenderer icon;
    // Start is called before the first frame update
    void Start()
    {
        // Setting cost
        if (costText == null) costText = transform.parent.transform.Find("Cost").GetComponent<TextMeshProUGUI>();
        costText.text = cost.ToString();
        // Setting icon
        icon = GetComponent<SpriteRenderer>();
        icon.sprite = TowerPrefab.transform.Find("Main").GetComponent<SpriteRenderer>().sprite;
        icon.color = TowerPrefab.transform.Find("Main").GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.current.money < cost){
            // If can't afford
            PurchaseMask.SetActive(true);
            SelectMask.SetActive(false);
            GameManager.current.SetBuyTower(null);
            return;
        } else PurchaseMask.SetActive(false);


        if (Input.GetMouseButtonDown(0)) {
            if (selected) {
                buying = !buying;
                SelectMask.SetActive(buying);
            }
            else SelectMask.SetActive(false);

            if (buying) GameManager.current.SetBuyTower(this);
            else GameManager.current.SetBuyTower(null);
        }
    }

    private void OnMouseOver() {
        selected = true;
    }

    private void OnMouseExit() {
        selected = false;
    }
}
