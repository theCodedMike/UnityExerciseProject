using System;
using System.Collections.Generic;
using UnityEngine;

namespace M12D17
{
    public class AssetsCacheHelper
    {
        private static AssetsCacheHelper instance;

        private Dictionary<string, AssetBundle> map;

        public static AssetsCacheHelper Instance
        {
            get
            {
                return instance ??= new AssetsCacheHelper();
            }
        }

        private AssetsCacheHelper()
        {
            map = new Dictionary<string, AssetBundle>(8);
        }


        public bool Contains(string assetName) => map.ContainsKey(assetName);


        public AssetBundle GetAssetBundle(string assetName) => Contains(assetName) ? map[assetName] : null;

        public void SetAssetBundle(string assetName, AssetBundle ab)
        {
            if (string.IsNullOrEmpty(assetName) || ab is null)
                throw new ArgumentException($"参数异常: assetName = {assetName}, assetBundle = {ab}");

            map.Add(assetName, ab);
        }
    }
}
