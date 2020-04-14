using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlanetSystemManager : MonoBehaviour
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

    private void Start()
    {
        CreateSolarSystem();
    }

    private void CreateSolarSystem()
    {
        CreatePlanets();
        СreatePlanetRoads();
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

        for (int i = 1; i <= _playersCount; i++)
        {
            var prevPlanet = planets[i - 1];

            var isPlayer = i == 1;
            var newPlanetPosition = new Vector3(prevPlanet.transform.position.x,_sun.transform.position.y + prevPlanet.transform.position.y + prevPlanet.GetComponent<SphereCollider>().radius + _planetsDistance, _sun.transform.position.z);
            var newPlanetRadiusFromCentre = prevPlanet.Radius + _planetsDistance;
            var newPlanetRandomSpeed = UnityEngine.Random.Range(20, 100);

            var planet = CreatePlanet(newPlanetPosition, newPlanetRadiusFromCentre, newPlanetRandomSpeed, UnityEngine.Random.ColorHSV(), isPlayer, true);

            planets.Add(planet);
        }
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
}
