using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : View
{
    public Image showImage;
    public List<Sprite> showList;
    public Button continueBtn;

    public void Init(bool isLandlord,bool isWin)
    {
        if(isLandlord)
        {
            if(isWin)
            {
                showImage.sprite = showList[0];
            }else
            {
                showImage.sprite = showList[1];
            }
        }else
        {
            if (isWin)
            {
                showImage.sprite = showList[2];
            }
            else
            {
                showImage.sprite = showList[3];
            }
        }
    }
}
