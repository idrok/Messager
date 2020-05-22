using System;
using System.Collections.Generic;
using System.IO;
using Tang;
using UnityEngine;

namespace Bnyx.AI
{
    public class ReadLogicTree
    {
        string filePath = Path.Combine(Application.dataPath, Definition.LogicTreeFile);
        private Dictionary<string, LogicTree> mCache;

        public ReadLogicTree()
        {
            mCache = ReadFromPath();
        }
        
        public Dictionary<string, LogicTree> ReadFromPath()
        {
            var json = File.ReadAllText(filePath);
            var dict = Tools.Json2Obj<Dictionary<string, LogicTree>>(json);
            
            // foreach (var kv in dict)
            // {
            //     Debug.LogFormat($"key:{kv.Key} value:{kv.Value.GivenIdle}");
            // }

            return dict;
        }

        // key = boss003 (Name)
        public string GetIdle(string key)
        {
            if (mCache != null) return mCache[key].GivenIdle;
            else throw new NullReferenceException("mCache");
        }

        public string GetWalkAndRun(string key)
        {
            if (mCache != null) return mCache[key].GivenWalkAndRun;
            else
            {
                throw new NullReferenceException("mCache");
            }
        }

        public string GetWalk(string key)
        {
            if (mCache != null) return mCache[key].GivenWalk;
            else throw new NullReferenceException("mCache");
        }
    }
}