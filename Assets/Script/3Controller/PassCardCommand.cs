using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ²»³öÅÆ
/// </summary>
public class PassCardCommand :Command
{
   [Inject]
   public RoundModel RoundModel { get; set; }

    public override void Execute()
    {
        RoundModel.Turn();
        Sound.Instance.PlayEffect(Const.PassCard[Random.Range(0, 3)]);
    }
}
