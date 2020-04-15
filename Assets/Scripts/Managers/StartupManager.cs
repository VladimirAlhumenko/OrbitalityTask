using System;
using UnityEngine;
public class StartupManager : Singleton<StartupManager>
{
    [SerializeField]
    private PlanetSystemManager _planetSystemManager;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _planetSystemManager.Init();
    }
}
