﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool IsStatic = false;
    public float Mass;
    public float StartSpeed;
    public Vector3 StartDirection;

    private List<Planet> activePlanets;
    private Vector3 direction;
    private bool isActive = false;
    private PlanetsMaster planetsMaster;
    private float timeScale;
    
    public bool IsActive
    {
        get
        {
            return isActive;
        }

        set
        {
            if (isActive != value)
            {
                if (value)
                {
                    direction = StartDirection.normalized * StartSpeed;
                    isActive = true;
                    planetsMaster.Add(this);
                }
                else
                {
                    isActive = false;
                    planetsMaster.Remove(this);
                }
            }
        }
    }


    void Awake()
    {
        activePlanets = new List<Planet>();
        planetsMaster = FindObjectOfType<PlanetsMaster>();
    }


    public void Calculate()
    {
        if (!IsStatic && isActive)
        {
            Planet otherPlanet;
            float distance;
            float force;
            Vector3 newDirection;
            foreach (Planet item in activePlanets)
            {
                otherPlanet = item;
                newDirection = otherPlanet.transform.position - transform.position;
                distance = newDirection.magnitude * 10; // The higher the coefficient, the greater the distance of one unit
                newDirection.Normalize();
                force = otherPlanet.Mass / Mathf.Pow(distance, 2);
                newDirection *= force * timeScale;
                direction += newDirection;
            }
        }
    }


    public void Move()
    {
        if(!IsStatic)
        {
            transform.Translate(direction * 0.1f * timeScale);
        }
    }


    public void UpdateList(List<Planet> planetsList)
    {
        activePlanets = new List<Planet>(planetsList);
        activePlanets.Remove(this);
    }


    public void UpdateTimeScale(float ts)
    {
        timeScale = ts;
    }

    private void OnDestroy()
    {
        planetsMaster.Remove(this);
    }
}
