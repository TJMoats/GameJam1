using UnityEngine;

namespace NPS
{
    public class Square : Shape
    {
        public override Vector3[] GetVertexes(Vector3 _position, float _sideLength)
        {
            return new Vector3[]
            {
                new Vector3(_position.x - (_sideLength/2), 0, _position.z - (_sideLength/2)),
                new Vector3(_position.x + (_sideLength/2), 0, _position.z - (_sideLength/2)),
                new Vector3(_position.x + (_sideLength/2), 0, _position.z + (_sideLength/2)),
                new Vector3(_position.x - (_sideLength/2), 0, _position.z + (_sideLength/2)),
            };
        }

        public override float GetVertexRadius(float _sideLength)
        {
            float SLSquare = _sideLength * _sideLength;
            return Mathf.Sqrt(SLSquare + SLSquare);
        }

        public override float GetSideRadius(float _sideLength)
        {
            return _sideLength;
        }

        public override Vector3 GetPositionInGrid(float _sideLength, Vector2Int _gridPosition, Vector2Int _gridSize)
        {
            Vector3 origin = new Vector3((_gridSize.x * _sideLength) / -2, 0, (_gridSize.y * _sideLength) / -2);
            Vector3 offset = new Vector3(_sideLength * _gridPosition.x, 0, _sideLength * _gridPosition.y);
            Vector3 halfSize = new Vector3(_sideLength / 2, 0, _sideLength / 2);
            return origin + offset + halfSize;
        }
    }
}