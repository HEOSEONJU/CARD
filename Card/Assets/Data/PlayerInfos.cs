using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public string ID;
    public string Password;
    public string Name;
    public int Level;
    public int Exp;
    public int Stamina;
    public int Gem;
    public int Gold;
    public int SpellCardPack;
    public int UsePack;
    public List<int> GetPack;
    public List<int> HaveCard;

    public List<int> DeckCards;
    public List<MonsterInfo> MonsterCards;
    public List<int> UseMonsterCards;


    public List<int> StageClear;//0Àá±Ý,1¿ÀÇÂ/2Å¬¸®¾î

    public int LGold = 10000;
    public int LGem = 1000;
    public int LCardPack = 10;
    public DateTime CurrentTime;
    public DateTime LastTime;


    public void Init_SuperAccount()
    {
        CurrentTime = DateTime.Now;
        ID = "super";
        Password = "123456";
        Name = "SUPER ACCOUNT";

        Level = 1;
        Exp = 0;
        Stamina = 60;
        Gem = 100000;
        Gold = 1000000;
        SpellCardPack = 1000;
        UsePack = 0;

        HaveCard = new List<int>();

        DeckCards = new List<int>();


        for(int i=1;i<51;i++)
        {
            HaveCard.Add(i);
            HaveCard.Add(i);
            HaveCard.Add(i);
        }




        MonsterCards = new List<MonsterInfo>();
        UseMonsterCards = new List<int>();

        MonsterInfo temp;

        for(int i=1;i<43;i++)
        {

            temp = new MonsterInfo();
            temp.Init_Monset(i);
            MonsterCards.Add(temp);
            if(i<=4)
            {
                UseMonsterCards.Add(temp.ID);
            }
        }
        
        StageClear = new List<int>();
        for (int i = 0; i < 18; i++)
        {
            StageClear.Add(0);
        }
        StageClear[0] = 1;
        CurrentTime = LastTime = DateTime.Now;

    }
    public void Init_Account(string id, string password, string Input_name)
    {
        CurrentTime = DateTime.Now;
        ID = id;
        Password = password;
        Name = Input_name;

        Level = 1;
        Exp = 0;
        Stamina = 60;
        Gem = 0;
        Gold = 10000;
        SpellCardPack = 10;
        UsePack = 0;

        HaveCard=new List<int>();

        DeckCards= new List<int>();

        
        DeckCards.Add(1);
        DeckCards.Add(1);
        DeckCards.Add(1);
        DeckCards.Add(2);
        DeckCards.Add(2);
        DeckCards.Add(2);
        DeckCards.Add(3);
        DeckCards.Add(3);
        DeckCards.Add(3);
        DeckCards.Add(7);



        MonsterCards = new List<MonsterInfo>();
        UseMonsterCards =new List<int>();

        MonsterInfo temp= new MonsterInfo();
        temp.Init_Monset(2);
        MonsterCards.Add(temp);
        UseMonsterCards.Add(temp.ID);

        temp = new MonsterInfo();
        temp.Init_Monset(3);
        MonsterCards.Add(temp);
        UseMonsterCards.Add(temp.ID);

        temp = new MonsterInfo();
        temp.Init_Monset(4);
        MonsterCards.Add(temp);
        UseMonsterCards.Add(temp.ID);

        temp = new MonsterInfo();
        temp.Init_Monset(5);
        MonsterCards.Add(temp);
        UseMonsterCards.Add(temp.ID);
        //Ã³À½ÀåÂøÇÒ4¸í





        StageClear = new List<int>();
        for (int i=0;i<18;i++)
        {
            StageClear.Add(0);
        }
        StageClear[0]=1;
        CurrentTime = LastTime = DateTime.Now;
    }



    public bool Try_Log(string id, string password)
    {
        if (ID == id && Password == password)
        {
            return true;
        }

        return false;
    }

}





    [System.Serializable]
public class MonsterInfo
{
    public int ID;
    public int Level;
    public int BreaK_Lim;

    public void Init_Monset(int id)
    {
        ID = id;
        Level = 1;
        BreaK_Lim = 1;

    }
}

