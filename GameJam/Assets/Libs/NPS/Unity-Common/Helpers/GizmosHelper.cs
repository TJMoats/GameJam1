using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS
{
    public class GizmosHelper
    {
        public static void DrawShape(Vector3[] _points)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                int j = i + 1;
                if (j == _points.Length)
                {
                    j = 0;
                }
                Gizmos.DrawLine(_points[i], _points[j]);
            }
        }
    }
}