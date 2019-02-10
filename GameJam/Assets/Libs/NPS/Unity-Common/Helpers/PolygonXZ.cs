using UnityEngine;

namespace NPS
{
    /// <summary>
    /// Represents a polygon in the XZ plane. This is not necessarily axis aligned.
    /// </summary>
    public class PolygonXZ
    {
        /// <summary>
        /// Represents an empty polygon, i.e. zero edges.
        /// </summary>
        public static readonly PolygonXZ empty = new PolygonXZ(new Vector3[0]);

        public Vector3[] _points;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonXZ"/> class.
        /// </summary>
        /// <param name="_points">The points making up the polygon.</param>
        public PolygonXZ(params Vector3[] _points)
        {
            this._points = _points;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonXZ"/> class.
        /// </summary>
        /// <param name="_capacity">The capacity.</param>
        public PolygonXZ(int _capacity)
        {
            _points = new Vector3[_capacity];
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get
            {
                return _points.Length;
            }
        }

        /// <summary>
        /// Gets the <see cref="Vector3"/> with the specified index. There is no bounds checking.
        /// </summary>
        /// <value>
        /// The <see cref="Vector3"/>.
        /// </value>
        /// <param name="_idx">The index.</param>
        /// <returns>The point at the index</returns>
        public Vector3 this[int _idx]
        {
            get
            {
                return _points[_idx];
            }
            set
            {
                _points[_idx] = value;
            }
        }

        /// <summary>
        /// Determines whether the specified point is contained within this polygon.
        /// </summary>
        /// <param name="_test">The point to test.</param>
        /// <returns><c>true</c> if the point is contained, otherwise <c>false</c></returns>
        public bool Contains(Vector3 _test)
        {
            int i;
            int j;
            bool result = false;
            for (i = 0, j = _points.Length - 1; i < _points.Length; j = i++)
            {
                if ((_points[i].z > _test.z) != (_points[j].z > _test.z) &&
                    (_test.x < ((_points[j].x - _points[i].x) * (_test.z - _points[i].z) / (_points[j].z - _points[i].z)) + _points[i].x))
                {
                    result = !result;
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the bounds.
        /// </summary>
        /// <returns>The bounding rectangle</returns>
        public Bounds CalculateBounds()
        {
            Vector3 pmax = _points[0];
            Vector3 pmin = pmax;

            for (int i = 1; i < _points.Length; i++)
            {
                Vector3 p = _points[i];
                if (p.x > pmax.x)
                {
                    pmax.x = p.x;
                }
                else if (p.x < pmin.x)
                {
                    pmin.x = p.x;
                }

                if (p.z > pmax.z)
                {
                    pmax.z = p.z;
                }
                else if (p.z < pmin.z)
                {
                    pmin.z = p.z;
                }

                if (p.y > pmax.y)
                {
                    pmax.y = p.y;
                }
                else if (p.y < pmin.y)
                {
                    pmin.y = p.y;
                }
            }

            Vector3 size = pmax - pmin + new Vector3(.1f, .1f, .1f);
            return new Bounds(pmin + (size / 2f), size);
        }

        public override string ToString()
        {
            string info = "PolygonXZ: [";
            for (int x = 0; x < _points.Length; x++)
            {
                info += _points[x] + ", ";
            }
            info.Remove(info.Length - 2);
            info += "]";
            return info;
        }
    }
}
