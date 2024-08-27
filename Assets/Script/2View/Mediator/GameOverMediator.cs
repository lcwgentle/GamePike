using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMediator : EventMediator
{

    [Inject]
   public GameOverView GameOverView { get; set; }
    public override void OnRegister()
    {
        dispatcher.AddListener(ViewEvent.UpdateGameOver, OnUpdateGameOver);
        GameOverView.continueBtn.onClick.AddListener(OnRestartClick);

        dispatcher.Dispatch(CommandEvent.UpdateGameOver);
    }

    
    public override void OnRemove()
    {
        dispatcher.RemoveListener(ViewEvent.UpdateGameOver, OnUpdateGameOver);
        GameOverView.continueBtn.onClick.RemoveListener(OnRestartClick);
    }
    private void OnUpdateGameOver(IEvent evt)
    {
        GameOverShowArgs e = (GameOverShowArgs)evt.data;
        GameOverView.Init(e.isLandlord, e.isWin);
    }
    private void OnRestartClick()
    {
        Destroy(this.gameObject);
        dispatcher.Dispatch(ViewEvent.RestartGame);
    }

}
