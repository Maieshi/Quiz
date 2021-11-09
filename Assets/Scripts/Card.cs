using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Card : MonoBehaviour,IPointerDownHandler
{

    
    SymbolData  symbolData;

    
    GameController controller;
    

    [SerializeField]
     CardAnimator cardAnimator;

    [HideInInspector]
    public bool isClickable =false;

    public void SetData(SymbolData symbolData, GameController controller)
    {
        cardAnimator.SetImage(symbolData.SymbolSprite);
        this.symbolData=symbolData;
        this.controller=controller;
    }

    


    public void OnPointerDown(PointerEventData eventData)
    {
        
        if(isClickable)
        {
            freezeClick = StartCoroutine("FreezeClick");
            if(controller.GetAnswer(symbolData))
            {
                cardAnimator.AnimTrue();
                
            }
            else
            {
                cardAnimator.AnimFalse();
            }
        }
        
    }
    
    Coroutine freezeClick;
    IEnumerator FreezeClick()
    {
        isClickable =false;
        yield return new WaitForSeconds(1.1f);
        isClickable =true;
    }

    public void DisableClick()
    {
        isClickable= false;
    }

    public void EnableClick()
    {
        isClickable=true;
    }

    public void AnimDisappear()
    {
        cardAnimator.Disappear();
    }

}
