using UnityEngine;

namespace GameInnvoation.ExcelReader
{
    public class MonoTests : MonoBehaviour
    {
        //     [ContextMenu("TestAsset")]
        //     public void TestAsset()
        //     {
        //         var asset = Readable.Asset;
        //         asset.Reader<ScriptableExcels>(null, scriptable =>
        //         {
        //             Debug.LogFormat(scriptable.ExcelPath);
        //         });
        //     }
        //
        //     [ContextMenu("TestInterpreterPattern")]
        //     public void TestInterpreterPattern()
        //     {
        //         InterpreterPattern.InterpreterPattern.Interpreter(null);
        //     }

        [ContextMenu("TestExcelHandler")]
        public void TestExcelHandler()
        {
            new ExcelHandler().Handler();
        }
    }
}