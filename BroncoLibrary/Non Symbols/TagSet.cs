using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class TagSet : IEnumerable<KeyValuePair<string, float>>
    {
        private readonly Dictionary<string, float> _tags = new Dictionary<string, float>();

        public int Count { get { return _tags.Count; } }

        public void AddTag(string tagName, float weight)
        {
            if(_tags.ContainsKey(tagName)) _tags[tagName] = weight;
            else _tags.Add(tagName, weight);
        }

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
            /*
            foreach (var tag in this)
                Console.Write($"{tag.Key}: {tag.Value} ");
            Console.WriteLine();

            foreach (var tag in other)
                Console.Write($"{tag.Key}: {tag.Value} ");

            Console.WriteLine();
            Console.WriteLine($"Total: {weight} : {matched}");
            */
            return matched ? weight : 0;
        }

        public TagSet Clone()
        {
            TagSet cloned = new TagSet();
            foreach(var tag in _tags)
                cloned.AddTag(tag.Key, tag.Value);

            return cloned;
        }

        public IEnumerator<KeyValuePair<string, float>> GetEnumerator()
        {
            return _tags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tags.GetEnumerator();
        }
    }
}
