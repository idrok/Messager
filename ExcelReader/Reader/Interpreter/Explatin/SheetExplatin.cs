using System.Linq;

namespace GameInnvoation.ExcelReader
{
    public class SheetExplatin : IKnowledge
    {
        private readonly string[] keys = {"sheet"};
        
        public InterpreterType Recognize(params string[] symbol)
        {
            foreach (var key in keys)
            {
                if (symbol.Contains(key) == true)
                {
                    return InterpreterType.Sheet;
                }
            }

            return InterpreterType.None;
        }
    }
}