using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCommand : EventCommand
{
    [Inject]
    public RoundModel RoundModel { get; set; }
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    [Inject]
    public CradModel CradModel { get; set; }
    public override void Execute()
    {
        int temp = IntegrationModel.Result;
        GameOverArgs e = (GameOverArgs)evt.data;
        //��������
        if (e.PlayerWin)
        {
            IntegrationModel.PlayerInteration += temp;
        }
        else
        {
            IntegrationModel.PlayerInteration -= temp;
        }
        if (e.ComputerLeftWin)
        {
            IntegrationModel.ComputerLeftIntegartion += temp;
        }
        else
        {
            IntegrationModel.ComputerLeftIntegartion -= temp;
        }
        if (e.ComputerRightWin)
        {
            IntegrationModel.ComputerRightIntegartion += temp;
        }
        else
        {
            IntegrationModel.ComputerRightIntegartion -= temp;
        }

        RoundModel.isLandlord = e.isLandlord;
        RoundModel.isWin = e.PlayerWin;
        //�洢����
        GameData data = new GameData()
        {
            computerRightIntegartion = IntegrationModel.ComputerRightIntegartion,
            computerLeftIntegartion = IntegrationModel.ComputerLeftIntegartion,
            playerInteration = IntegrationModel.PlayerInteration
        };
        Tool.SaveData(data);

        //��ʾ����
        GameData gameData = new GameData()
        {
            playerInteration = IntegrationModel.PlayerInteration,
            computerLeftIntegartion = IntegrationModel.ComputerLeftIntegartion,
            computerRightIntegartion = IntegrationModel.ComputerRightIntegartion
        };
        dispatcher.Dispatch(ViewEvent.UpdateIntegrationModel, gameData);


        //������
        Tool.CreateUIPanel(PanelType.GameOverPanel);

        //�����Ϸ����
        RoundModel.InitRount();
        CradModel.InitCardLibrary();
    }
}
