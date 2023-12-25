namespace ComponentTests;
public class IoCTestClass
{
    private class NonRegisteredClass{}
    [Fact]
    void IsRegisterTest(){
        IContainer IoC = new IoCBase();
        IoC.Register<IMovable<int[],int[]>, MovableAdapter2D>();
        bool IsRegistered = IoC.IsRegistered<IMovable<int[],int[]>>();
        Assert.True(IsRegistered);
        IsRegistered = IoC.IsRegistered<NonRegisteredClass>();
        Assert.False(IsRegistered);

    }

    [Fact]
    void ResolveTestInstanceCanNotFindConstructor(){
        IContainer IoC = new IoCBase();
        IoC.Register<IMovable<int[],int[]>, MovableAdapter2D>();

        IUobject obj = new UObject("Object№1");
        //Конструктор MovableAdapter2D(IUobject obj) единственный
        //Попытка использовать конструктор по умолчанию
        IMovable<int[],int[]> movObj;
        Assert.Throws<ArgumentException>(()=>{movObj = IoC.Resolve<IMovable<int[],int[]>>();});
    }

    // [Fact]
    // void ResolveTestInstanceWithExistConstructor(){
    //     IContainer IoC = new IoCBase();
    //     IoC.Register<IMovable<int[],int[]>, MovableAdapter2D>();
    //     IUobject obj = new UObject("Object№1");
    //     IMovable<int[],int[]> movObj = IoC.Resolve<IMovable<int[],int[]>>(new Dictionary<string, object>{{"obj", obj}});
    //     Assert.Equal(movObj.GetPosition(), new int[] { 0, 0 });
    // }
}
