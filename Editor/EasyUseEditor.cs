using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Excel;
using System.Data;
using System.IO;
public class EasyUseEditor : UnityEditor.EditorWindow
{
    //====================================================================================================================================
    //显示文件路径
        [MenuItem("GameObject/Show Path", false, 3)]
        public static void ShowPath()
        {
            if (Selection.objects.Length <= 0)
                return;
            if (Selection.transforms != null && Selection.transforms.Length > 0)
            {
                var node = Selection.transforms[0];
                var path = GetNodePath(node);
                Debug.Log(path);
            }
            else
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
                Debug.Log(assetPath);
            }

        }
        public static string GetNodePath(Transform node)
        {
            string path = node.name;
            while (node.parent != null)
            {
                node = node.parent;
                path = node.name + "/" + path;
            }
            return path;
        }    
    //读取Excel并存储
    private static DataRowCollection ReadExcelByPath(string path, ref int col,ref int row)
    {
        FileStream stream = File.Open(path,FileMode.Open,FileAccess.Read,FileShare.Read);
        IExcelDataReader excelDataReader =  ExcelReaderFactory.CreateBinaryReader(stream);
        DataSet result = excelDataReader.AsDataSet();
        col = result.Tables[0].Columns.Count;
        row = result.Tables[0].Rows.Count;
        return result.Tables[0].Rows;
    }
    
    [MenuItem("Tools/CreateHeroExcelAsset")]
    public static void CreateHeroExcelAsset()
    { 
        int col = 0;
        int row = 0;
        DataRowCollection collection = ReadExcelByPath(ExcelConfig.playerPropertyExcelPath,ref col,ref row);
        PlayerPropertyData data = ScriptableObject.CreateInstance<PlayerPropertyData>();
        PlayerPropertyItem[] playerItems  = new PlayerPropertyItem[row - 2];
        //从第二行开始才是数据
        for (int i = 2; i < row; i++)
        {
            PlayerPropertyItem item = new PlayerPropertyItem();
            item.name = collection[i][0].ToString();
            item.hp   = int.Parse(collection[i][1].ToString());
            item.attack = int.Parse(collection[i][2].ToString());
            float.TryParse(collection[i][3].ToString(),out item.attackTime);
            item.Skill1ID = collection[i][4].ToString();
            item.Skill2ID = collection[i][5].ToString();
            item.Skill3ID = collection[i][6].ToString();
            float.TryParse(collection[i][7].ToString(),out item.moveSpeed);
            playerItems[i-2] = item;
        }
        data.playerItems = playerItems;
        if(!Directory.Exists(ExcelConfig.assetPath))
        {
            Directory.CreateDirectory(ExcelConfig.assetPath);
        }
        string assetPath = string.Format(string.Format("{0}{1}.asset", ExcelConfig.assetPath, "/playerPropertyData"));
        AssetDatabase.CreateAsset(data,assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    } 
    [MenuItem("Tools/CreateEnemyExcelAsset")]
    public static void CreateEnemyExcelAsset()
    { 
        int col = 0;
        int row = 0;
        DataRowCollection collection = ReadExcelByPath(ExcelConfig.EnemyExcelPath,ref col,ref row);
        EnemyPropertyData data = ScriptableObject.CreateInstance<EnemyPropertyData>();
        EnemyPropertyItem[] enemyItems  = new EnemyPropertyItem[row - 2];
        //从第二行开始才是数据
        for (int i = 2; i < row; i++)
        {
            EnemyPropertyItem item = new EnemyPropertyItem();
            item.name = collection[i][0].ToString();
            item.hp   = int.Parse(collection[i][1].ToString());
            item.attack = int.Parse(collection[i][2].ToString());
            float.TryParse(collection[i][3].ToString(),out item.attackTime);
            item.Skill1ID = collection[i][4].ToString();
            item.Skill2ID = collection[i][5].ToString();
            item.Skill3ID = collection[i][6].ToString();
            float.TryParse(collection[i][7].ToString(),out item.moveSpeed);
            enemyItems[i-2] = item;
        }
        data.enemyItems = enemyItems;
        if(!Directory.Exists(ExcelConfig.assetPath))
        {
            Directory.CreateDirectory(ExcelConfig.assetPath);
        }
        string assetPath = string.Format(string.Format("{0}{1}.asset", ExcelConfig.assetPath, "/enemyPropertyData"));
        AssetDatabase.CreateAsset(data,assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }          
    [MenuItem("Tools/CreateAbilityExcelAsset")]
    public static void CreateAbilityExcelAsset()
    {
        const string HaveThisAction = "1";
        int col = 0;
        int row = 0;
        DataRowCollection collection = ReadExcelByPath(ExcelConfig.AbilityExcelPath,ref col,ref row);
        AbilityData data = ScriptableObject.CreateInstance<AbilityData>();
        AbilityItem[] abilityItems  = new AbilityItem[row - 1];
        //从第二行开始才是数据
        for (int i = 1; i < row; i++)
        {
          AbilityItem item = new AbilityItem();
          item.AbilityID = collection[i][0].ToString();
          float.TryParse(collection[i][1].ToString(),out item.AbilityCD);
          item.isHaveChangeHpAction = false;
          if(collection[i][2].ToString() == HaveThisAction)
          {
            ChangeHpAction changeHp = new ChangeHpAction();
            changeHp.actionType = EcsConst.ABilityActionType.ChangeHp;
            changeHp.camp  = GetEntityCamp(collection[i][3].ToString());
            changeHp.AreaType = GetABilityAreaType(collection[i][4].ToString());
            changeHp.availDistance = int.Parse(collection[i][5].ToString());
            changeHp.HpNum = int.Parse(collection[i][6].ToString());
            item.changeHpData = changeHp;
            item.isHaveChangeHpAction = true;
          }
          item.isHaveChangePropertyAction = false;
          if(collection[i][7].ToString() == HaveThisAction)
          {
           ChangePropertyAction changeProperty = new ChangePropertyAction();
           changeProperty.actionType = EcsConst.ABilityActionType.ChangeProperty;
           changeProperty.camp  = GetEntityCamp(collection[i][8].ToString());
           float.TryParse(collection[i][9].ToString(),out changeProperty.availTime);
           changeProperty.changePropertyType = GetPropertyType(collection[i][10].ToString());
           changeProperty.propertyNum = int.Parse(collection[i][11].ToString());
           item.changePropertyData =changeProperty;
           item.isHaveChangePropertyAction = true;
          }
          item.isHaveChangePostionAction = false;
          if(collection[i][12].ToString() == HaveThisAction)
          {
            ChangePostionAction changePostion = new ChangePostionAction();
            changePostion.actionType =  EcsConst.ABilityActionType.ChangePostion;
            changePostion.camp = EcsConst.EntityCamp.Player;
            changePostion.distanceNum = int.Parse(collection[i][14].ToString());
            item.changePostionData = changePostion;
            item.isHaveChangePostionAction = true;
          } 
          abilityItems[i-1] = item;         
        }
        data.AbilityItems = abilityItems;
        if(!Directory.Exists(ExcelConfig.assetPath))
        {
            Directory.CreateDirectory(ExcelConfig.assetPath);
        }
        string assetPath = string.Format(string.Format("{0}{1}.asset", ExcelConfig.assetPath, "/AbilityData"));
        AssetDatabase.CreateAsset(data,assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }  
    public static EcsConst.EntityCamp GetEntityCamp(string type)
    {
       if(type == "Player")
       return EcsConst.EntityCamp.Player;
       else if(type == "Enemy")
       return EcsConst.EntityCamp.Enemy;

       return EcsConst.EntityCamp.Enemy;
    }
    public static EcsConst.ABilityAreaType GetABilityAreaType(string type)
    {
       if(type == "PlayerSelf")
       return EcsConst.ABilityAreaType.PlayerSelf;
       else if(type == "Forward")
       return EcsConst.ABilityAreaType.Forward;
       else if(type == "MousePostion")
       return EcsConst.ABilityAreaType.MousePostion;       

       return EcsConst.ABilityAreaType.MousePostion;        
    }
    public static EcsConst.PropertyType GetPropertyType(string type)
    {
       if(type == "Attack")
       return EcsConst.PropertyType.Attack;
       else if(type == "AttackTime")
       return EcsConst.PropertyType.AttackTime;
       else if(type == "Speed")
       return EcsConst.PropertyType.Speed;       

       return EcsConst.PropertyType.Attack;        
    }

    [MenuItem("Tools/CreateABPackage")]
    public static void BuildAB()
    {
        string dir = Application.streamingAssetsPath;    //定义AB包路径：工程目录下的StreamingAssets
        if (Directory.Exists(dir) == false)//如果不存在文件夹，那么新建一个
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
        Debug.Log("打包完成");
    }

}
public  class ExcelConfig
{
    public static readonly string playerPropertyExcelPath = Application.dataPath + "/ExcelData/玩家角色属性.xls";
    public static readonly string AbilityExcelPath = Application.dataPath + "/ExcelData/能力属性表.xls";
    public static readonly string EnemyExcelPath = Application.dataPath + "/ExcelData/敌方角色属性.xls";
    public static readonly string assetPath = "Assets/ScriptableData"; 

}