using UnityEngine;

namespace NPS
{
    public class Hexagon : Shape
    {
        public override Vector3[] GetVertexes(Vector3 _position, float _sideLength)
        {
            Vector3[] vertexes = new Vector3[6];
            for (int i = 0; i < vertexes.Length; i++)
            {
                vertexes[i] = Math.FindPointAlongCircle(_position, _sideLength, i);
            }
            return vertexes;
        }

        public override float GetVertexRadius(float _sideLength)
        {
            return _sideLength;
        }

        public override float GetSideRadius(float _sideLength)
        {
            float cSq = _sideLength * _sideLength;
            float bSq = (_sideLength / 2) * (_sideLength / 2);
            return Mathf.Sqrt(cSq - bSq);
        }

        public override Vector3 GetPositionInGrid(float _sideLength, Vector2Int _gridPosition, Vector2Int _gridSize)
        {
            float VertexRadius = GetVertexRadius(_sideLength);
            float SideRadius = GetSideRadius(_sideLength);

            Vector3 origin = new Vector3(VertexRadius * 2 + _sideLength * _gridSize.x * -1, 0, _gridSize.y * _sideLength * -1);

            if (_gridSize.y % 2 == 0)
            {
                origin.z += SideRadius;
            }

            Vector3 offset = new Vector3((VertexRadius + (_sideLength / 2)) * _gridPosition.x, 0, SideRadius * _gridPosition.y * 2);

            if (_gridPosition.x % 2 != 0)
            {
                offset.z += SideRadius;
            }

            return origin + offset;
        }
    }
}