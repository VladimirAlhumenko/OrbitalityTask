using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class PlanetModel
    {
        public Vector3 Position { get; set; }

        public float Radius { get; set; }

        public float RotationSpeed { get; set; }

        public RocketModel Rocket { get; set; }
    }
}
