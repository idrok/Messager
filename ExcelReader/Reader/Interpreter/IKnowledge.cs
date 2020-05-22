namespace GameInnvoation.ExcelReader
{
    public interface IKnowledge
    {
        InterpreterType Recognize(params string[] symbol);
    }
}