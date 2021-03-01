using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsMaster : MonoBehaviour
{

    // _______
    bool start = false;
    private int count = 0;
    [Range(0, 100)]
    public int TimeScale = 0;
    public Text text;
    // _______
    public Dictionary<Guid, Planet> InteractionsDictonary;
   


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
    private void Update()
    {
        text.text = count.ToString();
    }
    private void FixedUpdate()
    {
        count++;
        if(count > TimeScale)
        {
            if (start)
            {
                foreach (var item in InteractionsDictonary)
                {
                    item.Value.Calculate();
                }
                foreach (var item in InteractionsDictonary)
                {
                    item.Value.Move(count);
                }
            }
            count = 0;
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
