using System;
using UITKUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UIDocument))]
public class RotateButton : MonoBehaviour
{
    [FormerlySerializedAs("objectToSpin")]
    public GameObject objectToSpinRotate;

    bool m_Spin;
    bool m_Rotate;
    
    const string SpinButtonName = "SpinButton";
    const string RotateButtonName = "RotateButton";
    const string StopButtonName = "StopButton";
    const string ChangeColorButtonName = "ChangeColorButton";
    const string ResetButtonName = "ResetButton";

    const float SpinSpeed = 5f;

    VisualElement m_Root;
    Button m_SpinButton;
    Button m_RotateButton;
    Button m_StopButton;
    Button m_ResetButton;
    Button m_ChangeColor;
    
    Material m_Material;
    [NonSerialized] UIDocument m_UIDocument;
    private Color bkpColor;
    void Awake()
    {
        m_UIDocument = GetComponent<UIDocument>();
        m_Root = m_UIDocument.rootVisualElement;
    }

    void OnEnable()
    {
        m_SpinButton = m_Root.Q<Button>(SpinButtonName);
        Validation.CheckQuery(m_SpinButton, SpinButtonName);
        m_SpinButton?.RegisterCallback<ClickEvent>(StartSpin);
        
        m_RotateButton = m_Root.Q<Button>(RotateButtonName);
        Validation.CheckQuery(m_RotateButton, RotateButtonName);
        m_RotateButton?.RegisterCallback<ClickEvent>(StartRotate);
        
        m_StopButton = m_Root.Q<Button>(StopButtonName);
        Validation.CheckQuery(m_StopButton, StopButtonName);
        m_StopButton?.RegisterCallback<ClickEvent>(Stop);

        m_ResetButton = m_Root.Q<Button>(ResetButtonName);
        Validation.CheckQuery(m_ResetButton, ResetButtonName);
        m_ResetButton?.RegisterCallback<ClickEvent>(Reset);
        m_ChangeColor = m_Root.Q<Button>(ChangeColorButtonName);
        Validation.CheckQuery(m_ChangeColor, ChangeColorButtonName);
        m_ChangeColor?.RegisterCallback<ClickEvent>(ChangeColor);
        
        m_Material = objectToSpinRotate.GetComponent<MeshRenderer>().material;
        bkpColor = m_Material.color;
    }

    private void ChangeColor(ClickEvent evt)
    {
        Color randomColor = new Color(Random.value,Random.value,Random.value);
        m_Material.color = randomColor;
    }

    void FixedUpdate()
    {
        SpinRotateObject();
    }

    void SpinRotateObject()
    {
        if (m_Spin)
        {
            objectToSpinRotate.transform.Rotate(Vector3.up,SpinSpeed, Space.World);
        }

        if (m_Rotate)
        {
            objectToSpinRotate.transform.Rotate(Vector3.left, SpinSpeed, Space.World);
        }
    }

    void StartSpin(ClickEvent evt)
    {
        m_Spin = true;
    }
    
    void StartRotate(ClickEvent evt)
    {
        m_Rotate = true;
    }
    
    void Stop(ClickEvent evt)
    {
        m_Spin = false;
        m_Rotate = false;
    }

    void Reset(ClickEvent evt)
    {
        Stop(evt);
        objectToSpinRotate.transform.rotation = Quaternion.identity;
        m_Material.color = bkpColor;
    }
}
