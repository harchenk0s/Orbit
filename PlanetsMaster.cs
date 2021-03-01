using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsMaster : MonoBehaviour
{

    // _______
    bool start = false;
    // _______
    public Dictionary<Guid, Planet> InteractionsDictonary;
    public int count = 0;


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
        Dictionary<Guid, Planet> tempDict = new Dictionary<Guid, Planet>(InteractionsDictonary);
        if(!tempDict.ContainsKey(id))
        {
            InteractionsDictonary.Add(id, pl);
            Planet planet;
            foreach (var item in InteractionsDictonary)
            {
                planet = item.Value;
                planet.ChangeLocalDictionary(InteractionsDictonary);
            }
        }
        
    }
    private void FixedUpdate()
    {
            if (start)
            {
                foreach (var item in InteractionsDictonary)
                {
                    item.Value.Calculate();
                }
                foreach (var item in InteractionsDictonary)
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
        InteractionsDictonary = new Dictionary<Guid, Planet>();
        GameObject[] gm = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject item in gm)
        {
            InteractionsDictonary.Add(item.GetComponent<Planet>().ID, item.GetComponent<Planet>());
        }
        foreach (GameObject item in gm)
        {
            item.GetComponent<Planet>().ChangeLocalDictionary(InteractionsDictonary);
        }
    }


}
