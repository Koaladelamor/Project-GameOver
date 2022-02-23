using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleControl : MonoBehaviour
{
    public enum BattleStates { NONE, SELECT_TARGET, MOVE_TO_TARGET, ATTACK_TARGET  }
    public BattleStates State;


    public GameObject BattleCanvas;

    [System.Serializable]
    public class BattleUIProperties
    {
        public Image LifeBar;
        public Text NameText;
        public Text LevelText;
    }
    public List<BattleUIProperties> BattleUI;

    public List<WarriorsManagers.WarriorProperties> BattleTeam;
    public WarriorsManagers.WarriorProperties CurrentBattleTeam;
   
    public List<WarriorsManagers.WarriorProperties> BattleWild;
    public WarriorsManagers.WarriorProperties CurrentBattleWild;

    public bool IsWildWarrior;

    private WarriorsManagers.WarriorProperties FinalWarrior;

    public void SetUIBattle()
    {
      
        BattleUI[0].NameText.text = CurrentBattleTeam.Name;
        BattleUI[0].LevelText.text = "Lvl: " + CurrentBattleTeam.Level;
        BattleUI[0].LifeBar.fillAmount = (float)CurrentBattleTeam.Life / (float)CurrentBattleTeam.TotalLife;

        BattleUI[1].NameText.text = CurrentBattleWild.Name;
        BattleUI[1].LevelText.text = "Lvl: " + CurrentBattleWild.Level;
        BattleUI[1].LifeBar.fillAmount = (float)CurrentBattleWild.Life / (float)CurrentBattleWild.TotalLife;



        BattleCanvas.SetActive(true);
    }

    private void BattleUpdate()
    {
        switch (State)
        {
            case BattleStates.SELECT_TARGET:

                ChangeState(BattleStates.MOVE_TO_TARGET);
                break;


            case BattleStates.MOVE_TO_TARGET:
                //HACER QUE SE MUEVA AL ENEMIGO SEGUN EL RANGO
                ChangeState(BattleStates.ATTACK_TARGET);
                break;


            case BattleStates.ATTACK_TARGET:


                float CriticalValueUser = Random.Range(0f, 100f);
                int FinalCriticalUser = 1;
                if (CriticalValueUser <= 6.25f)
                {
                    FinalCriticalUser = 2;
                }
                float Modifier = (FinalCriticalUser * Random.Range(0.85f, 1.0f));




                int Damage = (int)(((((((2 * CurrentBattleTeam.Level) / 5) + 2)))));

                CurrentBattleWild.Life -= Damage;

                BattleUI[1].LifeBar.fillAmount = (float)CurrentBattleWild.Life / (float)CurrentBattleWild.TotalLife;


                if (CurrentBattleWild.Life <= 0)
                {
                    CurrentBattleWild.Life = 0;




                    BattleUI[1].LifeBar.fillAmount = (float)CurrentBattleWild.Life / (float)CurrentBattleWild.TotalLife;


                    float ValueWild = 1;
                   

                    int TempExp = (int)(ValueWild  * CurrentBattleWild.Level) / 7;

                    LevelUpWarrior(TempExp);
                    

                }
                else
                {   
                   
                }
                break;
        } 
    }
    private void ChangeState(BattleStates NewState)
    {
        State = NewState;
        BattleUpdate();
    }
    private void LevelUpWarrior(int _exp)
    {
        FinalWarrior = WarriorsManagers.CloneWarrior(CurrentBattleTeam);

        
        FinalWarrior.Exp += _exp;
        if (FinalWarrior.Exp >= FinalWarrior.NextLevelExp)
        {
            FinalWarrior.Level++;
            FinalWarrior.NextLevelExp = (int)(FinalWarrior.Level * 0.1f + FinalWarrior.Level * 2);
            FinalWarrior.NextLevelExp = (int)((FinalWarrior.Level + 1) * 0.1f + FinalWarrior.Level + 1);

           
           
            while (FinalWarrior.Exp > FinalWarrior.NextLevelExp && FinalWarrior.Level < WarriorsManagers.MaxLevel)
            {
                FinalWarrior.Level++;
                FinalWarrior.NextLevelExp = (int)(FinalWarrior.Level * 0.1f + FinalWarrior.Level * 2);
                FinalWarrior.NextLevelExp = (int)((FinalWarrior.Level + 1) * 0.1f + FinalWarrior.Level + 1 );
              
               
            }
        }

       

    }
}
