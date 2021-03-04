using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Dropdown dropdown;

    public Toggle isStatic, isActive, isFocus, isTrailOn, isTrailsOneColor;
    public InputField Name, Mass, StartSpeed, X, Y, Z;
    public Slider FOV;

    private Camera cam;
    private List<Planet> planets = new List<Planet>();

    private Planet ChoosenPlanet;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        FindPlanets();
    }

    void FindPlanets()
    {
        planets.Clear();
        dropdown.options.Clear();
        planets.AddRange(FindObjectsOfType<Planet>());
        foreach (Planet item in planets)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = item.name });
        }
        ChangePlanet(0);
    }
    void Add()
    {

    }

    public void Delete()
    {
        if (dropdown.options.Count > 1)
        {
            Destroy(ChoosenPlanet.gameObject);
            dropdown.options.RemoveAt(dropdown.value);
            planets.RemoveAt(dropdown.value);
            if (dropdown.value - 1 > 0)
            {
                dropdown.value--;
            }
            else
            {
                dropdown.value++;
            }
            ChangePlanet(0);
        }
    }

    public void ChangePlanet(int n)
    {
        ChoosenPlanet = planets[dropdown.value];
        isStatic.isOn = ChoosenPlanet.IsStatic;
        isActive.isOn = ChoosenPlanet.IsActive;
        Name.SetTextWithoutNotify(ChoosenPlanet.name);
        Mass.text = ChoosenPlanet.Mass.ToString();

    }

    public void ChangeStatic(bool b)
    {
        ChoosenPlanet.IsStatic = isStatic.isOn;
    }

    public void ChangeName(string str)
    {
        ChoosenPlanet.name = Name.text;
        dropdown.captionText.text = Name.text;
        FindPlanets();
    }

    public void ChangeMass(string str)
    {
        if(str == "")
        {
            ChoosenPlanet.Mass = Convert.ToSingle(Mass.text);
        }
        if(str == "+")
        {
            ChoosenPlanet.Mass++;
            Mass.text = ChoosenPlanet.Mass.ToString();
        }
        if(str == "-")
        {
            ChoosenPlanet.Mass--;
            Mass.text = ChoosenPlanet.Mass.ToString();
        }
        
    }
    void ChangeFOV()
    {

    }

    void HideUnhidePanel()
    {

    }
}
