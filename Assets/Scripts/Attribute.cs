using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
    public string Name;
    public string NameEnum;
    public float Value;
    public float ValueIncrease;

    public Attribute(string name, string nameEnum, float value, float valueIncrease)
    {
        Name = name;
        NameEnum = nameEnum;
        Value = value;
        ValueIncrease = valueIncrease;
    }
}
