using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField]
    private Button _pauseButton;

    [SerializeField]
    private GameObject _pauseMenu;

    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(Pause);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(Pause);
    }

    private void Pause()
    {
        Time.timeScale = 0;

        _pauseMenu.SetActive(true);
    }
}
