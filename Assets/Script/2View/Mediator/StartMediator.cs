using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMediator : EventMediator
{
    [Inject]
    public StartView startView { get; set; }
    public override void OnRegister()
    {
        startView.one.onClick.AddListener(OnOneClick);
        startView.two.onClick.AddListener(OnTwoClick);
    }
    public override void OnRemove()
    {
        startView.one.onClick.RemoveListener(OnOneClick);
        startView.two.onClick.RemoveListener(OnTwoClick);
    }
    private void OnOneClick()
    {
        //1. ¸Ämodel
        //2.É¾³ýÃæ°å
        dispatcher.Dispatch(CommandEvent.ChangeMulitiple,1);
        Destroy(startView.gameObject);
    }
    private void OnTwoClick()
    {
        dispatcher.Dispatch(CommandEvent.ChangeMulitiple, 2);
        Destroy(startView.gameObject);
    }
}
