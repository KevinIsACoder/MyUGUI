using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public class AbBuilderEditor : EditorWindow
    {
        private static EditorWindow wd;
        [MenuItem("AbBuilder/ShowAbWindow", false)]
        static void ShowAbWindow()
        {
            wd = new EditorWindow();
            wd.Show();
        }
        
    }   
}
