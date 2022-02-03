using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : ScriptableObject
{
    public string name;
    public Sprite icon;
    public string description;

    public void Use()
    {
        Debug.Log(name + " was used.");
    }
}
