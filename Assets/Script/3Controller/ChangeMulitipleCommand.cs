using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMulitipleCommand : EventCommand
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    public override void Execute()
    {
        //��model
        IntegrationModel.Mulitple *= (int)evt.data;
        //������
        Tool.CreateUIPanel(PanelType.CharacterPanel);
        Tool.CreateUIPanel(PanelType.InteractionPnael);
    }
}
