using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制玩家 电脑  头像 积分显示
/// </summary>
public class CharacterUI : MonoBehaviour
{
    public Image head;
    public Text score;
    public Text remain;

    private void Awake()
    {
        GameData data = Tool.GetData();
        if(gameObject.name== "ComputerLeft")
        {
            SetIntergation(data.computerLeftIntegartion);
        }else if (gameObject.name == "ComputerRight")
        {
            SetIntergation(data.computerRightIntegartion);
        }else if (gameObject.name == "Player")
        {
            SetIntergation(data.playerInteration);
        }
    }
    public void SetIdentity(Identity identity)
    {
        switch(identity)
        {
            case Identity.Farmer:
                head.sprite = Resources.Load<Sprite>("Pokers/Role_Farmer");
                break;
            case Identity.Landlord:
                head.sprite = Resources.Load<Sprite>("Pokers/Role_Landlord");
                break;
        }
    }
    /// <summary>
    /// 设置积分
    /// </summary>
    /// <param name="score"></param>
    public void SetIntergation(int score)
    {
        this.score.text = score.ToString();
    }
    /// <summary>
    /// 设置手牌
    /// </summary>
    /// <param name="score"></param>
    public void SetRemain(int number)
    {
        remain.text ="剩余手牌："+ number.ToString();
    }
}
