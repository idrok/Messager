using System.IO;
using ExcelDataReader;
using UnityEngine;

namespace GameInnvoation.ExcelReader
{
    public class ExcelHandler
    {
        public void Handler()
        {
            void Callback(ScriptableExcels excel)
            {
                var data = excel.ExcelMemoryData;

                using (var stream = new MemoryStream(data))
                {
                    var source = ExcelReaderFactory.CreateReader(stream, null);
                    var set = source.AsDataSet();
                    var tables = set.Tables;
                    if (source.Read() == true)
                    {
                        Debug.LogFormat($"----line:{source.GetString(0)} name:{source.Name}");
                    }

                    if (source.NextResult() == true)
                    {
                        if (source.Read() == true)
                        {
                            Debug.LogFormat($"----line:{source.GetString(0)} name:{source.Name}");
                        }
                    }
                }
            }

            Readable.Asset.Reader<ScriptableExcels>(null, Callback);
        }
    }
}