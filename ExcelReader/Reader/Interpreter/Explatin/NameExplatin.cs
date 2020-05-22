using System.Linq;

namespace GameInnvoation.ExcelReader
{
    public class NameExplatin : IKnowledge
    {
        private readonly string[] keys = {"name"};
        
        public InterpreterType Recognize(params string[] symbol)
        {
            foreach (var key in keys)
            {
                if (symbol.Contains(key) == true)
                {
                    return InterpreterType.Name;
                }
            }

            return InterpreterType.None;
        }
    }
}