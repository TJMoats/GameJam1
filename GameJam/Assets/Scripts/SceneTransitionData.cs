using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Icon Dictionary", menuName = "Triggers/SceneTransitionData")]
public class SceneTransitionData : SerializedScriptableObject
{
    [SerializeField]
    private Scene scene1;

    [SerializeField]
    private int transitionId1;

    [SerializeField]
    private Scene scene2;

    [SerializeField]
    private int trnsitionId2;
}
