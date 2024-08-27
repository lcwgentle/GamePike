using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestUpdateCommand : EventCommand
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    public override void Execute()
    {
        GameData gameData = new GameData()
        {
            playerInteration = IntegrationModel.PlayerInteration,
            computerLeftIntegartion = IntegrationModel.ComputerLeftIntegartion,
            computerRightIntegartion = IntegrationModel.ComputerRightIntegartion
        };
        dispatcher.Dispatch(ViewEvent.UpdateIntegrationModel, gameData);
    }
}
