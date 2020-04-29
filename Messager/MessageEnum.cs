namespace Bnyx.Messager
{
    /// <summary>
    /// custom message enum for poolprovider
    /// exp: 我想把消息推送给活动页面和背包
    /// unit MaxValue = 4294967295;
    /// </summary>
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