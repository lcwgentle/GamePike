using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 一副牌
/// </summary>
public class CradModel 
{
    CharacterType cType = CharacterType.Library;
    Queue<Card> cardLibrary = new Queue<Card>();        

    /// <summary>
    /// 剩余牌数
    /// </summary>
    public int CardCount
    {
        get
        {
            return cardLibrary.Count;
        }
    }
    /// <summary>
    /// 54张牌
    /// </summary>
    public void InitCardLibrary()
    {
        //52张牌
        for(int color=1;color<5;color++)
        {
            for(int weight=0;weight<13;weight++)
            {
                Colors c = (Colors)color;
                Weight w = (Weight)weight;
                string name = c.ToString() + w.ToString();
                Card card = new Card(name,c, w, cType);
                cardLibrary.Enqueue(card);
            }
        }

        Card sJoker = new Card("SJoker", Colors.None, Weight.SJoker, cType);
        Card lJoker = new Card("LJoker", Colors.None, Weight.LJoker, cType);
        cardLibrary.Enqueue(sJoker);
        cardLibrary.Enqueue(lJoker);
    }

    public void Shuffle()
    {
        List<Card> newList = new List<Card>();
        foreach(var card in cardLibrary)
        {
            int index = UnityEngine.Random.Range(0, newList.Count + 1);
            newList.Insert(index,card);
        }

        cardLibrary.Clear();
        foreach(var card in newList)
        {
            cardLibrary.Enqueue(card);
        }
        newList.Clear();
    }

    /// <summary>
    /// 最开始发牌
    /// </summary>
    /// <param name="sendTo"></param>
    public Card DealCard(CharacterType sendTo)
    {
        Card card = cardLibrary.Dequeue();
        card.BelongTo = sendTo;
        return card;
    }
}
