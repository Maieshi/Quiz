using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    SymbolData CurrentAnswer;

    [SerializeField]
    Spawner spawner;

    [SerializeField]
    Text text;

    [SerializeField]
    GameObject button;
    
    void Start()
    {
       
        spawner.StartRoutine("tiles");
    
    }

    public void SetAnswer(SymbolData answer)
    {
        CurrentAnswer = answer;

        text.text = "Find "+answer.SymbolValue;
    }

    

    public bool GetAnswer(SymbolData symbolData)
    {
        if(CurrentAnswer.SymbolValue == symbolData.SymbolValue)
            {
                Debug.Log(spawner.GetCardsCount());
                if(spawner.GetCardsCount()>=9)
                {
                    spawner.StartRoutine("field");
                    button.SetActive(true);
                }
                else spawner.StartRoutine("answer");
                
                return true;
            }
            else{
                 return false;
            }
    }
    
    
}
