using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerControl : CharacterBase
{
    public CharacterUI characterUI;

    public CanvasGroup group;

    public ComputerAI computerAI;

    /// <summary>
    /// ��ǰҪ������
    /// </summary>
    public List<Card> SelectCards
    {
        get
        {
            return computerAI.selectCards;
        }
    }
    /// <summary>
    /// ��ǰҪ���Ƶ�����
    /// </summary>
    public CradType CurrType
    {
        get
        {
            return computerAI.currType;
        }
    }

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
        Card card = base.DealCard();
        characterUI.SetRemain(CardCount);
        return card;
    }
    /// <summary>
    /// ���Գ���
    /// </summary>
    /// <param name="cards">����</param>
    /// <param name="cradType">��ǰ��������</param>
    /// <param name="weight">���ƴ�С</param>
    /// <param name="length">����</param>
    /// <param name="isBig">�ǲ���������</param>
    public bool SmartSelectedCards(CradType cradType, int weight, int length, bool isBig)
    {
        computerAI.SmartSelectedCards(CardList, cradType, weight, length, isBig);
        if(SelectCards.Count!=0)
        {
            //ɾ������
            DestoryCards();
            return true;
        }else
        {
            ComputerPass();
            return false;
        }
    }

    private void DestoryCards()
    {
        CardUI[] cardsUI = CreatePoint.GetComponentsInChildren<CardUI>();
        for(int i=0;i<cardsUI.Length;i++)
        {
            for(int j=0;j<SelectCards.Count;j++)
            {
                if(cardsUI[i].Card==SelectCards[j])
                {
                    cardsUI[i].Destory();
                    CardList.Remove(SelectCards[j]);
                }
            }
        }
        SortCardUI(CardList);
        characterUI.SetRemain(CardCount);
    }

    /// <summary>
    /// ��ʾpassUI
    /// </summary>
    public void ComputerPass()
    {
        group.alpha = 1;
        StartCoroutine(Pass());
    }
    IEnumerator Pass()
    {
        yield return new WaitForSeconds(1.5f);
        group.alpha = 0;
    }
}
