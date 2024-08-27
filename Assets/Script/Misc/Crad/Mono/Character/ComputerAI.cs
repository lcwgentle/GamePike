using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 电脑出牌
/// </summary>
public class ComputerAI : MonoBehaviour
{
    public List<Card> selectCards = new List<Card>();

    public CradType currType = CradType.None;

    public void SmartSelectedCards(List<Card> cards,CradType cradType,int weight,int length,bool isBig)
    {
        cradType = isBig ? CradType.None : cradType;
        currType = cradType;

        selectCards.Clear();

        switch (cradType)
        {
            case CradType.None:
                //随机出牌
                selectCards = FindSmallestCards(cards);
                break;
            case CradType.Single:
                selectCards = FindSingle(cards, weight);
                break;
            case CradType.Double:
                selectCards = FindDouble(cards, weight);
                break;
            case CradType.Straight:
                selectCards = FindStraight(cards, weight,length);
                if(selectCards.Count==0)
                {
                    selectCards = FindBoom(cards, -1);
                    currType = CradType.Boom;
                    if(selectCards.Count==0)
                    {
                        selectCards = FindJokerBoom(cards);
                        currType = CradType.JokerBoom;
                    }
                }
                break;
            case CradType.DoubleStraght:
                selectCards = FindDoubleStraight(cards, weight, length);
                if (selectCards.Count == 0)
                {
                    selectCards = FindBoom(cards, -1);
                    currType = CradType.Boom;
                    if (selectCards.Count == 0)
                    {
                        selectCards = FindJokerBoom(cards);
                        currType = CradType.JokerBoom;
                    }
                }
                break;
            case CradType.TripleStraght:
                selectCards = FindBoom(cards, -1);
                currType = CradType.Boom;
                if (selectCards.Count == 0)
                {
                    selectCards = FindJokerBoom(cards);
                    currType = CradType.JokerBoom;
                }
                break;
            case CradType.Three:
                selectCards = FindThree(cards, weight);
                break;
            case CradType.ThreeAndOne:
                selectCards = FindThreeAndOne(cards, weight);
                break;
            case CradType.ThreeAndTwo:
                selectCards = FindThreeAndDouble(cards, weight);
                break;
            case CradType.Boom:
                selectCards = FindBoom(cards, weight);
                if (selectCards.Count == 0)
                {
                    selectCards = FindJokerBoom(cards);
                    currType = CradType.JokerBoom;
                }
                break;
            case CradType.JokerBoom:
                break;
        }
    }

    private List<Card> FindSmallestCards(List<Card> cards)
    {
        List<Card> select = new List<Card>();

        for(int i=12;i>=5;i--)
        {
            select = FindStraight(cards, -1, i);
            if(select.Count!=0)
            {
                currType = CradType.Straight;
                break;
            }
        }
        if(select.Count==0)
        {
            for(int i=0;i<36;i+=3)
            {
                select = FindThreeAndDouble(cards, i - 1);
                if(select.Count!=0)
                {
                    currType = CradType.ThreeAndTwo;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThreeAndOne(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CradType.ThreeAndOne;
                    break;
                }
            }
        }

        if (select.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThree(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CradType.Three;
                    break;
                }
            }
        }

        if (select.Count == 0)
        {
            for (int i = 0; i < 24; i += 2)
            {
                select = FindDouble(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CradType.Double;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            select = FindSingle(cards, -1);
            currType = CradType.Single;
        }

        return select;
    }

    public List<Card> FindSingle(List<Card> cards,int weight)
    {
        List<Card> select = new List<Card>();

        for(int i=0;i<cards.Count;i++)
        {
            if((int)cards[i].CardWeight>weight)
            {
                select.Add(cards[i]);
                break;
            }
        }
        return select;
    }

    public List<Card> FindDouble(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();

        for (int i = 0; i < cards.Count-1; i++)
        {
            if ((int)cards[i].CardWeight == (int)cards[i+1].CardWeight)
            {
                int totalWeight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight;
                if(totalWeight>weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i+1]);
                    break;
                }
            }
        }
        return select;
    }

