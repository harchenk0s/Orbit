using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool IsStatic;
    public float Mass;
    public float StartSpeed;
    public Guid ID;
    public PlanetsMaster PlanetsMaster;
    public Vector3 StartDirection;
    //public Vector3 StartDirection
    //{
    //    get
    //    {
    //        Vector3 vec = StartDirection;
    //        return vec.normalized;
    //    }
    //}

    private Vector3 direction;
    public bool isActive = true;
    private Dictionary<Guid, Planet> ActivePlanets;


    void Awake()
    {
        ID = Guid.NewGuid();
        ActivePlanets = new Dictionary<Guid, Planet>();
        direction = StartDirection * StartSpeed;
    }

    public void Calculate()
    {
        if (!IsStatic && isActive)
        {
            Planet otherPlanet;
            float distance;
            float force;
            Vector3 newDirection;
            foreach (var item in ActivePlanets)
            {
                if(item.Value.ID != ID)
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


    public void ChangeLocalDictionary(Dictionary<Guid, Planet> dict)
    {
        ActivePlanets = new Dictionary<Guid, Planet>(dict);
    }
}
