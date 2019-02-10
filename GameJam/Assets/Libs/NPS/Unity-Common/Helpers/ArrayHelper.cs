using System;
using System.Linq;

namespace NPS
{
    public static class ArrayHelper
    {
        public static T[,] GetSubSet<T>(T[,] _arrayIn, int _startX, int _startY, int _sizeX, int _sizeY)
        {
            if (_sizeX < 0 || _sizeY < 0)
            {
                throw new OverflowException();
            }
            if (_startX + _sizeX > _arrayIn.GetLength(0)  || _startX < 0 || _startY + _sizeY > _arrayIn.GetLength(1) || _startY < 0)
            {
                // Debug.Log($"({_startX}+{_sizeX}=={_startX + _sizeX})/({_arrayIn.GetLength(0)})");
                // Debug.Log($"({_startY}+{_sizeY}=={_startY + _sizeY})/({_arrayIn.GetLength(1)})");
                throw new IndexOutOfRangeException();
            }
            T[,] arrayOut = new T[_sizeX, _sizeY];
            for (int x = 0; x < _sizeX; x++)
            {
                for (int y = 0; y < _sizeY; y++)
                {
                    arrayOut[x, y] = _arrayIn[x + _startX, y + _startY];
                }
            }
            return arrayOut;
        }

        public static T[] GetSubSet<T>(T[] _arrayIn, int _start, int _size)
        {
            if (_start < 0 || _start + _size > _arrayIn.Length)
            {
                throw new IndexOutOfRangeException();
            }
            T[] arrayOut = new T[_size];
            for (int i = 0; i < _size; i++)
            {
                arrayOut[i] = _arrayIn[_start + i];
            }
            return arrayOut;
        }

        public static T[,] FilledArray<T>(T _value, int _sizeX, int _sizeY)
        {
            T[,] arrayOut = new T[_sizeX, _sizeY];
            for (int i = 0; i < _sizeX; i++)
            {
                for (int j = 0; j < _sizeY; j++)
                {
                    arrayOut[i, j] = _value;
                }
            }
            return arrayOut;
        }

        public static T[] FilledArray<T>(T _value, int _size)
        {
            T[] arrayOut = new T[_size];
            for (int i = 0; i < _size; i++)
            {
                arrayOut[i] = _value;
            }
            return arrayOut;
        }

        public static void PrintArray<T>(T[] _arrayIn)
        {
            string output = $"Array {typeof(T)}[{_arrayIn.Length}] = [";
            for (int i = 0; i < _arrayIn.Length; i++)
            {
                output += $"{_arrayIn[i]}, ";
            }
            output = output.TrimEnd(new char[] { ',', ' ' });
            output += "]";
            Debug.Log(output);
        }

        public static void PrintArray<T>(T[,] _arrayIn, bool _lineBreak = false)
        {

            if (_lineBreak)
            {
                Debug.Log($"Array {typeof(T)}[{_arrayIn.GetLength(0)},{_arrayIn.GetLength(1)}] = [");
                for (int i = 0; i < _arrayIn.GetLength(1); i++)
                {
                    string output = $"Row {i}: [";
                    for (int j = 0; j < _arrayIn.GetLength(0); j++)
                    {
                        output += $"{_arrayIn[j, i]}, ";
                    }
                    output = output.TrimEnd(new char[] { ',', ' ' });
                    output += "]";
                    Debug.Log(output);
                }
                Debug.Log("\n]");
            }
            else
            {
                string output = $"Array {typeof(T)}[{_arrayIn.GetLength(0)},{_arrayIn.GetLength(1)}] = [";

                for (int i = 0; i < _arrayIn.GetLength(1); i++)
                {
                    output += "\n\t[";
                    for (int j = 0; j < _arrayIn.GetLength(0); j++)
                    {
                        output += $"{_arrayIn[j, i]}, ";
                    }
                    output = output.TrimEnd(new char[] { ',', ' ' });
                    output += "]";
                }
                output += "\n]";
                Debug.Log(output);
            }

        }

        public static void ForEach<T>(this Array _array, Action<T, int[]> _action)
        {
            int[] dimensionSizes = Enumerable.Range(0, _array.Rank).Select(i => _array.GetLength(i)).ToArray();
            ArrayForEach(dimensionSizes, _action, new int[] { }, _array);
        }

        private static void ArrayForEach<T>(int[] _dimensionSizes, Action<T, int[]> _action, int[] _externalCoordinates, Array _masterArray)
        {
            if (_dimensionSizes.Length == 1)
            {
                for (int i = 0; i < _dimensionSizes[0]; i++)
                {
                    int[] globalCoordinates = _externalCoordinates.Concat(new[] { i }).ToArray();
                    T value = (T)_masterArray.GetValue(globalCoordinates);
                    _action(value, globalCoordinates);
                }
            }
            else
            {
                for (int i = 0; i < _dimensionSizes[0]; i++)
                {
                    ArrayForEach(_dimensionSizes.Skip(1).ToArray(), _action, _externalCoordinates.Concat(new[] { i }).ToArray(), _masterArray);
                }
            }
        }

        public static void PopulateArray<T>(this Array _array, Func<int[], T> _calculateElement)
        {
            _array.ForEach<T>((element, indexArray) => _array.SetValue(_calculateElement(indexArray), indexArray));
        }

        public static T[,] Invert<T>(T[,] _arrayIn)
        {
            T[,] arrayOut = new T[_arrayIn.GetLength(1), _arrayIn.GetLength(0)];
            _arrayIn.ForEach<T>((item, coord) =>
            {
                arrayOut[coord[1], coord[0]] = item;
            });
            return arrayOut;
        }

        public static T[] Flatten<T>(T[,] _arrayIn)
        {
            T[] arrayOut = new T[_arrayIn.GetLength(0) * _arrayIn.GetLength(1)];
            _arrayIn.ForEach<T>((item, coord) =>
            {
                arrayOut[(coord[0] * _arrayIn.GetLength(1)) + coord[1]] = item;
            });
            return arrayOut;
        }

        public static T[,] Inflate<T>(T[] _arrayIn, int _width, int _height)
        {
            if (_arrayIn.Length != _width * _height)
            {
                throw new Exception("Array Size Missmatch.");
            }
            T[,] arrayOut = new T[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    arrayOut[i, j] = _arrayIn[(i * _height) + j];
                }
            }

            return arrayOut;
        }

        public static T[,] Rotate<T>(T[,] _arrayIn)
        {
            T[,] ret = new T[_arrayIn.GetLength(1), _arrayIn.GetLength(0)];

            for (int i = 0; i < ret.GetLength(0); ++i)
            {
                for (int j = 0; j < ret.GetLength(1); ++j)
                {
                    ret[i, j] = _arrayIn[_arrayIn.GetLength(0) - j - 1, i];
                }
            }

            return ret;
        }
    }
}