using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbLoad : MonoBehaviour
{
    private AssetBundle _assetBundle;
    private GameObject cube;
    private GameObject assets;
    void Start()
    {
         // _assetBundle = AssetBundle.LoadFromFile("Assets/StreamingAssets/prefabs");
         assets = Resources.Load<GameObject>("Cube");
         cube = GameObject.Instantiate(assets);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 200, 200), ""))
        {
           //GameObject.Destroy(cube);
           Resources.UnloadAsset(assets);
        }
        Resources.UnloadUnusedAssets();
    }
}
