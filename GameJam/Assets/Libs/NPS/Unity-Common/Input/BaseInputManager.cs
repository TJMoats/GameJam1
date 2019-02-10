using UnityEngine;
using UnityEngine.EventSystems;

namespace NPS
{
    public class BaseInputManager : BaseManager
    {
        public Vector3 MousePosition
        {
            get
            {
                return Input.mousePosition;
            }
        }

        public bool IsOverUiElement
        {
            get
            {
                return EventSystem.current.IsPointerOverGameObject();
            }
        }

        public Vector3 GetMouseTerrainPosition()
        {
            return MasterManager.MainCamera.ScreenToGroundPoint(MousePosition);
        }
    }
}