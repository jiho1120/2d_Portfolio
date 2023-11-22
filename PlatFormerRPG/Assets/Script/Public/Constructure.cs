public class Constructure
{
    public struct MonsterStat
    {
        public float hP;
        public float maxHP;
        public float att;
        public float giveExp;
        public int giveMoney;
        public MonsterStat(int level)
        {
            this.hP = 20 * level;
            this.maxHP = 20 * level;
            this.att = 10 * level;
            this.giveExp = 50 * level;
            this.giveMoney = 100 * level;
        }
    }
}
