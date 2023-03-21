using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.Rendering;

public class CustomImage : Image
{
    private Material m_UnmaskMaterial;

    private Graphic m_Graphic;
    public Graphic graphic
    {
        get { return m_Graphic ?? (m_Graphic = transform.GetComponent<Graphic>()); }
    }

    public override Material GetModifiedMaterial(Material baseMaterial)
    {
        var maskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Greater, ColorWriteMask.All);
        StencilMaterial.Remove(m_MaskMaterial);
        m_MaskMaterial = maskMaterial;
        
        var unmaskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, 0);
        StencilMaterial.Remove(m_UnmaskMaterial);
        m_UnmaskMaterial = unmaskMaterial;
        graphic.canvasRenderer.popMaterialCount = 1;
        graphic.canvasRenderer.SetPopMaterial(m_UnmaskMaterial, 0);
        return m_MaskMaterial;
    }
}
