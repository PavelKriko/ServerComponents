namespace Components.Lib;
public interface IMovable<TPos, TVel>
{
    TPos GetPosition();
    void SetPosition(TPos newPosition);

    TVel GetVelocity();
    void SetVelocity(TVel newVelocity);
}
