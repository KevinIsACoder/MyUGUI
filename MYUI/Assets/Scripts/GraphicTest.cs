using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GraphicTest : MaskableGraphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Debug.Log(0 % 1);
        base.OnPopulateMesh(vh);
    }
}
