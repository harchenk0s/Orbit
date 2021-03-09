using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Dropdown dropdown;
    public Toggle isStatic, isActive, isTrailOn;
    public Slider FOV, TimeScale;
    public Button SetFocusBtn, AddBtn, DeleteBtn, PlayBtn, StopBtn;
    public Text TimeMultiplierText;

    private Camera cam;
    private List<Planet> allPlanets = new List<Planet>();
    private Planet choosenPlanet;
    private PlanetsMaster PM;
    


    void Start()
    {
        cam = FindObjectOfType<Camera>();
        PM = FindObjectOfType<PlanetsMaster>();
        RefreshDropdown();
        ChangePlanet(0);
    }


    void RefreshDropdown()
    {
        dropdown.options.Clear();
        RefreshPlanetsList();
        foreach (Planet item in allPlanets)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = item.name });
        }
    }


    void RefreshPlanetsList()
    {
        allPlanets.Clear();
        allPlanets.AddRange(FindObjectsOfType<Planet>());
    }

    public void Add()
    {

    }


    public void Delete()
    {
        if (choosenPlanet.GetComponentInChildren<Camera>())
        {
            SetFocus(false);
        }
        Destroy(choosenPlanet.gameObject);
        dropdown.options.RemoveAt(dropdown.value);
        allPlanets.Remove(choosenPlanet);
        dropdown.RefreshShownValue();
        ChangePlanet(0);
    }


    public void StopPlanets()
    {
        foreach (Planet item in allPlanets)
        {
            item.IsActive = false;
            item.transform.position = item.StartPosition;
        }
        isActive.isOn = choosenPlanet.IsActive;
        isStatic.isOn = choosenPlanet.IsStatic;
    }

    public void PlayPlanets()
    {
        foreach (Planet item in allPlanets)
        {
            item.IsActive = true;
        }
        isActive.isOn = choosenPlanet.IsActive;
        isStatic.isOn = choosenPlanet.IsStatic;
    }
    public void ChangePlanet(int n)
    {
        choosenPlanet = allPlanets[dropdown.value];
        isActive.isOn = choosenPlanet.IsActive;
        isStatic.isOn = choosenPlanet.IsStatic;
    }


    public void SetFOV(float val)
    {
        if (val == 0)
        {
            cam.orthographicSize = FOV.value;
        }
        else
        {
            if (cam.orthographicSize < 0.01f)
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


    public void SetTimeScale(float val)
    {
        PM.TimeScale = TimeScale.value;
        TimeMultiplierText.text = TimeScale.value.ToString("F2") + "x";
    }


    public void SetFocus(bool b)
    {
        if (!b)
        {
            cam.transform.parent = null;
        }
        else
        {
            cam.transform.SetParent(choosenPlanet.transform, false);
        }

    }


    public void SetActive(bool b)
    {
        choosenPlanet.IsActive = isActive.isOn;
    }


    public void SetStatic(bool b)
    {
        choosenPlanet.IsStatic = isStatic.isOn;
    }
}