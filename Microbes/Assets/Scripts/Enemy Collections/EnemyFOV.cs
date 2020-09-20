using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float viewradius;
    [Range(0, 360)]
    public float viewangle;

    public LayerMask targetMask;
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewradius, targetMask);
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 DirectionToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.up,DirectionToTarget)<viewangle / 2)
            {
                float DistanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, DirectionToTarget, DistanceToTarget))
                {
                    if (target.name != this.name)
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }
    }
    public Vector2 DistanceFromAngle(float AngleInDegrees, bool AngleIsGlobal)
    {
        if (!AngleIsGlobal)
        {
            AngleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(AngleInDegrees * Mathf.Deg2Rad), Mathf.Cos(AngleInDegrees * Mathf.Deg2Rad));
    }
}
