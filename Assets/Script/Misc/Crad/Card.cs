using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每一张牌持有的数据
/// </summary>
public class Card 
{
    string cardName;
    Weight cardWeight;
    Colors cardColor;
    CharacterType belongTo;

    public string CardName { get => cardName;  }
    public Weight CardWeight { get => cardWeight; }
    public Colors CardColor { get => cardColor; }
    public CharacterType BelongTo { get => belongTo; set => belongTo = value; }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="name">卡牌名字</param>
    /// <param name="color">卡牌花色</param>
    /// <param name="weight">卡牌大小</param>
    /// <param name="characterType">归属与谁</param>
    public Card(string name,Colors color,Weight weight,CharacterType characterType)
    {
        this.cardName = name;
        this.cardColor = color;
        this.cardWeight = weight;
        this.belongTo = characterType;
    }
}
