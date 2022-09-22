using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BindingData : MonoBehaviour
{

    [Serializable]
    public class ObjectField
    {
        public string name;

        public string typeName;

        public UnityEngine.Object value;

        public ObjectField(string name, string typeName,UnityEngine.Object @object)
        {
            this.name = name;
            this.typeName = typeName;
            value = @object;
        }
    }
    [Serializable]
    public class InternalData
    {
        public List<ObjectField> objects = new List<ObjectField>();

    }

    [SerializeField]
    public InternalData mData = new InternalData();

    public static BindingData Get(GameObject go, bool createNew = false)
    {
        BindingData bindingData = go.GetComponent<BindingData>();
        if (bindingData == null && createNew)
        {
            bindingData = go.AddComponent<BindingData>();
        }

        return bindingData;
    }


    public UnityEngine.Object GetObject(string name, UnityEngine.Object defVal = null)
    {
        foreach (ObjectField @object in mData.objects)
        {
            if (@object.name == name)
            {
                return @object.value;
            }
        }
        return defVal;
    }

}
