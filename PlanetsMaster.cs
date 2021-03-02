using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsMaster : MonoBehaviour
{

    // _______
    bool start = false;
    public Text text;
    // _______
    private Dictionary<Guid, Planet> ActivePlanets;
   


    //public void ChangeDictionary(Guid id)
    //{
    //    if (InteractionsDictonary.ContainsKey(id))
    //    {
    //        InteractionsDictonary.Remove(id);
    //        Planet planet;
    //        foreach (var item in InteractionsDictonary)
    //        {
    //            planet = item.Value;
    //            planet.ChangeLocalDictionary(InteractionsDictonary);
    //        }
    //    }
        
    //}

    public void ChangeDictionary(Guid id, Planet pl)
    {
        Dictionary<Guid, Planet> tempDict = new Dictionary<Guid, Planet>(ActivePlanets);
        if(!tempDict.ContainsKey(id))
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

    private void FixedUpdate()
    {
            if (start)
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
        
        
        if (Input.GetKey(KeyCode.Space))
        {
                start = true;
        }
        
    }
    private void Start()
    {
        ActivePlanets = new Dictionary<Guid, Planet>();
        Planet[] allPlanets = GameObject.FindObjectsOfType<Planet>();
        foreach (Planet item in allPlanets)
        {
            ActivePlanets.Add(item.ID, item);
        }
        foreach (Planet item in allPlanets)
        {
            item.ChangeLocalDictionary(ActivePlanets);
        }
    }


}
