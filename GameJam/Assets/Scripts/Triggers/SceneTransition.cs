using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Trigger
{
    [SerializeField]
    private Scene targetScene;

    [SerializeField]
    private int transitionIndex;

    protected override void OnCharacterEnter(CharacterController _characterController)
    {
        if (_characterController == MasterManager.Instance.Player)
        {

        }
    }

    private void SwitchScene()
    {

    }
}
