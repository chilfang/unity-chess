using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_meshSizeCollector : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Debug.Log(gameObject.GetComponent<MeshFilter>().mesh.bounds);
        Debug.Log(gameObject.GetComponent<MeshRenderer>().bounds);
        Debug.Log(gameObject.GetComponent<MeshFilter>().mesh.bounds.extents.x);
        Debug.Log(gameObject.GetComponent<MeshFilter>().name);
        Debug.Log(gameObject.GetComponent<MeshFilter>().mesh.name);

    }
}   
