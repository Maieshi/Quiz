using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;


public class LevelLoader : MonoBehaviour
{
    
    public string levelName;

    [SerializeField]
    CanvasGroup canvasGroup;

    private Sequence sequence;

    void Awake()
    {
        DOTween.Init();
        sequence = DOTween.Sequence();
    }
    

    void Start()
    {
        
        //sequence.Append(canvasGroup.DOFade(0.5f,1));
    }

    public void Load()
    {
        gameObject.GetComponent<Button>().interactable = false;

        StartCoroutine("AsyncLoad");
    }



    IEnumerator AsyncLoad()
    {


        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync (levelName);
        asyncOperation.allowSceneActivation = false;
        sequence.Append(canvasGroup.DOFade(1f,1));
        yield return new WaitForSeconds(1f);
        asyncOperation.allowSceneActivation = true;
        

    }
}
