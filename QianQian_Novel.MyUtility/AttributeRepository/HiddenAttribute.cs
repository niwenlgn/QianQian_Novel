namespace QianQian_Novel.MyUtility.AttributeRepository
{
    /// <summary>
    /// swagger拦截器
    /// 过滤所标记的类或接口,使其不在swagger中显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HiddenAttribute: Attribute
    {
    }
}
