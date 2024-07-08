using UnityEditor;
using UnityEngine;

namespace UI.Editor
{
    public class UIReloader : AssetModificationProcessor
    {
        protected static string[] OnWillSaveAssets(string[] paths)
        {
            Debug.Log("Reloading UI");
            SassCompiler.Refresh();
            return paths;
        }
    }
}