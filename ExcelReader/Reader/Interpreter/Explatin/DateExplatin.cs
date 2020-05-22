using System.Linq;

namespace GameInnvoation.ExcelReader
{
    public class DataExplatin : IKnowledge
    {
        private readonly string[] keys = {"data"};
        
        public InterpreterType Recognize(params string[] symbol)
        {
            foreach (var key in keys)
            {
                if (symbol.Contains(key) == true)
                {
                    return InterpreterType.Data;
                }
            }

            return InterpreterType.None;
        }
    }
}