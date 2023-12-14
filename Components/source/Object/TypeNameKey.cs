namespace Components.Lib;

public class TypeNameKey
{
    //Тип свойства
    public Type _type { get; protected set; }
    //Имя свойства(опционально)
    public string? _name { get; protected set; }

    public TypeNameKey(Type type, string? name = null)
    {
        if (type != null)
        {
            _type = type;
            _name = name;
        }
        else
        {
            throw new ArgumentNullException("type must be non-nullable");
        }

    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        TypeNameKey compareTo = obj as TypeNameKey;
        if (ReferenceEquals(this, compareTo))
        {
            return true;
        }
        if (compareTo == null)
        {
            return false;
        }

        return _type.Equals(compareTo._type) && string.Equals(_name, compareTo._name);
    }
    //Необходимо переопределить, т.к. методы с коллекций вычисляют его 
    public override int GetHashCode()
    {
        return HashCode.Combine(_type, _name);
    }

    public override string ToString()
    {
        const string format = "type:{0} , name:{1}, hashCode:{2}";
        return string.Format(format, this._type,
        this._name ?? "null",
        this.GetHashCode());
    }
}