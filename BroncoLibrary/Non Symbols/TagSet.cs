using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class TagSet
    {
        private Dictionary<string, float> _tags = new Dictionary<string, float>();

        public void AddTag(string tagName, float weight)
            => _tags.Add(tagName, weight);

        public void AddTag(string tagName) => AddTag(tagName, 1.0f);

        public float CompareTags(TagSet other)
        {
            if(this._tags.Count == 0 &&
                other._tags.Count == 0) return 1;

            float weight = 1.0f;
            bool matched = false;
            Dictionary<string, float> shorter 
                = other._tags.Count <= this._tags.Count ? other._tags : this._tags;
            Dictionary<string, float> longer
                = other._tags.Count > this._tags.Count ? other._tags : this._tags;

            foreach(var tag in shorter)
            {
                float otherWeight;
                if(longer.TryGetValue(tag.Key, out otherWeight))
                {
                    weight *= tag.Value * otherWeight;
                    matched = true;
                }
            }

            return matched ? weight : 0;
        }
    }
}
