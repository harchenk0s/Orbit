using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public bool IsStatic;
    public float Mass;
    public float StartSpeed;
    public Vector2 StartDirection;
    public Guid ID;
    public PlanetsMaster PlanetsMaster;

    private float speed;
    private Vector2 direction;
    public bool isActive;
    private bool calculationDone = false;
    private Dictionary<Guid, Planet> interactions;

    void Start()
    {
        ID = Guid.NewGuid();
        interactions = new Dictionary<Guid, Planet>();
    }


    public void Active(bool value)
    {
        if (value)
        {
            PlanetsMaster.ChangeDictionary(ID, this);
        }
        else
        {
            PlanetsMaster.ChangeDictionary(ID);
        }
        isActive = value;
    }

    public void ChangeDictionary(Dictionary<Guid, Planet> dict)
    {
        dict.Remove(ID);
        interactions = dict;
    }
}
