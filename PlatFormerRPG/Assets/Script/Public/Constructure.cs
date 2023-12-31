using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructure
{
    public struct Stat
    {
        //public string Name;
        public float HP;
        public float MaxHP;
        public float Att;
        public float Skill;
        public float ExpVal;
        public float MaxExpVal;
        public int Level;
        public int Money;

        public Stat(/*string name,*/ float hp, float att, float skill, float exp, float maxEXP, int level, int Money)
        {
            //this.Name = name;
            this.HP = hp;
            this.MaxHP = hp;
            this.Att = att;
            this.Skill = skill;
            this.ExpVal = exp;
            this.MaxExpVal = maxEXP;
            this.Level = level;
            this.Money = Money;
        }
    }
    
    public struct MonsterStat
    {
        public float hP;
        public float maxHP;
        public float att;
        public float giveExp;
        public int giveMoney;
        public MonsterStat(int level)
        {
            this.hP = 10 + (20 * level);
            this.maxHP = 10 + (20 * level);
            this.att = 1 + (1 * level);
            this.giveExp = 10 + (1 * level);
            this.giveMoney = 100 + (1 * level);
        }
    }
}
