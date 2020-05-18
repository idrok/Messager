using System;
using UnityEngine;

namespace GameInnvoation.ExcelReader
{
    /// <summary>
    /// 该类为保存数据的持久化
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "innvoation", menuName = "ExcelReader", order = 0)]
    public class ScriptableExcels : ScriptableObject
    {
        // 相对文件路径，相对于Application.dataPath
        // 在编辑器中的项目的相对跟路径即就是Assets目录里那一层
        // C:/work/bnyx/Assets
        [Header("是否是相对文件路径")]
        public bool _IsRelativePath;
        
        [Multiline, Header("xlsx文件路径，多个文件逗号(,)分隔")]
        public string _ExcelPath;

        private byte[] _ExcelMemoryData;

        public byte[] ExcelMemoryData
        {
            get => _ExcelMemoryData;
            set => _ExcelMemoryData = value;
        }

        private void OnEnable()
        {
            Debug.LogFormat($"OnEnable");
        }

        private void OnDisable()
        {
            Debug.LogFormat($"OnDisable");
        }

        private void OnDestroy()
        {
            Debug.LogFormat($"OnDestroy");
        }
    }
}