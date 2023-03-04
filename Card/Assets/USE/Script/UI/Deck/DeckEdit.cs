using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckEdit : MonoBehaviour
{
    public Decklist DeckInfo;
    
    public int CardNumber;//자기 카드번호
    public SpellCard number;
    [SerializeField]
    List<Material> materials;
    int num;

    public void Init(Decklist DeckInfos,int num)
    {
        DeckInfo = DeckInfos;
        CardNumber = num + 1;
        GetComponent<Image>().enabled=false;
        //Temp.material = materials[DeckInfo.CardDataFile.cards[CardNumber].Rank - 1];
        transform.parent.GetChild(0).GetComponent<Image>().sprite = DeckInfo.CardDataFile.cards[CardNumber].BG_Image;
        //transform.GetComponent<Button>().onClick.AddListener(imageChange);




    }
    public void imageChange()
    {

        
        
        int count = 0;
        
        
        
            for (int i = 0; i < DeckInfo.HaveData.cards.Count; i++)
            {
                if (DeckInfo.HaveData.cards[i] == CardNumber)
                {

                    count++;
                }
            }
            
        
     
            for (int i = 0; i < DeckInfo.DeckData.cards.Count; i++)
            {
                if (DeckInfo.DeckData.cards[i] == CardNumber)
                {

                    count++;
                }
            }
            DeckInfo.CountText.text = "" + count;
            DeckInfo.CountSentenceText.text = "Have";


        DeckInfo.MainCardPosi.GetChild(0).GetComponent<Image>().sprite = DeckInfo.CardDataFile.cards[CardNumber].BG_Image;
        Image Temp_Image = DeckInfo.MainCardPosi.GetChild(1).GetComponent<Image>();
        Temp_Image.sprite = DeckInfo.CardDataFile.cards[CardNumber].Image;
        Temp_Image.material = materials[DeckInfo.CardDataFile.cards[CardNumber].Rank - 1];


        DeckInfo.MainCardPosi.GetChild(4).GetComponent<TextMeshProUGUI>().text = "" + DeckInfo.CardDataFile.cards[CardNumber].cost;
        DeckInfo.MainCardPosi.GetChild(5).GetComponent<TextMeshProUGUI>().text = DeckInfo.CardDataFile.cards[CardNumber].CardName;
        DeckInfo.MainCardPosi.GetChild(6).GetComponent<TextMeshProUGUI>().text = DeckInfo.CardDataFile.cards[CardNumber].exp;
        DeckInfo.MainCardPosi.GetChild(7).GetComponent<Image>().enabled = false;

        DeckInfo.Main.CardNumber = CardNumber;
        //MainImage.sprite = CardDataFile.cards[num].Image;
        //RankImage.sprite = CardDataFile.cards[num].RankImage;
        //CostText.text = "" + CardDataFile.cards[num].cost;
        //number.CardNumber = DeckInfo.CardDataFile.cards[num].id;
        //NameText.text = CardDataFile.cards[num].name;
        //EffectText.text = CardDataFile.cards[num].exp;
        //Num.SettingSelect(CardDataFile.cards[num].id, CardDataFile.cards[num].cost, CardDataFile.cards[num].name, CardDataFile.cards[num].exp);






    }
}
