using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public PlayerStatus status;
    public Button buttonDash;
    public Button buttonSword;
    public Button sword;

    public void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    public void UpgradeShoot()
    {
        status.upgradeShoot = true;
    }
    public void UpgradeDash()
    {
        status.upgradeDodge = true;
    }
    public void UpgradeSword()
    {
        status.upgradeSword = true;
    }

}
