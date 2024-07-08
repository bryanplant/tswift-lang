using UnityEditor;
using UnityEngine;

namespace UI.Editor
{
    public static class ToolsWindow
    	{
            [MenuItem("Tools/Better UI/Sassy Refresh")]
            private static void OpenPersistentDataPath()
            {
                SassCompiler.Refresh();
            }
    }
}