using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), Time.deltaTime);
        print("sdgfd");
    }
}
