﻿using System;
using UnityEngine;


namespace Models
{
    [Serializable]
    public class RocketModel
    {
        [SerializeField]
        private int damage;
        [SerializeField]
        private int cooldown;
        [SerializeField]
        private int speed;

        [SerializeField]
        private int lifetime;
        public int Damage { get => damage; set => damage = value; }
        public int Cooldown { get => cooldown; set => cooldown = value; }
        public int Speed { get => speed; set => speed = value; }

        public int LifeTime { get => lifetime; set => lifetime = value; }
    }
}