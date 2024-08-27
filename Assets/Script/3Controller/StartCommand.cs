using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartCommand : Command
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }

    [Inject]
    public RoundModel RoundModel { get; set; }

    [Inject]
    public CradModel CradModel { get; set; }
    /// <summary>
    /// Ö´ÐÐÊÂ¼þ
    /// </summary>
    public override void Execute()
    {
        Tool.CreateUIPanel(PanelType.StartPanel);

        IntegrationModel.InitIntegration();
        RoundModel.InitRount();
        CradModel.InitCardLibrary();

        GetData();

        Sound.Instance.PlayBG(Const.Bg);
    }
    public void GetData()
    {
        FileInfo info = new FileInfo(Const.DataPath);
        if(info.Exists)
        {
            GameData data = Tool.GetData();
            IntegrationModel.PlayerInteration = data.playerInteration;
            IntegrationModel.ComputerLeftIntegartion = data.computerLeftIntegartion;
            IntegrationModel.ComputerRightIntegartion = data.computerRightIntegartion;
        }
    }
}
