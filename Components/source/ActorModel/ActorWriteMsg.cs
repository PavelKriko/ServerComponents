namespace ActorSystem;

public class ActorWriteMsg : BaseActor{
    private string _text;

    private ActorWriteMsg(){}

    public void addReference(string name, BaseActor actor){}
    public ActorWriteMsg(string text){
        this._text = text;
    }

    public void onSend(Message mgs){
        Console.WriteLine(_text);
    }
}