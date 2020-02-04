using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[CustomEditor(typeof(CameraFollow))]

public class CameraFollowEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraFollow cf = (CameraFollow)target;  // Checkinf if button is clicked
        if (GUILayout.Button("Set Min Cam Position"))
        {
            cf.SetMinCamPosition();


        }

        if (GUILayout.Button("Set Max Cam Position"))
        {
            cf.SetMaxCamPosition();

        }


    }
}
