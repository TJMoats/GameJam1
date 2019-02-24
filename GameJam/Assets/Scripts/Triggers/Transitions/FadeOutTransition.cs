using NPS;
using Sirenix.OdinInspector;
using UnityEngine;

public class FadeOutTransition : SceneTransition
{
    [SerializeField]
    private SpawnPoint targetSpawnPoint;
    private Vector2 SpawnPosition
    {
        get => SpawnPoint.GetSpawnPosition(spawnPointGuid);
    }

    [SerializeField]
    private string spawnPointGuid = "";
    
    public override TransitionType TransitionType
    {
        get => TransitionType.fade;
    }

    public string MySceneName
    {
        get
        {
            SceneController sceneController = GetComponentInParent<SceneController>();
            return sceneController.SceneName;
        }
    }

    protected override void OnCharacterEnter(CharacterController _characterController)
    {
        if (!Triggered && _characterController == MasterManager.Instance.Player)
        {
            Triggered = true;
            FadeEffect.Instance.FadeOut(FadeFinished);
        }
    }

    private void FadeFinished()
    {
        SceneHelper.MakeAllScenesInactive();
        SceneHelper.MakeSceneActive(TargetSceneName);
        SceneHelper.PrimarySceneName = TargetSceneName;
        PlayerController.Instance.gameObject.transform.position = SpawnPosition;
        FadeEffect.Instance.FadeIn(null);
        Triggered = false;
    }

    [Button]
    public void GrabGuid()
    {
        if (spawnPointGuid == "" && targetSpawnPoint != null)
        {
            spawnPointGuid = targetSpawnPoint.GUID;
        }
    }

    private void OnValidate()
    {
        GrabGuid();
    }
}
