using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CardAnimator : MonoBehaviour
{
    public Image image;
    private Sequence sequence;

  
    void Awake()
    {
        DOTween.Init();
        sequence = DOTween.Sequence();
    }
    
    void Start()
    {
        Appear();
    }

    public void Appear()
    {
        //sequence.AppendInterval(3);
        // sequence.Append(image.transform.DOScale(new Vector3(.2f,.2f,.2f),0));
        // sequence.Append(image.transform.DOScale(new Vector3(1f,1f,1f),0.4f));
        sequence.Append(image.transform.DOPunchScale(new Vector3(.5f,.5f,.5f),.5f,15,1f));


    }

    public void Disappear()
    {
            sequence.Append(image.transform.DOScale(new Vector3(0f,0f,0f),0.4f));
    }
    
    public void AnimTrue()
    {
        sequence.Append(image.transform.DOPunchPosition(new Vector3(0f,10f,0f),1f,10,0,true));
    }

    public void AnimFalse()
    {
        sequence.Append(image.transform.DOShakeRotation(1f,new Vector3(0f,0f,10f),15,45,true));
        
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
