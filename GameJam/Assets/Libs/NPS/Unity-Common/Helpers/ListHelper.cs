using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPS
{
    public static class ListHelper
    {
        public static void Resize<T>(this List<T> _list, int _sz, T _c)
        {
            int cur = _list.Count;
            if (_sz < cur)
            {
                _list.RemoveRange(_sz, cur - _sz);
            }
            else if (_sz > cur)
            {
                // This bit is purely an optimisation, to avoid multiple automatic capacity changes.
                if (_sz > _list.Capacity) 
                {
                    _list.Capacity = _sz;
                }
                _list.AddRange(Enumerable.Repeat(_c, _sz - cur));
            }
        }

        public static void Resize<T>(this List<T> _list, int _sz) where T : new()
        {
            Resize(_list, _sz, new T());
        }
    }
}