using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingManager : Singleton<ShootingManager>
{
    [SerializeField]
    private Rocket[] _rockets;

    private Dictionary<Planet, Rocket> _playersRockets = new Dictionary<Planet, Rocket>();

    private void OnPlayerShoot(object[] arg0)
    {
        var rocket = _playersRockets.Single(x => x.Key.IsPlayer == true);

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
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("OnShootButtonClicked", OnPlayerShoot);

        EventManager.Unsubscribe("OnPlanetsInited", InitRockets);
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

    private Rocket GetRandomRocket()
    {
        return _rockets[UnityEngine.Random.Range(0, _rockets.Length)];
    }
}
