using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public static CardData instance;

    public CardScriptable CardDataFile;
    public DeckScriptTable DeckDataFile;
    public HaveScriptTable HaveDataFile;
    public CharCardScriptTable CharDataFile;
    public GameObject PlayerInfoFile;
    public EnemyScriptTable EnemyDataFile;
    public Char_Skill_ScriptTable CharSkillFile;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void Start()
    {

        HaveDataFile.cards = PlayerInfoFile.GetComponent<PlayerInfos>().HaveCard;
        DeckDataFile.cards = PlayerInfoFile.GetComponent<PlayerInfos>().DeckCards;

        Debug.Log(PlayerInfoFile.GetComponent<PlayerInfos>().CurrentTime);
    }
}
