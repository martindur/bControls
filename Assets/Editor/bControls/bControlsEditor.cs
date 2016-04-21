using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(GameObject))]
public class bControlsEditor : Editor
{
    private bGrab bgrab;
    private bScale bscale;
    private bRotate brotate;
    
    //Default object settings
    Vector3 defaultScale;
    Vector3 defaultPos;
    Quaternion defaultRot;
    
    Vector2 mousePos;
    Vector2 selXY;
    //private Vector3 curPos;
    private Vector2 mouseOffset;
    private Transform selectedObj;
    
    //Scale settings
    private Vector3 initialDist;
    private float initMag;
    
    public override void OnInspectorGUI()
    {

    }
    
    public enum state{none, grab, scale, rotate}
    public state myState;
    
    void Awake(){
        bgrab = new bGrab(this);
        bscale = new bScale(this);
        brotate = new bRotate(this);
        myState = state.none;
    }
    
    void OnSceneGUI()
    {   
        if (Selection.activeTransform == null) return;
        
        //Keep track on mouse position and selected object.
        mousePos = GetMousePos();
        selectedObj = Selection.activeTransform;
        CheckEvent(selectedObj);
        
        if (myState == state.grab)
        {
            bgrab.Grab(selectedObj);
            bgrab.CheckAxis(selectedObj);
        }
        else if(myState == state.scale){
            bscale.Scale(selectedObj);
            bscale.CheckAxis(selectedObj);
        }
        else if(myState == state.rotate){
            brotate.RotateFree(selectedObj);
        }

        if (Event.current.type == EventType.MouseDown)
        {
            if (Event.current.button == 0)
            {
                if (myState != state.none)
                {
                    Tools.hidden = false;
                }
            }
        }
        
    }

    void CheckEvent(Transform selectedObj)
    {
        
        if (Event.current.type == EventType.keyDown)
        {
            defaultPos = selectedObj.position;
            //defaultRot = selectedObj.rotation;
            defaultScale = selectedObj.localScale;
            if (Event.current.keyCode == (KeyCode.G))
            {
                bgrab.Init(selectedObj);
            }
            if (Event.current.keyCode == (KeyCode.S))
            {
                bscale.Init(selectedObj);
            }
            if(Event.current.keyCode == (KeyCode.R))
            {
                brotate.Init(selectedObj);
            }
        }
    }


    //Space transformations
    public Vector3 World2Screen(Vector3 world) { return Camera.current.WorldToScreenPoint(world); }
    public Vector3 Screen2World(Vector3 screen) { return Camera.current.ScreenToWorldPoint(screen); }
    public Vector3 Screen2View(Vector3 screen) { return Camera.current.ScreenToViewportPoint(screen); }
    public Vector3 View2World(Vector3 view) { return Camera.current.ViewportToWorldPoint(view); }

    //Positions
    public Vector2 GetMousePos() { return new Vector2(Event.current.mousePosition.x, Camera.current.pixelHeight - Event.current.mousePosition.y); }
    public float GetZDepth(Vector3 pos){return Vector3.Distance(Camera.current.transform.position, pos);}

}
