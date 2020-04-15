
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {


        Destroy(gameObject);
    }
}
