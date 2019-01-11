using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Trigger
{
    private Scene
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            
        }
        
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene()
    }
}
