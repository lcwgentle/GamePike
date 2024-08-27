using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : CharacterBase
{
    public CharacterUI characterUI;

    Identity identity;

    /// <summary>
    /// ��ɫ���
    /// </summary>
    public Identity Identity 
    { 
        get
        {
            return identity;
        }
        set
        {
            identity = value;
            characterUI.SetIdentity(value);
        }
    }

    public override void AddCard(Card card, bool selected)
    {
        base.AddCard(card, selected);
        characterUI.SetRemain(CardCount);


    }

    public override Card DealCard()
    {
        Card card= base.DealCard();
        characterUI.SetRemain(CardCount);
        return card;
    }

    List<Card> tempCard = null;
    List<CardUI> tempUI = null;
    /// <summary>
    /// �ҵ�ѡ�е�����
    /// </summary>
    /// <returns></returns>
    public List<Card> FindSelectCard()
    {
        CardUI[] cardUIs = CreatePoint.GetComponentsInChildren<CardUI>();
        tempCard = new List<Card>();
        tempUI = new List<CardUI>();
        for(int i=0;i<cardUIs.Length;i++)
        {
            if(cardUIs[i].IsSelected)
            {
                tempUI.Add(cardUIs[i]);
                tempCard.Add(cardUIs[i].Card);
            }
        }
        Tool.Sort(tempCard,true);
        return tempCard;
    }

    /// <summary>
    /// ɾ������/�ɹ�����
    /// </summary>

    public void DestorySelectedCard()
    {
        if (tempCard == null || tempUI == null)
            return;
        else
        {
            for(int i=0;i<tempCard.Count;i++)
            {
                tempUI[i].Destory();
                CardList.Remove(tempCard[i]);
            }
            SortCardUI(CardList);
            characterUI.SetRemain(CardCount);
        }
    }
}
