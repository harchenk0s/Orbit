using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsMaster : MonoBehaviour
{
    private Dictionary<Guid, Planet> ActivePlanets;


    public void ChangeDictionary(Guid id) // Delete from dictionary use id
    {
        if (ActivePlanets.ContainsKey(id))
        {
            ActivePlanets.Remove(id);
            Planet planet;
            foreach (var item in ActivePlanets)
            {
                planet = item.Value;
                planet.ChangeLocalDictionary(ActivePlanets);
            }
        }
    }
    public void ChangeDictionary(Guid id, Planet pl) // Add to dictionary another planet
    {
        if(!ActivePlanets.ContainsKey(id))
        {
            ActivePlanets.Add(id, pl);
            Planet planet;
            foreach (var item in ActivePlanets)
            {
                planet = item.Value;
                planet.ChangeLocalDictionary(ActivePlanets);
            }
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
        foreach (Planet item in allPlanets)
        {
            item.ChangeLocalDictionary(ActivePlanets);
        }
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
