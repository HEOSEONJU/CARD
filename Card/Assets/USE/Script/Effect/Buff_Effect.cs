using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Effect : Base_Effect
{
    [SerializeField]
    protected bool Multi;//True면 광역
    [SerializeField]
    public List<Effect_Value> _Effect_Value;
    
    bool Active = false;

    List<int> Skip;
    public override void Effect_Solo_Function(Manager _Manager)
    {
        
        
        switch (Multi)
        {
            case true:
                for(int i=0;i< _Manager.Char_Manager.CombatChar.Count;i++)
                {
                    if (Skip.Contains(i))
                        continue;
                    Function(_Manager, _Manager.Char_Manager.CombatChar[i]);
                }
                Skip.Clear();
                break;
            default:

                Function(_Manager,_Manager.Target_Solo);
                break;
                

        }
        
            _Manager._Result = Card_Result.Success;
        
    }

    public void Function(Manager _Manager, GameCard GC)
    {
        SpellEffect E = new SpellEffect();
        E.ClearValue();
        foreach (Effect_Value v in _Effect_Value)
        {
            E.Effect_Type_Value.Add(v);
        }
        if (_Manager.SelectedCard != null)
        {
            GC.Active_Effect(_Manager.SelectedCard.Value_Char_Effect_Num);
            E.Init(_Manager.SelectedCard.CardNumber);
            GC.Spell_Effects.Add(E);
        }
        else
        {
            E.Init(_Manager.Char_Manager.Skill_NUM);
            GC.Skill_Effects.Add(E);
        }
        
            

            
            
        
        
    }

    public override void RequireMent(Manager _Manager)
    {
        Skip=new List<int>();
        switch (Multi)
        {
            case true:
                int L = 0;//생존수
                int D = 0;//중복
                foreach (GameCard GC in _Manager.Char_Manager.CombatChar)
                {
                    
                    if(GC.Live)
                    {
                        L++;
                        for (int j = 0; j < _Manager.Target_Solo.Spell_Effects.Count; j++)
                        {
                            if (_Manager.Target_Solo.Spell_Effects[j].ID == _Manager.SelectedCard.CardNumber)
                            {
                                D++;
                                Skip.Add(j);
                                break;
                            }
                        }
                    }

                }
                if(L==D)//생존중복이같다는건 모두 같은버프를갖고있음
                {
                    _Manager._Result = Card_Result.Duplication;
                    Require = false;
                }
                break;
            default:

                for (int j = 0; j < _Manager.Target_Solo.Spell_Effects.Count; j++)
                {

                    if (_Manager.Target_Solo.Spell_Effects[j].ID == _Manager.SelectedCard.CardNumber)
                    {
                        
                        _Manager._Result = Card_Result.Duplication;
                        Require = false;
                        return;

                    }


                }
                break;


        }



        Require = true;
    }
    public void Check_Buff(Manager _Manager,GameCard GC)
    {
        for (int j = 0; j < GC.Spell_Effects.Count; j++)
        {

            if (GC.Spell_Effects[j].ID == _Manager.SelectedCard.CardNumber)
            {
                //Debug.Log("이미적용된효과");
                _Manager._Result = Card_Result.Duplication;
                
                return;

            }


        }

    }
}