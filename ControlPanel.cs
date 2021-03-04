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
    public Text xText, yText, zText;
    public Slider FOV, TimeScale;
    public Button StartButton;

    private Camera cam;
    private List<Planet> planets = new List<Planet>();
    private PlanetsMaster PM;
    private Planet ChoosenPlanet;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        PM = FindObjectOfType<PlanetsMaster>();
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

    public void StartMoving()
    {
        foreach (Planet item in planets)
        {
            item.IsActive = true;
        }
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

    public void ChangeFOV(float val)
    {
        if(val == 0)
        {
            cam.orthographicSize = FOV.value;
        }
        else
        {
            if(cam.orthographicSize < 0.01f)
            {
                cam.orthographicSize = 0.01f;
            }
            else
            {
                cam.orthographicSize += val;
                FOV.value = cam.orthographicSize;
            }
        }
        
    }

    public void ChangeTimeScale(float val)
    {
        PM.ChangeTimeScale(TimeScale.value);
    }
    public void SetFocus(bool b)
    {
        cam.transform.SetParent(ChoosenPlanet.transform, false);
    }

    void HideUnhidePanel()
    {

    }

    private void FixedUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float axisValue = Input.GetAxis("Mouse ScrollWheel");
            if(FOV.value < 10)
            {
                ChangeFOV(axisValue);
            }
            if(FOV.value > 10)
            {
                ChangeFOV(axisValue * 3);
            }
            if (FOV.value > 50)
            {
                ChangeFOV(axisValue * 4);
            }
            if (FOV.value > 100)
            {
                ChangeFOV(axisValue * 6);
            }
            if (FOV.value > 300)
            {
                ChangeFOV(axisValue * 7);
            }
            if (FOV.value > 500)
            {
                ChangeFOV(axisValue * 8);
            }
        }
        xText.text = ChoosenPlanet.transform.position.x.ToString("****");
        yText.text = ChoosenPlanet.transform.position.y.ToString();
        zText.text = ChoosenPlanet.transform.position.z.ToString();
    }

    
}
