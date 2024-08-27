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
    
    //����
    public List<Card> CardList { get => cardList;}

    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public bool HashCard { get { return cardList.Count != 0; } }

    /// <summary>
    /// ����Ŀ
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
    /// �����
    /// </summary>
    /// <param name="card">��ӵ���</param>
    /// <param name="selected">�Ƿ�����</param>
    public virtual void AddCard(Card card,bool selected)
    {
        cardList.Add(card);
        //������������˭
        card.BelongTo = characterType;
        CreateCradUI(card, cardList.Count - 1, selected);
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="asc"></param>
    public void Sort(bool asc)
    {
        Tool.Sort(CardList,asc);

        SortCardUI(CardList);
    }
    /// <summary>
    /// ����CardUI
    /// </summary>
    /// <param name="cards">�ź����List</param>
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
    /// �������ݴ���cardUI
    /// </summary>
    /// <param name="card">����</param>
    /// <param name="index">����</param>
    /// <param name="isSelected">������</param>
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
    /// ����
    /// </summary>
    /// <returns></returns>
    public virtual Card DealCard()
    {
        Card card = cardList[CardCount - 1];
        cardList.Remove(card);
        return card;
    }

}
