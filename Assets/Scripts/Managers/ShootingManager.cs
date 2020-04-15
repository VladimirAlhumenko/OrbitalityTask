using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingManager : Singleton<ShootingManager>
{
    [SerializeField]
    private Models.Rocket[] _rockets;

    private Dictionary<Planet,Models.Rocket> _shootingPlanets = new Dictionary<Planet, Models.Rocket>();

    private Planet _selectedPlanet;

    [SerializeField]
    private GameObject _rocketPrefab;

    public float shootAngle = 30;

    private float power = 2;

    private void OnPlayerShoot(object[] arg0)
    {
        var player = _shootingPlanets.Single(x => x.Key.IsPlayer == true);

        var rocketSpawnPosition = new Vector2(player.Key.transform.position.x, player.Key.transform.position.y + player.Key.GetComponent<SphereCollider>().radius + 1);

        Shoot(rocketSpawnPosition, _selectedPlanet.transform,player.Value);

        StartCoroutine(Cooldown(player.Value.Cooldown));
    }

    private IEnumerator Cooldown(int cooldowm)
    {
        EventManager.SendEvent("StartCooldown",cooldowm);

        yield return new WaitForSeconds(cooldowm);

        EventManager.SendEvent("OnPlayerRocketCooldownDone");
    }

    private void OnEnable()
    {
        EventManager.Subscribe("OnShootButtonClicked", OnPlayerShoot);

        EventManager.Subscribe("OnPlanetsInited",InitRockets);

        EventManager.Subscribe("OnPlanetSelected",SelectPlanet);

        EventManager.Subscribe("PlanetDestroyed", RemovePlanet);
    }

    private void RemovePlanet(object[] arg0)
    {
        var planet = arg0[0] as Planet;

        _selectedPlanet = null;
        _shootingPlanets.Remove(planet);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("OnShootButtonClicked", OnPlayerShoot);

        EventManager.Unsubscribe("OnPlanetsInited", InitRockets);

        EventManager.Unsubscribe("OnPlanetSelected", SelectPlanet);

        EventManager.Unsubscribe("PlanetDestroyed", RemovePlanet);
    }

    private void InitRockets(object[] arg0)
    {
        var planets = arg0[0] as List<Planet>;

        foreach (var planet in planets)
        {
             var rocket = GetRandomRocket();

            _shootingPlanets.Add(planet,rocket);
        }
    }

    private Vector3 CalculateParabolTrajectory(Vector3 planetPosition,Transform target, float angle)
    {
        var dir = target.position - planetPosition;
        var h = dir.y;
        var dist = dir.magnitude;
        var a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += h / Mathf.Tan(a);

        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));

        return vel * dir.normalized;
    }

    private void Shoot(Vector3 planetPosition,Transform targetPlanetPosition,Models.Rocket rocketProperties)
    {
        var rocket = Instantiate(_rocketPrefab, planetPosition, Quaternion.identity);
        //    rocket.GetComponent<Rigidbody>().velocity = CalculateParabolTrajectory(planetPosition,targetPlanetPosition, shootAngle);

        rocket.GetComponent<Rocket>().RocketProperties = rocketProperties;

        rocket.GetComponent<Rigidbody>().AddForce(GetForceFrom(planetPosition, targetPlanetPosition.position), ForceMode.Impulse);

        Destroy(rocket.gameObject, 5);
    }

    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power;
    }

    private void SelectPlanet(object[] arg0)
    {
        _selectedPlanet = arg0[0] as Planet;
    }

    private Models.Rocket GetRandomRocket()
    {
        return _rockets[UnityEngine.Random.Range(0, _rockets.Length)];
    }
}
