using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AbAssets
{
    [Serializable]
    public class AbAssetManifest
    {
        [SerializeField] 
        private int id;
        [SerializeField] 
        private string bundleName;
        [SerializeField] 
        private int[] dependences;
    }   
}
