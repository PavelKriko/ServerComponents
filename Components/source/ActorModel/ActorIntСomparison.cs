namespace ActorSystem;

public class ActorIntComparison : BaseActor{

    private int _value;
    private BaseActor _trueActor;
    private BaseActor _falseActor;

    private ActorIntComparison(){}

    public void addReference(string name, BaseActor actor){
        if(name == "IfTrue"){
            // Console.WriteLine("** Был актор истины");
            this._trueActor = actor;
        }
        if(name == "IfFalse"){
            // Console.WriteLine("** Был актор ложности");
            this._falseActor = actor;
        }
    }
    public ActorIntComparison(int value){
        _value = value;
    }
    
    public void onSend(Message mgs){
        if((int)mgs.Content > this._value){
            _trueActor.onSend(mgs);
        }
        else{
            _falseActor.onSend(mgs);
        }

    }
}