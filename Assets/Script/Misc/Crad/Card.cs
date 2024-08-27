using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÿһ���Ƴ��е�����
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
    /// ��ʼ��
    /// </summary>
    /// <param name="name">��������</param>
    /// <param name="color">���ƻ�ɫ</param>
    /// <param name="weight">���ƴ�С</param>
    /// <param name="characterType">������˭</param>
    public Card(string name,Colors color,Weight weight,CharacterType characterType)
    {
        this.cardName = name;
        this.cardColor = color;
        this.cardWeight = weight;
        this.belongTo = characterType;
    }
}
