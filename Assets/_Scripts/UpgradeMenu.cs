using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    PlayerStatus status;

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
