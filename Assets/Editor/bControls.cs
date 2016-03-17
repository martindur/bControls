using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class bControls : MonoBehaviour {

    Vector2 mousePos;
    Transform selectedObj;
    Vector2 selXY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnSceneGUI () {
        
        selectedObj = Selection.activeTransform;
        selXY = new Vector2(selectedObj.transform.position.x, selectedObj.transform.position.y);
	    mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        
        Event e = Event.current;
        switch(e.type)
        {
            
        }
        if(Event.current.keyCode == (KeyCode.G)){
            if(Event.current.button == 0){
                return;
            }
            else{
                Vector3 offset = new Vector3(selXY.x - mousePos.x,selXY.y - mousePos.y, selectedObj.position.z);
                selectedObj.position = new Vector3(mousePos.x, mousePos.y, selectedObj.position.z) - offset;
            }
        }
	}
    
    void Grab(Vector2 mousePos, Vector2 selPos){
        
    }
}
