using Assets.Scripts.Abstraction;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlanetSystemManager : Singleton<PlanetSystemManager>,ISavedData<List<PlanetModel>>
{
    [SerializeField]
    private Planet _planetPrefab;

    [SerializeField]
    private int _playersCount;

    private List<Planet> planets = new List<Planet>();

    [SerializeField]
    private Planet _sun;

    [SerializeField]
    private Road _road;

    private const float _planetsDistance = 1.5f;

    private void OnEnable()
    {
        EventManager.Subscribe("OnPlanetSelected", SelectPlayer);

        EventManager.Subscribe("OnRocketCollited", TakePlanetDamage);

        EventManager.Subscribe("PlanetDestroyed", RemovePlanet);
    }

    private void RemovePlanet(object[] arg0)
    {
        var planet = arg0[0] as Planet;

        planets.Remove(planet);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("OnPlanetSelected", SelectPlayer);

        EventManager.Unsubscribe("OnRocketCollited", TakePlanetDamage);

        EventManager.Unsubscribe("PlanetDestroyed", RemovePlanet);
    }

    private void TakePlanetDamage(object[] args)
    {
        var planet = args[0] as Planet;
        var rocketDamage = (int)args[1];

        var damagedPlanet = planets.Find(x => x.gameObject.name == planet.name);

        damagedPlanet.TakeDamage(rocketDamage);
    }

    public void Init()
    {
        CreateSolarSystem();
    }

    private void CreateSolarSystem()
    {
        CreatePlanets();
        СreatePlanetRoads();

        EventManager.SendEvent("OnPlanetsInited", planets);
    }

    private void СreatePlanetRoads()
    {
        foreach (var planet in planets)
        {
            var road = Instantiate(_road);

            road.Init(planet.Radius);
        }
    }

    private void CreatePlanets()
    {
        planets.Add(_sun);

        var randomPlayerIndex = UnityEngine.Random.Range(1, _playersCount);

        for (int i = 1; i <= _playersCount; i++)
        {
            var prevPlanet = planets[i - 1];

            var isPlayer = i == randomPlayerIndex;
            var newPlanetPosition = new Vector3(prevPlanet.transform.position.x, _sun.transform.position.y + prevPlanet.transform.position.y + prevPlanet.GetComponent<SphereCollider>().radius + _planetsDistance, _sun.transform.position.z);
            var newPlanetRadiusFromCentre = prevPlanet.Radius + _planetsDistance;
            var newPlanetRandomSpeed = UnityEngine.Random.Range(20, 100);

            var planet = CreatePlanet(newPlanetPosition, newPlanetRadiusFromCentre, newPlanetRandomSpeed, UnityEngine.Random.ColorHSV(), isPlayer, true);

            planets.Add(planet);
        }

        planets.Remove(_sun);
    }

    private Planet CreatePlanet(Vector3 position, float radius, float speed, Color color, bool isPlayer, bool isSateliteAvailable)
    {
        Planet satelite = null;

        var planet = Instantiate(_planetPrefab, new Vector3(position.x, position.y, position.z), Quaternion.identity);

        if (isSateliteAvailable)
        {
            satelite = CreateSatelite(planet.transform.position);
        }

        //   if (satelite != null)
        //   {
        //        satelite.Init(planet.transform.position, Random.ColorHSV(), 0.5f, 2, false); ;
        //    }

        planet.Init(_sun.transform.position, color, radius, speed, isPlayer, satelite);

        return planet;
    }

    private Planet CreateSatelite(Vector3 position)
    {
        var sateliteRandom = UnityEngine.Random.Range(0f, 1f);

        if (sateliteRandom > 0.5f)
        {
            // return CreatePlanet(position, false);
        }

        return null;
    }

    private void SelectPlayer(object[] arg0)
    {
        var player = arg0[0] as Planet;

        foreach (var planet in planets)
        {
            planet.Hud.EnableArrow(false);
        }

        var selectedPlanet = planets.Find(x => x == player);

        if (selectedPlanet != null)
            selectedPlanet.Hud.EnableArrow(true);      
    }

    public List<PlanetModel> GetData()
    {
        var planetsModels = new List<PlanetModel>();

        foreach (var planet in planets)
        {
            var planetModel = new PlanetModel()
            {
                Position = planet.transform.position,
                Radius = planet.Radius,
                RotationSpeed = planet.RotationSpeed
            };

            planetsModels.Add(planetModel);
        }

        return planetsModels;
    }
}
