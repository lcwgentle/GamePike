using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 判断能不能出牌
/// </summary>
public class Rulers 
{
    /// <summary>
    /// 可否出牌
    /// </summary>
    /// <param name="cards">传入的牌</param>
    /// <param name="type">出牌类型</param>
    /// <returns></returns>
   public static bool CanPop(List<Card> cards,out CradType type)
    {
        type = CradType.None;
        bool can = false;
        switch (cards.Count)
        {
            case 1:
                if(IsSingle(cards))
                {
                    can = true;
                    type = CradType.Single;
                }
                break;
            case 2:
                if(IsDouble(cards))
                {
                    can = true;
                    type = CradType.Double;
                }
                else if(IsJokerBoom(cards))
                {
                    can = true;
                    type = CradType.JokerBoom;
                }
                break;
            case 3:
                if (IsThree(cards))
                {
                    can = true;
                    type = CradType.Three;
                }
                break;
            case 4:
                if (IsBoom(cards))
                {
                    can = true;
                    type = CradType.Boom;
                }
                else if (IsThreeOne(cards))
                {
                    can = true;
                    type = CradType.ThreeAndOne;
                }
                break;
            case 5:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }
                else if (IsThreeTwo(cards))
                {
                    can = true;
                    type = CradType.ThreeAndTwo;
                }
                break;
            case 6:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }
                else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CradType.TripleStraght;
                }
                break;
            case 7:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }
                break;
            case 8:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }
                break;
            case 9:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }
                else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CradType.TripleStraght;
                }
                break;
            case 10:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }
                break;
            case 11:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }
                break;
            case 12:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CradType.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }
                else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CradType.TripleStraght;
                }
                break;
            case 13:
                break;
            case 14:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }
                break;
            case 15:
                if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CradType.TripleStraght;
                }
                break;
            case 16:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }
                break;
            case 17:
                break;
            case 18:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CradType.TripleStraght;
                }
                break;
            case 19:
                break;
            case 20:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CradType.DoubleStraght;
                }
                break;
            default:
                break;
        }
        return can;
    }
    public static bool IsSingle(List<Card> cards)
    {
        return cards.Count == 1;
    }
    /// <summary>
    /// 判断对子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>

    public static bool IsDouble(List<Card> cards)
    {
        if(cards.Count==2)
        {
            if(cards[0].CardWeight==cards[1].CardWeight)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 是否是顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsStraight(List<Card> cards)
    {
        if(cards.Count<5||cards.Count>12)
        {
            return false;
        }
        for(int i=0;i<cards.Count-1;i++)
        {
            if(cards[i+1].CardWeight-cards[i].CardWeight!=1)
            {
                return false;
            }
            //不能超过A
            if(cards[i].CardWeight>Weight.One|| cards[i+1].CardWeight > Weight.One)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 判断是否双顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsDoubleStraight(List<Card> cards)
    {
        if(cards.Count<6||cards.Count%2!=0)
        {
            return false;
        }
        for(int i=0;i<cards.Count-2;i+=2)
        {
            if (cards[i + 1].CardWeight != cards[i].CardWeight)
                return false;

            if (cards[i + 2].CardWeight - cards[i].CardWeight != 1)
            {
                return false;
            }
            //不能超过A
            if (cards[i].CardWeight > Weight.One || cards[i + 2].CardWeight > Weight.One)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 是否是飞机
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsTripleStraight(List<Card> cards)
    {
        if (cards.Count < 6 || cards.Count % 3!= 0)
        {
            return false;
        }
        for (int i = 0; i < cards.Count - 3; i++)
        {
            if (cards[i + 1].CardWeight != cards[i].CardWeight)
                return false;
            if (cards[i + 2].CardWeight != cards[i].CardWeight)
                return false;
            if (cards[i + 1].CardWeight != cards[i+2].CardWeight)
                return false;

            if (cards[i + 3].CardWeight - cards[i].CardWeight != 1)
            {
                return false;
            }
            //不能超过A
            if (cards[i].CardWeight > Weight.One || cards[i + 3].CardWeight > Weight.One)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 三不带
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThree(List<Card> cards)
    {
        if (cards.Count!=3)
        {
            return false;
        }
        
            if (cards[1].CardWeight != cards[0].CardWeight)
                return false;
            if (cards[1].CardWeight != cards[2].CardWeight)
                return false;
            if (cards[0].CardWeight != cards[2].CardWeight)
                return false;

        return true;
    }
    /// <summary>
    /// 三带一
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeOne(List<Card> cards)
    {
        if (cards.Count != 4)
        {
            return false;
        }

        if (cards[1].CardWeight == cards[0].CardWeight && cards[1].CardWeight == cards[2].CardWeight && cards[0].CardWeight == cards[2].CardWeight)
            return true;
        else if(cards[1].CardWeight == cards[3].CardWeight && cards[1].CardWeight == cards[2].CardWeight && cards[3].CardWeight == cards[2].CardWeight)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 三带二
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeTwo(List<Card> cards)
    {
        if (cards.Count != 5)
        {
            return false;
        }

        if (cards[1].CardWeight == cards[0].CardWeight && cards[1].CardWeight == cards[2].CardWeight && cards[0].CardWeight == cards[2].CardWeight)
        {
            if(cards[3].CardWeight == cards[4].CardWeight)
            return true;
        }
        else if (cards[4].CardWeight == cards[3].CardWeight && cards[4].CardWeight == cards[2].CardWeight && cards[3].CardWeight == cards[2].CardWeight)
        {
            if (cards[0].CardWeight == cards[1].CardWeight)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 炸弹
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsBoom(List<Card> cards)
    {
        if (cards.Count != 4)
        {
            return false;
        }

        if (cards[1].CardWeight != cards[0].CardWeight)
            return false;
        if (cards[1].CardWeight != cards[2].CardWeight)
            return false;
        if (cards[3].CardWeight != cards[2].CardWeight)
            return false;

        return true;
    }

    /// <summary>
    /// 王炸
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsJokerBoom(List<Card> cards)
    {
        if (cards.Count != 2)
        {
            return false;
        }

        if (cards[0].CardWeight==Weight.SJoker&& cards[1].CardWeight == Weight.LJoker )
            return true;

        return false;
    }
}
