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
    /// 生成panel
    /// </summary>
    /// <param name="type">面板类型</param>
    /// <returns></returns>
    public static GameObject CreateUIPanel(PanelType type)
    {
        GameObject go = Resources.Load<GameObject>(type.ToString());
        if (go == null)
        {
            Debug.Log(type.ToString() + "不存在");
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
    /// 排序
    /// </summary>
    /// <param name="cards">要排序的牌</param>
    /// <param name="asc">升序456</param>
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
    /// 获取牌的大小
    /// </summary>
    /// <param name="cards">出的牌</param>
    /// <param name="cradType">出牌类型</param>
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
    /// 数据存储
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
            // StreamWriter的Close方法会自动关闭stream，所以这里不需要再次关闭stream
        }
        // 注意：这里不再需要关闭stream，因为它已经在using块中自动关闭了
    }

    /// <summary>
    /// 获取数据
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
