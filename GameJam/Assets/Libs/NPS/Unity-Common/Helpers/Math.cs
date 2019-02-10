using System;
using UnityEngine;

namespace NPS
{
    public class Math
    {
        public static int ToNextNearestPowerOf2(int _value)
        {
            return Convert.ToInt32(System.Math.Pow(2, System.Math.Round(System.Math.Log(_value) / System.Math.Log(2))));
        }

        public static int NearestPowerOf2(int _value)
        {
            int next = ToNextNearestPowerOf2(_value);
            int prev = next >> 1;
            return next - _value < _value - prev ? next : prev;
        }

        public static float RoundTo(float _number, float _factor = 1)
        {
            return Mathf.Round(_number / _factor) * _factor;
        }

        public static int RoundToOdd(float _number)
        {
            int intValue = Mathf.RoundToInt(_number);
            if (intValue %2 == 0)
            {
                return intValue - 1;
            }
            return intValue;
        }

        public static Vector3 FindPointAlongCircle(Vector3 _c, float _r, int _i, float _angleSteps = 60f)
        {
            return _c + Quaternion.AngleAxis(_angleSteps * _i, Vector3.up) * (Vector3.right * _r);
        }
    }
}