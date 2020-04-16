using Newtonsoft.Json;
using UnityEngine;

public class DataManager : Singleton<DataManager>
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

        var json = JsonConvert.SerializeObject(planets);
    }

    public void Load()
    {

    }
}
