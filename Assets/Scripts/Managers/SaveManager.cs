using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]
    private PlanetSystemManager planetSystemManager;

    private void OnEnable()
    {
        EventManager.Subscribe("OnSaveButtonClicked",Save);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("OnSaveButtonClicked", Save);
    }

    public void Save(object [] args)
    {
        var planets = planetSystemManager.GetData();

        var json = JsonConvert.SerializeObject(planets,Formatting.Indented);

        var savesPath = Application.persistentDataPath + Consts.savesPath;

        if (!File.Exists(savesPath))
        {
             File.Create(savesPath);
        }

        File.WriteAllText(savesPath, json);
    }
}
