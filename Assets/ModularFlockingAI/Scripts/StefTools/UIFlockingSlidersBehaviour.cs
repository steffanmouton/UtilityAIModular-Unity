using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlockingSlidersBehaviour : MonoBehaviour
{
    public Slider maxSpeedSlider = null;
    public Slider separationSlider = null;
    public Slider cohesionSlider = null;
    public Slider alignmentSlider = null;
    
    

    public ScriptableFloat MaxSpeed;
    public ScriptableFloat Separation;
    public ScriptableFloat Cohesion;
    public ScriptableFloat Alignment;
    

    public void Start()
    {
        maxSpeedSlider.value = MaxSpeed.Value;
        maxSpeedSlider.onValueChanged.AddListener( ( value ) => MaxSpeed.Value = value );
        
        separationSlider.value = Separation.Value;
        separationSlider.onValueChanged.AddListener( ( value ) => Separation.Value = value );

        cohesionSlider.value = Cohesion.Value;
        cohesionSlider.onValueChanged.AddListener( ( value ) => Cohesion.Value = value );
        
        alignmentSlider.value = Alignment.Value;
        alignmentSlider.onValueChanged.AddListener( ( value ) => Alignment.Value = value );

    }
}
