namespace GameInnvoation.ExcelReader
{
    public enum ReaderType : byte
    {
        /// <summary>
        /// default: read from asset file
        /// </summary>
        Asset            = 1,  
        
        /// <summary>
        /// read from a file path
        /// </summary>
        Path             = 2,  
        
        /// <summary>
        /// read from network stream
        /// </summary>
        Stream           = 3,  
        
        /// <summary>
        /// read from unity addressable update package
        /// </summary>
        Addressable      = 4    
        
    }
}