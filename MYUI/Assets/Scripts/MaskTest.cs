using System;
using UnityEngine;using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]

public class MaskTest : Mask
{
    [NonSerialized] 
    private Material m_MaskMaterial;

    [NonSerialized] 
    private Material m_UnmaskMaterial;
    
    private RectTransform m_RectTransform;
    public RectTransform rectTransform
    {
        get { return m_RectTransform ?? (m_RectTransform = GetComponent<RectTransform>()); }
    }

    private Graphic m_graphic;

    public Graphic graphic
    {
        get
        {
            return m_graphic ?? ( m_graphic = GetComponent<Graphic>());
        }
        
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        if (graphic != null)
        {
            graphic.canvasRenderer.hasPopInstruction = true;
            graphic.SetMaterialDirty();
        }

        MaskUtilities.NotifyStencilStateChanged(this);
    }
    protected override void OnDisable()
    {
        // we call base OnDisable first here
        // as we need to have the IsActive return the
        // correct value when we notify the children
        // that the mask state has changed.
        base.OnDisable();
        if (graphic != null)
        {
            graphic.SetMaterialDirty();
            graphic.canvasRenderer.hasPopInstruction = false;
            graphic.canvasRenderer.popMaterialCount = 0;
        }

        StencilMaterial.Remove(m_MaskMaterial);
        m_MaskMaterial = null;
        StencilMaterial.Remove(m_UnmaskMaterial);
        m_UnmaskMaterial = null;

        MaskUtilities.NotifyStencilStateChanged(this);
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (!IsActive())
            return;

        if (graphic != null)
            graphic.SetMaterialDirty();

        MaskUtilities.NotifyStencilStateChanged(this);
    }

#endif
    public override Material GetModifiedMaterial(Material baseMaterial)
    {
        if (graphic == null)
            return baseMaterial;
        var rootSortCanvas = MaskUtilities.FindRootSortOverrideCanvas(transform);
        var stencilDepth = MaskUtilities.GetStencilDepth(transform, rootSortCanvas); //获取到深度值
        if (stencilDepth >= 8)
        {
            Debug.LogError("Attempting to use a stencil mask with depth > 8", gameObject);
            return baseMaterial;
        }
        var diserdDepthBit = 1 << stencilDepth;
        
        if (diserdDepthBit == 1)
        {
            var maskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Always, showMaskGraphic ? ColorWriteMask.All : 0);
            StencilMaterial.Remove(m_MaskMaterial);
            m_MaskMaterial = maskMaterial;
    
            var unmaskMaterial = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, 0);
            StencilMaterial.Remove(m_UnmaskMaterial);
            m_UnmaskMaterial = unmaskMaterial;
            graphic.canvasRenderer.popMaterialCount = 1;
            graphic.canvasRenderer.SetPopMaterial(m_UnmaskMaterial, 0);
    
            return m_MaskMaterial;
            
        }
        return baseMaterial;
    }
    
    // public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    // {
    //     if (!isActiveAndEnabled)
    //         return true;
    //
    //     return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sp, eventCamera);
    // }
}
