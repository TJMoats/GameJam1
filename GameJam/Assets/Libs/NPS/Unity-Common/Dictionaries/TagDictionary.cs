using System.Collections.Generic;
using UnityEngine;

namespace NPS
{
    [CreateAssetMenu(fileName = "Tag Dictionary", menuName = "Dictionary/Tag Dictionary")]
    public class TagDictionary : BaseDictionary
    {
        [SerializeField]
        private List<string> tags;
        public List<string> Tags
        {
            get => tags;
        }

        public override void Populate()
        {
            tags = new List<string>();
        }
    }
}