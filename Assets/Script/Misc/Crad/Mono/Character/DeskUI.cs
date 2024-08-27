using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskUI : MonoBehaviour
{
    Transform showPoint;

    public Transform ShowPoint
    {
        get
        {
            if (showPoint == null)
                showPoint = transform.Find("ShowPoint").transform;
            return showPoint;
        }
    }
    public CanvasGroup ShowGroup { get { return ShowPoint.GetComponent<CanvasGroup>(); } }

    /// <summary>
    /// ������ʾ�ĵ�����
    /// </summary>
    /// <param name="card">��ʾ������Ϣ</param>
    /// <param name="index">����</param>
    public void SetShowCard(Card card,int index)
    {
        Image[] showCards = ShowPoint.GetComponentsInChildren<Image>();
        showCards[index].sprite= Resources.Load<Sprite>("Pokers/"+card.CardName);
        SetAlpha(1);
    }
    public void SetAlpha(int i)
    {
        ShowGroup.alpha = i;
    }
}
