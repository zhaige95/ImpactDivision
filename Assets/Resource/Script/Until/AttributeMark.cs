using UnityEngine;
using System.Collections;

public class AttributeMark
{
    public C_Attributes attributes;
    public string name;
    public float value;
    public string operation = "+";

    public AttributeMark() { }

    public AttributeMark(C_Attributes _attributes, string _name, float _value, string _operation)
    {
        this.attributes = _attributes;
        this.name = _name;
        this.value = _value;
        this.operation = _operation;
    }
}
