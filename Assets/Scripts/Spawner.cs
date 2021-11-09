using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject tilePrefab;

    public Transform Grid;

    [SerializeField]
    GameController controller;

    List<SymbolData> answerData = new List<SymbolData>();

    [SerializeField]
    List< SymbolData>  symbols;

    private SymbolData [,] symbolsOnGrid = new SymbolData[3,3];

    int cardType;

    

    void Start()
    {
       //StartCoroutine("SpawnTiles") ;
    }



    void GenerateArray()
    {
       
        int value = Random.Range(0,2);//выбор типа карты

        SymbolData currentAnswer;

       //создание наббора всех карт соответсвующих типу

        List<SymbolData> tileList= new List<SymbolData>();

        tileList= symbols.FindAll(x=> (int)x.SymbolType==value);//список всех возможных карт нужной категории

        currentAnswer =  ChooseAnswer(tileList);
        
        controller.SetAnswer(currentAnswer);
        

        bool arrayContainsAnswer = false;

        for(int i=0;i<3;i++)
        {

            bool isOnNullString = (symbolsOnGrid[i,0]== null)?true:false;

            //Debug.Log(symbolsOnGrid[i,0]);

            for(int j=0;j<3;j++)
            {
                int rnd = Random.Range(0,tileList.Count-1);

                SymbolData temp = tileList[rnd];
                //Debug.Log(rnd +" // "+temp.SymbolValue+" // "+tileList.Count);
                if(temp.SymbolValue==currentAnswer.SymbolValue)
                {
                    arrayContainsAnswer = true;
                    answerData.Add(temp);
                }
                else if(!arrayContainsAnswer&&j==2&isOnNullString)
                {
                    arrayContainsAnswer = true;
                    temp = currentAnswer;
                    answerData.Add(temp);
                }
                symbolsOnGrid[i,j] = temp;
                tileList.Remove(temp);
                
            }
            if(arrayContainsAnswer&&isOnNullString)break;
        }

    }
    List<Card> cards = new List<Card>();
    IEnumerator SpawnTiles()
    {
        GenerateArray();

        yield return new WaitForSeconds(.5f);

        Card tileCard =  tilePrefab.GetComponent<Card>();

        CardAnimator tileAnimator = tilePrefab.GetComponent<CardAnimator>();

        Spawner spawner = gameObject.GetComponent<Spawner>();

        

        for(int i=0;i<3;i++)
        {
            if(symbolsOnGrid[i,0]== null) break;
            for(int j=0;j<3;j++)
            {
                GameObject spawnedCard = Instantiate(tilePrefab,Grid);
                tileCard = spawnedCard.GetComponent<Card>();
                tileCard.SetData(symbolsOnGrid[i,j],controller);
                tileCard.isClickable = false;
                spawnedCard.GetComponent<CardAnimator>().SetImage(symbolsOnGrid[i,j].SymbolSprite);
                
                cards.Add(tileCard);

                yield return new WaitForSeconds(.7f);
            }
            
        }

        foreach (var c in cards)
        {
            c.isClickable = true;
        }
        
    }

    SymbolData ChooseAnswer(List<SymbolData> Bundle)
    {
        //Debug.Log(answerData.Count);
        List <SymbolData> symbolBundle = Bundle;

        symbolBundle.RemoveAll(x=>(symbolBundle.Contains(x)==true&&answerData.Contains(x)==true));//сключение всех карт из набора которые содержатся в списке прошлых ответов

        //Debug.Log(symbols.Count);

        answerData.Add(symbolBundle[Random.Range(0,symbolBundle.Count)]);//выбор случайного ответа

        return answerData[GetAnswerIndex()];
    }

    public bool GetAnswer(SymbolData symbolData)
    {
        
        if(answerData[GetAnswerIndex()].SymbolValue == symbolData.SymbolValue)
            {
                //добавить вызов метода появлния частиц 
                 StartCoroutine ("OnTrueAnswer");
                return true;
            }
            else{
                 return false;
            }
    }

    int GetAnswerIndex()
    {
        if(answerData.Count==0) return 0;
        else return answerData.Count-1;
        
    }

    public IEnumerator OnTrueAnswer()
    {
        StartCoroutine ("ClearField");
        Debug.Log(GetCardsCount());
        if(GetCardsCount()<9)
        {
            int cardsCount = GetCardsCount();
            
            
            
            yield return new WaitForSeconds(cardsCount *.6f);
           
            
            
            StartCoroutine("SpawnTiles");
        }
    }

    IEnumerator ClearField()
    {
        cards.Reverse();
        DisableCards();

        yield return new WaitForSeconds(.5f);
        foreach(var c in cards.ToArray())
        {
            
            c.AnimDisappear();
            yield return new WaitForSeconds(.5f);
            Destroy(c.gameObject);
            cards.Remove(c);
        }
         
    }

    void DisableCards()
    {
        foreach (var c in cards)
        {
            c.DisableClick();
        }
    }

   

    public int GetCardsCount()
    {
        return cards.Count;
    }
    void EndGame()
    {

    }

    //  public void StartRoutiness(string str);
    // {
    //     if(str == "tiles")StartCoroutine("SpawnTiles");
    //     
    // }

    public void StartRoutine(string str)
    {
        if(str == "tiles")StartCoroutine("SpawnTiles");
        else if(str =="answer")StartCoroutine("OnTrueAnswer");
        else if(str == "field")StartCoroutine("ClearField");
    }
}
