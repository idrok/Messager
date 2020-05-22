using System.Collections.Generic;

namespace GameInnvoation.ExcelReader
{
    public class Explatin
    {
        private IKnowledge annotate = new AnnotateExplatin();
        private IKnowledge title = new TitleExplatin();
        private IKnowledge name = new NameExplatin();
        private IKnowledge type = new TypeExplatin();
        private IKnowledge data = new DataExplatin();
        private IKnowledge sheet = new SheetExplatin();
        
        List<IKnowledge> explatins = new List<IKnowledge>(6);

        public Explatin()
        {
            explatins.Add(annotate);
            explatins.Add(title);
            explatins.Add(name);
            explatins.Add(type);
            explatins.Add(data);
            explatins.Add(sheet);
        }

        public InterpreterType Interpreter(params string[] keys)
        {
            foreach (var knowledge in explatins)
            {
                var type = knowledge.Recognize(keys);
                if (type != InterpreterType.None)
                {
                    return type;
                }
            }

            return InterpreterType.None;
        } 
    }
}