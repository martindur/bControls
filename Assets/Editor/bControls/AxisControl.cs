using UnityEngine;
using UnityEditor;
using System.Collections;

public class AxisControl : Editor {


    public void DrawAxis(Vector3 pos, int id)
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
    public Vector3 GetDrawAxis(Vector3 curPos, int axisID, int point)
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
