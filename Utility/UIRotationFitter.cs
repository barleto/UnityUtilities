using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(RawImage))]
public class UIRotationFitter : MonoBehaviour {

    public RawImage rawImage;

    private RectTransform rectTransform;
    private RectTransform parent;
    private float aspectwh;

    private void Reset()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        /*aspectwh = (float)rawImage.texture.width / (float)rawImage.texture.height;
        rectTransform = GetComponent<RectTransform>();
        if(rectTransform.parent is RectTransform){
            parent = rectTransform.parent as RectTransform;
            if (parent.rect.height > parent.rect.width)
            {
                isParentPortrait = true;
            }else{
                isParentPortrait = false;
            }
            if(rawImage.texture.height > rawImage.texture.width){
                isImagePortrait = true;
            }else{
                isImagePortrait = false;
            }
        }

        Debug.Log(isImagePortrait+"/"+isParentPortrait);
        if(isImagePortrait && isParentPortrait){
            StretchToFitInParentWidth();
        }else if(!isImagePortrait && isParentPortrait){
            StretchToFitInParentHeight();
        }else if(isImagePortrait && !isParentPortrait){
            StretchToFitInParentWidth();
        }else if(!isImagePortrait && !isParentPortrait){
            StretchToFitInParentHeight();
        }*/

        if (rectTransform.parent is RectTransform)
        {
            parent = rectTransform.parent as RectTransform;
            var absSin = Mathf.Abs(Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad));
            aspectwh = Mathf.Lerp(
                (float)rawImage.texture.width / (float)rawImage.texture.height, 
                (float)rawImage.texture.height / (float)rawImage.texture.width, 
                absSin);
            var sizex = parent.rect.width;
            var sizey = parent.rect.width / aspectwh;


            if(sizey < parent.rect.height){
                sizex = parent.rect.height * aspectwh;
                sizey = parent.rect.height;
            }

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(
                sizex,
                sizey,
                absSin));
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(
                sizey,
                sizex,
                absSin));
        }
    }

    void StretchToFitInParentWidth()
    {
        var sizex = Mathf.Lerp(parent.rect.width, parent.rect.height, Mathf.Abs(Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad)));
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizex);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizex / aspectwh);
    }

    void StretchToFitInParentHeight()
    {
        var sizey = Mathf.Lerp(parent.rect.height, parent.rect.width, Mathf.Abs(Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad)));
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizey * aspectwh);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizey);
    }

 }