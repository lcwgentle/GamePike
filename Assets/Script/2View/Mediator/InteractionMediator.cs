using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMediator :EventMediator
{
    [Inject]
    public InteractionView InteractionView { get; set; }
    [Inject]
    public RoundModel RoundModel { get; set; }
    public override void OnRegister()
    {
        InteractionView.Deal.onClick.AddListener(OnDealClick);
        InteractionView.Grab.onClick.AddListener(OnGrabClick);
        InteractionView.DisGrab.onClick.AddListener(OnDisGrabClick);
        InteractionView.Play.onClick.AddListener(OnPlayClick);
        InteractionView.Pass.onClick.AddListener(OnPassClick);


        dispatcher.AddListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.AddListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.AddListener(ViewEvent.RestartGame, OnRestartGame);

        RoundModel.PlayerHandle += RoundModel_PlayerHandle;
    }


    public override void OnRemove()
    {
        InteractionView.Deal.onClick.RemoveListener(OnDealClick);
        InteractionView.Grab.onClick.RemoveListener(OnGrabClick);
        InteractionView.DisGrab.onClick.RemoveListener(OnDisGrabClick);
        InteractionView.Play.onClick.RemoveListener(OnPlayClick);
        InteractionView.Pass.onClick.RemoveListener(OnPassClick);

        dispatcher.RemoveListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.RestartGame, OnRestartGame);

        RoundModel.PlayerHandle -= RoundModel_PlayerHandle;
    }

    private void OnDealClick()
    {
        dispatcher.Dispatch(CommandEvent.RequestDeal);
        InteractionView.DeactiveAll();
    }
    /// <summary>
    /// 发牌结束
    /// </summary>
    /// <param name="payload"></param>
    private void OnCompleteDeal(IEvent payload)
    {
        InteractionView.ActiveGrapAndDisGrap();
    }
    /// <summary>
    /// 抢地主
    /// </summary>
    private void OnGrabClick()
    {
        InteractionView.DeactiveAll();
        GrabAndDisGrabArgs e = new GrabAndDisGrabArgs() { cType = CharacterType.Player };
        dispatcher.Dispatch(CommandEvent.GrabLandlord, e);
        Sound.Instance.PlayEffect(Const.Grab);
    }
    /// <summary>
    /// 不抢地主
    /// </summary>
    private void OnDisGrabClick()
    {
        InteractionView.DeactiveAll();
        CharacterType temp = (CharacterType)UnityEngine.Random.Range(2, 4);
        GrabAndDisGrabArgs e = new GrabAndDisGrabArgs() { cType = temp };
        dispatcher.Dispatch(CommandEvent.GrabLandlord, e);
        Sound.Instance.PlayEffect(Const.DisGrab);
    }

    private void RoundModel_PlayerHandle(bool canClick)
    {
        InteractionView.ActiveDealAndPass(canClick);
    }
    private void OnPlayClick()
    {
        dispatcher.Dispatch(ViewEvent.RequestPlay);
    }
    private void OnSuccessPlay()
    {
        InteractionView.DeactiveAll();
    }
    private void OnPassClick()
    {
        InteractionView.DeactiveAll();
        dispatcher.Dispatch(CommandEvent.PassCard);
    }
    private void OnRestartGame()
    {
        InteractionView.DeactiveAll();
        InteractionView.AvtivePlay();
    }
}
