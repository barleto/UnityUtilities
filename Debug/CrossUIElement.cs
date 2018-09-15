using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossUIElement : MonoBehaviour {
    public RectTransform rectTransform;
    public LineRenderer lineRenderer;
	
	// Update is called once per frame
	void Update () {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var pos = new Vector3[4]{corners[0], corners[2], corners[3], corners[1]};
        lineRenderer.SetPositions(pos);
	}
}
