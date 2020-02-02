using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public PlayerStatus status;
    public Button buttonDash;
    public Button buttonSword;
    public Button buttonShoot;

    public Animator parentAnimator;
    public GameObject fader;

    public void Awake()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    public void OnEnable()
    {
        buttonDash.interactable = !status.upgradeDodge;
        buttonSword.interactable = !status.upgradeSword;
        buttonShoot.interactable = !status.upgradeShoot;
        fader.SetActive(false);
    }

    public void UpgradeShoot()
    {
        status.upgradeShoot = true;
        buttonDash.interactable = false;
        buttonSword.interactable = false;
        buttonShoot.interactable = false;
        StartCoroutine(FadeOutRoutine());
    }
    public void UpgradeDash()
    {
        status.upgradeDodge = true;
        buttonDash.interactable = false;
        buttonSword.interactable = false;
        buttonShoot.interactable = false;
        StartCoroutine(FadeOutRoutine());
    }
    public void UpgradeSword()
    {
        status.upgradeSword = true;
        buttonDash.interactable =  false;
        buttonSword.interactable = false;
        buttonShoot.interactable = false;
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        yield return null;
        parentAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1f);
        parentAnimator.gameObject.SetActive(false);
    }

}
