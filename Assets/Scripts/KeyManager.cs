using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;
    public List<KeyScriptableObject> keyList = new List<KeyScriptableObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public KeyScriptableObject GiveRandomKey()
    {
        int randomIndex = Random.Range(0, keyList.Count);
        KeyScriptableObject key = keyList[randomIndex];
        return key;
    }
}