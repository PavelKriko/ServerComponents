
namespace Components.Lib;
public interface IUobject
{
    public void SetProperty<TProp>(TProp newPropertyValue, string? nameProperty = null);
    public void SetProperty(Type prop, object newPropertyValue, string? nameProperty = null);

    public TProp GetProperty<TProp>(string? nameProperty = null);
    public (Type, object) GetProperty(string nameProperty);

    public void AddProperty<TProp>(TProp newProperty, string? nameProperty = null);
    public void AddProperty(Type prop, object newProperty, string? nameProperty = null);
    public void DropProperty<TProp>(string? nameProperty = null);
    public void DropProperty(string nameProperty);
    public bool HaveProperty<TProp>(string? nameProperty = null);
    public bool HaveProperty(string nameProperty);

    public string GetID();

}