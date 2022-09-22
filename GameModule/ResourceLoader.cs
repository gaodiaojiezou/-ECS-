using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ResourceLoader 
{
      AssetBundle ab ;
      public void Initialize()
      {
         ab = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "prefabs"));
      }
      
      public T LoadResource<T>(string path) where T :Object
      {
         T obj = null; 
         #if UNITY_EDITOR
           obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);        
         #else
         string fileName = Path.GetFileNameWithoutExtension(path);
         obj = ab.LoadAsset<T>(path);
         #endif
         
         if (obj == null)
         {
            Debug.LogError("未能成功加载资源"+path);
         }
         return obj;
      }
}
