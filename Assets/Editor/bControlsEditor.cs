using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameObject))]
public class bControlsEditor : Editor {

    Vector2 mousePos;
    Vector2 selXY;

    private bool isGrabbing = false;
    private bool isGrabbingAxis = false;
    private float zDepth;
    private Vector3 curPos;
    private Vector3 mouseOffset;
    
	// Use this for initialization
	public override void OnInspectorGUI(){
        
    }
	
	// Update is called once per frame
	void OnSceneGUI () {
        mousePos = Event.current.mousePosition;
        mousePos.y = Camera.current.pixelHeight - Event.current.mousePosition.y;
        
        if(Selection.activeTransform == null)
            return;
        Transform selectedObj = Selection.activeTransform;

        if(Event.current.type == EventType.keyDown)
        {
                if(Event.current.keyCode == (KeyCode.G))
                {
                    isGrabbing = true;
                    zDepth = Vector3.Distance(Camera.current.transform.position, selectedObj.position);
                    Tools.hidden = true;
                    curPos = selectedObj.position;
                    
                }
        }
        
        if(isGrabbing){
            Grab(selectedObj, zDepth);
            if(Event.current.keyCode == (KeyCode.X)){
                isGrabbing = false;
                selectedObj.position = curPos;
                isGrabbingAxis = true;
            }
        }
        
        if(isGrabbingAxis){
            Grab(selectedObj, 0);
        }
        
        if(Event.current.type == EventType.MouseDown){
            if(Event.current.button == 0){
                if(isGrabbing || isGrabbingAxis)
                {
                    isGrabbing = false;
                    Tools.hidden = false;
                }
            }
        }
	}
    
    
    void Grab(Transform selectedObj, float zDepth){
        Vector3 CurPosScreen = World2Screen(curPos);
        CurPosScreen = new Vector3(mousePos.x, mousePos.y, 0f) - CurPosScreen;
        float dist = CurPosScreen.magnitude;
        CurPosScreen = CurPosScreen / dist;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(CurPosScreen.x, CurPosScreen.y, zDepth));
        Debug.Log(curPos);
        //Debug.DrawRay(Camera.current.transform.position, ray.direction,Color.green, 10f);
        selectedObj.position = ray.GetPoint(zDepth);
    }
    
    void Grab(Transform selectedObj, int id){
        Vector3 mousePos = Event.current.mousePosition;
        mousePos = View2World(mousePos);
        float mouseY = mousePos.y;
        selectedObj.transform.position = new Vector3(mouseY, selectedObj.position.y, selectedObj.position.z);
        Debug.Log(mouseY);
    }
    
    Vector3 World2Screen(Vector3 world){ return Camera.current.WorldToScreenPoint(world);}
    Vector3 Screen2World(Vector3 screen){return Camera.current.ScreenToWorldPoint(screen);}
    Vector3 Screen2View(Vector3 screen){return Camera.current.ScreenToViewportPoint(screen);}
    Vector3 View2World(Vector3 view){return Camera.current.ViewportToWorldPoint(view);}
    
}
