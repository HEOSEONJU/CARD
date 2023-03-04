using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIDataUpdate : MonoBehaviour
{
    public PlayerInfos Data;
    [Header("TextUI")]
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExpText;
    public TextMeshProUGUI StaminaText;
    public TextMeshProUGUI GemText;
    public TextMeshProUGUI GoldText;
    // Update is called once per frame

    private void OnEnable()
    {
        Invoke("UpdateData",Time.deltaTime);
        
    }

    private void Update()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        if (NameText != null)
        {
            NameText.text = Data.Name;

        }
        if (LevelText != null)
        {
            LevelText.text = "" + Data.Level;

        }
        if (ExpText != null)
        {
            ExpText.text = "" + Data.Exp;

        }
        if (StaminaText != null)
        {
            StaminaText.text = "" + Data.Stamina;

        }
        if (GemText != null)
        {
            GemText.text = "" + Data.Gem;

        }
        if (GoldText != null)
        {
            GoldText.text = "" + Data.Gold;

        }
    }

    
}
