using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossaryItem
{
    public string name;
    public string description;
    public int value;
    public Sprite image;

    public GlossaryItem(string name, string description, int value, Sprite image)
    {
        this.name = name;
        this.description = description;
        this.value = value;
        this.image = image;
    }
}
