using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorsManagers : MonoBehaviour
{
    static public int MaxTeam = 3;
    static public int MaxLevel = 100;




    [System.Serializable]
    public class WarriorProperties
    {
        public string Name;
        public int Level;



        public int BaseLife;
        public int Life;

        public int TotalLife;

        public int Exp;

        public int NextLevelExp;
        public int PrevLevelExp;



        public int BaseAtk;
        public int BaseDef;
        public int BaseSpeed;

        public int Atk;
        public int Def;
        public int Speed;

        [System.Serializable]
        public class MySkillProperties
        {
            public SkillManager.SkillProperties Skill;
            public bool Active;
            public bool CanLearn;
            public bool Canceled;
        }
        public List<MySkillProperties> MySkills;

        public class SkillbyLevelProperties
        {
            public SkillManager.SkillProperties Skill;
            public int Level;
        }
        public List<SkillbyLevelProperties> SkillbyLevel;




    }

    static public List<WarriorProperties> Warrior = new List<WarriorProperties>()
    {

        new WarriorProperties(){
            Name = "Grodnar",    BaseLife = 60, BaseAtk = 30, BaseDef = 50, BaseSpeed = 30,
            SkillbyLevel = new List<WarriorProperties.SkillbyLevelProperties>
                {
                 new WarriorProperties.SkillbyLevelProperties()  {Skill =  SkillManager. GetSkillByID(0), Level = 1,},
                },

        },

         new WarriorProperties(){
            Name = "Lobo",    BaseLife = 20, BaseAtk = 50, BaseDef = 20, BaseSpeed = 50,
            SkillbyLevel = new List<WarriorProperties.SkillbyLevelProperties>
                {
                 new WarriorProperties.SkillbyLevelProperties()  {Skill =  SkillManager. GetSkillByID(1), Level = 1,},
                },

        },


    };


    static public WarriorProperties GetPetByName(string name, bool Random_IV, bool IsWild, int Level = -1)
    {
        for (int i = 0; i < Warrior.Count; i++)
        {
            if (Warrior[i].Name == name)
            {
                WarriorProperties TempWarrior = CloneWarrior(Warrior[i]);


                if (Level != -1)
                {
                    TempWarrior.Level = Level;
                    TempWarrior.TotalLife = GetHPStats(TempWarrior.BaseLife, TempWarrior.Level);
                    TempWarrior.Life = TempWarrior.TotalLife;

                    TempWarrior.Atk = GetOtherStats(TempWarrior.BaseAtk, TempWarrior.Level);
                    TempWarrior.Def = GetOtherStats(TempWarrior.BaseDef, TempWarrior.Level);
                    TempWarrior.Speed = GetOtherStats(TempWarrior.BaseSpeed, TempWarrior.Level);


                    TempWarrior.PrevLevelExp = (int)(TempWarrior.Level * 0.1f);
                    TempWarrior.NextLevelExp = (int)((TempWarrior.Level + 1) * 0.1f);
                    TempWarrior.Exp = TempWarrior.PrevLevelExp;




                }



                return TempWarrior;
            }
        }
        return null;
    }
    static public int GetHPStats(int Base, int Level)
    {
        int result = ((((Base) * 2) * Level) / 100) + Level + 10;
        return result;

    }

    static public int GetOtherStats(int Base, int Level)
    {
        int result = ((((Base) * 2) * Level) / 100) + 5;
        return result;
    }




    static public WarriorProperties CloneWarrior(WarriorProperties _warrior)
    {
        WarriorProperties NewWarrior = new WarriorProperties()
        {
            Name = _warrior.Name,



            BaseLife = _warrior.BaseLife,
            BaseAtk = _warrior.BaseAtk,
            BaseDef = _warrior.BaseDef,
            BaseSpeed = _warrior.BaseSpeed,
            Level = _warrior.Level,
            Life = _warrior.Life,
            TotalLife = _warrior.TotalLife,
            Atk = _warrior.Atk,
            Def = _warrior.Def,
            Speed = _warrior.Speed,
            Exp = _warrior.Exp,
            NextLevelExp = _warrior.NextLevelExp,
            PrevLevelExp = _warrior.PrevLevelExp
        };

        return NewWarrior;
    }





}