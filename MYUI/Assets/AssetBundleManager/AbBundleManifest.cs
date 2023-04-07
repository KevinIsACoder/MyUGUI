using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AbAssets
{
    [Serializable]
    public class AbBundleManifest
    {
        [SerializeField] 
        private int id;
        [SerializeField] 
        private string bundleName;
        [SerializeField] 
        private AbAssetManifest[] assetNames;
        [SerializeField] 
        private int[] dependences;
        [SerializeField] 
        private float bundleSize;
    }   
}
