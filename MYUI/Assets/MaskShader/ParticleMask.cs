using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class ParticleMask : MonoBehaviour
{
    public Material m_stancilmaterial;
    // Start is called before the first frame update
    void Start()
    {
        //----------------利用设置alpha实现------------------
        // var rt = gameObject.GetComponent<RectTransform>();
        // var vec4 = new Vector3[4];
        // rt.GetWorldCorners(vec4);
        // m_maskMaterial.SetVector("_ClipRect", new  Vector4(vec4[0].x, vec4[0].y, vec4[2].x, vec4[2].y));
        
        var renderer = this.gameObject.GetComponent<Renderer>();
        m_stancilmaterial = renderer.sharedMaterial;
        var rootCanvas = MaskUtilities.FindRootSortOverrideCanvas(this.transform);
        var m_stencilDepth = MaskUtilities.GetStencilDepth(this.transform, rootCanvas);
        
        var toUse = renderer.sharedMaterial;
        if (m_stencilDepth > 0)
        {
            Debug.Log(m_stencilDepth);
            var maskMat_ = StencilMaterial.Add(toUse, (1 << m_stencilDepth) - 1, StencilOp.Keep, 
                CompareFunction.Equal, ColorWriteMask.All, (1 << m_stencilDepth) - 1, 0);
            StencilMaterial.Remove(m_stancilmaterial);
            m_stancilmaterial = maskMat_;
            toUse = m_stancilmaterial;
        }
        renderer.material = toUse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
