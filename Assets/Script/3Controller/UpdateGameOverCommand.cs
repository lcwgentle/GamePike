using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameOverCommand :EventCommand
{
    [Inject]
    public RoundModel RoundModel { get; set; }
    public override void Execute()
    {
        GameOverShowArgs e = new GameOverShowArgs()
        {
            isLandlord = RoundModel.isLandlord,
            isWin = RoundModel.isWin
        };

        dispatcher.Dispatch(ViewEvent.UpdateGameOver, e);
    }
}
