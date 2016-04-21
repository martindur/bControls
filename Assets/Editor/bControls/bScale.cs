using UnityEngine;
using UnityEditor;
using System.Collections;

public class bScale : Editor {

    bControlsEditor controls;
    AxisControl axis;
    float initMag;
    int axisID;
    bool axisOn;
    Vector2 objPosScreen;
    Vector3 initScale;

    public bScale(bControlsEditor controls){
        this.controls = controls;
    }
    
    void Awake(){
        axis = new AxisControl();
    }
    
    
    public void Init(Transform selectedObj)
    {
        controls.myState = bControlsEditor.state.scale;
        objPosScreen = controls.World2Screen(selectedObj.position);
        Vector2 initialDist = controls.GetMousePos() - objPosScreen;
        initMag = initialDist.magnitude * 0.01f;
        initScale = selectedObj.localScale;
        axisOn = false;
        Tools.hidden = true;
    }
    
    public void Scale(Transform selectedObj){
        if(axisOn){ScaleAxis(selectedObj);}
        else if(!axisOn){ScaleFree(selectedObj);}
    }
    public void ScaleFree(Transform selectedObj)
    {
        Vector3 curDist = controls.GetMousePos() - objPosScreen;
        //float zDepth = controls.GetZDepth(curDist);
        //Ray ray = Camera.current.ScreenPointToRay(new Vector3(curDist.x, curDist.y, zDepth));
        //curDist = ray.GetPoint(zDepth);
        float curMag = curDist.magnitude * 0.01f;
        selectedObj.localScale = initScale * (1f+(curMag - initMag));
        Handles.color = Color.black;
        Handles.DrawLine(selectedObj.position, controls.Screen2World(controls.GetMousePos()));
    }
    
        public void ScaleAxis(Transform selectedObj)
    {
        Vector3 curDist = controls.GetMousePos() - objPosScreen;
        float curMag = curDist.magnitude * 0.01f;
        axis.DrawAxis(selectedObj.position, axisID);
        switch (axisID)
        {
            case 0:
                float x = initScale.x * (1f+(curMag - initMag));
                selectedObj.localScale = new Vector3(x, selectedObj.localScale.y, selectedObj.localScale.z);
                break;
            case 1:
                float y = initScale.x * (1f+(curMag - initMag));
                selectedObj.localScale = new Vector3(selectedObj.localScale.x,y, selectedObj.localScale.z);                break;
            case 2:
                float z = initScale.x * (1f+(curMag - initMag));
                selectedObj.localScale = new Vector3(selectedObj.localScale.x, selectedObj.localScale.y, z);                break;
            default:
                break;
        }
    }
    
        public void CheckAxis(Transform selectedObj){
        if(Event.current.keyCode == (KeyCode.X))
            {
                axisID = 0; 
                axisOn = true;
                selectedObj.localScale = initScale;
            }
        else if(Event.current.keyCode == (KeyCode.Y))
            {
                axisID = 1; 
                axisOn = true;
                selectedObj.localScale = initScale;
            }
        else if(Event.current.keyCode == (KeyCode.Z))
            {
                axisID = 2; 
                axisOn = true;
                selectedObj.localScale = initScale;
            }
    }
}
