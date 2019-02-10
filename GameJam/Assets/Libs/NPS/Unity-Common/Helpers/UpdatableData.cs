using Sirenix.OdinInspector;

namespace NPS
{
    public class UpdatableData : SerializedScriptableObject
    {
        public event System.Action OnValuesUpdated;
        public bool autoUpdate;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (autoUpdate)
            {
                UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
            }
        }

        [Button]
        public void NotifyOfUpdatedValues()
        {
            UnityEditor.EditorApplication.update -= NotifyOfUpdatedValues;
            OnValuesUpdated?.Invoke();
        }
#endif
    }
}