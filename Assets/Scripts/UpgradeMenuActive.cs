using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuActive : MonoBehaviour
{
    public UpgradeManagement upgradeManagement;
    // Start is called before the first frame update
    void Start()
    {
        if (upgradeManagement == null) upgradeManagement = transform.parent.GetComponent<UpgradeManagement>();
    }

    private void OnMouseOver() {
        upgradeManagement.SetSelect(true);
    }

    private void OnMouseExit() {
        upgradeManagement.SetSelect(false);
    }
}
