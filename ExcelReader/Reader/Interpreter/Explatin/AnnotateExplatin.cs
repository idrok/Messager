using System.Linq;

namespace GameInnvoation.ExcelReader
{
    public class AnnotateExplatin : IKnowledge
    {
        private readonly string[] keys = {"#", "//", "--"};
        
        public InterpreterType Recognize(params string[] symbol)
        {
            foreach (var key in keys)
            {
                if (symbol.Contains(key) == true)
                {
                    return InterpreterType.Annotate;
                }
            }

            return InterpreterType.None;
        }
    }
}