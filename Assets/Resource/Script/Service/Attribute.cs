using UnityEngine;
using System.Collections;
using Unity.Entities;

public class Attribute
{
    struct Mark {
        public string name;
        public float value;
    }
    public static AttributeMark AddRate(ref C_Attributes attributes, float value, string operation = "+")
    {
        AddOperate(ref attributes.rate, value, operation);
        return new AttributeMark(attributes, "rate", (operation.Equals("=") ? attributes.rate : value), operation);
    }

    public static void DelRate(AttributeMark mark)
    {
        DelOperate(ref mark.attributes.rate, mark.value, mark.operation);
    }

    public static AttributeMark Add(ref C_Attributes attributes, string pName, float value, string operation = "+")
    {
        switch (pName)
        {
            case "HP":
                AddOperate(ref attributes.HP, value, operation);
                return new AttributeMark(attributes, "rate", (operation.Equals("=") ? attributes.HP : value), operation);
            //case "speed":
            //    AddOperate(ref attributes.speed, value, operation);
            //    return new AttributeMark(attributes, "rate", (operation.Equals("=") ? attributes.speed : value), operation);
            //case "rate":
            //    AddOperate(ref attributes.rate, value, operation);
            //    return new AttributeMark(attributes, "rate", (operation.Equals("=") ? attributes.rate : value), operation);
        }
        return null;
    }

    protected static void AddOperate(ref float attribute, float value, string operation)
    {
        switch (operation)
        {
            case "+": attribute += value; break;
            case "-": attribute -= value; break;
            case "*": attribute *= value; break;
            case "/": attribute /= value; break;
            case "=": attribute = value; break;
        }
    }

    protected static void DelOperate(ref float attribute, float value, string operation)
    {
        switch (operation)
        {
            case "+": attribute -= value; break;
            case "-": attribute += value; break;
            case "*": attribute /= value; break;
            case "/": attribute *= value; break;
            case "=": attribute = value; break;
        }
    }

}
