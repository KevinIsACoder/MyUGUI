using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
public class UIRender3D : UIBehaviour
{
    public GameObject toRenderObj;
    private CanvasRenderer m_canvasRender;

    public CanvasRenderer canvasRender
    {
        get { return m_canvasRender ?? (m_canvasRender = transform.GetComponent<CanvasRenderer>()); }
    }

    public Material renderMaterial;
    public Texture renderTexture;
    private Comparison<TestData> compareFunc = CompareData;
    void Start()
    {
        // List<TestData> arry = new List<TestData>(2)
        // {
        //     new TestData(2), 
        //     new TestData(1)
        // };
        // arry.Sort(compareFunc);
        // Debug.Log(arry[0].a);
        // Debug.Log(arry[1].a);

        // var ms = CreateMesh();
        // SetMesh(ms);

        var dir = "Assets/Prefabs/Tests/a.prefab";
        var pos = 0;
        while ((pos = dir.LastIndexOf("/")) != -1)
        {
            dir = dir.Substring(0, pos);
            Debug.Log(dir);
        }
        Debug.Log(dir);
    }
    
    public static int CompareData(TestData x, TestData y)
    {
        return x.a - y.a;
    }

    public Mesh CreateMesh()
    {
        var ms = new Mesh();
        Vector3[] verticles = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 100, 0),
            new Vector3(100, 100, 0),
            new Vector3(100, 0, 0)
        };
        //顶点信息，uv保存在顶点坐标中
        List<UIVertex> vtxes = new List<UIVertex>()
        {
            new UIVertex(){ position = verticles[0], color = new Color(1,1,1,1), uv0 = new Vector2(0, 0)},
            new UIVertex(){ position = verticles[1], color = new Color(1,1,1,1), uv0 = new Vector2(0, 1)},
            new UIVertex(){ position = verticles[2], color = new Color(1,1,1,1), uv0 = new Vector2(1, 1)},
            new UIVertex(){ position = verticles[3], color = new Color(1,1,1,1), uv0 = new Vector2(1, 0)},
        };

        SetCenter(verticles);
        List<int> indices = new List<int>()  //这个是对上面顶点数组的索引
        {
            0, 1, 2,
            0, 2, 3
        };
        VertexHelper vh = new VertexHelper();
        vh.AddUIVertexStream(vtxes, indices);
        vh.FillMesh(ms);
        ms.RecalculateBounds();
        return ms;
    }

    void SetCenter(Vector3[] verticles)
    {
        if(verticles == null) return;
        Vector3 center = new Vector3(50, 50, 50);
        Vector3 pos;
        for (int i = 0, count = verticles.Length; i < count; i++)
        {
            pos = verticles[i] - center;
            verticles[i] = pos;
        }
    }
    
    public void SetMesh(Mesh ms)
    {
        canvasRender.Clear();
        canvasRender.SetMesh(ms);
        canvasRender.SetColor(new Color(1,1,1,1));
        canvasRender.SetMaterial(renderMaterial, renderTexture);
    }
    
}

public class TestData
{
    public TestData(int num)
    {
        a = num;
    }
    public int a;
}