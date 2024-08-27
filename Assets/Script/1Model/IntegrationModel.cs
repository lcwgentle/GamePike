using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationModel 
{
    /// <summary>
    /// 底分
    /// </summary>
    public int BasePoint;
    /// <summary>
    /// 倍数
    /// </summary>
    public int Mulitple;
    /// <summary>
    /// 一轮积分
    /// </summary>
    public int Result
    {
        get
        {
            return (Mulitple * BasePoint);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private int playerInteration;

    public int PlayerInteration { get => playerInteration; 
        set 
        {
            if (value <0)
                playerInteration = 0;
            else
            playerInteration = value; 
        } 
    }

    private int computerLeftIntegartion;

    public int ComputerLeftIntegartion
    {
        get => computerLeftIntegartion; 
        set
        {
            if (value <0)
                computerLeftIntegartion = 0;
            else
            computerLeftIntegartion = value;
        }
    }

    private int computerRightIntegartion;

    public int ComputerRightIntegartion { 
        get => computerRightIntegartion;
        set
        {
            if (value < 0)
                computerRightIntegartion = 0;
            else
                computerRightIntegartion = value;
        }
    }

    public void  InitIntegration()
    {
        Mulitple = 1;
        BasePoint = 100;
        PlayerInteration = 3000;
        ComputerLeftIntegartion = 3000;
        ComputerRightIntegartion = 3000;
    }
}