public class PlayerInfos : MonoBehaviour
{
    [SerializeField]
    PlayerInfoData PlayerDataBox;
    public string ID;
    string Password;
    public string Name;
    public int Level;
    public int Exp;
    public int Stamina;
    public int Gem;
    public int Gold;
    public int SpellCardPack;
    public int UsePack;
    public List<int> GetPack;
    public List<int> HaveCard;

    public List<int> DeckCards;
    public List<MonsterInfo> MonsterCards;
    public List<int> UseMonsterCards;


    public List<int> StageClear;//0Àá±Ý,1¿ÀÇÂ/2Å¬¸®¾î

    public int LGold=10000;
    public int LGem = 1000;
    public int LCardPack = 10;

    [SerializeField]
    GameObject Camera;
    [SerializeField]
    GameObject Canvas;

    
    public DateTime CurrentTime;
    
    public DateTime LastTime;

    int Max_Stamina = 60;
    int RegenTimer = 10;
         


    private void Awake()
    {
        ID = PlayerPrefs.GetString("Sucess_ID");
        Load_Data(ID);
        Camera.SetActive(true);
        Camera.GetComponent<ManagementMainUI>().Init();
        Canvas.SetActive(true);
    }


    private void Update()
    {
        CurrentTime=DateTime.Now;
        StamainaAdd();
    }
    public void Load_Data(string MYID)
    {
        for(int i=0;i<PlayerDataBox.Player.Count;i++)
        {
            if(PlayerDataBox.Player[i].ID == MYID)
            {
                Name = PlayerDataBox.Player[i].Name;
                Level = PlayerDataBox.Player[i].Level;
                Stamina = PlayerDataBox.Player[i].Stamina;
                Gem = PlayerDataBox.Player[i].Gem;
                Gold = PlayerDataBox.Player[i].Gold;
                SpellCardPack = PlayerDataBox.Player[i].SpellCardPack;
                
                GetPack = PlayerDataBox.Player[i].GetPack;
                HaveCard = PlayerDataBox.Player[i].HaveCard;
                DeckCards = PlayerDataBox.Player[i].DeckCards;
                MonsterCards = PlayerDataBox.Player[i].MonsterCards;
                UseMonsterCards = PlayerDataBox.Player[i].UseMonsterCards;
                StageClear = PlayerDataBox.Player[i].StageClear;

                CurrentTime =DateTime.Now;





                StamainaAdd();







                Saved_Data();
                return;


            }


        }

        
    }

    void StamainaAdd()
    {

        TimeSpan temp = CurrentTime - LastTime;

        if (temp.Minutes >= RegenTimer)
        {
            InfiniteLoopDetector.Run();
            LastTime.AddMinutes(RegenTimer);
            if (Stamina < Max_Stamina)
            {
                Stamina += 1;
            }
        }
        Saved_Data();
    }

    public void Saved_Data()
    {
        for (int i = 0; i < PlayerDataBox.Player.Count; i++)
        {
            if (PlayerDataBox.Player[i].ID == ID)
            {
                
                PlayerDataBox.Player[i].Level=Level;
                PlayerDataBox.Player[i].Stamina= Stamina;
                PlayerDataBox.Player[i].Gem=Gem;
                PlayerDataBox.Player[i].Gold=Gold;
                PlayerDataBox.Player[i].SpellCardPack= SpellCardPack;
                
                PlayerDataBox.Player[i].GetPack=GetPack;
                PlayerDataBox.Player[i].HaveCard= HaveCard;
                PlayerDataBox.Player[i].DeckCards= DeckCards;
                PlayerDataBox.Player[i].MonsterCards= MonsterCards;
                PlayerDataBox.Player[i].UseMonsterCards= UseMonsterCards;
                PlayerDataBox.Player[i].StageClear=StageClear;
                LastTime= DateTime.Now;
                return;


            }


        }
    }


    public void StageClear_Function(int i)
    {
        StageClear[i] = 2;
        if(i<StageClear.Count-1)
        {
            if (StageClear[i + 1] == 0)
            {
                StageClear[i + 1] = 1;
            }
        }

    }

    public bool LevelUp()
    {
        if(Exp>=200)
        {
            Level += 1;
            Exp -= 200;


            Gold += LGold;
            Gem += LGem;
            SpellCardPack += LCardPack;
            return true;
        }
        else
        {
            return false;
        }
    }
    

    
}

