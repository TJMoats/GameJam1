using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDictionary : MonoBehaviour
{
    private Dictionary<string, Scene> dictionary;
    public Dictionary<string, Scene> Dictionary
    {
        get
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, Scene>();
            }
            return dictionary;
        }
    }
}
