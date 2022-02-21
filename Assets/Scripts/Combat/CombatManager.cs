using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private GameObject[] pawns;

    private GameObject[] players;
    private GameObject[] enemies;

    int turn;

    public bool startCombat;
    public bool turnDone;

    // Start is called before the first frame update
    void Start()
    {
        turnDone = false;
        turn = 1;

        pawns = GameObject.FindGameObjectsWithTag("Pawn");

        foreach (GameObject pawn in pawns) {
            if (pawn.GetComponent<PawnController>().m_turnOrder == 1) {
                players[0] = pawn;
            }
            else if (pawn.GetComponent<PawnController>().m_turnOrder == 3)
            {
                players[1] = pawn;
            }
            else if (pawn.GetComponent<PawnController>().m_turnOrder == 5)
            {
                players[2] = pawn;
            }
            else if (pawn.GetComponent<PawnController>().m_turnOrder == 2)
            {
                enemies[0] = pawn;
            }
            else if (pawn.GetComponent<PawnController>().m_turnOrder == 4)
            {
                enemies[1] = pawn;
            }
            else if (pawn.GetComponent<PawnController>().m_turnOrder == 6)
            {
                enemies[2] = pawn;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (startCombat)
        {
            if (turn == 1)
            {
                players[0].GetComponent<PawnController>().m_isMyTurn = true;
                if (turnDone)
                {
                    turn++;
                    turnDone = !turnDone;
                }
            }

            if (turn == 2)
            {
                enemies[0].GetComponent<PawnController>().m_isMyTurn = true;
                if (turnDone)
                {
                    turn++;
                    turnDone = !turnDone;
                }
            }
        }




        /*switch (turn) {
            default: break;
            case 1:
                players[0].GetComponent<PawnController>().m_isMyTurn = true;

                break;
            case 2:

        }*/


    }
}
