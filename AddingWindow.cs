using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingWindow : MonoBehaviour
{
    public InputField nameField, massField, speedField;
    public InputField posXField, posYField, posZField;
    public InputField dirXField, dirYField, dirZField;
    public GameObject ShadowPanel;
    
    private ControlPanel controlPanel;
    private TrailRenderer trail;
    private PlanetsMaster planetsMaster;
    private Planet planet;

    private void Awake()
    {
        controlPanel = FindObjectOfType<ControlPanel>();
    }

    private void OnEnable()
    {
        planet = controlPanel.ChoosenPlanet;
        planetsMaster = FindObjectOfType<PlanetsMaster>();
        ShadowPanel.SetActive(true);
        trail = planet.GetTrail();
        if (!planetsMaster.IsStop)
        {
            RefreshFields(false);
        }
        else
        {
            RefreshFields(true);
        }
    }

    private void OnDisable()
    {
        ShadowPanel.SetActive(false);
    }


    private void RefreshFields(bool interactableValue)
    {
        nameField.text = planet.name;
        massField.text = planet.Mass.ToString();
        speedField.text = planet.StartSpeed.ToString();
        posXField.text = planet.StartPosition.x.ToString("F5");
        posYField.text = planet.StartPosition.y.ToString("F5");
        posZField.text = planet.StartPosition.z.ToString("F5");
        dirXField.text = planet.StartDirection.x.ToString("F3");
        dirYField.text = planet.StartDirection.y.ToString("F3");
        dirZField.text = planet.StartDirection.z.ToString("F3");
        massField.interactable = interactableValue;
        speedField.interactable = interactableValue;
        posXField.interactable = interactableValue;
        posYField.interactable = interactableValue;
        posZField.interactable = interactableValue;
        dirXField.interactable = interactableValue;
        dirYField.interactable = interactableValue;
        dirZField.interactable = interactableValue;
    }


    public void CloseWindow()
    {
        this.gameObject.SetActive(false);
    }


    public void AcceptChanges()
    {
        if (planetsMaster.IsStop)
        {
            planet.name = nameField.text;
            planet.Mass = Convert.ToSingle(massField.text);
            planet.StartSpeed = Convert.ToSingle(speedField.text);
            planet.transform.position = new Vector3(Convert.ToSingle(posXField.text), Convert.ToSingle(posYField.text), Convert.ToSingle(posZField.text));
            planet.StartPosition = planet.transform.position;
            planet.StartDirection = new Vector3(Convert.ToSingle(dirXField.text), Convert.ToSingle(dirYField.text), Convert.ToSingle(dirZField.text));
        }
        this.gameObject.SetActive(false);
        controlPanel.RefreshDropdown();
        controlPanel.DropdownPlanets.RefreshShownValue();
    }
}
