using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �غ�����
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
    /// �������˵ĳ��ƴ�С
    /// </summary>
    public int CurrentWeight { get => currentWeight; set => currentWeight = value; }
    /// <summary>
    /// ���Ƴ���
    /// </summary>
    public int CurrentLength { get => currentLength; set => currentLength = value; }
    /// <summary>
    /// ��������
    /// </summary>
    public CharacterType BiggestCharacter { get => biggestCharacter; set => biggestCharacter = value; }
    /// <summary>
    /// ���ڸ�˭����
    /// </summary>
    public CharacterType CurrentCharacter { get => currentCharacter; set => currentCharacter = value; }
    /// <summary>
    /// ���ڳ�������
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
    /// ���������˴���
    /// </summary>
    public void Start(CharacterType cType)
    {
        this.biggestCharacter = cType;
        this.CurrentCharacter = cType;
        BeginWith(cType);
    }
    /// <summary>
    /// ����
    /// </summary>
    public void BeginWith(CharacterType cType)
    {
        if(cType==CharacterType.Player)
        {
            //��ҳ���
            if(PlayerHandle!=null)
            {
                PlayerHandle(BiggestCharacter!=CharacterType.Player);
            }
        }else
        {
            //���Գ���
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
    /// �ֻ�����
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
