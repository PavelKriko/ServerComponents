namespace ActorSystem;

public interface BaseActor{
    public void onSend(Message msg);
    public void addReference(string name, BaseActor actor);
}


public class Message{
    public string? Sender { get; set; }
    public string? Receiver { get; set; }
    public object? Content { get; set; }
}