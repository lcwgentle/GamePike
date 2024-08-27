using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLandlordCommand : EventCommand
{
    [Inject]
    public RoundModel RoundModel { get; set; }

    public override void Execute()
    {
        GrabAndDisGrabArgs e = (GrabAndDisGrabArgs)evt.data;
        dispatcher.Dispatch(ViewEvent.DealThreeCard,e);
        RoundModel.Start(e.cType);
    }
}
