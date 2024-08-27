using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const 
{
    //存储数据路径
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
    ChangeMulitiple,//加倍不加倍
    RequestDeal,      //请求发牌
    GrabLandlord,   //抢地主
    PlayCard, //出牌
    PassCard,  //不出牌
    GameOver,  //游戏结束
    RequestUpdate, //数据更新
    UpdateGameOver, //游戏结束
}
public enum ViewEvent
{
    DealCard, //给每个人发牌
    CompleteDeal,     //发牌结束
    DealThreeCard,   //发地主牌
    RequestPlay,   //玩家请求出牌
    SuccessPlay,    //成功出牌
    UpdateIntegrationModel, //更新分数
    UpdateGameOver,
    RestartGame,
}
/// <summary>
/// 牌的归属
/// </summary>
public enum CharacterType
{
    Library,//牌库
    Player,
    ComputerRight,
    ComputerLeft,
    Desk
}
/// <summary>
/// 花色
/// </summary>
public enum Colors
{
    None,//大小王
    Club,//梅花
    Spade,//黑桃
    Square,//方块
    Heart //红桃
}

/// <summary>
/// 牌的大小
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
/// 出牌类型
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
/// 身份
/// </summary>
public enum Identity
{
    Farmer,
    Landlord
}
/// <summary>
/// Desk生成位置
/// </summary>
public enum ShowPoint
{
    Player,
    ComputerRight,
    ComputerLeft,
    Desk,
}