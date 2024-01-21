using UnityEditor;
using UnityEngine;

namespace XephTools
{
    public class DebugMonitorWindow : EditorWindow
    {
        [MenuItem("Window/Debug Monitor")]
        public static new void Show()
        {
            GetWindow(typeof(DebugMonitorWindow), false, "Debug Monitor");
        }

        private void Update()
        {
            Repaint();
        }

        private void OnGUI()
        {
            if (!EditorApplication.isPlaying)
            {
                GUILayout.Label("Monitor values will appear here at runtime...");
                return;
            }
            if (DebugMonitor.Data.Count < 1)
            {
                GUILayout.Label("No values to monitor...");
                return;
            }

            foreach (var d in DebugMonitor.Data)
            {
                GUILayout.Label(d.Key + ": " + d.Value.ToString());
            }
        }
    }
}
