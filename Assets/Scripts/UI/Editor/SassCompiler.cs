using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UI.Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace UI.Editor
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class SassCompiler : AssetsModifiedProcessor
    {
        public static void Refresh()
        {
            var files = new HashSet<string>();
            var options = new EnumerationOptions
            {
                RecurseSubdirectories = true
            };
            foreach (var path in new DirectoryInfo("./Assets").GetFiles("*.scss", options))
            {
                Track(ref files, path.FullName);
            }

            CompileFiles(files);
            ES3.Save("files", files);
        }

        protected override void OnAssetsModified(string[] changedAssets,
            string[] addedAssets,
            string[] deletedAssets,
            AssetMoveInfo[] movedAssets)
        {
            var files = ES3.Load("files", new HashSet<string>());

            var changed = false;

            foreach (var path in addedAssets)
            {
                changed = changed || Track(ref files, path);
            }

            foreach (var path in changedAssets)
            {
                changed = changed || Track(ref files, path);
            }

            foreach (var moveInfo in movedAssets)
            {
                changed = changed || Track(ref files, moveInfo.destinationAssetPath);
                Untrack(ref files, moveInfo.sourceAssetPath);
            }

            foreach (var path in deletedAssets)
            {
                Untrack(ref files, path);
            }

            if (changed)
            {
                CompileFiles(files);
            }

            ES3.Save("files", files);
        }

        private static bool Track(ref HashSet<string> files, string path)
        {
            if (Path.GetExtension(path) != ".scss")
            {
                return false;
            }

            if (Path.GetFileName(path).StartsWith('_'))
            {
                return true;
            }

            files.Add(path);

            return true;
        }

        private static void Untrack(ref HashSet<string> files, string path)
        {
            files.Remove(path);
        }

        private static void CompileFiles(HashSet<string> files)
        {
            var filenames = files.Aggregate("", (current, file) => current + (file + "\n"));
            Debug.Log("Compiling Sass files:\n" + filenames);
            foreach (var file in files)
            {
                var dir = Path.GetDirectoryName(file);
                var name = Path.GetFileNameWithoutExtension(file);

                var inFile = dir + "/" + name + ".scss";
                var outFile = dir + "/Compiled/" + name + ".uss";

                var startInfo = new ProcessStartInfo
                {
                    FileName = "sass",
                    Arguments = "--no-source-map " + inFile + " " + outFile,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process.Start(startInfo)?.WaitForExit();
            }

            AssetDatabase.Refresh();

            foreach (var loader in Object.FindObjectsByType(typeof(UILoader), FindObjectsSortMode.None))
            {
                loader.GetComponent<UILoader>().Refresh();
            }
        }
    }
}