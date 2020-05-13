namespace Bnyx.Messager
{
    /// <summary>
    /// custom message enum for poolprovider
    /// exp: 我想把消息推送给活动页面和背包
    /// thinking 2.0
    /// 使用页面来划分数据是最不科学的方式，比如金币 在 顶部，背包，page1,page2怎么办？
    /// 当前页面需要用到货币、技能、卡牌、天赋
    ///
    /// unit MaxValue = 4294967295;
    /// </summary>
    
    public enum MessageVer2 : uint
    {
//        EMPTY = 0,     do not need empty placeholder
        BASE_CURRENCY       = 1,      //货币数据
        BASE_PAYPAL         = 2,      //支付数据
        CORE_SKILL          = 4,      //技能数据
        CORE_TALENTED       = 8,      //天赋数据
        CORE_ITEM           = 16,     //道具数据
        CORE_CARD           = 32,     //卡牌数据
        
        
    }
    
    public enum Message : uint
    {
//        EMPTY = 0,     do not need empty placeholder
        UI_TOP_HEAD = 1,      //顶部数据
        UI_BAG = 2,           //背包数据
        UI_CORE_AWARD = 4,    //战斗结算数据
        UI_CORE_FORM  = 8,    //战斗统计数据
    }

    public enum MessageFixed : byte
    {
        FIXED_ASSERT, // 资源
        FIXED_GLOBAL, // 全局
        FIXED_ACTIVITY, // 活动
        FIXED_PAYPAL, // 充值
        FIXED_STORE //商城
    }
}