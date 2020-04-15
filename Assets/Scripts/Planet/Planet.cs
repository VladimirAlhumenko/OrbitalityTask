using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private Vector3 _center;

    [SerializeField]
    private Vector3 axis = Vector3.up;

    [SerializeField]
    private Vector3 desiredPosition;

    [SerializeField]
    private float _radius;

    [SerializeField]
    private float radiusSpeed = 0.5f;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private HUD _hud;

    private Renderer _renderer;

    private int health = 100;

    public bool IsPlayer { get; set; }
    public float Radius { get => _radius; set => _radius = value; }
    public int Health
    {
        get => health;

        set
        {


            health = value;
        }
    }

    public HUD Hud { get => _hud; set => _hud = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

    public void Init(Vector2 planetСenter, Color color, float radius, float rotationSpeed, bool isPlayer, Planet satelite = null)
    {
        _renderer = GetComponent<Renderer>();

        _center = planetСenter;
        transform.position = (transform.position - _center).normalized * radius + _center;
        _radius = radius;
        _rotationSpeed = rotationSpeed;
        IsPlayer = isPlayer;

        _renderer.material.color = color;

        _hud.EnableSlider(isPlayer);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if(Health <= 0)
        {
            EventManager.SendEvent("PlanetDestroyed",this);

            Destroy(gameObject);
        }

        Hud.UpdateSlider(amount);
    }

    private void Update()
    {
        transform.RotateAround(_center, axis, _rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - _center).normalized * _radius + _center;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }

    private void OnMouseDown()
    {
        if (IsPlayer) return;

        EventManager.SendEvent("OnPlanetSelected",this);
    }
}
