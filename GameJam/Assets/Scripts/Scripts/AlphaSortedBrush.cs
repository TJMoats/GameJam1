using System;
using UnityEngine;

namespace UnityEditor
{
    [CustomGridBrush(true, true, true, "Alpha Sorted Brush")]
    public class AlphaSortedBrush : GridBrush
    {
    }

    [CustomEditor(typeof(AlphaSortedBrush))]
    public class AlphaSortedBrushEditor : GridBrushEditor
    {
        public override GameObject[] validTargets
        {
            get
            {
                GameObject[] vt = base.validTargets;
                Array.Sort(vt, (go1, go2) => string.Compare(go1.name, go2.name));
                return vt;
            }
        }
    }
}