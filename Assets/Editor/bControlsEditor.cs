using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(bControls))]
public class bControlsEditor : Editor {

    Vector2 mousePos;
    Vector2 selXY;

    private bool isGrabbing = false;
    private float offset;
    
	// Use this for initialization
	public override void OnInspectorGUI(){
        
    }
	
	// Update is called once per frame
	void OnSceneGUI () {
        Debug.Log("Scene!");
        Transform selectedObj = Selection.activeTransform;

        if(Event.current.type == EventType.keyDown)
        {
                if(Event.current.keyCode == (KeyCode.G))
                {
                    isGrabbing = true;
                    offset = Vector3.Distance(Camera.current.transform.position, selectedObj.position);
                }
        }
        
        if(isGrabbing){
            test(selectedObj, offset);
        }
        
        if(Event.current.type == EventType.MouseDown){
            if(Event.current.button == 0){
                if(isGrabbing)
                {
                    isGrabbing = false;
                }
            }
        }
	}
    
    
    void test(Transform selectedObj, float offset){
                float delay = Time.time;
                Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x, Camera.current.pixelHeight - Event.current.mousePosition.y, offset));
                mousePos = Event.current.mousePosition;
                selectedObj.position = ray.GetPoint(offset);

        //Vector3 pos = cam.ScreenToWorldPoint(mousePos);
        //selectedObj.position = new Vector3(pos.x, -pos.y, (cam.transform.position.z - selectedObj.position.z));
        
        //Debug.Log("Mouse world: " + pos);
        //Debug.Log("Obj world: " + selectedObj.position);
        

    }
    void Grab(Transform selectedObj){
        Vector3 screenPos = Camera.current.WorldToScreenPoint(selectedObj.position);
        Vector3 mouse3 = new Vector3(mousePos.x, mousePos.y, 0f);
        
        while(Event.current.type != EventType.MouseDown && Event.current.button != 0)
        {
            Vector3 pos = World2Screen(selectedObj.position);
            Vector2 offset = new Vector2(pos.x, pos.y) - mousePos;
            pos = new Vector3(mousePos.x - offset.x, mousePos.x - offset.y, selectedObj.position.z);
            selectedObj.position = pos;
        }
    }
    
    Vector3 World2Screen(Vector3 world){ return Camera.current.WorldToScreenPoint(world);}
    Vector3 Screen2World(Vector3 screen){return Camera.current.ScreenToWorldPoint(screen);}
    Vector3 Screen2View(Vector3 screen){return Camera.current.ScreenToViewportPoint(screen);}
    Vector3 View2World(Vector3 view){return Camera.current.ViewportToWorldPoint(view);}
    
}
