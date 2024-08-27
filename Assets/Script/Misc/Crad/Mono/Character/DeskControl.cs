using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskControl : CharacterBase
{
    public DeskUI deskUI;
    /// <summary>
    /// player computer的手牌
    /// </summary>
    List<Card> playerCardList = new List<Card>();
   
    public List<Card> PlayerCardList { get => playerCardList; }

    List<Card> computerLeftCardList = new List<Card>();
    public List<Card> ComputerLeftCardList { get => computerLeftCardList; }

    List<Card> computerRightCardList = new List<Card>();
    public List<Card> ComputerRightCardList { get => computerRightCardList; }

    /// <summary>
    /// player computer的手牌生成的位置
    /// </summary>
    Transform playerPoint;
    public Transform PlayerPoint
    {
        get
        {
            if (playerPoint == null)
                playerPoint = transform.Find("PlayerPoint").transform;
            return playerPoint;
        }
    }
    Transform computerLeftPoint;
    public Transform ComputerLeftPoint
    {
        get
        {
            if (computerLeftPoint == null)
                computerLeftPoint = transform.Find("ComputerLeftPoint").transform;
            return computerLeftPoint;
        }
    }
    Transform computerRightPoint;
    public Transform ComputerRightPoint
    {
        get
        {
            if (computerRightPoint == null)
                computerRightPoint = transform.Find("ComputerRightPoint").transform;
            return computerRightPoint;
        }
    }

    public void SetShowCard(Card card,int index)
    {
        deskUI.SetShowCard(card,index);
    }

    public void CreateCradUI(Card card, int index, bool isSelected,ShowPoint pos)
    {
        GameObject go = LeanPool.Spawn(prefab);
        go.name = characterType.ToString() + index.ToString();
        CardUI cardUI = go.GetComponent<CardUI>();
        cardUI.Card = card;
        cardUI.IsSelected = isSelected;
        switch (pos) 
        {
            case ShowPoint.Player:
                cardUI.SetPosition(PlayerPoint, index);
                break;
            case ShowPoint.ComputerRight:
                cardUI.SetPosition(ComputerRightPoint, index);
                break;
            case ShowPoint.ComputerLeft:
                cardUI.SetPosition(ComputerLeftPoint, index);
                break;
            case ShowPoint.Desk:
                cardUI.SetPosition(CreatePoint, index);
                break;
        }
    }

    public  void AddCard(Card card, bool selected,ShowPoint pos)
    {
        switch (pos)
        {
            case ShowPoint.Player:
                PlayerCardList.Add(card);
                //先设置牌属于谁
                card.BelongTo = characterType;
                CreateCradUI(card, PlayerCardList.Count - 1, selected, pos);
                break;
            case ShowPoint.ComputerRight:
                ComputerRightCardList.Add(card);
                //先设置牌属于谁
                card.BelongTo = characterType;
                CreateCradUI(card, ComputerRightCardList.Count - 1, selected, pos);
                break;
            case ShowPoint.ComputerLeft:
                ComputerLeftCardList.Add(card);
                //先设置牌属于谁
                card.BelongTo = characterType;
                CreateCradUI(card, ComputerLeftCardList.Count - 1, selected, pos);
                break;
            case ShowPoint.Desk:
                CardList.Add(card);
                //先设置牌属于谁
                card.BelongTo = characterType;
                CreateCradUI(card, CardList.Count - 1, selected,pos);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 桌面清空
    /// </summary>
    /// <param name="pos"></param>
    public void Clear(ShowPoint pos)
    {
        switch (pos)
        {
            case ShowPoint.Player:
                PlayerCardList.Clear();
                CardUI[] cardUIPlayer = PlayerPoint.GetComponentsInChildren<CardUI>();
                for(int i=0;i<cardUIPlayer.Length;i++)
                {
                    cardUIPlayer[i].Destory();
                }
                break;
            case ShowPoint.ComputerRight:
                ComputerRightCardList.Clear();
                CardUI[] cardUIRight = ComputerRightPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUIRight.Length; i++)
                {
                    cardUIRight[i].Destory();
                }
                break;
            case ShowPoint.ComputerLeft:
                ComputerLeftCardList.Clear();
                CardUI[] cardUILeft = ComputerLeftPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUILeft.Length; i++)
                {
                    cardUILeft[i].Destory();
                }
                break;
            case ShowPoint.Desk:
                CardList.Clear();
                CardUI[] cardUIs = CreatePoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUIs.Length; i++)
                {
                    cardUIs[i].Destory();
                }
                break;
        }
    }
}
