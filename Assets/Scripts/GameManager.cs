using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState {START, PLAYER1TURN, PLAYER2TURN, END }   

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public Player[] m_Players;

    public GameState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.START;
        SetupPhase();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetupPhase ()
    {
        m_Players[0].m_PlayerColor = "blue";
        m_Players[1].m_PlayerColor = "red";

        for (int i = 0;  i < m_Players.Length; i++)
        {
            m_Players[i].m_PlayerNumber = i + 1;

            m_Players[i].m_BrickCount = 0;
            m_Players[i].m_LumberCount = 0;
            m_Players[i].m_OreCount = 0;
            m_Players[i].m_GrainCount = 0;
            m_Players[i].m_WoolCount = 0;

            m_Players[i].m_VictoryPoints = 0;
        }
    }
}