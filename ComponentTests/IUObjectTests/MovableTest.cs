namespace ComponentTests;


public class MovableTestClass
{
    [Fact]
    public void initTest()
    {
        IUobject obj = new UObject("obj№1");
        IMovable<int[], int[]> moveObj = new MovableAdapter2D(obj);
        Assert.Equal(moveObj.GetPosition(), new int[] { 0, 0 });
        Assert.Equal(moveObj.GetVelocity(), new int[] { 0, 0 });
    }

    [Fact]
    public void MoveTest()
    {
        int[] pos = new int[] { 12, 5 };
        int[] vel = new int[] { -7, 3 };

        IUobject obj = new UObject("obj№1");
        IMovable<int[], int[]> moveObj = new MovableAdapter2D(obj);
        moveObj.SetPosition(pos);
        moveObj.SetVelocity(vel);

        //newPos = oldPos + vel
        moveObj.SetPosition(moveObj.GetPosition().Zip(moveObj.GetVelocity(), (a, b) => a + b).ToArray());
        Assert.Equal(moveObj.GetPosition(), new int[] { 5, 8 });
    }
}