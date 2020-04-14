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

    public bool IsPlayer { get; set; }
    public float Radius { get => _radius; set => _radius = value; }

    public Rocket Rocket { get; set; }

    public void Init(Vector2 planetСenter, Color color, float radius, float rotationSpeed, bool isPlayer, Planet satelite = null)
    {
        _renderer = GetComponent<Renderer>();

        _center = planetСenter;
        transform.position = (transform.position - _center).normalized * radius + _center;
        _radius = radius;
        _rotationSpeed = rotationSpeed;
        IsPlayer = isPlayer;

        _renderer.material.color = color;

        _hud.UpdateSlider(isPlayer);
    }

    private void OnMouseDown()
    {
        
    }

    private void Update()
    {
        transform.RotateAround(_center, axis, _rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - _center).normalized * _radius + _center;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }
}
