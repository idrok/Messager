using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using Tang.Editor;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace GameInnvoation.ExcelReader
{
    [CustomEditor(typeof(ScriptableExcels))]
    public class ScriptableExcelsEditor : Editor
    {
        private string COLOR_HEX = "<color=#00FF00>{0}</color>";
        private List<string> mVaildPaths = new List<string>();
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
                ImportBytes();
            }

            if (btnDataVaild)
            {
                VaildateFile();
            }
        }

        private void CheckResPath() 
        {
             var asset = (ScriptableExcels)target;
             var origin = asset._ExcelPath.Replace("，", ",");
             var paths = origin.Split(',');
             var filePath = "";
             mVaildPaths.Clear();
             
             foreach (var path in paths)
             {
                 if (string.IsNullOrWhiteSpace(path) == false)
                 {
                     if (asset._IsRelativePath == true)
                     {
                         filePath = Path.Combine(Application.dataPath, path);
                     }
                     else
                     {
                         filePath = path;
                     }

                     var valid = File.Exists(filePath);
                     if (valid == false)
                     {
                         EditorUtility.DisplayDialog("文件路径校验", $"{filePath}\r\n文件无法读取？", "确定");
                     }
                     else
                     {
                         Debug.LogFormat(COLOR_HEX, "Step1 ----------> 资源路径检查 OK");
                         mVaildPaths.Add(filePath);
                     }
                 }
                 else
                 {
                     // EditorUtility.DisplayDialog("文件路径校验", $"{filePath}\r\n路径无效？", "确定");
                 }
             }
        }

        private void ImportBytes()
        {
            var asset = (ScriptableExcels)target;
            foreach (var path in mVaildPaths)
            {
                var bytes =  File.ReadAllBytes(path);
                
                // todo only support single file
                asset.ExcelMemoryData = bytes;
                
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssets();
                Debug.LogFormat(COLOR_HEX, "Step2 ----------> 导入文件数据 OK");
            }   
        }

        private void VaildateFile()
        {
            var asset = (ScriptableExcels)target;
            if (asset.ExcelMemoryData != null && asset.ExcelMemoryData.Length > 0)
            {
                var data = asset.ExcelMemoryData;
                using (var stream = new MemoryStream(data))
                {
                    try
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            Debug.LogFormat(COLOR_HEX, "Step3 ----------> 校验文件数据 OK");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        EditorUtility.DisplayDialog("文件数据校验", $"当前保存的数据无法读取？", "确定");
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("文件数据校验", $"当前保存的数据无法读取？", "确定");
            }
        }
    }
}