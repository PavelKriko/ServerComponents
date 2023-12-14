namespace Components.Lib;
public class UObject : IUobject
{

    /*
    Идея заключается в том, 
    что свойство может быть уникальным классом 
    или же не уникальным(число, вектор и т.д.),
    но с различными названиями("позиция", "вектор скорости"),
    поэтому хочется иметь доступ к свойству и по классу свойства 
    и по названию, также важное условие, если у свойства есть название
    оно должно быть уникальным
    */
    private Dictionary<TypeNameKey, object> _propertyStore;

    public string _ID { get; protected set; }

    public UObject(string ID)
    {
        if (ID == null)
        {
            throw new ArgumentNullException("ID must be not Null");
        }
        _ID = ID;
        _propertyStore = new Dictionary<TypeNameKey, object>();
    }
    private UObject() { }
    public void SetProperty<TProp>(TProp newPropertyValue, string? nameProperty = null)
    {
        if (newPropertyValue != null)
        {
            SetProperty(typeof(TProp), newPropertyValue, nameProperty);
        }
        else
        {
            throw new ArgumentNullException("Null value was given");
        }

    }

    public void SetProperty(Type prop, object newPropertyValue, string? nameProperty = null)
    {
        var key = new TypeNameKey(prop, nameProperty);
        if (_propertyStore.ContainsKey(key))
        {
            _propertyStore[key] = newPropertyValue;
        }
        else
        {
            throw new KeyNotFoundException(key.ToString());
        }
    }

    public TProp GetProperty<TProp>(string? nameProperty = null)
    {
        var key = new TypeNameKey(typeof(TProp), nameProperty);
        if (_propertyStore.ContainsKey(key))
        {
            return (TProp)_propertyStore[key];
        }
        else
        {
            throw new KeyNotFoundException(key.ToString());
        }

    }

    public (Type, object) GetProperty(string nameProperty)
    {
        if (nameProperty == null)
        {
            throw new ArgumentNullException(nameof(nameProperty));
        }
        foreach (var key in _propertyStore.Keys)
        {
            if (key._name == nameProperty)
            {
                return (key._type, _propertyStore[key]);
            }
        }
        throw new KeyNotFoundException(nameProperty);
    }

    public void AddProperty<TProp>(TProp newProperty, string? nameProperty = null)
    {
        AddProperty(typeof(TProp), newProperty, nameProperty);
    }

    public void AddProperty(Type prop, object newProperty, string? nameProperty = null)
    {
        var key = new TypeNameKey(prop, nameProperty);
        if (!_propertyStore.ContainsKey(key))
        {
            _propertyStore.Add(key, newProperty);
        }
        else
        {
            throw new ArgumentException(key.ToString());
        }
    }

    public void DropProperty<TProp>(string? nameProperty = null)
    {
        var key = new TypeNameKey(typeof(TProp), nameProperty);
        if (_propertyStore.ContainsKey(key))
        {
            _propertyStore.Remove(key);
        }
        else
        {
            throw new KeyNotFoundException(key.ToString());
        }
    }

    public void DropProperty(string nameProperty)
    {
        if (nameProperty == null)
        {
            throw new ArgumentNullException(nameof(nameProperty));
        }
        else
        {
            foreach (var key in _propertyStore.Keys)
            {
                if (key._name == nameProperty)
                {
                    _propertyStore.Remove(key);
                    return;
                }
                throw new KeyNotFoundException(nameProperty);
            }
        }
    }

    public bool HaveProperty<TProp>(string? nameProperty = null)
    {
        return _propertyStore.ContainsKey(new TypeNameKey(typeof(TProp), nameProperty));
    }

    public bool HaveProperty(string nameProperty)
    {
        if (nameProperty == null)
        {
            throw new ArgumentNullException(nameof(nameProperty));
        }
        else
        {
            foreach (var key in _propertyStore.Keys)
            {
                if (key._name == nameProperty)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public string GetID()
    {
        return _ID;
    }
}


