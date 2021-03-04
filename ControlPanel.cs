using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    private Dropdown dropdown;

    private bool isStatic, isActive, isFocus, isTrailOn, isTrailsOneColor; 
    private Planet[] planets;

    private Planet ChoosenPlanet
    {
        get;
        set;
    }

    void Start()
    {
        planets = FindObjectsOfType<Planet>();
        foreach (Planet item in planets)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = item.name });
        }
        ChoosenPlanet = planets[0];

    }

    void Add()
    {

    }

    void Delete()
    {

    }

    void ChangePlanet()
    {

    }

    void ChangeColors()
    {

    }
}
