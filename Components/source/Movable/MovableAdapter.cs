namespace Components.Lib;
public class MovableAdapter2D : IMovable<int[], int[]>
{
    //ссылка на объект
    private IUobject _obj;

    private MovableAdapter2D() { }
    private class CDT { }

    //
    public MovableAdapter2D(IUobject obj)
    {
        _obj = obj;
        if (!_obj.HaveProperty<int[]>("Velocity"))
        {
            _obj.AddProperty<int[]>(new int[2] { 0, 0 }, "Velocity");
        }
        if (!_obj.HaveProperty<int[]>("Position"))
        {
            _obj.AddProperty<int[]>(new int[] { 0, 0 }, "Position");
            _obj.AddProperty<CDT>(new CDT());

        }
    }

    public int[] GetPosition()
    {
        return _obj.GetProperty<int[]>("Position");
    }

    public void SetPosition(int[] newPosition)
    {
        _obj.SetProperty<int[]>(newPosition, "Position");
    }

    public int[] GetVelocity()
    {
        return _obj.GetProperty<int[]>("Velocity");
    }

    public void SetVelocity(int[] newVelocity)
    {
        _obj.SetProperty<int[]>(newVelocity, "Velocity");
    }
}