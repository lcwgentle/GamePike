using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CharacterBase : MonoBehaviour
{
    public CharacterType characterType;

    public GameObject prefab;

    Transform createPoint;

    List<Card> cardList = new List<Card>();
    
    //手牌
    public List<Card> CardList { get => cardList;}

    /// <summary>
    /// 是否有牌
    /// </summary>
    public bool HashCard { get { return cardList.Count != 0; } }

    /// <summary>
    /// 牌数目
    /// </summary>
    public int CardCount { get { return cardList.Count; } }

    public Transform CreatePoint
    {
        get
        {
            if (createPoint == null)
                createPoint = transform.Find("CreatePoint");
            return createPoint;
        }

    }

    /// <summary>
    /// 添加牌
    /// </summary>
    /// <param name="card">添加的牌</param>
    /// <param name="selected">是否增高</param>
    public virtual void AddCard(Card card,bool selected)
    {
        cardList.Add(card);
        //先设置牌属于谁
        card.BelongTo = characterType;
        CreateCradUI(card, cardList.Count - 1, selected);
    }
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="asc"></param>
    public void Sort(bool asc)
    {
        Tool.Sort(CardList,asc);

        SortCardUI(CardList);
    }
    /// <summary>
    /// 排序CardUI
    /// </summary>
    /// <param name="cards">排好序的List</param>
    public void SortCardUI(List<Card> cards)
    {
        CardUI[] cardUIs= CreatePoint.GetComponentsInChildren<CardUI>();
        for(int i=0;i< cards.Count;i++)
        {
            for(int j=0;j<cardUIs.Length;j++)
            {
                if(cardUIs[j].Card== cards[i])
                {
                    cardUIs[j].SetPosition(CreatePoint, i);
                }
            }
        }
    }
    /// <summary>
    /// 根据数据创建cardUI
    /// </summary>
    /// <param name="card">数据</param>
    /// <param name="index">索引</param>
    /// <param name="isSelected">上升？</param>
    public void CreateCradUI(Card card,int index,bool isSelected)
    {
        GameObject go = LeanPool.Spawn(prefab);
        go.name = characterType.ToString()+index.ToString();
        CardUI cardUI = go.GetComponent<CardUI>();
        cardUI.Card = card;
        cardUI.IsSelected = isSelected;
        cardUI.SetPosition(CreatePoint, index);
    }

    /// <summary>
    /// 出牌
    /// </summary>
    /// <returns></returns>
    public virtual Card DealCard()
    {
        Card card = cardList[CardCount - 1];
        cardList.Remove(card);
        return card;
    }

}