    public List<Card> FindStraight(List<Card> cards, int minWeight,int length)
    {
        List<Card> select = new List<Card>();
        int counter = 1;
        List<int> indexList = new List<int>();

        for (int i = 0; i < cards.Count-4; i++)
        {
            int weight = (int)cards[i].CardWeight;

            if(weight>minWeight)
            {
                counter = 1;
                indexList.Clear();
                for(int j=i+1;j< cards.Count; j++)
                {
                    if(cards[j].CardWeight>Weight.One)
                    {
                        break;
                    }
                    if((int)cards[j].CardWeight-weight==counter)
                    {
                        counter++;
                        indexList.Add(j);
                    }
                    if(counter==length)
                    {
                        break;
                    }
                }
            }
            if (counter == length)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        if(counter==length)
        {
            for(int i=0;i<indexList.Count;i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }    
        return select;
    }

    public List<Card> FindDoubleStraight(List<Card> cards, int minWeight, int length)
    {
        List<Card> select = new List<Card>();
        int counter = 0;
        List<int> indexList = new List<int>();

        for (int i = 0; i < cards.Count - 4; i++)
        {
            int weight = (int)cards[i].CardWeight;

            if (weight > minWeight)
            {
                counter = 0;
                indexList.Clear();

                int temp = 0;
                for (int j = i + 1; j < cards.Count; j++)
                {
                    if (cards[j].CardWeight > Weight.One)
                    {
                        break;
                    }
                    if ((int)cards[j].CardWeight - weight == counter)
                    {
                        temp++;
                        if(temp%2==1)
                        {
                            counter++;
                        }
                        indexList.Add(j);
                    }
                    if (counter == length/2)
                    {
                        break;
                    }
                }
            }
            if (counter == length/2)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        if (counter == length/2)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }
        return select;
    }

    public List<Card> FindThree(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();

        for (int i = 0; i < cards.Count - 3; i++)
        {
            if ((int)cards[i].CardWeight == (int)cards[i + 1].CardWeight&& 
                (int)cards[i].CardWeight == (int)cards[i + 2].CardWeight)
            {
                int totalWeight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight+ (int)cards[i + 2].CardWeight;
                if (totalWeight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    break;
                }
            }
        }
        return select;
    }

    public List<Card> FindThreeAndDouble(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> three = FindThree(cards, weight);

        if(three.Count>0)
        {
            foreach(var card in three)
            {
                cards.Remove(card);
            }

            List<Card> two = FindDouble(cards, -1);

            if(two.Count!=0)
            {
                select.AddRange(three);
                select.AddRange(two);
            }
        }
        return select;
    }

    public List<Card> FindThreeAndOne(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> three = FindThree(cards, weight);

        if (three.Count > 0)
        {
            foreach (var card in three)
            {
                cards.Remove(card);
            }

            List<Card> one= FindSingle(cards, -1);

            if (one.Count != 0)
            {
                select.AddRange(three);
                select.AddRange(one);
            }
        }
        return select;
    }

    public List<Card> FindBoom(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();

        for (int i = 0; i < cards.Count - 4; i++)
        {
            if ((int)cards[i].CardWeight == (int)cards[i + 1].CardWeight &&
                (int)cards[i].CardWeight == (int)cards[i + 2].CardWeight&&
                (int)cards[i].CardWeight == (int)cards[i + 3].CardWeight)
            {
                int totalWeight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight + 
                    (int)cards[i + 2].CardWeight+ (int)cards[i + 3].CardWeight;
                if (totalWeight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    select.Add(cards[i + 3]);
                    break;
                }
            }
        }
        return select;
    }

    public List<Card> FindJokerBoom(List<Card> cards)
    {
        List<Card> select = new List<Card>();

        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i].CardWeight ==Weight.SJoker&& cards[i + 1].CardWeight==Weight.LJoker)
            {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    break;
            }
        }
        return select;
    }
}
