using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutTransition : SceneTransition
{
    [SerializeField]
    private string targetTransitionId;

    [SerializeField]
    private string transitionId = System.Guid.NewGuid().ToString();
    public string TransitionId
    {
        get
        {
            if (string.IsNullOrEmpty(TransitionId))
            {
                transitionId = System.Guid.NewGuid().ToString();
            }
            return transitionId;
        }
    }

    [SerializeField]
    private Transform spawnPoint;
    public Transform SpawnPoint
    {
        get
        {
            if (spawnPoint == null)
            {
                Transform sp = transform.Find("SpawnPoint");
                if (sp == null)
                {
                    GameObject spgo = new GameObject("SpawnPoint");
                    spgo.transform.SetParent(transform);
                    spgo.transform.localPosition = Vector3.zero;
                    sp = spgo.transform;
                }
                spawnPoint = sp;
            }
            return spawnPoint;
        }
        private set => spawnPoint = value;
    }

    public override TransitionType TransitionType
    {
        get => TransitionType.fade;
    }

    protected override void OnCharacterEnter(CharacterController _characterController)
    {
        if (!Triggered && _characterController == MasterManager.Instance.Player)
        {
            Triggered = true;
            // WorldManager.Instance.MakeSceneActive(TargetSceneName);
        }
    }

    private void OnValidate()
    {
        SpawnPoint = SpawnPoint;
    }

    private void OnDrawGizmos()
    {
        if (SpawnPoint != null)
        {
            Gizmos.color = new Color(0, .2f, .8f, .5f);
            Gizmos.DrawSphere(SpawnPoint.position, .5f);
        }
    }
}
