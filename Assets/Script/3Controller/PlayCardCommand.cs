using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardCommand : EventCommand
{
    [Inject]
    public RoundModel RoundModel { get; set; }

    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    public override void Execute()
    {
        PlayCardArgs e = (PlayCardArgs)evt.data;

        if(e.characterType==CharacterType.Player)
        {
            if (e.CradType == RoundModel.CurrentType &&e.length==RoundModel.CurrentLength&& e.weight > RoundModel.CurrentWeight)
            {
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            }
           else if(e.CradType==CradType.Boom&&RoundModel.CurrentType!=CradType.Boom)
            {
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            }
           else if(e.CradType==CradType.JokerBoom)
            {
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            }
           else if(e.characterType==RoundModel.BiggestCharacter)
            {
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            }else
            {
                Debug.Log("重新选择");
                return;
            }
        }
        if(e.characterType!=RoundModel.BiggestCharacter&&e.CradType!=CradType.Single&&e.CradType!=CradType.Double)
        {
            Sound.Instance.PlayEffect(Const.PlayCard[Random.Range(0, 3)]);
        }else
        {
            switch (e.CradType)  
            {
                case CradType.None:
                    break;
                case CradType.Single:
                    Sound.Instance.PlayEffect(Const.Single[e.weight]);
                    break;
                case CradType.Double:
                    Sound.Instance.PlayEffect(Const.Double[e.weight/2]);
                    break;
                case CradType.Straight:
                    Sound.Instance.PlayEffect(Const.Straight);
                    break;
                case CradType.DoubleStraght:
                    Sound.Instance.PlayEffect(Const.DoubleStraight);
                    break;
                case CradType.TripleStraght:
                    Sound.Instance.PlayEffect(Const.TripleStraight);
                    break;
                case CradType.Three:
                    Sound.Instance.PlayEffect(Const.Three);
                    break;
                case CradType.ThreeAndOne:
                    Sound.Instance.PlayEffect(Const.ThreeAndOne);
                    break;
                case CradType.ThreeAndTwo:
                    Sound.Instance.PlayEffect(Const.ThreeAndTwo);
                    break;
                case CradType.Boom:
                    Sound.Instance.PlayEffect(Const.Boom);
                    break;
                case CradType.JokerBoom:
                    Sound.Instance.PlayEffect(Const.JokerBoom);
                    break;
            }
        }
        //更新数据
        RoundModel.BiggestCharacter = e.characterType;
        RoundModel.CurrentLength = e.length;
        RoundModel.CurrentWeight = e.weight;
        RoundModel.CurrentType = e.CradType;

        //积分翻倍
        if(e.CradType==CradType.Boom)
        {
            IntegrationModel.Mulitple *= 2;
        }else if(e.CradType==CradType.JokerBoom)
        {
            IntegrationModel.Mulitple *= 4;
        }

        RoundModel.Turn();
    }
}
