using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CombatPlayer : CombatManager
{
    public GameObject FenetrePlayer;
    public GameObject BarreDeVie;
    public GameObject Manager;
//    public GameObject EndGameManager;
//    EndGame EndGameManagerCD = new EndGame();

    GameObject NPCCol;
    GameObject PlayerCol;


    public Image ImagePV;

    public float PointDeVie;

    public bool PlayerKO = false;

    Animator anime;


    void Start()
    {
        FenetrePlayer.SetActive(false);
        anime = GetComponent<Animator>();

    }

    void Update()
    {
        ImagePV.GetComponent<Image>().fillAmount = PointDeVie/100;
        CombatInit();
    }

    public void DebutDeTour()
    {

        FenetrePlayer.SetActive(true);
        CombatInit();
    }
    public void FDT() //à la fin du tour
    {
        FenetrePlayer.SetActive(false);
        CombatInit();


    }

    public void OnTriggerEnter(Collider col)
    {

        if (col.tag == "NPC")
        {

            NPCCol = col.gameObject;


            EngagementPlayer(this.gameObject, NPCCol);



        }
        if (col.tag == "Player")
        {
            PlayerCol = col.gameObject;
            //EngagementPlayer(this.gameObject, NPCCol);

        }
    }
    public void OnTriggerExit(Collider col)
    {

        if (col.tag == "NPC")
        {

            NPCCol = col.gameObject;


            DesengagementPlayer(this.gameObject, NPCCol);


        }
        if (col.tag == "Player")
        {
            PlayerCol = col.gameObject;
            //DesengagementPlayer(this.gameObject, PlayerCol);

        }
    }

    public void SelectionDeCible()
    {
        FenetrePlayer.transform.Find("EndTurn").gameObject.SetActive(false);
        FenetrePlayer.transform.Find("Attaque").gameObject.SetActive(false);
        FenetrePlayer.transform.Find("MenuPrecedent").gameObject.SetActive(true);
        if (this.name == "Player1")
        {
            if (!NPC1EstMort && P1ContactAvecNPC1)
            {
                FenetrePlayer.transform.Find("NPC1Attaque").gameObject.SetActive(true);
            }
            if (!NPC2EstMort && P1ContactAvecNPC2)
            {
                FenetrePlayer.transform.Find("NPC2Attaque").gameObject.SetActive(true);
            }
        }
        if (this.name == "Player2")
        {
            if (!NPC1EstMort && P2ContactAvecNPC1)
            {
                FenetrePlayer.transform.Find("NPC1Attaque").gameObject.SetActive(true);
            }
            if (!NPC2EstMort && P2ContactAvecNPC2)
            {
                FenetrePlayer.transform.Find("NPC2Attaque").gameObject.SetActive(true);
            }
        }



    }
    public void menuPrecedent()
    {
        FenetrePlayer.transform.Find("EndTurn").gameObject.SetActive(true);
        FenetrePlayer.transform.Find("Attaque").gameObject.SetActive(true);
        FenetrePlayer.transform.Find("NPC1Attaque").gameObject.SetActive(false);
        FenetrePlayer.transform.Find("NPC2Attaque").gameObject.SetActive(false);

        FenetrePlayer.transform.Find("MenuPrecedent").gameObject.SetActive(false);
    }



    public void PertePV(int dgt)
    {
        if (gameObject.name == "Player1")
        {
            anime.SetBool("PrendDesDegat", true);
        }
        if (gameObject.name == "Player2")
        {
            anime.SetBool("PrendDesDegats2", true);
        }

        StartCoroutine (animeDegat());
        PointDeVie -= dgt;




        if (PointDeVie <= 0)
            {
            ImagePV.GetComponent<Image>().fillAmount = 0;
            MortDuPerso();
            }

    }

    public void MortDuPerso()
    {
        PlayerKO = true;
        anime.SetBool("MortDuPerso", true);

        if(gameObject.name == "Player1")
        {
            Player1EstMort = true;
            Manager.GetComponent<CombatManager>().Player1EstMort = true;
            Player2.GetComponent<CombatPlayer>().CombatInit();
            if (!Player2EstMort)
            {
                FindDuJeu();
                GetComponent<CombatPlayer>().enabled = false;
            }

        }
        if (gameObject.name == "Player2")
        {
            Manager.GetComponent<CombatManager>().Player2EstMort = true;
            Player2EstMort = true;
            Player1.GetComponent<CombatPlayer>().CombatInit();

            if (!Player1EstMort)
            {
                FindDuJeu();
                GetComponent<CombatPlayer>().enabled = false;
            }


        }

    }
    IEnumerator animeDegat()
    {
        yield return new WaitForSeconds(1);
        anime.SetBool("PrendDesDegat", false);
        anime.SetBool("PrendDesDegats2", false);
    }
}
