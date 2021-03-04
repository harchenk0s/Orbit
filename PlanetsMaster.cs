using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsMaster : MonoBehaviour
{
    [Range(0.01f, 10)]
    public float TimeScale = 1;

    private Dictionary<Guid, Planet> ActivePlanets;
    


    public void Add(Guid id, Planet pl) // Add to dictionary another planet
    {
        if (!ActivePlanets.ContainsKey(id))
        {
            ActivePlanets.Add(id, pl);
            SendChanges();
        }
    }


    public void Remove(Guid id) // Delete from dictionary use id
    {
        if (ActivePlanets.ContainsKey(id))
        {
            ActivePlanets.Remove(id);
            SendChanges();
        }
    }


    private void SendChanges()
    {
        foreach (var item in ActivePlanets)
        {
            item.Value.NewParams(ActivePlanets);
        }
    }

    public void ChangeTimeScale(float scale)
    {
        foreach (var item in ActivePlanets)
        {
            item.Value.NewParams(scale);
        }
    }
    private void Start()
    {
        ActivePlanets = new Dictionary<Guid, Planet>();
        Planet[] allPlanets = FindObjectsOfType<Planet>();
        foreach (Planet item in allPlanets)
        {
            ActivePlanets.Add(item.ID, item);
        }
        SendChanges();
    }


    private void FixedUpdate()
    {
        foreach (var item in ActivePlanets)
        {
            item.Value.Calculate();
        }
        foreach (var item in ActivePlanets)
        {
            item.Value.Move();
        }
    }

}
