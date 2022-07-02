using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public class TagMult : DynamicSymbol
    {
        public static float TagMultiply(TagSet t1, TagSet t2)
            => TagMultiply(t1, t2, out int notUsed);

        public static float TagMultiply(TagSet t1, TagSet t2, out int matches)
        {
            float weight = 1.0f;
            int currentMatches = 0;

            Dictionary<string, float> shorter
                = t2.Count <= t1.Count ? t2.Tags : t1.Tags;
            Dictionary<string, float> longer
                = t2.Count > t1.Count ? t2.Tags : t1.Tags;

            foreach (var tag in shorter)
            {
                float s2Weight;
                if (longer.TryGetValue(tag.Key, out s2Weight))
                {
                    weight *= tag.Value * s2Weight;
                    currentMatches++;
                }
            }

            matches = currentMatches;
            return weight;
        }

        public TagMult()
            => AddEvaluation<MetaData, MetaData>(Mult);

        public ISymbol Mult(MetaData s1, MetaData s2)
        {
            return new FloatSymbol(TagMultiply(s1.Tags, s2.Tags));
        }
    }

    public class TagContains : DynamicSymbol
    {
        public TagContains()
            => AddEvaluation<MetaData, MetaData>(Contains);

        public ISymbol Contains(MetaData s1, MetaData s2)
        {
            TagSet t1 = s1.Tags;
            TagSet t2 = s2.Tags;

            bool t1Empty = t1.Count == 0;
            bool t2Empty = t2.Count == 0;

            if (t1Empty && t2Empty) return new FloatSymbol(1);
            if (t1Empty) return new FloatSymbol(0);

            int matches;
            float result = TagMult.TagMultiply(t1, t2, out matches);
            if (matches != t2.Count) return new FloatSymbol(0);

            return new FloatSymbol(result);
        }
    }

    public class TagNoOverlap : DynamicSymbol
    {
        public TagNoOverlap()
            => AddEvaluation<MetaData, MetaData>(NoOverlap);

        public ISymbol NoOverlap(MetaData s1, MetaData s2)
        {
            TagSet t1 = s1.Tags;
            TagSet t2 = s2.Tags;

            Dictionary<string, float> shorter
                = t2.Count <= t1.Count ? t2.Tags : t1.Tags;
            Dictionary<string, float> longer
                = t2.Count > t1.Count ? t2.Tags : t1.Tags;

            foreach (var tag in shorter)
            {
                if (longer.ContainsKey(tag.Key)) return new FloatSymbol(0);
            }

            return new FloatSymbol(1);
        }
    }

    public class TagOverlap : DynamicSymbol
    {
        public TagOverlap()
            => AddEvaluation<MetaData, MetaData>(Overlap);

        public ISymbol Overlap(MetaData s1, MetaData s2)
        {
            TagSet t1 = s1.Tags;
            TagSet t2 = s2.Tags;

            bool t1Empty = t1.Count == 0;
            bool t2Empty = t2.Count == 0;

            if (t1Empty && t2Empty) return new FloatSymbol(1);
            if (t1Empty || t2Empty) return new FloatSymbol(0);

            int matches;
            float result = TagMult.TagMultiply(t1, t2, out matches);
            if (matches == 0) result = 0;

            return new FloatSymbol(result);
        }
    }
}
