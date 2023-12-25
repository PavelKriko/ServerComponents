
namespace Components.Lib;

public interface IContainer
{
    //TypeFrom - тип интерфейса, TypeTo - тип класса который вернется
    //Суть IoC сводится к тому, чтобы получить инстанс(экземпляр) некоторого объекта
    //Способы получения этого инстанса лежат в IoContainer-е
    //createInstanceDelegate - это делегат который по входным аргументам создает инстанс
    public void Register(Type from, Type to, string? instanceName = null);
    public void Register<TFrom, TTo>(string? instanceName = null) where TTo : TFrom;
    public void Register(Type type, Func<Dictionary<string, object>, object> createInstanceDelegate, string? instanceName = null);
    public void Register<T>(Func<Dictionary<string, object>, T> createInstanceDelegate, string? instanceName = null);
    public bool IsRegistered<T>(string? instanceName = null);
    public bool IsRegistered(Type type, string? instanceName = null);

    

    T Resolve<T>(Dictionary<string, object>? kwargs = null, string? instanceName = null);
    
}
