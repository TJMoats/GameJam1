using UnityEngine;

namespace NPS
{
    /// <summary>
    /// Various extension to Unity camera.
    /// </summary>
    public static partial class ExtensionMethods
    {
        private static readonly Plane _xzPlane = new Plane(Vector3.up, Vector3.zero);

        /// <summary>
        /// Gets the collider at position.
        /// </summary>
        /// <param name="_cam">The camera.</param>
        /// <param name="_screenPos">The screen position.</param>
        /// <param name="_layerMask">The layer mask.</param>
        /// <param name="_maxDistance">The maximum distance.</param>
        /// <returns>The first collider found in the game world at the specified screen position.</returns>
        public static Collider GetColliderAtPosition(this Camera _cam, Vector3 _screenPos, LayerMask _layerMask, float _maxDistance = 1000.0f)
        {
            Ray ray = _cam.ScreenPointToRay(_screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
            {
                return hit.collider;
            }
            return null;
        }

        /// <summary>
        /// Casts a ray from the camera to the specified position.
        /// </summary>
        /// <param name="_cam">The camera.</param>
        /// <param name="_screenPos">The screen position.</param>
        /// <param name="_layerMask">The layer mask.</param>
        /// <param name="_maxDistance">The maximum distance.</param>
        /// <param name="_hit">The hit details.</param>
        /// <returns><c>true</c> if the ray hit something, otherwise <c>false</c></returns>
        public static bool ScreenToLayerHit(this Camera _cam, Vector3 _screenPos, LayerMask _layerMask, float _maxDistance, out RaycastHit _hit)
        {
            Ray ray = _cam.ScreenPointToRay(_screenPos);
            return Physics.Raycast(ray, out _hit, _maxDistance, _layerMask);
        }

        /// <summary>
        /// Casts a ray from the camera to the xz plane through the specified screen point and returns the point the ray intersects the xz plane in world coordinates.
        /// </summary>
        /// <param name="_cam">The camera.</param>
        /// <param name="_screenPos">The screen position.</param>
        /// <returns>The intersection point on the xz plane in world coordinates</returns>
        public static Vector3 ScreenToGroundPoint(this Camera _cam, Vector3 _screenPos)
        {
            Ray ray = _cam.ScreenPointToRay(_screenPos);

            if (_xzPlane.Raycast(ray, out float d))
            {
                return ray.GetPoint(d);
            }

            return Vector3.zero;
        }

        /// <summary>
        /// Casts a ray from the camera to the xz plane through the specified screen point and returns the point the ray intersects the xz plane in world coordinates.
        /// </summary>
        /// <param name="_cam">The camera.</param>
        /// <param name="_screenPos">The screen position.</param>
        /// <param name="_groundHeight">Height (y-coordinate) that the ground level is at.</param>
        /// <returns>The intersection point on the xz plane in world coordinates</returns>
        public static Vector3 ScreenToGroundPoint(this Camera _cam, Vector3 _screenPos, float _groundHeight)
        {
            Ray ray = _cam.ScreenPointToRay(_screenPos);
            Plane xzElevatedPlane = new Plane(Vector3.up, new Vector3(0f, _groundHeight, 0f));

            if (xzElevatedPlane.Raycast(ray, out float d))
            {
                return ray.GetPoint(d);
            }

            return Vector3.zero;
        }

        /// <summary>
        /// Generates a bounding box based on the screen points provided.
        /// This box is a 3d polygon that extends outward from the camera to the terrain.
        /// </summary>
        /// <param name="_startScreen">The starting corner of the box on the screen.</param>
        /// <param name="_endScreen">The ending corner of the box on the screen.</param>
        /// <returns></returns>
        public static PolygonXZ GetBoundingPoly(this Camera _cam, Vector3 _startScreen, Vector3 _endScreen)
        {
            // The first thing to do is get a height to work with
            Vector3 p1 = new Vector3(_endScreen.x, _startScreen.y);
            Vector3 p2 = new Vector3(_startScreen.x, _endScreen.y);
            Vector3 center = _startScreen + ((_endScreen - _startScreen) / 2f);

            float? height = _cam.GetFirstTerrainHeight(center, _startScreen, _endScreen, p1, p2);
            if (!height.HasValue)
            {
                return PolygonXZ.empty;
            }

            Vector3 c1 = _cam.ScreenToGroundPoint(_startScreen, height.Value);
            Vector3 c2 = _cam.ScreenToGroundPoint(p1, height.Value);
            Vector3 c3 = _cam.ScreenToGroundPoint(_endScreen, height.Value);
            Vector3 c4 = _cam.ScreenToGroundPoint(p2, height.Value);

            return new PolygonXZ(c1, c2, c3, c4);
        }

        /// <summary>
        /// Gets the terrain height at a given set of points.
        /// </summary>
        /// <param name="_pokes">The areas to check the height of.</param>
        /// <returns></returns>
        public static float? GetFirstTerrainHeight(this Camera _cam, params Vector3[] _pokes)
        {

            for (int i = 0; i < _pokes.Length; i++)
            {
                if (_cam.ScreenToLayerHit(_pokes[i], LayerHelper.Terrain, 10000.0f, out RaycastHit hit))
                {
                    return hit.point.y;
                }
            }

            return null;
        }
    }
}
