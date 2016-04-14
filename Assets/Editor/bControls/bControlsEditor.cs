using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(GameObject))]
public class bControlsEditor : Editor
{
    Vector2 mousePos;
    Vector2 selXY;

    private bool isGrabbing = false;
    private bool isGrabbingAxis = false;
    private float zDepth;
    private Vector3 curPos;
    private Vector2 mouseOffset;
    private Transform selectedObj;

    private int axisID = 0;
    
    //Scale settings
    private Vector3 initialDist;
    private float initMag;
    private bool isScaling;
    
    public override void OnInspectorGUI()
    {

    }
    
    void OnSceneGUI()
    {
        Undo.SetSnapshotTarget(this, "Changed Settings");
        
        mousePos = GetMousePos();
        if (Selection.activeTransform == null) return;
        
        selectedObj = Selection.activeTransform;
        Handles.color = Color.black;
        Handles.DrawLine(selectedObj.position, Screen2World(mousePos));
        CheckEvent(selectedObj);
        if (isGrabbing)
        {
            Grab(selectedObj, zDepth);
            if (Event.current.keyCode == (KeyCode.X))
            {
                isGrabbing = false;
                selectedObj.position = curPos;
                isGrabbingAxis = true;
                axisID = 0;
                mouseOffset = mousePos;
            }
            if (Event.current.keyCode == (KeyCode.Y))
            {
                isGrabbing = false;
                selectedObj.position = curPos;
                isGrabbingAxis = true;
                axisID = 1;
                mouseOffset = mousePos;
            }
            if (Event.current.keyCode == (KeyCode.Z))
            {
                isGrabbing = false;
                selectedObj.position = curPos;
                isGrabbingAxis = true;
                axisID = 2;
                mouseOffset = mousePos;
            }
        }


        if (isGrabbingAxis)
        {
            Grab(selectedObj, axisID);
        }
        if(isScaling)
        {
            Scale(selectedObj);
        }

        if (Event.current.type == EventType.MouseDown)
        {
            if (Event.current.button == 0)
            {
                if (isGrabbing || isGrabbingAxis)
                {
                    isGrabbing = false;
                    Tools.hidden = false;
                }
            }
        }
        
    }

    void CheckEvent(Transform selectedObj)
    {

        if (Event.current.type == EventType.keyDown)
        {
            if (Event.current.keyCode == (KeyCode.G))
            {
                isGrabbing = true;
                zDepth = GetZDepth(selectedObj.position);
                Tools.hidden = true;
                curPos = selectedObj.position;
                Vector2 CurPosScreen = World2Screen(curPos);
                mouseOffset = mousePos - CurPosScreen;
            }
            if (Event.current.keyCode == (KeyCode.S))
            {
                isScaling = true;
                zDepth = GetZDepth(selectedObj.position);
                Tools.hidden = true;
                curPos = selectedObj.position;
                Vector2 CurPosScreen = World2Screen(curPos);
                mouseOffset = CurPosScreen;
                InitScale();
            }
        }
    }

    void Grab(Transform selectedObj, float zDepth)
    {
        Vector2 CurPosScreen = mousePos - mouseOffset;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(CurPosScreen.x, CurPosScreen.y, zDepth));
        selectedObj.position = ray.GetPoint(zDepth);
    }

    void Grab(Transform selectedObj, int id)
    {
        float mouseY = ((mousePos.y - mouseOffset.y) * zDepth) * 0.006f;
        DrawAxis(selectedObj.position, id);
        switch (id)
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
    
    void Rotate(Transform selectedObj, float zDepth)
    {
        
    }
    
    
    void InitScale()
    {
        initialDist = mousePos - mouseOffset;
        initMag = initialDist.magnitude * 0.001f;
        Debug.Log("Ran");
    }
    void Scale(Transform selectedObj)
    {
        Vector3 curDist = mousePos - mouseOffset;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(curDist.x, curDist.y, zDepth));
        curDist = ray.GetPoint(zDepth);
        float curMag = curDist.magnitude * 0.001f;
        selectedObj.localScale *= curMag / initMag;
        Debug.Log(mousePos);
        Handles.color = Color.black;
        Handles.DrawLine(selectedObj.position, curDist);
    }

    void DrawAxis(Vector3 pos, int id)
    {
        switch (id)
        {
            case 0:
                Handles.color = Color.red;
                Handles.DrawLine(GetDrawAxis(pos, id, 0), GetDrawAxis(pos, id, 1));
                break;
            case 1:
                Handles.color = Color.green;
                Handles.DrawLine(GetDrawAxis(pos, id, 0), GetDrawAxis(pos, id, 1));
                break;
            case 2:
                Handles.color = Color.blue;
                Handles.DrawLine(GetDrawAxis(pos, id, 0), GetDrawAxis(pos, id, 1));
                break;
            default:
                break;
        }
    }

    //Space transformations
    Vector3 World2Screen(Vector3 world) { return Camera.current.WorldToScreenPoint(world); }
    Vector3 Screen2World(Vector3 screen) { return Camera.current.ScreenToWorldPoint(screen); }
    Vector3 Screen2View(Vector3 screen) { return Camera.current.ScreenToViewportPoint(screen); }
    Vector3 View2World(Vector3 view) { return Camera.current.ViewportToWorldPoint(view); }

    //Positions
    Vector2 GetMousePos() { return new Vector2(Event.current.mousePosition.x, Camera.current.pixelHeight - Event.current.mousePosition.y); }
    float GetZDepth(Vector3 pos){return Vector3.Distance(Camera.current.transform.position, pos);}

    //Handle draws
    Vector3 GetDrawAxis(Vector3 curPos, int axisID, int point)
    {
        switch (axisID)
        {
            case 0:
                if (point == 0)
                    return new Vector3(-99999f, curPos.y, curPos.z);
                else
                    return new Vector3(99999f, curPos.y, curPos.z);
            case 1:
                if (point == 0)
                    return new Vector3(curPos.x, -99999f, curPos.z);
                else
                    return new Vector3(curPos.x, 99999f, curPos.z);
            case 2:
                if (point == 0)
                    return new Vector3(curPos.x, curPos.y, -99999f);
                else
                    return new Vector3(curPos.x, curPos.y, 99999f);
            default:
                return curPos;
        }

    }

}
