using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������� ����  ͷ�� ������ʾ
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
    /// ���û���
    /// </summary>
    /// <param name="score"></param>
    public void SetIntergation(int score)
    {
        this.score.text = score.ToString();
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="score"></param>
    public void SetRemain(int number)
    {
        remain.text ="ʣ�����ƣ�"+ number.ToString();
    }
}
