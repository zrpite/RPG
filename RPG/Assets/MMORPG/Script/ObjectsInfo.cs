using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 物品信息管理类
/// </summary>
public class ObjectsInfo : MonoBehaviour 
{

    public static ObjectsInfo _instance;
    public static ObjectsInfo Instance()
    {
        return _instance;
    }
    /// <summary>
    /// 存储从文本文件中读取的信息的字典
    /// </summary>
    /// <typeparam name="int">id</typeparam>
    /// <typeparam name="ObjectInfo">ObjectInfo对象</typeparam>
    /// <returns></returns>
    private Dictionary<int, ObjectInfo> objectInfoDict = new Dictionary<int, ObjectInfo>();
    /// <summary>
    /// 存储所有物品信息的文本文件
    /// </summary>
    public TextAsset objectsInfoTxt;

    void Awake() 
    {
        _instance = this;
        ReadInfo();
    }
    /// <summary>
    /// 从文本文件中读取信息到字典中
    /// </summary>
    void ReadInfo() 
    {
        string text = objectsInfoTxt.text;
        string[] lineArray = text.Split('\n');
        foreach (string line in lineArray) 
        {
            string[] perlineArray = line.Split(',');
            ObjectInfo objInfo = new ObjectInfo();
            int tempid = int.Parse(perlineArray[0]);
            string tempname = perlineArray[1];
            string tempicon_name = perlineArray[2];
            ObjectInfo.ObjectType temptypeobj = ObjectInfo.ObjectType.Drug;
            switch (perlineArray[3]) 
            {
                case "Drug":  temptypeobj = ObjectInfo.ObjectType.Drug;  break;
                case "Equip": temptypeobj = ObjectInfo.ObjectType.Equip; break;
            }
            objInfo.id = tempid; 
			objInfo.name = tempname; 
			objInfo.icon_name = tempicon_name;
            objInfo.type = temptypeobj;
            if (temptypeobj ==  ObjectInfo.ObjectType.Drug) 
            {
                int temphp = int.Parse(perlineArray[4]);
                int tempmp = int.Parse(perlineArray[5]);
                int tempprice_sell = int.Parse(perlineArray[6]);
                int tempprice_buy = int.Parse(perlineArray[7]);
                objInfo.hp = temphp;  
                objInfo.mp = tempmp;
                objInfo.price_buy = tempprice_buy;  
                objInfo.price_sell = tempprice_sell;
            } 
            if(temptypeobj == ObjectInfo.ObjectType.Equip)
            {
                int tempattack = int.Parse(perlineArray[4]);
                int tempdef = int.Parse(perlineArray[5]);
                int tempspeed = int.Parse(perlineArray[6]);
                ObjectInfo.WearType temptypewear = ObjectInfo.WearType.Headgear;
                switch(perlineArray[7])
                {
                    case "Headgear" :  temptypewear = ObjectInfo.WearType.Headgear;  break;
                    case "Armor" :     temptypewear = ObjectInfo.WearType.Armor;     break;
                    case "Hand" :      temptypewear = ObjectInfo.WearType.Hand;      break;
                    case "Accessory" : temptypewear = ObjectInfo.WearType.Accessory; break;
                    case "Shoe" :      temptypewear = ObjectInfo.WearType.Shoe;      break;
                }
                PlayerStatusInfo.Playertype temptypesuit = PlayerStatusInfo.Playertype.Swordman;
                switch(perlineArray[8])
                {
                    case "Swordman" : temptypesuit = PlayerStatusInfo.Playertype.Swordman; break;
                    case "Magician" : temptypesuit = PlayerStatusInfo.Playertype.Magician; break;
                }
                int tempprice_sell = int.Parse(perlineArray[9]);
                int tempprice_buy = int.Parse(perlineArray[10]);
                objInfo.attack = tempattack;
                objInfo.def = tempdef;
                objInfo.speed = tempspeed;
                objInfo.weartype = temptypewear;
                objInfo.suittype = temptypesuit;
                objInfo.price_buy = tempprice_buy;  
                objInfo.price_sell = tempprice_sell;
            }
            objectInfoDict.Add(tempid, objInfo);
        }
    }
    /// <summary>
    /// 从字典中获取id对应的ObjectInfo对象
    /// </summary>
    /// <param name="id">物品id</param>
    /// <returns>返回id对应的ObjectInfo对象</returns>
    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectInfoDict.TryGetValue(id, out info);
        return info;
    }
}