using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Dropdown dropdown;
    public Toggle isStatic, isActive, isTrailOn;
    public InputField Name, Mass, StartSpeed, X, Y, Z;
    public Text xText, yText, zText;
    public Slider FOV, TimeScale;
    public GameObject prefabPlanet;

    private Camera cam;
    private List<Planet> planets = new List<Planet>();
    private PlanetsMaster PM;
    private Planet choosenPlanet;


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


    public void Add()
    {
        Instantiate(prefabPlanet);
        FindPlanets();
    }


    public void Delete()
    {
        if (dropdown.options.Count > 1)
        {
            Destroy(choosenPlanet.gameObject);
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
            FindPlanets();
            SetFocus(false);
        }
    }


    public void ChangePlanet(int n)
    {
        choosenPlanet = planets[dropdown.value];
        isStatic.isOn = choosenPlanet.IsStatic;
        isActive.isOn = choosenPlanet.IsActive;
        Name.SetTextWithoutNotify(choosenPlanet.name);
        Mass.text = choosenPlanet.Mass.ToString();
        StartSpeed.text = choosenPlanet.StartSpeed.ToString();
        xText.text = choosenPlanet.transform.position.x.ToString();
        yText.text = choosenPlanet.transform.position.y.ToString();
        zText.text = choosenPlanet.transform.position.z.ToString();

    }


    public void ChangeStatic(bool b)
    {
        choosenPlanet.IsStatic = isStatic.isOn;
    }


    public void ChangeName(string str)
    {
        choosenPlanet.name = Name.text;
        dropdown.captionText.text = Name.text;
        FindPlanets();
    }


    public void ChangeMass(string str)
    {
        if(str == "")
        {
            choosenPlanet.Mass = Convert.ToSingle(Mass.text);
        }
        if(str == "+")
        {
            choosenPlanet.Mass++;
            Mass.text = choosenPlanet.Mass.ToString();
        }
        if(str == "-")
        {
            choosenPlanet.Mass--;
            Mass.text = choosenPlanet.Mass.ToString();
        }
    }


    public void ChangePosition(string str)
    {
        if(str == "x")
        {
            choosenPlanet.transform.position = new Vector3(Convert.ToSingle(X.text), choosenPlanet.transform.position.y, choosenPlanet.transform.position.z);
        }
        if (str == "y")
        {
            choosenPlanet.transform.position = new Vector3(choosenPlanet.transform.position.x, Convert.ToSingle(Y.text), choosenPlanet.transform.position.z);
        }
        if (str == "z")
        {
            choosenPlanet.transform.position = new Vector3(choosenPlanet.transform.position.x, choosenPlanet.transform.position.y, Convert.ToSingle(Z.text));
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
        cam.transform.SetParent(choosenPlanet.transform, false);
    }


    public void ChangeSpeed(string str)
    {
        choosenPlanet.StartSpeed = Convert.ToSingle(StartSpeed.text);
    }


    void HideUnhidePanel()
    {

    }


    private void FixedUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float axisValue = -Input.GetAxis("Mouse ScrollWheel");
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
        xText.text = choosenPlanet.transform.position.x.ToString();
        yText.text = choosenPlanet.transform.position.y.ToString();
        zText.text = choosenPlanet.transform.position.z.ToString();
    }

    
}
