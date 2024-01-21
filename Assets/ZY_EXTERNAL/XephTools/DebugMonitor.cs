using System.Collections.Generic;

namespace XephTools
{
    public static class DebugMonitor
    {
        static Dictionary<string, string> _data = new Dictionary<string, string>();
        public static Dictionary<string, string> Data { get { return _data; } }
        public static void UpdateValue(string key, object value)
        {
#if UNITY_EDITOR
            if (!_data.ContainsKey(key))
            {
                _data.Add(key, value.ToString());
            }
            else
            {
                _data[key] = value.ToString();
            }
#endif
        }
    }
}
