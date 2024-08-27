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
        //¸Ämodel
        IntegrationModel.Mulitple *= (int)evt.data;
        //Ìí¼ÓÃæ°å
        Tool.CreateUIPanel(PanelType.CharacterPanel);
        Tool.CreateUIPanel(PanelType.InteractionPnael);
    }
}
