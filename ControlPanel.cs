using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public SettingsWindow Settings;
    public Dropdown DropdownPlanets;
    public Toggle IsStatic, IsActive, IsTrailOn;
    public GameObject Prefab;
    public Button SetFocusBtn, AddBtn, DeleteBtn, PlayBtn, StopBtn;
    public Text TimeMultiplierText;
    public Slider FOV, TimeScale;

    private List<Planet> allPlanets = new List<Planet>();
    private Camera cam;
    private PlanetsMaster planetsMaster;

    public Planet ChoosenPlanet { get; private set; }


    void Start()
    {
        cam = FindObjectOfType<Camera>();
        planetsMaster = FindObjectOfType<PlanetsMaster>();
        RefreshDropdown();
        ChangePlanet();
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
        Instantiate(Prefab).name = "Planet";
        RefreshDropdown();
        DropdownPlanets.value = 0;
        ChangePlanet();
        DropdownPlanets.RefreshShownValue();
        Settings.gameObject.SetActive(true);
    }


    public void Delete()
    {
        if (ChoosenPlanet.GetComponentInChildren<Camera>())
        {
            SetFocus(false);
        }
        Destroy(ChoosenPlanet.gameObject);
        DropdownPlanets.options.RemoveAt(DropdownPlanets.value);
        allPlanets.Remove(ChoosenPlanet);
        if(allPlanets.Count == 0)
        {
            Add();
            return;
        }
        ChangePlanet();
        DropdownPlanets.RefreshShownValue();
    }


    public void OpenSettings()
    {
        Settings.gameObject.SetActive(true);
    }


    public void StopPlanets()
    {
        foreach (Planet item in allPlanets)
        {
            item.IsActive = false;
            item.transform.position = item.StartPosition;
        }
        ChangePlanet();
    }


    public void PlayPlanets()
    {
        foreach (Planet item in allPlanets)
        {
            item.IsActive = true;
        }
        ChangePlanet();
    }


    public void ChangePlanet()
    {
        if(DropdownPlanets.value > allPlanets.Count - 1)
        {
            DropdownPlanets.value--;
        }
        ChoosenPlanet = allPlanets[DropdownPlanets.value];
        IsActive.isOn = ChoosenPlanet.IsActive;
        IsStatic.isOn = ChoosenPlanet.IsStatic;
    }


    public void SetActive(bool b)
    {
        ChoosenPlanet.IsActive = b;
    }


    public void SetFocus(bool b)
    {
        if (!b)
        {
            cam.transform.parent = null;
        }
        else
        {
            cam.transform.SetParent(ChoosenPlanet.transform, false);
        }
    }


    public void SetStatic(bool b)
    {
        ChoosenPlanet.IsStatic = b;
    }


    public void SetTrail(bool b)
    {
        ChoosenPlanet.SetTrail(b);
    }


    public void SetTimeScale(float val)
    {
        planetsMaster.TimeScale = val;
        TimeMultiplierText.text = TimeScale.value.ToString("F2") + "x";
    }


    public void SetFOV(float val)
    {
        cam.orthographicSize = val;
    }


    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}