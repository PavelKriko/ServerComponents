namespace Components.Lib;

public class IoCBase : IContainer
{
    private Dictionary<TypeNameKey, Func<Dictionary<string, object>, object>> _storeDependence;

    public IoCBase()
    {
        _storeDependence = new();
    }
    public void Register<TFrom, TTo>(string? instanceName = null) where TTo : TFrom
    {
        Register(typeof(TFrom), typeof(TTo), instanceName);
    }

    public void Register(Type from, Type to, string? instanceName = null)
    {
        if (to == null)
            throw new ArgumentNullException("to");

        if (!from.IsAssignableFrom(to))
        {
            string errorMessage = string.Format("Error trying to register the instance: '{0}' is not assignable from '{1}'",
                from.FullName, to.FullName);

            throw new InvalidOperationException(errorMessage);
        }

        Func<Dictionary<string, object>, object> createInstanceDelegate = (kwargs) =>
        {
            if (kwargs == null) kwargs = new Dictionary<string, object>();
            //Список имен переменных и их типов
            HashSet<(string, Type)> kwargsTypeNameSet = kwargs.Select(kv => (kv.Key, kv.Value.GetType())).ToHashSet();
            System.Console.WriteLine(kwargsTypeNameSet);
            //С помощью рефлексии перебираем конструкторы
            foreach (var constructor in to.GetConstructors())
            {
                HashSet<(string, Type)> constructorTypeNameSet = constructor.GetParameters().Select(kv => (kv.Name, kv.ParameterType)).ToHashSet();
                //После найденого конструктора нужно выставить аргументы в правильном порядке
                if (kwargsTypeNameSet.SetEquals(constructorTypeNameSet))
                {
                    var orderArgs = constructor.GetParameters().ToDictionary(item => item.Name, item => item.Position)
                    .OrderBy(kv => kv.Value).Select(kv => kwargs[kv.Key]).ToArray();
                    return constructor.Invoke(orderArgs);
                }

            }

            throw new ArgumentException($"Can't find constructor for {to} with init args: {kwargs}");

        };
        Register(from, createInstanceDelegate, instanceName);
    }

    public void Register(Type type, Func<Dictionary<string, object>, object> createInstanceDelegate, string? instanceName = null)
    {
        if (type == null)
            throw new ArgumentNullException("type");

        if (createInstanceDelegate == null)
            throw new ArgumentNullException("createInstanceDelegate");


        var key = new TypeNameKey(type, instanceName);

        if (_storeDependence.ContainsKey(key))
        {
            const string errorMessageFormat = "The requested mapping already exists - {0}";
            throw new InvalidOperationException(string.Format(errorMessageFormat, key.ToString()));
        }

        _storeDependence.Add(key, createInstanceDelegate);
    }

    public void Register<T>(Func<Dictionary<string, object>, T> createInstance, string? instanceName = null)
    {
        if (createInstance == null)
        {
            throw new ArgumentNullException("delegate createInstance was null");
        }
        Func<Dictionary<string, object>, object> delegateInstance = createInstance as Func<Dictionary<string, object>, object>;
        Register(typeof(T), delegateInstance, instanceName);
    }

    public bool IsRegistered<T>(string? instanceName = null)
    {
        return IsRegistered(typeof(T), instanceName);
    }

    public bool IsRegistered(Type type, string? instanceName = null)
    {
        var key = new TypeNameKey(type, instanceName);
        return _storeDependence.ContainsKey(key);
    }

    public T Resolve<T>(Dictionary<string, object>? kwargs = null, string? instanceName = null)
    {
        var key = new TypeNameKey(typeof(T), instanceName);
        Func<Dictionary<string, object>, object> funcCreateInstance;
        if (_storeDependence.TryGetValue(key, out funcCreateInstance))
        {
            var instance = funcCreateInstance(kwargs);
            return (T)instance;
        }
        const string errorMessageFormat = "Could not find mapping for type '{0}'";
        throw new InvalidOperationException(string.Format(errorMessageFormat, typeof(T).FullName));
    }
}


