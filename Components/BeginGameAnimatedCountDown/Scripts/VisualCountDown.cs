using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisualCountDown : MonoBehaviour {

    public float time = 3;
    public iTween.EaseType easing = iTween.EaseType.linear;
    public GameObject[] countdownObjects;
    public UnityEvent onCompleteCountdownEvent;

    private int currentItem = 0;
    private float itemTime = 0;

    private void Awake()
    {
        foreach(var go in countdownObjects){
            go.SetActive(false);
        }
    }

    private void OnEnable()
    {
        currentItem = 0;
        itemTime = time / ((float)countdownObjects.Length);
        countdownObjects[currentItem].SetActive(true);
        countdownObjects[currentItem].GrowScaleAnimated(itemTime, easing, OnNextItem);
    }

    void OnCompleteCountdown(){
        onCompleteCountdownEvent.Invoke();
    }

    public void OnNextItem(){
        countdownObjects[currentItem].SetActive(false);
        currentItem++;
        if(currentItem < countdownObjects.Length){
            countdownObjects[currentItem].SetActive(true);
            gameObject.GrowScaleAnimated(itemTime, easing, OnNextItem);
        }else{
            currentItem = 0;
            OnCompleteCountdown();
            gameObject.SetActive(false);
        }
    }

}
