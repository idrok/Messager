using System.IO;
using Tang.Editor;
using UnityEditor;
using UnityEngine;

namespace GameInnvoation.ExcelReader
{
    [CustomEditor(typeof(ScriptableExcels))]
    public class ScriptableExcelsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            GUILayout.Space(10);
            var btnVaildPath = GUILayout.Button("Step1.验证xlsx资源的路径", GUILayout.Height(30));
            GUILayout.Space(5);
            var btnImport =    GUILayout.Button("Step2.导入xlsx数据到资源", GUILayout.Height(30));
            GUILayout.Space(5);
            var btnDataVaild = GUILayout.Button("Step3.校验xlsx数据的完整", GUILayout.Height(30));

            if (btnVaildPath)
            {
                CheckResPath();
            }
            
            if (btnImport)
            {
                var scriptable = (ScriptableExcels) target;
                Debug.LogFormat(scriptable._IsRelativePath + "");
            }
        }

        private void CheckResPath() 
        {
             var scriptable = (ScriptableExcels)target;
             var origin = scriptable._ExcelPath;
             var paths = origin.Split(',');

             foreach (var path in paths)
             {
                 if (scriptable._IsRelativePath == false)
                 {
                     var vaild = File.Exists(path);
                     if (vaild == false)
                     {
                         EditorUtility.DisplayDialog("文件路径校验", $"{path}\r\n路径无效？", "确定");
                     }
                 }
                 
                 if (scriptable._IsRelativePath == true)
                 {
                     var filePath = Path.Combine(Application.dataPath, path);
                     var valid = File.Exists(filePath);
                     if (valid == false)
                     {
                         EditorUtility.DisplayDialog("文件路径校验", $"{path}\r\n路径无效？", "确定");
                     }
                 }
             }
             //File.Exists()

        }
    }
}