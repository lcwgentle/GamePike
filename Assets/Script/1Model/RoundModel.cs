using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 回合数据
/// </summary>
public class RoundModel 
{
    public bool isLandlord = false;
    public bool isWin = false;

    public static event Action<bool> PlayerHandle;
    public static event Action<ComputerSmartArgs> ComputerHandle;

    int currentWeight;
    int currentLength;

    CradType currentType;
    CharacterType biggestCharacter;
    CharacterType currentCharacter;

    /// <summary>
    /// 最大出牌人的出牌大小
    /// </summary>
    public int CurrentWeight { get => currentWeight; set => currentWeight = value; }
    /// <summary>
    /// 出牌长度
    /// </summary>
    public int CurrentLength { get => currentLength; set => currentLength = value; }
    /// <summary>
    /// 最大出牌者
    /// </summary>
    public CharacterType BiggestCharacter { get => biggestCharacter; set => biggestCharacter = value; }
    /// <summary>
    /// 现在该谁出牌
    /// </summary>
    public CharacterType CurrentCharacter { get => currentCharacter; set => currentCharacter = value; }
    /// <summary>
    /// 现在出牌类型
    /// </summary>
    public CradType CurrentType { get => currentType; set => currentType = value; }

    public void InitRount()
    {
        this.currentType = CradType.None;
        this.currentWeight = -1;
        this.CurrentLength = -1;
        this.biggestCharacter = CharacterType.Desk;
        this.CurrentCharacter = CharacterType.Desk;

    }
    /// <summary>
    /// 抢地主的人触发
    /// </summary>
    public void Start(CharacterType cType)
    {
        this.biggestCharacter = cType;
        this.CurrentCharacter = cType;
        BeginWith(cType);
    }
    /// <summary>
    /// 出牌
    /// </summary>
    public void BeginWith(CharacterType cType)
    {
        if(cType==CharacterType.Player)
        {
            //玩家出牌
            if(PlayerHandle!=null)
            {
                PlayerHandle(BiggestCharacter!=CharacterType.Player);
            }
        }else
        {
            //电脑出牌
            if(ComputerHandle!=null)
            {
                ComputerSmartArgs e = new ComputerSmartArgs()
                {
                    CradType = currentType,
                    Length = currentLength,
                    Weight = currentWeight,
                    IsBiggest = biggestCharacter,
                    CharacterType = currentCharacter
                };
                ComputerHandle(e);
            }    
        }
    }
    /// <summary>
    /// 轮换出牌
    /// </summary>
    public void Turn()
    {
        currentCharacter++;
        if(currentCharacter==CharacterType.Desk||currentCharacter==CharacterType.Library)
        {
            currentCharacter = CharacterType.Player;
        }
        BeginWith(currentCharacter);
    }
}
