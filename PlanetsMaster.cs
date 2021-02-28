using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsMaster : MonoBehaviour
{
    public Dictionary<Guid, Planet> InteractionsDictonary;

    public void ChangeDictionary(Guid id)
    {
        if (InteractionsDictonary.ContainsKey(id))
        {
            InteractionsDictonary.Remove(id);
            Planet planet;
            foreach (var item in InteractionsDictonary)
            {
                planet = item.Value;
                planet.ChangeDictionary(InteractionsDictonary);
            }
        }
        
    }

    public void ChangeDictionary(Guid id, Planet pl)
    {
        if(!InteractionsDictonary.ContainsKey(id))
        {
            InteractionsDictonary.Add(id, pl);
            Planet planet;
            foreach (var item in InteractionsDictonary)
            {
                planet = item.Value;
                planet.ChangeDictionary(InteractionsDictonary);
            }
        }
        
    }

    private void Start()
    {
        InteractionsDictonary = new Dictionary<Guid, Planet>();
    }


}
