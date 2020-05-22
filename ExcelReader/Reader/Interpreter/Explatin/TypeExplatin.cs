using System.Linq;

namespace GameInnvoation.ExcelReader
{
    public class TypeExplatin : IKnowledge
    {
        private readonly string[] keys = {"type"};
        
        public InterpreterType Recognize(params string[] symbol)
        {
            foreach (var key in keys)
            {
                if (symbol.Contains(key) == true)
                {
                    return InterpreterType.Type;
                }
            }

            return InterpreterType.None;
        }
    }
}