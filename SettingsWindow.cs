using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    public InputField NameField, MassField, SpeedField;
    public InputField PosXField, PosYField, PosZField;
    public InputField DirXField, DirYField, DirZField;
    public InputField SizeField;
    public Slider SizeSlider, TrailSlider;
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
    private float tmpSize, tmpTrail;

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
        tmpSize = planet.transform.localScale.x;
        tmpTrail = trail.startWidth;
        SizeSlider.value = planet.transform.localScale.x;
        TrailSlider.value = trail.startWidth;
        SizeField.text = planet.transform.localScale.x.ToString("F4");
    }

    private void OnDisable()
    {
        ShadowPanel.SetActive(false);
    }


    public void AcceptChanges()
    {
        if (planetsMaster.IsStop)
        {
            planet.name = NameField.text;
            planet.Mass = Convert.ToSingle(MassField.text);
            planet.StartSpeed = Convert.ToSingle(SpeedField.text);
            planet.transform.position = new Vector3(Convert.ToSingle(PosXField.text), Convert.ToSingle(PosYField.text), Convert.ToSingle(PosZField.text));
            planet.StartPosition = planet.transform.position;
            planet.StartDirection = new Vector3(Convert.ToSingle(DirXField.text), Convert.ToSingle(DirYField.text), Convert.ToSingle(DirZField.text));
        }
        planet.GetComponent<SpriteRenderer>().color = ColorPlanet.color;
        trail.colorGradient = OneColorGradient(ColorTrail.color);
        this.gameObject.SetActive(false);
        controlPanel.RefreshDropdown();
        controlPanel.DropdownPlanets.RefreshShownValue();
    }


    private void RefreshFields(bool interactableValue)
    {
        NameField.text = planet.name;
        MassField.text = planet.Mass.ToString();
        SpeedField.text = planet.StartSpeed.ToString();
        PosXField.text = planet.StartPosition.x.ToString("F5");
        PosYField.text = planet.StartPosition.y.ToString("F5");
        PosZField.text = planet.StartPosition.z.ToString("F5");
        DirXField.text = planet.StartDirection.x.ToString("F3");
        DirYField.text = planet.StartDirection.y.ToString("F3");
        DirZField.text = planet.StartDirection.z.ToString("F3");
        MassField.interactable = interactableValue;
        SpeedField.interactable = interactableValue;
        PosXField.interactable = interactableValue;
        PosYField.interactable = interactableValue;
        PosZField.interactable = interactableValue;
        DirXField.interactable = interactableValue;
        DirYField.interactable = interactableValue;
        DirZField.interactable = interactableValue;
    }


    public void CloseWindow()
    {
        planet.transform.localScale = new Vector3(tmpSize, tmpSize, 1);
        trail.startWidth = tmpTrail;
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


    public void SetSize(float value)
    {
        planet.transform.localScale = new Vector3(value, value, 1);
        SizeField.text = planet.transform.localScale.x.ToString("F2");
    }


    public void SetSize(string value)
    {
        float valueConv = Convert.ToSingle(value);
        planet.transform.localScale = new Vector3(valueConv, valueConv, 1);
        SizeSlider.value = valueConv;
    }


    public void SetWidthTrail(float value)
    {
        trail.startWidth = value;
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
