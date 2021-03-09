using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingWindow : MonoBehaviour
{
    [SerializeField]
    private InputField nameField, massField, speedField;
    [SerializeField]
    private InputField posXField, posYField, posZField;
    [SerializeField]
    private InputField dirXField, dirYField, dirZField;
    private GameObject Window;
    [SerializeField]
    private GameObject ShadowPanel;
    private PlanetsMaster PM;

    private void Awake()
    {
        Window = this.gameObject;
    }

    private void OnEnable()
    {
        PM = FindObjectOfType<PlanetsMaster>();
        ShadowPanel.SetActive(true);
        if (!PM.IsStop)
        {
            massField.interactable = false;
        }
        else
        {
            massField.interactable = true;
        }
    }

    private void OnDisable()
    {
        ShadowPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            massField.interactable = false;
        }
    }

}
