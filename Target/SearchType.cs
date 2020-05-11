namespace AI.Target
{
    public enum SearchType
    {
        BodyFar_05,  // 5米范围内           
        BodyFar_10,  // 10米范围内      
        BodyFar_15,  // 15米范围内      
        BodyFar_User // [任意]米范围内      
    }

    // 当前怪物的可视范围 = 第一次查询半径，默认50米
    public enum SearchMatch
    {
        BodyFar_50,  // 50米范围内
    }
}