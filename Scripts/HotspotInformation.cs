using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotInformation
{
    string name;
    Dictionary<string, Vector3> otherHotspots = new Dictionary<string, Vector3>();
    public HotspotInformation(string name)
    {
        this.name = name;
    }

    public void addHotspotInformation(string name, Vector3 eulerAngle)
    {
        otherHotspots.Add(name, new Vector3(0,90,0) + eulerAngle);
    }

    public bool hasHotspotInformation(string name)
    {
        return otherHotspots.ContainsKey(name);
    }

    public Vector3 getHotspotInformation(string name)
    {
        return otherHotspots[name];
    }
}
