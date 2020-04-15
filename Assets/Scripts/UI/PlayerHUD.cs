using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private Button _shotingButton;

    [SerializeField]
    private TextMeshProUGUI _cooldownText;

    private void Start()
    {
        _shotingButton.interactable = false;
    }

    private void OnEnable()
    {
        EventManager.Subscribe("OnPlayerRocketCooldownDone",OnCooldownDone);

        _shotingButton.onClick.AddListener(Shoot);

        EventManager.Subscribe("StartCooldown", StartTimer);

        EventManager.Subscribe("OnPlanetSelected", ActiveShootButton);
    }

    private void ActiveShootButton(object[] arg0)
    {
        _shotingButton.interactable = true;
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("OnPlayerRocketCooldownDone", OnCooldownDone);

        _shotingButton.onClick.RemoveListener(Shoot);

        EventManager.Unsubscribe("StartCooldown", StartTimer);

        EventManager.Unsubscribe("OnPlanetSelected", ActiveShootButton);
    }


    private void OnCooldownDone(object[] arg0)
    {
        _shotingButton.interactable = true;
    }

    private void Shoot()
    {
        EventManager.SendEvent("OnShootButtonClicked");

        _shotingButton.interactable = false;
    }

    private void StartTimer(object [] args)
    {
        int tine = (int)args[0];

        StartCoroutine(CooldownTimer(tine));
    }    

    private IEnumerator CooldownTimer(int time)
    {
        _cooldownText.gameObject.SetActive(true);
        _cooldownText.text = $"{time}";

        for (int i = time; i > 0; i--)
        {
            yield return new WaitForSeconds(1);

            _cooldownText.text = $"{i}";
        }

        _cooldownText.gameObject.SetActive(false);
    }
}
