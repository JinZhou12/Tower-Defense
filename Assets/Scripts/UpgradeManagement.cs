using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManagement : MonoBehaviour
{
    // Upgrade types: 1 - damage up, 2 - projectile damage up, 3 - attack speed up, 4 - projectile speed up
    [Serializable]
    public class UpgradeInfo{
        public int cost;
        public float amount;
    }
    [Serializable]
    public class UpgradePath{
        public int type;
        public UpgradeInfo[] upgrades;
    }
    [SerializeField] private UpgradePath[] upgradepaths = new UpgradePath[3];
    private bool selected;
    private int[] upgradeProgress = {0, 0, 0};
    private Color purchasableColor = new Color(0,0,0,225);
    private Color unpurchasableColor = new Color(224,224,224,225);
    // References 
    private GameObject upgradeMenu;
    public BoxCollider2D offTrigger;
    public PolygonCollider2D onTrigger;
    private Image[] subUpgradeIcons = new Image[3];
    private TextMeshProUGUI[] subUpgradeCosts = new TextMeshProUGUI[3];

    void Start()
    {
        // Find upgrade menu and upgrad icon refs
        upgradeMenu = transform.Find("UpgradeMenu").gameObject;
        GameObject canvas = upgradeMenu.transform.Find("Canvas").gameObject;
        // Find cost text objects
        subUpgradeIcons[0] = canvas.transform.Find("Upgrade1").GetComponent<Image>();
        subUpgradeIcons[1] = canvas.transform.Find("Upgrade2").GetComponent<Image>();
        subUpgradeIcons[2] = canvas.transform.Find("Upgrade3").GetComponent<Image>();
        subUpgradeCosts[0] = canvas.transform.Find("Cost1").GetComponent<TextMeshProUGUI>();
        subUpgradeCosts[1] = canvas.transform.Find("Cost2").GetComponent<TextMeshProUGUI>();
        subUpgradeCosts[2] = canvas.transform.Find("Cost3").GetComponent<TextMeshProUGUI>();
        UpdateUpgrade(0);
        UpdateUpgrade(1);
        UpdateUpgrade(2);
        // Setting trigger boxes
        offTrigger.enabled = true;
        onTrigger.enabled = false;
    }

    private void UpdateUpgrade(int upgradePathInd){
        UpgradePath currUpgradePath = upgradepaths[upgradePathInd];
        // if already upgraded everything in this path, do nothing
        if (upgradeProgress[upgradePathInd] > currUpgradePath.upgrades.Length) return;

        // Some parameters for clarification
        upgradeProgress[upgradePathInd] += 1;
        int currUpgrade = upgradeProgress[upgradePathInd];

        if (currUpgrade > currUpgradePath.upgrades.Length){
            subUpgradeCosts[upgradePathInd].text = "";
        } else{
            subUpgradeCosts[upgradePathInd].text = currUpgradePath.upgrades[currUpgrade-1].cost.ToString();
        }
    }

    public void Upgrade(int upgradePathInd) {
        UpgradePath currUpgradePath = upgradepaths[upgradePathInd];
        // if already upgraded everything in this path, do nothing
        if (upgradeProgress[upgradePathInd] > currUpgradePath.upgrades.Length) return;

        int currUpgrade = upgradeProgress[upgradePathInd];
        int cost = currUpgradePath.upgrades[currUpgrade - 1].cost;

        // if not enough money, do nothing
        if (GameManager.current.money < cost) return;

        Stats stat = transform.Find("Main").gameObject.GetComponent<Stats>();
        float amountChange = currUpgradePath.upgrades[currUpgrade - 1].amount;
        // Update Move SubUpgrade to next
        UpdateUpgrade(upgradePathInd);
        // Update currency
        GameManager.current.ChangeMoney(-cost);

        switch (currUpgradePath.type){
            // Damage
            case 1:
                stat.damage += (int)amountChange;
                // Debug.Log(string.Format("Damage increased to {0}", stat.damage));
                return;
            // Projectile Damage
            case 2:
                stat.projectileDamage += (int)amountChange;
                // Debug.Log(string.Format("Proj Damage increased to {0}", stat.projectileDamage));
                return;
            // Attack Speed
            case 3:
                stat.cooldown -= amountChange;
                // Debug.Log(string.Format("Cooldown increased to {0}", stat.cooldown));
                return;
            // Projectile Speed
            case 4:
                stat.projectileSpeed += amountChange;
                // Debug.Log(string.Format("Proj Speed increased to {0}", stat.projectileSpeed));
                return;
        };
    }

    void Update() {
        // Deactivate menu if clicked away
        if (Input.GetMouseButtonDown(0)) {
            if (selected) {
                offTrigger.enabled = false;
                onTrigger.enabled = true;
                upgradeMenu.SetActive(true);
            }
            else {
                offTrigger.enabled = true;
                onTrigger.enabled = false;
                upgradeMenu.SetActive(false);
            }
        }

        // Toggle upgrades on and off based on current money
        for (int i = 0; i < 3 ; i++){
            UpgradePath currUpgradePath = upgradepaths[i];
            int currUpgrade = upgradeProgress[i];
            if ( currUpgrade > currUpgradePath.upgrades.Length ||
                GameManager.current.money < currUpgradePath.upgrades[currUpgrade - 1].cost)
            {
                subUpgradeIcons[i].color = unpurchasableColor;
            }  else{
                subUpgradeIcons[i].color = purchasableColor;
            }
        }
    }

    private void OnMouseOver() {
        selected = true;
    }

    private void OnMouseExit() {
        selected = false;
    }
}
