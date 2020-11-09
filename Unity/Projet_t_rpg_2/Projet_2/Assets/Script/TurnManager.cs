﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>(); // permet de faire appelle à la liste de tuiles contenu dans TacticsMove
    static Queue<string> turnKey = new Queue<string>(); // Queue est une classe générique. Elle permet de crée une chaine avec des éléments on s'en sert comme d'une file d'attente
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();          // permet de gérer les tours

    bool StartGameBool = false ;


    // Update is called once per frame
    void Update()
    {
        if (StartGameBool)
        {
            if (turnTeam.Count == 0)
            {
                InitTeamTurnQueue();
            }
        }

    }

    public void StartGame()
    {
        StartGameBool = true;
    }
    static void InitTeamTurnQueue()
    {


            List<TacticsMove> teamList = units[turnKey.Peek()];

            foreach (TacticsMove unit in teamList)
            {

                if (unit.tag == "Player")
                {
                    if (!unit.GetComponent<CombatPlayer>().PlayerKO)
                    {
                        turnTeam.Enqueue(unit);
                    }
                }
                if (unit.tag == "NPC")
                {
                    if (!unit.GetComponent<CombatNPC>().NPCKO)
                    {
                        turnTeam.Enqueue(unit);
                    }
                }

            

            StartTurn();

        }
    }

    public static void StartTurn()
    {
            if (turnTeam.Count > 0)
            {
                turnTeam.Peek().BeginTurn();
            }
    }
    public void FinDeTour()
    {
        EndTurn();

    }
    public static void EndTurn()
    {
        
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }
     public static void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }





}