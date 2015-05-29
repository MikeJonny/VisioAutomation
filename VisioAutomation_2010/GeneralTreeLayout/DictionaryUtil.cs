using System.Collections.Generic;

namespace GeneralTreeLayout
{
    static class DictionaryUtil
    {
        public static V GetValue<K, V>(Dictionary<K, V> dic, K key, V defval)
        {
            V the_item;
            bool contains = dic.TryGetValue(key, out the_item);

            if (!contains)
            {
                return defval;
            }

            return the_item;
        }
    }
}