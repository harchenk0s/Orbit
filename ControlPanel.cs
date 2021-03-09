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
    [SerializeField]
    private GameObject prefabPlanet;

    private Camera cam;
    private List<Planet> allPlanets = new List<Planet>();
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
        dropdown.options.Clear();
        foreach (Planet item in allPlanets)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = item.name });
        }
        ChangePlanet(0);
    }


    public void StartMoving()
    {
    }


    public void Add()
    {
        Instantiate(prefabPlanet);
        dropdown.value = 0;
        FindPlanets();
    }


    public void Delete()
    {
        if (choosenPlanet.GetComponentInChildren<Camera>())
        {
            SetFocus(false);
        }
        if (dropdown.options.Count > 1)
        {
            Destroy(choosenPlanet.gameObject);
            dropdown.options.RemoveAt(dropdown.value);
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

    }


    public void ChangeStatic(bool b)
    {
        choosenPlanet.IsStatic = isStatic.isOn;
    }


    public void ChangeFOV(float val)
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


    public void ChangeTimeScale(float val)
    {
        PM.ChangeTimeScale(TimeScale.value);
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

    }

    private void FixedUpdate()
    {
    }
}