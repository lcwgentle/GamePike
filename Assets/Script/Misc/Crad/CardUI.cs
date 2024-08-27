using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    Card card;
    Image image;
    bool isSelected;
    LearnButton btn;

    public Card Card { get => card; set { card = value;SetImage(); } }

    /// <summary>
    /// �����Ƿ���
    /// </summary>
    public bool IsSelected { get => isSelected; 
        set 
        {
            if (card.BelongTo != CharacterType.Player||IsSelected==value)
                return;
            if(value)
            {
                transform.localPosition += Vector3.up * 10;
            }else
            {
                transform.localPosition -= Vector3.up * 10;
            }
            isSelected = value;
        } 
    }

    /// <summary>
    /// ����ͼƬ
    /// </summary>
    public void SetImage()
    {
        if(card.BelongTo==CharacterType.Player||card.BelongTo==CharacterType.Desk)
        {
            image.sprite = Resources.Load<Sprite>("Pokers/"+card.CardName);
        }
        else  //��ʾ����
        {
            image.sprite = Resources.Load<Sprite>("Pokers/FixedBack");
        }
    }
    /// <summary>
    /// ��һ�ε����� 
    /// </summary>
    public void SetImageAgain()
    {
        image.sprite = Resources.Load<Sprite>("Pokers/CardBack");
    }

    /// <summary>
    /// ����λ���Լ�ƫ��
    /// </summary>
    /// <param name="parent">������</param>
    /// <param name="index">����������</param>
    public void SetPosition(Transform parent,int index)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(index);

        if(card.BelongTo==CharacterType.Desk||card.BelongTo==CharacterType.Player)
        {
            transform.localPosition = Vector3.right * index*20;
            //��ֹ��ԭ
            if(isSelected)
            {
                transform.localPosition += Vector3.up * 10;
            }
           
        }
        else if (card.BelongTo == CharacterType.ComputerRight || card.BelongTo == CharacterType.ComputerLeft)
        {
            transform.localPosition = -Vector3.up * 8 * index + Vector3.left * 8 * index;
        }
    }

    public void OnSpawn()
    {
        image = GetComponent<Image>();
        btn = GetComponent<LearnButton>();
        btn.PressedButton += Btn_PressedButton;
        btn.HighlightedButton += Btn_HighlightedButton;
    }

    private void Btn_HighlightedButton()
    {
        if(Input.GetMouseButton(1))
        {
            if (card.BelongTo == CharacterType.Player)
            {
                IsSelected = !isSelected;
                Sound.Instance.PlayEffect(Const.Select);
            }
        }
    }

    private void Btn_PressedButton()
    {
        if (card.BelongTo == CharacterType.Player)
        {
            IsSelected = !isSelected;
            Sound.Instance.PlayEffect(Const.Select);
        }
    }

 
    public void OnDespawn()
    {
        btn.PressedButton -= Btn_PressedButton;
        btn.HighlightedButton -= Btn_HighlightedButton;
        IsSelected = false;
        image.sprite = null;
        card = null;
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Destory()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
