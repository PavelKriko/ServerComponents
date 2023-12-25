namespace ActorSystem;

using System;
using System.Linq;
using System.Xml.Linq;

class ActorSystem{
    private Dictionary<string, BaseActor> dict;

    public ActorSystem(){
        dict = new Dictionary<string, BaseActor>();
    }

    public void loadFromXml(string pathToXml){
        XDocument doc = XDocument.Load(pathToXml);
        foreach(var node in doc.Descendants("BusinessProcess").Elements()){
            switch(node.Name.LocalName){
                case "Validator":
                    dict[node.Element("Name").Value] = new ActorIntValidator();
                break;

                case "Comparison":
                    dict[node.Element("Name").Value] = new ActorIntComparison(int.Parse(node.Element("Value").Value));
                break;

                case "MsgWriter":
                    dict[node.Element("Name").Value] = new ActorWriteMsg(node.Element("Text").Value);
                break;

                case "Handler":
                    dict[node.Element("Name").Value] = new ActorErrorHandler();
                break;
            }
        }

        foreach(var node in doc.Descendants("BusinessProcess").Elements()){
             switch(node.Name.LocalName){
                case "Validator":
                    dict[node.Element("Name").Value].addReference("ErrorHandler", dict[node.Element("ErrorHandler").Value]);
  
                    dict[node.Element("Name").Value].addReference("NextStep", dict[node.Element("NextStep").Value]);
                break;

                case "Comparison":
                    dict[node.Element("Name").Value].addReference("IfTrue", dict[node.Element("IfTrue").Value]);
                    dict[node.Element("Name").Value].addReference("IfFalse", dict[node.Element("IfFalse").Value]);
                break;

                case "MsgWriter":
                break;

                case "Handler":
                break;
            }
        }
    }
}