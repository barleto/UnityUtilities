using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorsOf : MonoBehaviour {
    
    public Image[] images;
    public SpineSlot[] spinesSockets;

    public void All(Color to)
    {
        var color = to;
        foreach(var i in images){
            i.color = color;
        }

        var newColor = color;
        foreach(var s in spinesSockets){
            var slot = s.skeletonAnimation.skeleton.FindSlot(s.socketName);
            newColor.a = slot.A;
            slot.SetColor(newColor);
        }
    }

    [System.Serializable]
    public class SpineSlot
    {
        public SkeletonAnimation skeletonAnimation;
        public string socketName;
    }

}
