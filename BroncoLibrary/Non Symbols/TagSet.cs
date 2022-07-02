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
        public readonly Dictionary<string, float> Tags = new Dictionary<string, float>();

        public int Count { get { return Tags.Count; } }

        public void AddTag(string tagName, float weight)
        {
            if(Tags.ContainsKey(tagName)) Tags[tagName] = weight;
            else Tags.Add(tagName, weight);
        }

        public void AddTag(string tagName) => AddTag(tagName, 1.0f);

        public TagSet Clone()
        {
            TagSet cloned = new TagSet();
            foreach(var tag in Tags)
                cloned.AddTag(tag.Key, tag.Value);

            return cloned;
        }

        public IEnumerator<KeyValuePair<string, float>> GetEnumerator()
        {
            return Tags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Tags.GetEnumerator();
        }
    }
}
