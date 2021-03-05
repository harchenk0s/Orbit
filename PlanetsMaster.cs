using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsMaster : MonoBehaviour
{
    [Range(0.01f, 10)]
    [SerializeField]
    private float timeScale = 1;
    private List<Planet> activePlanets = new List<Planet>();

    public float TimeScale
    {
        get { return timeScale; }
        set
        {
            if(value > 0 && value < 100)
            {
                timeScale = value;
            }
        }
    }
    

    public void Add(Planet planet)
    {
        if (!activePlanets.Contains(planet))
        {
            activePlanets.Add(planet);
            planet.UpdateTimeScale(timeScale);
            NotifyPlanets();
        }
    }


    public void Remove(Planet planet)
    {
        if (activePlanets.Contains(planet))
        {
            activePlanets.Remove(planet);
            NotifyPlanets();
        }
    }


    private void NotifyPlanets()
    {
        foreach (Planet item in activePlanets)
        {
            item.UpdateList(activePlanets);
        }
    }


    public void ChangeTimeScale(float scale)
    {
        foreach (Planet item in activePlanets)
        {
            item.UpdateTimeScale(scale);
        }
    }


    private void FixedUpdate()
    {
        foreach (Planet item in activePlanets)
        {
            item.Calculate();
        }
        foreach (Planet item in activePlanets)
        {
            item.Move();
        }
    }


}
