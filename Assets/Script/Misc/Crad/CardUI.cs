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
    /// 处理是否点击
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
    /// 设置图片
    /// </summary>
    public void SetImage()
    {
        if(card.BelongTo==CharacterType.Player||card.BelongTo==CharacterType.Desk)
        {
            image.sprite = Resources.Load<Sprite>("Pokers/"+card.CardName);
        }
        else  //显示背面
        {
            image.sprite = Resources.Load<Sprite>("Pokers/FixedBack");
        }
    }
    /// <summary>
    /// 第一次地主牌 
    /// </summary>
    public void SetImageAgain()
    {
        image.sprite = Resources.Load<Sprite>("Pokers/CardBack");
    }

    /// <summary>
    /// 设置位置以及偏移
    /// </summary>
    /// <param name="parent">父物体</param>
    /// <param name="index">子物体索引</param>
    public void SetPosition(Transform parent,int index)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(index);

        if(card.BelongTo==CharacterType.Desk||card.BelongTo==CharacterType.Player)
        {
            transform.localPosition = Vector3.right * index*20;
            //防止还原
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
    /// 回收
    /// </summary>
    public void Destory()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
}
