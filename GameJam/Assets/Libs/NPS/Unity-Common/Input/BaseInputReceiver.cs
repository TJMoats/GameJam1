using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NPS
{
    public class BaseInputReceiver : SerializedMonoBehaviour
    {
        public Vector3 MousePosition
        {
            get => Input.mousePosition;
        }

        public bool MouseIsOverUiElement
        {
            get => EventSystem.current.IsPointerOverGameObject();
        }

        public Vector3 MouseTerrainPosition
        {
            get => MasterManager.MainCamera.ScreenToGroundPoint(MousePosition);
        }

        public Collider GetColliderAtMousePosition(LayerMask _layerMask)
        {
            return MasterManager.MainCamera.GetColliderAtPosition(MousePosition, _layerMask);
        }
    }
}