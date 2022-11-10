namespace QianQian_Novel.MyUtility.AttributeRepository
{
    /// <summary>
    /// swagger属性拦截器
    /// 过滤所标记的属性,使其不在swagger中显示
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HiddenFieldAttribute: Attribute
    {

    }
}
