namespace ActorSystem;
public class ActorIntValidator : BaseActor{
    private BaseActor _ActorToSend;
    private BaseActor _ActorErrorHandler;

    public void addReference(string name, BaseActor actor){
        if(name == "ErrorHandler"){
            // Console.WriteLine("*** Был добавлен обработчик");
            this._ActorErrorHandler = actor;
        }
        if(name == "NextStep"){
            // Console.WriteLine("*** Был добавлен следующий шаг");
            this._ActorToSend = actor;
        }
    }

    public void onSend(Message msg){
        if(msg.Content is string){
            try{
                int number = int.Parse((string)msg.Content);
                Message numberMsg = new Message();
                numberMsg.Content = number;
                this._ActorToSend.onSend(numberMsg);
            }
            catch(FormatException e){
                Message errorMsg = new Message();
                errorMsg.Content = e.ToString();
                this._ActorErrorHandler.onSend(errorMsg);
            }

        }
        else{
            Message errorMsg = new Message();
            errorMsg.Content = $"{nameof(ActorIntValidator)} accepts only string type";
            this._ActorErrorHandler.onSend(errorMsg);
        }
    }
}