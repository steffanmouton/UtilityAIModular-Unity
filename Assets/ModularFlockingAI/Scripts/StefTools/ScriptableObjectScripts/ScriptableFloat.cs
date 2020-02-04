using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float")]
public class ScriptableFloat : ScriptableObject
{
    [SerializeField]
    private float _value;

    public float Value
    {
        get => _value;
        set => _value = value;
    }
}
