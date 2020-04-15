
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Models.Rocket RocketProperties { get; set; }

    public void OnCollisionEnter(Collision collision)
    {
        var planet = collision.gameObject.GetComponent<Planet>();

        if (planet == null) return;

        if (!planet.CompareTag("Sun"))
            EventManager.SendEvent("OnRocketCollited", planet, RocketProperties.Damage);

        Destroy(gameObject);
    }
}
