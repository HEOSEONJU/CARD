using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Decklist : MonoBehaviour
{
    
    public CardScriptable CardDataFile;
    public HaveScriptTable HaveData;
    public DeckScriptTable DeckData;
    public int MaxCount = 40;
    //public SelectCard CardNum;
    public SpellCard Main;
    //public List<int> CardHaveNumber;

    public Transform MainCardPosi;
    public GameObject DeckCardObject;
    public GameObject ObjectPrefab;

    public TextMeshProUGUI CountText;
    public TextMeshProUGUI CountSentenceText;

    public DeckEdit First;

    public FailWinodw POPUP_1;

    [SerializeField]
    GameObject Prefab_Object;
    [SerializeField]
    Transform CreateParent;
    private void Awake()
    {
        //CardHaveNumber = new List<int>();
        CardDataFile = GameObject.Find("CardData").GetComponent<CardData>().CardDataFile;
        DeckData = GameObject.Find("CardData").GetComponent<CardData>().DeckDataFile;
        HaveData = GameObject.Find("CardData").GetComponent<CardData>().HaveDataFile;
        //DeckData.cards.Clear();
        //HaveData.cards.Clear();



        for(int i=0;i<CardDataFile.cards.Count-1;i++)
        {
            
                var e = Instantiate(Prefab_Object, CreateParent);
                e.transform.GetChild(1).GetComponent<DeckEdit>().Init(this, i);
            
        }






        for(int i=0; i<DeckData.cards.Count; i++)
        {

            CardCreate(DeckData.cards[i]);


        }
        CreateParent.GetChild(0).GetChild(1).GetComponent<DeckEdit>().imageChange();
        //First.imageChange();



    }
    
    public void AddCard()
    {
        int CardOverlapCount=0;
        for(int i = 0; i < DeckData.cards.Count; i++)//���� ���� ī�� �����ִ���Ž��
        {
            if(DeckData.cards[i]== Main.CardNumber)
                CardOverlapCount++;

        }
        int num=0;

        int check=0;
        for (int i = 0; i < HaveData.cards.Count; i++)//����ī�尡���Ҵ���Ȯ��
        {
            if (HaveData.cards[i] == Main.CardNumber)
            {
                check += 1;
                
            }

        }
        

        if (MaxCount > DeckData.cards.Count & CardOverlapCount <= 2 & check>=1)
        {
            for (int i = 0; i < CardDataFile.cards.Count; i++)
            {
                if (CardDataFile.cards[i].id == Main.CardNumber)
                {
                    num = i;
                    break;
                }

            }
            CardCreate(num);

            DeckData.cards.Add(Main.CardNumber);
            HaveData.cards.Remove(Main.CardNumber);
            
            ReloadCount(Main.CardNumber);
            SortingDeckCard();  

            //CardHaveNumber.Add(Main.CardNumber);
            //e.transform.GetChild(0).GetComponent<DeckEdit>().Num = Main;
            //e.transform.GetChild(0).GetComponent<DeckEdit>().MainImage = Main.Front.transform.GetChild(1).GetComponent<Image>();

            //e.transform.GetChild(0).GetComponent<DeckEdit>().CostText = Main.Front.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            //e.transform.GetChild(0).GetComponent<DeckEdit>().NameText = Main.Front.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            //e.transform.GetChild(0).GetComponent<DeckEdit>().EffectText = Main.Front.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
            //e.transform.GetChild(0).GetComponent<DeckEdit>().RankImage = Main.Front.transform.GetChild(7).GetComponent<Image>();

        }
        else if(CardOverlapCount>=3)
        {
            POPUP_1.View_Text("���� ���� ī�尡 3�����ֽ��ϴ�");
        }
        else if(check<=0)
        {
            POPUP_1.View_Text("���� ���� ī�尡 �������� �ʽ��ϴ�.");
        }
        else
        {
            POPUP_1.View_Text("���� �̹� ����á���ϴ�.");
        }
    }

    public void SubCard()
    {
        bool Active = true;
        if (0 < DeckData.cards.Count)
        {
            for (int i = 0; i < DeckData.cards.Count; i++)
            {
                if(DeckCardObject.transform.GetChild(i).GetChild(1).GetComponent<DeckEdit>().CardNumber== Main.CardNumber)
                {

                    DeckData.cards.Remove(Main.CardNumber);
                    HaveData.cards.Add(Main.CardNumber);
                    Destroy(DeckCardObject.transform.GetChild(i).gameObject);
                    ReloadCount(Main.CardNumber);
                    Active = false;
                    SortingDeckCard();
                    break;
                }
            }

            if(Active)
            {
                POPUP_1.View_Text("������ �� ī�尡 �������� �ʽ��ϴ�.");
            }

            
        }
        else
        {
            POPUP_1.View_Text("���� ī�尡 �������� �ʽ��ϴ�.");
        }
        
    }

    public void CardCreate(int num)
    {
        var e = Instantiate(Prefab_Object, DeckCardObject.transform);
        e.transform.GetChild(1).GetComponent<DeckEdit>().Init(this, num-1);
        //e.transform.GetChild(0).GetComponent<Image>().sprite = CardDataFile.cards[num].Image;
        //e.transform.GetChild(0).GetComponent<DeckEdit>().CardNumber = CardDataFile.cards[num].id;
        //e.transform.GetChild(0).GetComponent<DeckEdit>().number = Main;
        
        //e.transform.GetChild(0).GetComponent<DeckEdit>().DeckInfo = this;
        e.transform.parent = DeckCardObject.transform;


        
        



        SortingDeckCard();
    }
    public void ReloadCount(int num)
    {
        int count = 0;
        for (int i = 0; i < HaveData.cards.Count; i++)
        {
            if (HaveData.cards[i] == num)
            {

                count++;
            }
        }
        for (int i = 0; i < DeckData.cards.Count; i++)
        {
            if (DeckData.cards[i] == num)
            {

                count++;
            }
        }
        CountText.text = "" + count;
    }

    public void SortingDeckCard()
    {
        for (int j = 0; j < DeckCardObject.transform.childCount - 1; j++)
        {
            
            for (int i = 0; i < DeckCardObject.transform.childCount - 1; i++)
            {
                
                if (DeckCardObject.transform.GetChild(i).GetChild(1).GetComponent<DeckEdit>().CardNumber < DeckCardObject.transform.GetChild(i + 1).GetChild(1).GetComponent<DeckEdit>().CardNumber)
                {

                    //DeckCardObject.transform.GetChild(i).SetSiblingIndex(i + 1);



                }


            }
        }

    }

    public void ExitDeckEdit()
    {
        if(DeckData.cards.Count>=10)
        {
            GameObject.Find("Main Camera").GetComponent<ManagementMainUI>().CloseDeckUI();
            
        }
            else
        {
            POPUP_1.View_Text("���� �ּ�10���� ī�尡 �־�����ϴ�.");
        }
    }

}
