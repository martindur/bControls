using UnityEngine;
using UnityEditor;
using System.Collections;

public class bGrab : Editor {

    bControlsEditor controls;
    AxisControl axis;
    float zDepth;
    int axisID;
    bool axisOn;
    Vector3 curPos;
    Vector2 mouseOffset;

    public bGrab(bControlsEditor controls){this.controls = controls;}
        void Awake(){
        axis = new AxisControl();
    }
    
    
    public void Init(Transform selectedObj){
        controls.myState = bControlsEditor.state.grab;
        zDepth = controls.GetZDepth(selectedObj.position);
        curPos = selectedObj.position;
        Vector2 curPosScreen = controls.World2Screen(curPos);
        mouseOffset = controls.GetMousePos() - curPosScreen;
        axisOn = false;
        Tools.hidden = true;
    }
    
    public void Grab(Transform selectedObj){
        if(axisOn){GrabAxis(selectedObj);}
        else if(!axisOn){GrabFree(selectedObj);}
    }
    public void GrabFree(Transform selectedObj)
    {
        Vector2 CurPosScreen = controls.GetMousePos() - mouseOffset;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(CurPosScreen.x, CurPosScreen.y, zDepth));
        selectedObj.position = ray.GetPoint(zDepth);
    }
    
    public void GrabAxis(Transform selectedObj)
    {
        float mouseY = ((controls.GetMousePos().y - mouseOffset.y) * zDepth) * 0.006f;
        axis.DrawAxis(selectedObj.position, axisID);
        switch (axisID)
        {
            case 0:
                selectedObj.transform.position = new Vector3(mouseY, selectedObj.position.y, selectedObj.position.z);
                break;
            case 1:
                selectedObj.transform.position = new Vector3(selectedObj.position.x, mouseY, selectedObj.position.z);
                break;
            case 2:
                selectedObj.transform.position = new Vector3(selectedObj.position.x, selectedObj.position.y, mouseY);
                break;
            default:
                break;
        }
    }
    
    public void CheckAxis(Transform selectedObj){
        if(Event.current.keyCode == (KeyCode.X))
            {
                axisID = 0; 
                axisOn = true;
                selectedObj.position = curPos;
                mouseOffset = controls.GetMousePos();
            }
        else if(Event.current.keyCode == (KeyCode.Y))
            {
                axisID = 1; 
                axisOn = true;
                selectedObj.position = curPos;
                mouseOffset = controls.GetMousePos();
            }
        else if(Event.current.keyCode == (KeyCode.Z))
            {
                axisID = 2; 
                axisOn = true;
                selectedObj.position = curPos;
                mouseOffset = controls.GetMousePos();
            }
    }
}
