using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenuAttribute(fileName = "Symbol Data", menuName = "Assets/Symbol Data")]
public class SymbolData : ScriptableObject
{
    
   

    [SerializeField]
    private  string symbolValue;

    public string SymbolValue
    {
        get{ return symbolValue;}
    }

    
    [SerializeField]
    private Sprite symbolSprite;
    public Sprite SymbolSprite
    {
        get{return symbolSprite;}
    }


    [SerializeField]
    private SymbolType symbolType;
    public SymbolType SymbolType
    {
        get {return symbolType;}
    }

  
}



public enum SymbolType
{
    Letter,
    Number
}
