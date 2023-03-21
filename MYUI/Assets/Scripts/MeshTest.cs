using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MeshTest : MaskableGraphic,ISerializationCallbackReceiver, ILayoutElement, ICanvasRaycastFilter
{
    private VertexHelper _vertexHelper = new VertexHelper();

    private Sprite overrideSprite;
    public enum FillMethod
    {
        Horizontal,
        Vertical,
        Radial90,
        Radial180,
        Radial360,
    }
    
    public enum Type
    {
        Simple,
        Sliced,
        Tiled,
        Filled
    }
    
    public override Texture mainTexture
    {
        get
        {
            if (overrideSprite == null)
            {
                if (material != null && material.mainTexture != null)
                {
                    return material.mainTexture;
                }
                return s_WhiteTexture;
            }

            return overrideSprite.texture;
        }
    }
    
    public override void SetNativeSize()
    {
        if (overrideSprite != null)
        {
            float w = overrideSprite.rect.width / pixelsPerUnit;
            float h = overrideSprite.rect.height / pixelsPerUnit;
            rectTransform.anchorMax = rectTransform.anchorMin;
            rectTransform.sizeDelta = new Vector2(w, h);
            SetAllDirty();
        }
    }
    [SerializeField] private Sprite m_Sprite;
    public Sprite sprite { get { return m_Sprite; } set { SetAllDirty(); } }
    /// Controls the origin point of the Fill process. Value means different things with each fill method.
    // [SerializeField] private int m_FillOrigin;
    // public int fillOrigin { get { return m_FillOrigin; } set { if (SetPropertyUtility.SetStruct(ref m_FillOrigin, value)) SetVerticesDirty(); } }
    [Range(0, 1)]
    [SerializeField] private float m_FillAmount = 1.0f;
    /// Filling method for filled sprites.
    [SerializeField] private FillMethod m_FillMethod = FillMethod.Radial360;
    [SerializeField] private Type m_Type = Type.Simple;
    public Type type { get { return m_Type; } set { SetVerticesDirty(); } }
    // Start is called before the first frame update
    void Start()
    {
        // var ms = new Mesh();
        // _vertexHelper.FillMesh(ms);
    }
    

    public virtual void OnBeforeSerialize()
    {
        
    }
    public virtual void CalculateLayoutInputHorizontal() {}
    public virtual void CalculateLayoutInputVertical() {}
    public virtual float minWidth { get { return 0; } }

    public virtual float preferredWidth
    {
        get
        {
            if (overrideSprite == null)
                return 0;
            //if (type == Type.Sliced || type == Type.Tiled)
                // return Sprites.DataUtility.GetMinSize(overrideSprite).x / pixelsPerUnit;
            return overrideSprite.rect.size.x / pixelsPerUnit;
        }
    }
    
    public float pixelsPerUnit
    {
        get
        {
            float spritePixelsPerUnit = 100;
            // if (sprite)
            //     spritePixelsPerUnit = sprite.pixelsPerUnit;

            float referencePixelsPerUnit = 100;
            if (canvas)
                referencePixelsPerUnit = canvas.referencePixelsPerUnit;

            return spritePixelsPerUnit / referencePixelsPerUnit;
        }
    }

    public virtual float flexibleWidth { get { return -1; } }

    public virtual float minHeight { get { return 0; } }

    public virtual float flexibleHeight { get { return -1; } }

    public virtual int layoutPriority { get { return 0; } }
    
    public virtual float preferredHeight
    {
        get
        {
            if (overrideSprite == null)
                return 0;
            //if (type == Type.Sliced || type == Type.Tiled)
                //return Sprites.DataUtility.GetMinSize(overrideSprite).y / pixelsPerUnit;
                return overrideSprite.rect.size.y / pixelsPerUnit;
        }
    }
    void GenerateSimpleSprite(VertexHelper vh, bool lPreserveAspect)
    {
        Vector4 v = GetDrawingDimensions(lPreserveAspect);
        var uv = Vector4.zero;

        var color32 = color;
        vh.Clear();
        vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(uv.x, uv.y));
        vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(uv.x, uv.w));
        vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(uv.z, uv.w));
        vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(uv.z, uv.y));

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }
    
    /// Image's dimensions used for drawing. X = left, Y = bottom, Z = right, W = top.
    private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
    {
        var padding = Vector4.zero;
        var size = overrideSprite == null ? Vector2.zero : new Vector2(overrideSprite.rect.width, overrideSprite.rect.height);

        Rect r = GetPixelAdjustedRect();
        // Debug.Log(string.Format("r:{2}, size:{0}, padding:{1}", size, padding, r));

        int spriteW = Mathf.RoundToInt(size.x);
        int spriteH = Mathf.RoundToInt(size.y);

        var v = new Vector4(
            padding.x / spriteW,
            padding.y / spriteH,
            (spriteW - padding.z) / spriteW,
            (spriteH - padding.w) / spriteH);

        if (shouldPreserveAspect && size.sqrMagnitude > 0.0f)
        {
            var spriteRatio = size.x / size.y;
            var rectRatio = r.width / r.height;

            if (spriteRatio > rectRatio)
            {
                var oldHeight = r.height;
                r.height = r.width * (1.0f / spriteRatio);
                r.y += (oldHeight - r.height) * rectTransform.pivot.y;
            }
            else
            {
                var oldWidth = r.width;
                r.width = r.height * spriteRatio;
                r.x += (oldWidth - r.width) * rectTransform.pivot.x;
            }
        }

        v = new Vector4(
            r.x + r.width * v.x,
            r.y + r.height * v.y,
            r.x + r.width * v.z,
            r.y + r.height * v.w
        );

        return v;
    }

    public virtual void OnAfterDeserialize()
    {
        // if (m_FillOrigin < 0)
        //     m_FillOrigin = 0;
        // else if (m_FillMethod == FillMethod.Horizontal && m_FillOrigin > 1)
        //     m_FillOrigin = 0;
        // else if (m_FillMethod == FillMethod.Vertical && m_FillOrigin > 1)
        //     m_FillOrigin = 0;
        // else if (m_FillOrigin > 3)
        //     m_FillOrigin = 0;

        m_FillAmount = Mathf.Clamp(m_FillAmount, 0f, 1f);
    }
    public bool hasBorder
    {
        get
        {
            if (overrideSprite != null)
            {
                Vector4 v = overrideSprite.border;
                return v.sqrMagnitude > 0f;
            }
            return false;
        }
    }
    
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        if (overrideSprite == null)
        {
            return;
        }

        GenerateSimpleSprite(vh, true);
    }

    public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return true;
    }

    public override void Rebuild(CanvasUpdate update)
    {
        base.Rebuild(update);
    }
}
