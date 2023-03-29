using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMask : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        var rt = GetComponent<RectTransform>();
        Vector3[] v3 = new Vector3[4];
        rt.GetWorldCorners(v3);
        var canvasTransform = canvas.GetComponent<Transform>();
        for (int i = 0; i < 4; ++i)
            v3[i] = canvasTransform.InverseTransformPoint(v3[i]);
        foreach (var VARIABLE in v3)
        {
            Debug.Log(VARIABLE);     
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
