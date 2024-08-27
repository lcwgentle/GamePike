using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const 
{
    //�洢����·��
    public static string DataPath = Application.dataPath + @"\score.xml";

    public static string Bg = "normal";
    public static string DealCard = "givecard";
    public static string DisGrab = "buqiang";
    public static string Grab = "qiangdizhu1";
    public static string Select = "select";
    public static List<string> PassCard = new List<string> { "buyao1", "buyao2", "buyao3" };
    public static List<string> PlayCard = new List<string> { "dani1", "dani2", "dani3" };
    public static List<string> Single = new List<string> {  "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13",
        "1", "2","14","15" };
    public static List<string> Double = new List<string> {  "dui3", "dui4", "dui5", "dui6", "dui7", 
        "dui8", "dui9", "dui10", "dui11","dui12", "dui13" ,"dui1", "dui2" };
    public static string Straight = "shunzi";
    public static string DoubleStraight = "liandui";
    public static string TripleStraight = "feiji";
    public static string Three = "sange";
    public static string ThreeAndOne = "sandaiyi";
    public static string ThreeAndTwo = "sandaiyidui";
    public static string Boom = "zhadan";
    public static string JokerBoom = "wangzha";

    public static string Win = "win";
    public static string Lose = "lose";
}
public enum PanelType
{
    StartPanel,
    CharacterPanel,
    InteractionPnael,
    GameOverPanel,
}
public enum CommandEvent
{
    ChangeMulitiple,//�ӱ����ӱ�
    RequestDeal,      //������
    GrabLandlord,   //������
    PlayCard, //����
    PassCard,  //������
    GameOver,  //��Ϸ����
    RequestUpdate, //���ݸ���
    UpdateGameOver, //��Ϸ����
}
public enum ViewEvent
{
    DealCard, //��ÿ���˷���
    CompleteDeal,     //���ƽ���
    DealThreeCard,   //��������
    RequestPlay,   //����������
    SuccessPlay,    //�ɹ�����
    UpdateIntegrationModel, //���·���
    UpdateGameOver,
    RestartGame,
}
/// <summary>
/// �ƵĹ���
/// </summary>
public enum CharacterType
{
    Library,//�ƿ�
    Player,
    ComputerRight,
    ComputerLeft,
    Desk
}
/// <summary>
/// ��ɫ
/// </summary>
public enum Colors
{
    None,//��С��
    Club,//÷��
    Spade,//����
    Square,//����
    Heart //����
}

/// <summary>
/// �ƵĴ�С
/// </summary>
public enum Weight
{
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker
}
/// <summary>
/// ��������
/// </summary>
public enum CradType
{
    None,
    Single,
    Double,
    Straight,
    DoubleStraght,
    TripleStraght,
    Three,
    ThreeAndOne,
    ThreeAndTwo,
    Boom,
    JokerBoom
}
/// <summary>
/// ���
/// </summary>
public enum Identity
{
    Farmer,
    Landlord
}
/// <summary>
/// Desk����λ��
/// </summary>
public enum ShowPoint
{
    Player,
    ComputerRight,
    ComputerLeft,
    Desk,
}