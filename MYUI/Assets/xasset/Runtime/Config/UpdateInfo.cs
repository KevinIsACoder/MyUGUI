using UnityEngine;

namespace xasset
{
    public class UpdateInfo : ScriptableObject
    {
        public static readonly string Filename = $"{nameof(UpdateInfo).ToLower()}.json";
        public string file;
        public string hash;
        public ulong size;
        public string version;
        public string downloadURL;
        public string playerDownloadURL;
    }
}