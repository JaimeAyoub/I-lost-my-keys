using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyScriptableObject", menuName = "Scriptable Objects/KeyScriptableObject")]
public class KeyScriptableObject : ScriptableObject
{
    public string keyName;
    public Sprite sprite;
    public List<int> IDs = new List<int>();

}
    