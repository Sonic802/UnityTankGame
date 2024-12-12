using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



/// <summary>
/// ���ݹ����� ͬһ�������ݵĴ洢�Ͷ�ȡ
/// </summary>
public class PlayerPrefsDataMgr 
{
    //����ĵ���ģʽ
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
    /// �洢����
    /// </summary>
    /// <param name="data">���ݶ���</param>
    /// <param name="keyName">���ݶ����ΨһKey</param>
    public void SaveData(object data,string keyName)
    {
        //ͨ��Type�õ��������ݶ�������е� �ֶΣ�Ȼ����PlayerPrefs���д洢 

        //step1����ȡ�������ݶ���������ֶ�
        Type dataType = data.GetType();
        FieldInfo[] infos = dataType.GetFields();


        //step2:�Զ���һ��Key���򣬽������ݴ洢
        //PlayerPrefs�洢Ҫ��֤Key��Ψһ��
        //�Զ��壺keyName_��������_�ֶ�����_�ֶ���

        //step3;������Щ�ֶ� �������ݴ洢
        string saveKeyName = "";
        for (int i = 0; i < infos.Length; i++)
        {
            //�� ÿһ���ֶν��д洢
            //�õ������ĳ���ֶ���Ϣ
            FieldInfo info = infos[i];
            //ͨ��FieldInfo��ȡ�ֶε����ͺ�����
            //�ֶ����ͣ�info.FieldType.Name
            //�ֶ����֣�info.Name
            //eg. Player1_Player_Int32_age
            saveKeyName = keyName + "_" + dataType.Name + "_" + info.FieldType.Name + "_" + info.Name;

            //�õ���Key�� ͨ��PlayerPrefs�������ݴ洢
            //��λ�ȡֵ
            //info.GetValue(data);

            //��װ��һ��������ר�����洢ֵ
            SaveValue(info.GetValue(data), saveKeyName);

        }

        PlayerPrefs.Save();
    }

    private void SaveValue(object value,string keyName)
    {
        //ͨ��PlayerPrefs�������ݴ洢
        //�����������͵Ĳ�ͬ���������ĸ�API���洢
        //PlayerPrefsֻ֧��3��
        //�ж� ����������ʲô���� Ȼ����þ��巽��
        Type fieldType = value.GetType();

        //�����ж�
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
            //�Լ��� �洢bool���� 1 true , 0 false
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        //����ж� �����������
        //ͨ������ �ж� ���ӹ�ϵ
        //�൱���ж� �ֶ��ǲ���IList������
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            //����װ���� 
            IList list = value as IList;
            //�ȴ洢����
            PlayerPrefs.SetInt(keyName, list.Count);

            int index = 0;//��֤Key��Ψһ�ԣ�
            foreach (object item in list)
            {
                //�洢�����ֵ 
                //ʹ�õݹ��ԭ����Ϊ��֪�����;��������
                SaveValue(item, keyName+ index);   
                index++;
            }
        }
        //�ж��Ƿ���Dictionary���� ͨ��Dictionary�ĸ������ж�
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            IDictionary dict = value as IDictionary;
            //�ȴ�������
            PlayerPrefs.SetInt(keyName, dict.Count);
            int index = 0;
            foreach(object key in dict.Keys)
            {
                SaveValue(key, keyName + "_key_" + index);
                SaveValue(dict[key], keyName + "_value_" + index);
                index++;
            }
        }
        //���ǻ����������� �ж�Ϊ�Զ������� �ٴδ���SaveData�������Զ�������зֽ⣬��һ�������е��ֶ�
        //ͨ���˴��� ������ܵõ���������
        else
        {
            SaveData(value, keyName);
        }

    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="type">��Ҫ��ȡ���ݵ� ��������</param>
    /// <param name="keyName">���ݶ����ΨһKey</param>
    /// <returns></returns>
    public object LoadData(Type type,string keyName)
    {
        //ʹ��Type���룬���ⲿ��Լһ�д��룺
        //�����object�������ȡPlayer���͵����ݣ���Ҫ���ⲿnewһ��������
        //����Type���������ڲ���̬����һ�����󷵻س���

        //���ݴ�������ͺ�keyName����ϴ洢����ʱKey��ƴ�ӹ���
        //�������ݵĻ�ȡ��ֵ ������
        
        //���ݴ����Type ����һ������ ���ڴ洢����
        object data = Activator.CreateInstance(type);
        //Ҫ�����new�����Ķ������������
        //�õ������ֶ�
        FieldInfo[] infos =type.GetFields();
        //����ƴ��key���ַ���
        string loadKeyName = "";
        //���ڴ洢 �����ֶε� ����
        FieldInfo info;
        for (int i = 0;i<infos.Length;i++)
        {
            info = infos[i];
            //key��ƴ�ӹ���һ��Ҫ�ʹ洢ʱһ�������������ҵ���Ӧ���ݣ�
            loadKeyName = keyName + "_" + type.Name + "_" + info.FieldType.Name + "_" + info.Name;

            //��key�Ϳ��Խ��PlayerPrefs����ȡ����
            //��������ݵ�data��
            info.SetValue(data, LoadValue(info.FieldType,loadKeyName));

        }        

        return data;
    }
    /// <summary>
    /// �õ��������ݵķ���
    /// </summary>
    /// <param name="fieldType">�ֶ����� �����ж� ���ĸ�API��ȡ</param>
    /// <param name="keyName">���ڻ�ȡ����</param>
    /// <returns></returns>
    public object LoadValue(Type fieldType,string keyName)
    {
        //�����ֶ������ж����ĸ�API��ȡ
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
            //�����Զ���洢bool���� ������ֵ�Ļ�ȡ
            return PlayerPrefs.GetInt(keyName, 0) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //�õ�����
            int count = PlayerPrefs.GetInt(keyName, 0);
            //ʵ����һ��List���� ���и�ֵ
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                //Ŀ�ģ��õ�List�з��͵�����
                //ֻ��һ������ ������0
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //�õ��ֵ䳤��
            int count = PlayerPrefs.GetInt(keyName, 0);
            //ʵ����һ���ֵ�
            IDictionary dict = Activator.CreateInstance(fieldType) as IDictionary;
            //�õ���������
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
