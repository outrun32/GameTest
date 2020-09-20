using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyFOV))]
public class EnemyFovEditor : Editor
{
    void OnSceneGUI()
    {
        EnemyFOV fov = (EnemyFOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.right, 360, fov.viewradius);
        Vector3 viewAngleA = fov.DistanceFromAngle(-fov.viewangle / 2, false);
        Vector3 viewAngleB = fov.DistanceFromAngle(fov.viewangle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewradius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewradius);

        Handles.color = Color.red;
        foreach(Transform visibleTargets in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTargets.position);
        }
    }
    
}
