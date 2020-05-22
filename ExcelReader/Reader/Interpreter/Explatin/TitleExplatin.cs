using System.Linq;

namespace GameInnvoation.ExcelReader
{
    public class TitleExplatin : IKnowledge
    {
        private readonly string[] keys = {"title"};
        
        public InterpreterType Recognize(params string[] symbol)
        {
            foreach (var key in keys)
            {
                if (symbol.Contains(key) == true)
                {
                    return InterpreterType.Title;
                }
            }

            return InterpreterType.None;
        }
    }
}