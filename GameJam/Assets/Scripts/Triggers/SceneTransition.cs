using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Trigger
{
    [SerializeField]
    private SceneData targetScene;

    [SerializeField]
    private int transitionIndex;

    protected override void OnCharacterEnter(CharacterController _characterController)
    {
        if (_characterController == MasterManager.Instance.Player)
        {
            WorldManager.ChangeScene(targetScene.name);
        }
    }
}
