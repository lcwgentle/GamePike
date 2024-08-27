using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Xml.Serialization;

public static class Tool
{
    static Transform uiParent;
    public static Transform UiParent
    {
        get
        {
            if (uiParent == null)
            {
                uiParent = GameObject.Find("Canvas").transform;
            }
            return uiParent;
        }
    }
    /// <summary>
    /// ����panel
    /// </summary>
    /// <param name="type">�������</param>
    /// <returns></returns>
    public static GameObject CreateUIPanel(PanelType type)
    {
        GameObject go = Resources.Load<GameObject>(type.ToString());
        if (go == null)
        {
            Debug.Log(type.ToString() + "������");
            return null;
        }
        else
        {
            GameObject panel = GameObject.Instantiate(go);
            panel.name = type.ToString();
            panel.transform.SetParent(UiParent, false);
            return panel;
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="cards">Ҫ�������</param>
    /// <param name="asc">����456</param>
    public static void Sort(List<Card> cards, bool asc)
    {
        cards.Sort(
            (Card a, Card b) =>
            {
                if (asc)
                    return a.CardWeight.CompareTo(b.CardWeight);
                else
                    return -a.CardWeight.CompareTo(b.CardWeight);
            });
    }

    /// <summary>
    /// ��ȡ�ƵĴ�С
    /// </summary>
    /// <param name="cards">������</param>
    /// <param name="cradType">��������</param>
    /// <returns></returns>
    public static int GetWeight(List<Card> cards, CradType cradType)
    {
        int totalWeight = 0;
        if (cradType == CradType.ThreeAndOne || cradType == CradType.ThreeAndTwo)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].CardWeight == cards[i + 1].CardWeight|| cards[i].CardWeight == cards[i + 2].CardWeight)
                {
                    totalWeight += (int)cards[i].CardWeight;
                    totalWeight *= 3;
                    break;
                }
            }
        }else if(cradType == CradType.Straight || cradType == CradType.DoubleStraght)
        {
            totalWeight = (int)cards[0].CardWeight;
        }else
        {
            for (int i = 0; i < cards.Count; i++)
            {
                totalWeight+= (int)cards[i].CardWeight;
            }
        }

        return totalWeight;
    }
    /// <summary>
    /// ���ݴ洢
    /// </summary>
    /// <param name="data"></param>
    public static void SaveData(GameData data)
    {
        using (Stream stream = new FileStream(Const.DataPath, FileMode.OpenOrCreate, FileAccess.Write))
        {
            using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
            {
                XmlSerializer xml = new XmlSerializer(data.GetType());
                xml.Serialize(sw, data);
            }
            // StreamWriter��Close�������Զ��ر�stream���������ﲻ��Ҫ�ٴιر�stream
        }
        // ע�⣺���ﲻ����Ҫ�ر�stream����Ϊ���Ѿ���using�����Զ��ر���
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <returns></returns>
    public static GameData GetData()
    {
        GameData data = new GameData();
        Stream stream = new FileStream(Const.DataPath, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(stream, true);
        XmlSerializer xml = new XmlSerializer(data.GetType());

        data = xml.Deserialize(sr) as GameData;
        stream.Close();
        sr.Close();

        return data;
    }
}
