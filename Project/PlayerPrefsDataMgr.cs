using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



/// <summary>
/// 数据管理类 同一管理数据的存储和读取
/// </summary>
public class PlayerPrefsDataMgr 
{
    //经典的单例模式
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();

    public static PlayerPrefsDataMgr Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerPrefsDataMgr()
    {

    }

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="keyName">数据对象的唯一Key</param>
    public void SaveData(object data,string keyName)
    {
        //通过Type得到传入数据对象的所有的 字段，然后结合PlayerPrefs进行存储 

        //step1：获取传入数据对象的所有字段
        Type dataType = data.GetType();
        FieldInfo[] infos = dataType.GetFields();


        //step2:自定义一个Key规则，进行数据存储
        //PlayerPrefs存储要保证Key的唯一性
        //自定义：keyName_数据类型_字段类型_字段名

        //step3;遍历这些字段 进行数据存储
        string saveKeyName = "";
        for (int i = 0; i < infos.Length; i++)
        {
            //对 每一个字段进行存储
            //得到具体的某个字段信息
            FieldInfo info = infos[i];
            //通过FieldInfo获取字段的类型和名字
            //字段类型：info.FieldType.Name
            //字段名字：info.Name
            //eg. Player1_Player_Int32_age
            saveKeyName = keyName + "_" + dataType.Name + "_" + info.FieldType.Name + "_" + info.Name;

            //得到了Key后 通过PlayerPrefs进行数据存储
            //如何获取值
            //info.GetValue(data);

            //封装了一个方法，专门来存储值
            SaveValue(info.GetValue(data), saveKeyName);

        }

        PlayerPrefs.Save();
    }

    private void SaveValue(object value,string keyName)
    {
        //通过PlayerPrefs进行数据存储
        //根据数据类型的不同来决定用哪个API来存储
        //PlayerPrefs只支持3种
        //判断 数据类型是什么类型 然后调用具体方法
        Type fieldType = value.GetType();

        //类型判断
        if (fieldType == typeof(int))
        {
            PlayerPrefs.SetInt(keyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            PlayerPrefs.SetFloat(keyName, (float)value);
        }
        else if (fieldType == typeof(string))
        {
            PlayerPrefs.SetString(keyName, value.ToString());
        }
        else if(fieldType == typeof(bool))
        {
            //自己定 存储bool规则 1 true , 0 false
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        //如何判断 泛型类的类型
        //通过反射 判断 父子关系
        //相当于判断 字段是不是IList的子类
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            //父类装子类 
            IList list = value as IList;
            //先存储数量
            PlayerPrefs.SetInt(keyName, list.Count);

            int index = 0;//保证Key的唯一性！
            foreach (object item in list)
            {
                //存储具体的值 
                //使用递归的原因，因为不知道泛型具体的类型
                SaveValue(item, keyName+ index);   
                index++;
            }
        }
        //判断是否是Dictionary类型 通过Dictionary的父类来判断
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            IDictionary dict = value as IDictionary;
            //先储存数量
            PlayerPrefs.SetInt(keyName, dict.Count);
            int index = 0;
            foreach(object key in dict.Keys)
            {
                SaveValue(key, keyName + "_key_" + index);
                SaveValue(dict[key], keyName + "_value_" + index);
                index++;
            }
        }
        //不是基础数据类型 判断为自定义类型 再次传入SaveData函数把自定义类进行分解，逐一遍历其中的字段
        //通过此处的 最后总能得到基础类型
        else
        {
            SaveData(value, keyName);
        }

    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="type">想要读取数据的 数据类型</param>
    /// <param name="keyName">数据对象的唯一Key</param>
    /// <returns></returns>
    public object LoadData(Type type,string keyName)
    {
        //使用Type传入，在外部节约一行代码：
        //如果用object，如果读取Player类型的数据，需要在外部new一个对象传入
        //传入Type，可以在内部动态创建一个对象返回出来

        //根据传入的类型和keyName，结合存储数据时Key的拼接规则
        //进行数据的获取赋值 并返回
        
        //根据传入的Type 创建一个对象 用于存储数据
        object data = Activator.CreateInstance(type);
        //要往这个new出来的对象中填充数据
        //得到所有字段
        FieldInfo[] infos =type.GetFields();
        //用于拼接key的字符串
        string loadKeyName = "";
        //用于存储 单个字段的 对象
        FieldInfo info;
        for (int i = 0;i<infos.Length;i++)
        {
            info = infos[i];
            //key的拼接规则一定要和存储时一样，这样才能找到对应数据！
            loadKeyName = keyName + "_" + type.Name + "_" + info.FieldType.Name + "_" + info.Name;

            //有key就可以结合PlayerPrefs来读取数据
            //并填充数据到data中
            info.SetValue(data, LoadValue(info.FieldType,loadKeyName));

        }        

        return data;
    }
    /// <summary>
    /// 得到单个数据的方法
    /// </summary>
    /// <param name="fieldType">字段类型 用于判断 用哪个API读取</param>
    /// <param name="keyName">用于获取数据</param>
    /// <returns></returns>
    public object LoadValue(Type fieldType,string keyName)
    {
        //根据字段类型判断用哪个API读取
        if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(keyName, 0);
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(keyName, 0);
        }
        else if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(keyName, "");
        }
        else if (fieldType == typeof(bool))
        {
            //根据自定义存储bool规则 来进行值的获取
            return PlayerPrefs.GetInt(keyName, 0) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //得到长度
            int count = PlayerPrefs.GetInt(keyName, 0);
            //实例化一个List对象 进行赋值
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                //目的：得到List中泛型的类型
                //只有一个泛型 所以填0
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //得到字典长度
            int count = PlayerPrefs.GetInt(keyName, 0);
            //实例化一个字典
            IDictionary dict = Activator.CreateInstance(fieldType) as IDictionary;
            //得到泛型类型
            Type[] kvType = fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                dict.Add(LoadValue(kvType[0], keyName + "_key_" + i),
                         LoadValue(kvType[1], keyName + "_value_" + i));
            }
            return dict;
        }
        else
        {
            return LoadData(fieldType, keyName);
        }
       
    }
}
