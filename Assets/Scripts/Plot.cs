using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    private bool selected = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            ShopItem tower = GameManager.current.selectedTower;
            if (tower == null) return;

            if (selected) {
                GameManager.current.ChangeMoney(-tower.cost);
                Instantiate(tower.TowerPrefab, transform.position, transform.rotation);
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
