using strange.extensions.command.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestDealCommand : EventCommand
{
    [Inject]
    public CradModel CradModel { get; set; }

    public DeskControl DeskControl { get { return GameObject.FindObjectOfType<DeskControl>(); } }
    public override void Execute()
    {
        CradModel.Shuffle();
        DeskControl.StartCoroutine(DealCard());
    }

    IEnumerator DealCard()
    {
        CharacterType curr = CharacterType.Player;
        for(int i=0;i<51;i++)
        {
            if(curr==CharacterType.Library|| curr == CharacterType.Desk)
            {
                curr = CharacterType.Player;
            }
            Deal(curr);
            curr++;
            yield return new WaitForSeconds(0.08f);
        }

        for(int i=0;i<3;i++)
        {
            Deal(CharacterType.Desk);
        }

        CardUI[] cardUIs = DeskControl.CreatePoint.GetComponentsInChildren<CardUI>();
        foreach(var ui in cardUIs)
        {
            ui.SetImageAgain();
        }
        //·¢ÅÆ½áÊø
        dispatcher.Dispatch(ViewEvent.CompleteDeal);
    }

    private void Deal(CharacterType curr)
    {
        Card card = CradModel.DealCard(curr);
        DealCardArgs e = new DealCardArgs() { card = card, cType = curr, isSelect = false };
        dispatcher.Dispatch(ViewEvent.DealCard,e);
    }
}
