using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAddObject{
    public void WordFinished(string contohObject){
        //ntr di sini add yg diminta object ke inventory(list)
        //ato bikin code baru aja ? hmm, keknya baru aja, ya ini nanti interfacenya dblg use void function dr code siapa
    }
}

public class AddObject{
    public void WordFinished(string contohObject){
        Debug.Log(contohObject);
    }
}
public class AddObjects{
    public void WordFinished(string contohObject){
        Debug.Log(contohObject+"hiyaaaaaaaaaaa");
    }
}
public class FinishWordDoFunction : MonoBehaviour
{
    public enum FunctionType{
        AddObject, Adds
    }
    public FunctionType type;

    private AddObject add;
    private AddObjects adds;


    private void Start() {
        add = new AddObject();
        adds = new AddObjects();
    }
    public void WordFinisheds(){
        if(type == FunctionType.AddObject){
            add.WordFinished("blaba");
        }
        if(type == FunctionType.Adds){
            adds.WordFinished("blabass");
        }
    }
}
