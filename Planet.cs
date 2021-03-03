using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool IsStatic;
    public float Mass;
    public float StartSpeed;
    public Vector3 StartDirection;

    private Guid id;
    private Vector3 direction;
    private bool isActive = false;
    private Dictionary<Guid, Planet> activePlanets;
    private PlanetsMaster planetsMaster;

    public Guid ID
    {
        get { return id; }
    }

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
                if (value == true)
                {
                    direction = StartDirection.normalized * StartSpeed;
                    isActive = true;
                    planetsMaster.Add(id, this);
                }
                else
                {
                    isActive = false;
                    planetsMaster.Remove(id);
                }
            }
        }
    }


    void Awake()
    {
        id = Guid.NewGuid();
        activePlanets = new Dictionary<Guid, Planet>();
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
            foreach (var item in activePlanets)
            {
                if(item.Value.id != id)
                {
                    otherPlanet = item.Value;
                    newDirection = otherPlanet.transform.position - transform.position;
                    distance = newDirection.magnitude * 10; // The higher the coefficient, the greater the distance of one unit
                    newDirection.Normalize();
                    force = otherPlanet.Mass / Mathf.Pow(distance, 2);
                    newDirection *= force;
                    direction += newDirection;
                }
            }
        }
    }


    public void Move()
    {
        if(!IsStatic && isActive)
        {
            transform.position += direction * 0.1f;
        }
    }


    public void RefreshDictionary(Dictionary<Guid, Planet> dict)
    {
        activePlanets = new Dictionary<Guid, Planet>(dict);
    }
}
