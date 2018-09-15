using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SetSpineSlotColor : MonoBehaviour {

	public SkeletonAnimation skeletonAnimation;
    public string socketName;
    public Color color = Color.white;

	// Use this for initialization
	void Start () {
        skeletonAnimation.skeleton.FindSlot(socketName).SetColor(color);
	}
}
