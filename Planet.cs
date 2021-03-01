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
    private bool isActive = false;
    private bool calculationDone = false;
    private Dictionary<Guid, Planet> localDictionary;


    void Awake()
    {
        ID = Guid.NewGuid();
        localDictionary = new Dictionary<Guid, Planet>();
        direction = StartDirection * StartSpeed;
    }

    public void Calculate()
    {
        if (!IsStatic)
        {
            Planet otherPlanet;
            float distance;
            float force;
            Vector3 newDirection;
            foreach (var item in localDictionary)
            {
                if(item.Value.ID != ID)
                {
                    otherPlanet = item.Value;
                    newDirection = otherPlanet.gameObject.transform.position - gameObject.transform.position;
                    distance = newDirection.magnitude * 10;
                    newDirection.Normalize();
                    force = otherPlanet.Mass / Mathf.Pow(distance, 2);
                    newDirection *= force;
                    direction += newDirection;
                }
            }
        }
        calculationDone = true;

    }

    public void Move(float coef)
    {
        if(!IsStatic)
        {
            transform.position += direction * 0.1f;
        }
    }


    public void ChangeLocalDictionary(Dictionary<Guid, Planet> dict)
    {
        localDictionary = new Dictionary<Guid, Planet>(dict);
    }
}
