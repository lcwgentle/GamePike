using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : View
{
    public PlayerControl playerControl;
    public DeskControl deskControl;
    public ComputerControl computerLeftControl;
    public ComputerControl computerRightControl;

    public void Init()
    {
        playerControl.Identity = Identity.Farmer;
        computerLeftControl.Identity = Identity.Farmer;
        computerRightControl.Identity = Identity.Farmer;
    }

    /// <summary>
    /// 添加牌
    /// </summary>
    /// <param name="cType">给谁发</param>
    /// <param name="card">发什么牌</param>
    /// <param name="isSelect">是否是地主牌</param>
    /// <param name="pos">桌子的位置</param>
    public void AddCard(CharacterType cType,Card card,bool isSelect,ShowPoint pos)
    {
        switch (cType)
        {
            case CharacterType.Player:
                playerControl.AddCard(card, isSelect);
                playerControl.Sort(false);
                break;
            case CharacterType.ComputerRight:
                computerRightControl.AddCard(card, isSelect);
                break;
            case CharacterType.ComputerLeft:
                computerLeftControl.AddCard(card, isSelect);
                break;
            case CharacterType.Desk:
                deskControl.AddCard(card, isSelect, pos);
                break;
        }
    }
    public void DealThreeCard(CharacterType cType)
    {
        Card card = null;
        switch (cType)
        {
            case CharacterType.Player:
                for(int i=0;i<3;i++)
                {
                    card=deskControl.DealCard();
                    playerControl.AddCard(card, true);
                    deskControl.SetShowCard(card, i);
                }
                playerControl.Identity = Identity.Landlord;
                playerControl.Sort(false);
                break;
            case CharacterType.ComputerRight:
                for (int i = 0; i < 3; i++)
                {
                    card = deskControl.DealCard();
                    computerRightControl.AddCard(card, false);
                    deskControl.SetShowCard(card, i);
                }
                computerRightControl.Identity = Identity.Landlord;
                computerRightControl.Sort(true);
                break;
            case CharacterType.ComputerLeft:
                for (int i = 0; i < 3; i++)
                {
                    card = deskControl.DealCard();
                    computerLeftControl.AddCard(card, false);
                    deskControl.SetShowCard(card, i);
                }
                computerLeftControl.Identity = Identity.Landlord;
                computerLeftControl.Sort(true);
                break;
        }
        deskControl.Clear(ShowPoint.Desk);   
    }
}
