using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionView :View
{
    public Button Deal;

    public Button Grab;
    public Button DisGrab;

    public Button Play;
    public Button Pass;

    /// <summary>
    /// 全部隐藏
    /// </summary>
    public void DeactiveAll()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Deal.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
    }
    /// <summary>
    /// 显示发牌
    /// </summary>
    public void AvtivePlay()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Deal.gameObject.SetActive(true);
        Pass.gameObject.SetActive(false);
    }
    /// <summary>
    /// 显示抢地主
    /// </summary>
    public void ActiveGrapAndDisGrap()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(true);
        DisGrab.gameObject.SetActive(true);
        Deal.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
    }

    public void ActiveDealAndPass(bool isActive=true)
    {
        Play.gameObject.SetActive(true);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Deal.gameObject.SetActive(false);
        Pass.gameObject.SetActive(true);  
        Pass.interactable = isActive;
    }
}
