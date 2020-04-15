using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingManager : Singleton<ShootingManager>
{
    [SerializeField]
    private Models.Rocket[] _rockets;

    private Dictionary<Planet,Models.Rocket> _playersRockets = new Dictionary<Planet, Models.Rocket>();

    private Planet _selectedPlanet;

    [SerializeField]
    private GameObject _rocketPrefab;

    public float shootAngle = 30;

    private void OnPlayerShoot(object[] arg0)
    {
        var rocket = _playersRockets.Single(x => x.Key.IsPlayer == true);

        Shoot(new Vector2(rocket.Key.transform.position.x, rocket.Key.transform.position.y + rocket.Key.GetComponent<SphereCollider>().radius + 1), _selectedPlanet.transform);

        StartCoroutine(Cooldown(rocket.Value.Cooldown));
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
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("OnShootButtonClicked", OnPlayerShoot);

        EventManager.Unsubscribe("OnPlanetsInited", InitRockets);

        EventManager.Unsubscribe("OnPlanetSelected", SelectPlanet);
    }

    private void InitRockets(object[] arg0)
    {
        var planets = arg0[0] as List<Planet>;

        foreach (var planet in planets)
        {
             var rocket = GetRandomRocket();

            _playersRockets.Add(planet,rocket);
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

    private void Shoot(Vector2 planetPosition,Transform targetPlanetPosition)
    {
        var rocket = Instantiate(_rocketPrefab, planetPosition, Quaternion.identity);
         rocket.GetComponent<Rigidbody>().velocity = CalculateParabolTrajectory(planetPosition,targetPlanetPosition, shootAngle);
        Destroy(rocket.gameObject, 10);
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
