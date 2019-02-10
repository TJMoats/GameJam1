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

    public enum SceneList
    {
        // Out of game
        mainMenu,

        // Indoors
        playerHome,

        // Outdoors
        townMain,
        townOutskirts_west,
        townOutskirts_north,
        townOutskirts_east,

        woods_west,
        hills,
        castle_outside,

        ratfolkDungeon_entrance,

        // Dungeons
        ratfolkDungeon
    }
}
