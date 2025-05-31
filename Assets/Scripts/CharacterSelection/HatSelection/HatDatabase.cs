using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HatDatabase : ScriptableObject
{
    public Hat[] hat;

    public int HatCount
    {
        get
        {
            return hat.Length;
        }
    }

    public Hat GetHat(int index)
    {
        return hat[index];
    }
}
