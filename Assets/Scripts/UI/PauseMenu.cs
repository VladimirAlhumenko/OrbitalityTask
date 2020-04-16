using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button _saveButton;

    [SerializeField]
    private Button _continueButton;

    [SerializeField]
    private Button _exitButton;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(Continue);
        _saveButton.onClick.AddListener(Save);
        _exitButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(Continue);
        _saveButton.onClick.RemoveListener(Save);
        _exitButton.onClick.RemoveListener(Exit);
    }

    private void Continue()
    {
        Time.timeScale = 1;

        gameObject.SetActive(false);
    }

    private void Save()
    {
        EventManager.SendEvent("OnSaveButtonClicked");
    }

    private void Exit()
    {
        
    }
}
