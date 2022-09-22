using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using Object = UnityEngine.Object;
[CustomEditor(typeof(BindingData))]
public class BindingDataEditor : Editor
{
    private BindingData bindingData;
    private List<BindingData.ObjectField> mData ;
    private List<BindingData.ObjectField> removeData = new List<BindingData.ObjectField>();
    private string currentFileName;
    private int currentSelectType = -1;
    private string[] selectTypes = new string[0];
    private string currentTypeName;
    private Object currentObject;
    private void OnEnable()
    {
        bindingData = (BindingData)target;
        mData = bindingData.mData.objects;
    }


    public override void OnInspectorGUI()
    {
        if (mData.Count > 0)
        {
            foreach (BindingData.ObjectField ex in mData)
            {
                DrawDataField(ex);
            }
        }
        foreach(BindingData.ObjectField ex in removeData)
        {
            mData.Remove(ex);
        }
        removeData.Clear();
        EditorGUILayout.Space();
        DrawPickModule();
        GUI.enabled = (!string.IsNullOrEmpty(currentFileName) && !string.IsNullOrEmpty(currentTypeName));
        if(GUILayout.Button("Add"))
        {
            Type type = GetType(currentTypeName);
            if(type != null)
            {
                SetRightCurrentType();
                BindingData.ObjectField ex = new BindingData.ObjectField(currentFileName,currentTypeName,currentObject);
                bool isExist = false;
                foreach(var go in mData)
                {
                    if(go.value == ex.value || go.name == ex.name)
                    {
                        isExist = true;
                        break;
                    }
                }
                if(!isExist)
                {
                    mData.Add(ex);
                    currentFileName = null;
                    currentTypeName = null;
                    currentSelectType = -1;
                }
            }
        }
    }
    public void DrawDataField(BindingData.ObjectField field)
    {
        GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        field.value = EditorGUILayout.ObjectField(field.name, field.value, typeof(UnityEngine.Object),true);
        if(GUILayout.Button("Delete",GUILayout.Width(50f)))
        {
            DeleteData(field);
        }
        GUILayout.EndHorizontal();
    }
    private void DeleteData(BindingData.ObjectField field)
    {
        removeData.Add(field);
    }

    private void DrawPickModule()
    {
        currentFileName = EditorGUILayout.TextField(new GUIContent("Field Name:", "字段名称"), currentFileName, Array.Empty<GUILayoutOption>());
        currentSelectType = EditorGUILayout.Popup(new GUIContent("Field Type", "字段类型"), currentSelectType, selectTypes, Array.Empty<GUILayoutOption>());
        if (currentSelectType >= 0 && currentSelectType < selectTypes.Length)
        {
            currentTypeName = selectTypes[currentSelectType];
        }
        else
        {
            currentTypeName = null;
        }
        Object NewObject = EditorGUILayout.ObjectField(new GUIContent("Object", "对象"), currentObject, typeof(Object), true, Array.Empty<GUILayoutOption>());
        if(NewObject != currentObject)
        {
            currentObject = NewObject;
            if(string.IsNullOrEmpty(currentFileName))
            {
                currentFileName = currentObject.name;
            }
            List<string> list = new List<string>();
            GameObject gameObject = currentObject as GameObject;
            list.Add(typeof(GameObject).FullName);
            Component[] components = gameObject.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                Component component = components[i];
                list.Add(component.GetType().FullName);
            }
            selectTypes = list.ToArray();
            currentSelectType = -1;
        }
    }
    private Type GetType(string name)
    {
        Type type = Type.GetType(name, false);
        if (type == null)
        {
            type = typeof(GameObject).Assembly.GetType(name, false);
        }
        /*
        if (type == null)
        {
            type = typeof(NGUIMenu).Assembly.GetType(name, false);
        }*/
        if(type == null)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach(Assembly assembly in assemblies)
            {
                type = assembly.GetType(name);
                if (type == null)
                    continue;
                return type;
            }    
        }
        return type;
    }
    private void SetRightCurrentType()
    {
        int num = currentTypeName.LastIndexOf(".");
        string text = currentTypeName;
        if (num != -1)
        {
            text = currentTypeName.Substring(num + 1);
        }
        GameObject gameObject = currentObject as GameObject;
        if(currentTypeName != typeof(GameObject).FullName)
        {
            currentObject = gameObject.GetComponent(text);
        }

    }

}
