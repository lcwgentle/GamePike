using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMediator : EventMediator
{
    [Inject] 
    public CharacterView CharacterView { get; set; }
    public override void OnRegister()
    {
        CharacterView.Init();
        dispatcher.Dispatch(CommandEvent.RequestUpdate);
        dispatcher.AddListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.AddListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.AddListener(ViewEvent.DealThreeCard, OnDealThreeCrad);
        dispatcher.AddListener(ViewEvent.RequestPlay, OnPlayerPlayCard);
        dispatcher.AddListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.AddListener(ViewEvent.UpdateIntegrationModel, OnUpdateIntegrationModel);
        dispatcher.AddListener(ViewEvent.RestartGame, OnRestartGame);

        RoundModel.ComputerHandle += RoundModel_ComputerHandle;
        RoundModel.PlayerHandle += RoundModel_PlayerHandle;
    }


    public override void OnRemove()
    {
        dispatcher.RemoveListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.DealThreeCard, OnDealThreeCrad);
        dispatcher.RemoveListener(ViewEvent.RequestPlay, OnPlayerPlayCard);
        dispatcher.RemoveListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.RemoveListener(ViewEvent.UpdateIntegrationModel, OnUpdateIntegrationModel);
        dispatcher.RemoveListener(ViewEvent.RestartGame, OnRestartGame);

        RoundModel.ComputerHandle -= RoundModel_ComputerHandle;
        RoundModel.PlayerHandle -= RoundModel_PlayerHandle;
    }

    private void OnDealCard(IEvent evt)
    {
        DealCardArgs e = (DealCardArgs)evt.data;
        CharacterView.AddCard(e.cType, e.card, e.isSelect, ShowPoint.Desk);
        Sound.Instance.PlayEffect(Const.DealCard);
    }
    private void OnCompleteDeal()
    {
        CharacterView.computerLeftControl.Sort(true);
        CharacterView.computerRightControl.Sort(true);
        CharacterView.deskControl.Sort(true);
    }
    private void OnDealThreeCrad(IEvent evt)
    {
        GrabAndDisGrabArgs e = (GrabAndDisGrabArgs)evt.data;
        CharacterView.DealThreeCard(e.cType);
    }
    /// <summary>
    /// 玩家出牌
    /// </summary>
    private void OnPlayerPlayCard()
    {
        //不能直接出牌，需要判断
        List<Card> cards = CharacterView.playerControl.FindSelectCard();
        CradType cradType;
        if(Rulers.CanPop(cards, out cradType))
        {
            PlayCardArgs e = new PlayCardArgs()
            {
                CradType = cradType,
                characterType = CharacterType.Player,
                length = cards.Count,
                weight = Tool.GetWeight(cards, cradType)
            };
            dispatcher.Dispatch(CommandEvent.PlayCard, e);
        }
    }
    //成功出牌
    private void OnSuccessPlay()
    {
        List<Card> cardList = CharacterView.playerControl.FindSelectCard();

        //清空桌面
        CharacterView.deskControl.Clear(ShowPoint.Player);
        //添加的桌面
        foreach(var crad in cardList)
        {
            CharacterView.deskControl.AddCard(crad, false,ShowPoint.Player);
        }

        CharacterView.playerControl.DestorySelectedCard();

        if(!CharacterView.playerControl.HashCard)
        {
            Identity r = CharacterView.computerRightControl.Identity;
            Identity l = CharacterView.computerLeftControl.Identity;
            Identity p = CharacterView.playerControl.Identity;
            GameOverArgs eee = new GameOverArgs()
            {
                ComputerRightWin = r == p ? true : false,
                ComputerLeftWin = l == p ? true : false,
                PlayerWin = true,
                isLandlord = p == Identity.Landlord,
            };
            //游戏结束
            dispatcher.Dispatch(CommandEvent.GameOver,eee);
            Sound.Instance.PlayEffect(Const.Win);
        }
    }
    private void RoundModel_ComputerHandle(ComputerSmartArgs e)
    {
        StartCoroutine(Delay(e));
    }
    private IEnumerator Delay(ComputerSmartArgs e)
    {
        bool can = false;
        yield return new WaitForSeconds(1.5f);
        switch (e.CharacterType)
        {
            case CharacterType.ComputerRight:
                CharacterView.deskControl.Clear(ShowPoint.ComputerRight);
               
               can = CharacterView.computerRightControl.SmartSelectedCards(e.CradType, e.Weight, e.Length,
                    e.IsBiggest == CharacterType.ComputerRight);
                if(can)
                {
                    List<Card> cards = CharacterView.computerRightControl.SelectCards;
                    CradType CurrType = CharacterView.computerRightControl.CurrType;
                    foreach (var card in cards)
                    {
                        CharacterView.deskControl.AddCard(card, false, ShowPoint.ComputerRight);
                    }
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        CradType = CurrType,
                        length = cards.Count,
                        characterType = CharacterType.ComputerRight,
                        weight = Tool.GetWeight(cards, CurrType)
                    };
                    //判断胜负
                    if(!CharacterView.computerRightControl.HashCard)
                    {
                        //游戏结束
                        Identity r = CharacterView.computerRightControl.Identity;
                        Identity l = CharacterView.computerLeftControl.Identity;
                        Identity p = CharacterView.playerControl.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerRightWin = true,
                            ComputerLeftWin = l == r ? true : false,
                            PlayerWin = p == r ? true : false,
                            isLandlord = p == Identity.Landlord,

                        };
                        //游戏结束
                        dispatcher.Dispatch(CommandEvent.GameOver, eee);

                        if(eee.PlayerWin==true)
                        {
                            Sound.Instance.PlayEffect(Const.Win);
                        }else
                        {
                            Sound.Instance.PlayEffect(Const.Lose);
                        }
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                    }
                }else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            case CharacterType.ComputerLeft:
                CharacterView.deskControl.Clear(ShowPoint.ComputerLeft);
                can = CharacterView.computerLeftControl.SmartSelectedCards(e.CradType, e.Weight, e.Length,
                     e.IsBiggest == CharacterType.ComputerLeft);
                if (can)
                {
                    List<Card> cards = CharacterView.computerLeftControl.SelectCards;
                    CradType CurrType = CharacterView.computerLeftControl.CurrType;
                    foreach (var card in cards)
                    {
                        CharacterView.deskControl.AddCard(card, false, ShowPoint.ComputerLeft);
                    }
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        CradType = CurrType,
                        length = cards.Count,
                        characterType = CharacterType.ComputerLeft,
                        weight = Tool.GetWeight(cards, CurrType)
                    };
                    //判断胜负
                    if (!CharacterView.computerLeftControl.HashCard)
                    {
                        //游戏结束
                        Identity r = CharacterView.computerRightControl.Identity;
                        Identity l = CharacterView.computerLeftControl.Identity;
                        Identity p = CharacterView.playerControl.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerLeftWin = true,
                            ComputerRightWin = r == l ? true : false,
                            PlayerWin = p == l ? true : false,
                            isLandlord = p == Identity.Landlord,

                        };
                        //游戏结束
                        dispatcher.Dispatch(CommandEvent.GameOver, eee);
                        if (eee.PlayerWin == true)
                        {
                            Sound.Instance.PlayEffect(Const.Win);
                        }
                        else
                        {
                            Sound.Instance.PlayEffect(Const.Lose);
                        }
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                    }
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
        }
    }

    private void RoundModel_PlayerHandle(bool obj)
    {
        CharacterView.deskControl.Clear(ShowPoint.Player);
    }
    private void OnUpdateIntegrationModel(IEvent data)
    {
        GameData gameData = (GameData)data.data;
        CharacterView.playerControl.characterUI.SetIntergation(gameData.playerInteration);
        CharacterView.computerRightControl.characterUI.SetIntergation(gameData.computerRightIntegartion);
        CharacterView.computerLeftControl.characterUI.SetIntergation(gameData.computerLeftIntegartion);
    }
    private void OnRestartGame()
    {
        //对象池回收
        Lean.Pool.LeanPool.DespawnAll();
        //数据移除
        CharacterView.playerControl.CardList.Clear();
        CharacterView.computerLeftControl.CardList.Clear();
        CharacterView.computerRightControl.CardList.Clear();
        CharacterView.deskControl.CardList.Clear();
        //初始化UI
        CharacterView.Init();
        CharacterView.playerControl.characterUI.SetRemain(0);
        CharacterView.computerLeftControl.characterUI.SetRemain(0);
        CharacterView.computerRightControl.characterUI.SetRemain(0);
        CharacterView.deskControl.deskUI.SetAlpha(0);
    }
}
