namespace GameInnvoation.ExcelReader
{
    public enum InterpreterType : byte
    {
        None          = 1,
        
        /// <summary>
        /// 用户注解
        /// </summary>
        Annotate      = 2,
        
        /// <summary>
        /// Excel注解
        /// </summary>
        AnnotateExcel = 3,
        
        /// <summary>
        /// 标题
        /// </summary>
        Title         = 4,
        
        /// <summary>
        /// 名称
        /// </summary>
        Name          = 5,
        
        /// <summary>
        /// 类型
        /// </summary>
        Type          = 6,
        
        /// <summary>
        /// 子表
        /// </summary>
        Sheet         = 7,
        
        /// <summary>
        /// 数据
        /// </summary>
        Data          = 8
    }
}