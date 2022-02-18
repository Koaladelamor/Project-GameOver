using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    GameObject m_pawn = null;
    bool m_isEmpty;

    private void Start()
    {
        m_isEmpty = true;
    }

    public bool AddPawn(GameObject p_pawn)
    {
        if (p_pawn.CompareTag("Pawn") && m_isEmpty)
        {
            p_pawn.GetComponent<Pawn>().SetPosition(transform.position);
            m_pawn = p_pawn;
            m_isEmpty = false;
            return true;
        }
        return false;
    }

    public void TakePawn()
    {
        if(m_pawn != null)
        {
            m_pawn = null;
            m_isEmpty = true;
        }
    }

    public bool IsEmpty
    {
        get { return m_isEmpty; }
    }
}
