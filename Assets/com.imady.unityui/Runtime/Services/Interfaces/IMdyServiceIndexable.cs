
namespace imady.NebuUI
{
    /// <summary>
    /// �޶��˸���Service�漰��������ģ��TClass��������ʽ
    /// </summary>
    /// <typeparam name="T">T���������ͣ�������Int64��GUID����string</typeparam>
    public interface IMdyServiceIndexable<T>
    {
        T objectIndex { get; set; }
    }
}
