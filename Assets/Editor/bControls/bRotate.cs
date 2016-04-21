using UnityEngine;
using UnityEditor;
using System.Collections;

public class bRotate : Editor {

    bControlsEditor controls;
    AxisControl axis;
    Vector2 initialMousePos;
    
    public bRotate(bControlsEditor controls){this.controls = controls;}
    
    void Awake(){
        axis = new AxisControl();
    }
    
    public void Init(Transform selectedObj){
        controls.myState = bControlsEditor.state.rotate;
        float zDepth = controls.GetZDepth(selectedObj.position);
        Vector2 initialMouseScreen = controls.GetMousePos();
        initialMousePos = new Vector3(initialMouseScreen.x, initialMouseScreen.y, zDepth);
        initialMousePos = controls.Screen2World(initialMousePos);
        Tools.hidden = true;
    }
    
    public void Rotate(Transform selectedObj){
        
    }
    
    public void RotateFree(Transform selectedObj){
        Vector2 curMouseScreen = controls.GetMousePos();
        float zDepth = controls.GetZDepth(selectedObj.position);
        Vector3 curMousePos = new Vector3(curMouseScreen.x, curMouseScreen.y, zDepth);
        curMousePos = controls.Screen2World(curMousePos);
        Ray ray = Camera.current.ScreenPointToRay(selectedObj.position);
        float rotateAmount = Vector2.Angle(initialMousePos, curMousePos);
        Vector3 objPos = ray.GetPoint(zDepth);
        selectedObj.rotation = Quaternion.AngleAxis(-rotateAmount, objPos);
        Debug.DrawRay(selectedObj.position, initialMousePos, Color.green);
        Debug.DrawRay(selectedObj.position, curMousePos, Color.red);
    }
    
    public void RotateAxis(Transform selectedObj){
        
    }
    
    public void CheckAxis(Transform selectedObj){}
}
