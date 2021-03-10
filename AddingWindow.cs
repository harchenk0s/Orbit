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
    public Image ColorPlanet, ColorTrail;
    public GameObject ShadowPanel;
    
    private ControlPanel controlPanel;
    private Color color;
    private Image tmpImage;
    private TrailRenderer trail;
    private Texture2D tex;
    private bool isColorPickingStart = false;
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
        RefreshFields(planetsMaster.IsStop);
        ColorPlanet.color = planet.GetComponent<SpriteRenderer>().color;
        ColorTrail.color = trail.colorGradient.colorKeys[0].color;
    }

    private void OnDisable()
    {
        ShadowPanel.SetActive(false);
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
        planet.GetComponent<SpriteRenderer>().color = ColorPlanet.color;
        trail.colorGradient = OneColorGradient(ColorTrail.color);
        this.gameObject.SetActive(false);
        controlPanel.RefreshDropdown();
        controlPanel.DropdownPlanets.RefreshShownValue();
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


    public void SetColor(string str)
    {
        if(str == "Planet")
        {
            tmpImage = ColorPlanet;
        }
        if(str == "Trail")
        {
            tmpImage = ColorTrail;
        }
        tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
        isColorPickingStart = true;
    }


    IEnumerator ReadPixelColor()
    {
        yield return new WaitForEndOfFrame();
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        tex.ReadPixels(new Rect(x, y, 1, 1), 0, 0);
        tex.Apply();
        color = tex.GetPixel(0, 0);
    }


    private Gradient OneColorGradient(Color color)
    {
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;
        Gradient gradient = new Gradient();

        colorKey = new GradientColorKey[2];
        colorKey[0].color = color;
        colorKey[0].time = 0.0f;
        colorKey[1].color = color;
        colorKey[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;
        gradient.SetKeys(colorKey, alphaKey);
        return gradient;
    }
    private void Update()
    {
        if (isColorPickingStart)
        {
            StartCoroutine(ReadPixelColor());
            tmpImage.color = color;
            if (Input.GetMouseButtonDown(0))
            {
                isColorPickingStart = false;
            }
        }
    }
}
