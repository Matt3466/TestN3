using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject Manager;
    GameObject Player1;
    GameObject Player2;
    GameObject NPC1;
    GameObject NPC2;

    public GameObject fenetre;
    public GameObject MessageDeVictoir;
    public GameObject MessageDeDefaite;
    public GameObject fenetreDebut;
    public GameObject QuitGame;
    public GameObject Panel;
    bool azer = false;
    void Start()
    {
        //Manager = GameObject.Find("Manager");
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        NPC1 = GameObject.Find("NPC1");
        NPC2 = GameObject.Find("NPC2");
        //fenetre = GameObject.Find("FenetreDebutEtFin");
        fenetre.SetActive(true);
        MessageDeDefaite.SetActive(false);
        MessageDeVictoir.SetActive(false);
        Panel.SetActive(false);


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            QuitGame.SetActive(azer);
            Panel.SetActive(azer);
            azer = !azer;
        }
    }
    public void GameOver()
    {

            FinDuJeuAffichage();

            MessageDeDefaite.SetActive(true);

    }
    public void GameWin()
    {



        MessageDeVictoir.SetActive(true);
        FinDuJeuAffichage();
        //Debug.Log("Vous avez gagner");
    }
    private void FinDuJeuAffichage()
    {


        fenetre.SetActive(true);
        QuitGame.SetActive(true);
        
        /*
        Manager.GetComponent<TurnManager>().enabled = false;
        Manager.GetComponent<CombatManager>().enabled = false;
        Player1.GetComponent<PlayerMove>().enabled = false;
        Player2.GetComponent<PlayerMove>().enabled = false;
        Player1.GetComponent<CombatPlayer>().enabled = false;
        Player2.GetComponent<CombatPlayer>().enabled = false;



        NPC1.GetComponent<NPCMove>().enabled = false;
        NPC2.GetComponent<NPCMove>().enabled = false;
        NPC1.GetComponent<CombatNPC>().enabled = false;
        NPC2.GetComponent<CombatNPC>().enabled = false;*/
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
