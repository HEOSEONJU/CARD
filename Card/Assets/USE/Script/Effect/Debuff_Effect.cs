using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Effect : Base_Effect
{
    [SerializeField]
    public List<Effect_Value> _Effect_Value;

    public override void Effect_Enemy_Function(Manager _Manager)
    {
        
        //GC.Active_Effect(_Manager.SelectedCard.Value_Char_Effect_Num);
        SpellEffect E = new SpellEffect();
        E.ClearValue();
        E.Init(_Manager.SelectedCard.CardNumber);
        foreach (Effect_Value v in _Effect_Value)
        {
            E.Effect_Type_Value.Add(v);
        }
        _Manager.Enemy_Manager.Spell_Effects_List.Add(E);
        _Manager._Result = Card_Result.Success;
    }
    public override void RequireMent(Manager _Manager)
    {
        foreach (SpellEffect SE in _Manager.Enemy_Manager.Spell_Effects_List)
        {
            if(_Manager.SelectedCard.CardNumber==SE.ID)
            {
                _Manager._Result = Card_Result.Duplication;
                Require = false;
                return;
            }


        }



        Require = true;
    }
}
