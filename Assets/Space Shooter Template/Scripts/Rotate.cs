using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 pos;

    void Update()
    {
        transform.Rotate(new Vector3(pos.x, pos.y, pos.z) * Time.deltaTime);
    }
}

