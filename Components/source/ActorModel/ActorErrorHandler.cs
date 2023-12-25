namespace ActorSystem;
public class ActorErrorHandler : BaseActor{
    public void onSend(Message msg){
        Console.WriteLine($"Было поймано исключение {msg.Content}");
    }

    public void addReference(string name, BaseActor actor){}
}