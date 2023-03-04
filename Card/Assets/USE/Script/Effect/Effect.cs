using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract  class Effect :MonoBehaviour
{
    //public int Spell_Number;
    
    [SerializeField]
    protected int Turn;


    public bool Require;

    public abstract void RequireMent(Manager Manager);
    public abstract void Effect_Function(Manager _Manager);
    public abstract void Effect_Solo_Function(Manager _Manager);
    public abstract void Effect_Enemy_Function(Manager _Manager);
    public abstract void Damage_Enemy_Function(EnemyManager Target);
}
public enum BUFF_TYPE
{
    Heal,
    ATK,
    DEF,
    MAXHP,
    PATK,
    PDEF,
    PHP,
    MG,
    Regen,
    AtC,
    CP,
    CD,
    DC


}
[System.Serializable]
public class Effect_Value
{ 
    public int Value;
    public BUFF_TYPE TYPE;
}
