using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Dropdown DropdownPlanets;
    public Toggle IsStatic, IsActive, IsTrailOn;
    public Slider FOV, TimeScale;
    public Button SetFocusBtn, AddBtn, DeleteBtn, PlayBtn, StopBtn;
    public Text TimeMultiplierText;
    public GameObject Prefab;

    private Camera cam;
    private List<Planet> allPlanets = new List<Planet>();
    private Planet choosenPlanet;
    private PlanetsMaster planetsMaster;
    private AddingWindow addingWindow;

    public Planet ChoosenPlanet
    {
        get { return choosenPlanet; }
    }


    void Start()
    {
        cam = FindObjectOfType<Camera>();
        planetsMaster = FindObjectOfType<PlanetsMaster>();
        addingWindow = FindObjectOfType<AddingWindow>();
        addingWindow.gameObject.SetActive(false);
        RefreshDropdown();
        ChangePlanet(0);
    }


    public void RefreshDropdown()
    {
        DropdownPlanets.options.Clear();
        allPlanets.Clear();
        allPlanets.AddRange(FindObjectsOfType<Planet>());
        foreach (Planet item in allPlanets)
        {
            DropdownPlanets.options.Add(new Dropdown.OptionData { text = item.name });
        }
    }


    public void Add()
    {
        Instantiate(Prefab);
        RefreshDropdown();
        DropdownPlanets.value = 0;
        ChangePlanet(0);
        DropdownPlanets.RefreshShownValue();
        addingWindow.gameObject.SetActive(true);
    }


    public void Delete()
    {
        if (choosenPlanet.GetComponentInChildren<Camera>())
        {
            SetFocus(false);
        }
        Destroy(choosenPlanet.gameObject);
        DropdownPlanets.options.RemoveAt(DropdownPlanets.value);
        allPlanets.Remove(choosenPlanet);
        if(allPlanets.Count == 0)
        {
            Add();
            return;
        }
        ChangePlanet(0);
        DropdownPlanets.RefreshShownValue();
    }


    public void OpenSettings()
    {
        addingWindow.gameObject.SetActive(true);
    }


    public void StopPlanets()
    {
        foreach (Planet item in allPlanets)
        {
            item.IsActive = false;
            item.transform.position = item.StartPosition;
        }
        ChangePlanet(0);
    }


    public void PlayPlanets()
    {
        foreach (Planet item in allPlanets)
        {
            item.IsActive = true;
        }
        ChangePlanet(0);
    }


    public void ChangePlanet(int n)
    {
        if(DropdownPlanets.value > allPlanets.Count - 1)
        {
            DropdownPlanets.value--;
        }
        choosenPlanet = allPlanets[DropdownPlanets.value];
        IsActive.isOn = choosenPlanet.IsActive;
        IsStatic.isOn = choosenPlanet.IsStatic;
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
        planetsMaster.TimeScale = TimeScale.value;
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
        choosenPlanet.IsActive = IsActive.isOn;
    }


    public void SetStatic(bool b)
    {
        choosenPlanet.IsStatic = IsStatic.isOn;
    }
}