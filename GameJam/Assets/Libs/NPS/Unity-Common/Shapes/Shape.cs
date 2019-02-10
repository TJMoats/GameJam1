using UnityEngine;

namespace NPS
{
    public abstract class Shape
    {
        public abstract Vector3[] GetVertexes(Vector3 _position, float _sideLength);
        public abstract float GetVertexRadius(float _sideLength);
        public abstract float GetSideRadius(float _sideLength);
        public abstract Vector3 GetPositionInGrid(float _sideLength, Vector2Int _gridPosition, Vector2Int _gridSize);
    }
}