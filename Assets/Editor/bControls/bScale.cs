using UnityEngine;
using UnityEditor;
using System.Collections;

public class bScale : Editor {

    // public void InitScale()
    // {
    //     initialDist = mousePos - mouseOffset;
    //     initMag = initialDist.magnitude * 0.001f;
    //     Debug.Log("Ran");
    // }
    // public void Scale(Transform selectedObj)
    // {
    //     Vector3 curDist = mousePos - mouseOffset;
    //     Ray ray = Camera.current.ScreenPointToRay(new Vector3(curDist.x, curDist.y, zDepth));
    //     curDist = ray.GetPoint(zDepth);
    //     float curMag = curDist.magnitude * 0.001f;
    //     selectedObj.localScale *= curMag / initMag;
    //     Debug.Log(mousePos);
    //     Handles.color = Color.black;
    //     Handles.DrawLine(selectedObj.position, curDist);
    // }
}
