using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatNPC : CombatManager
{

    public float PointDeVie;
    public bool ActionAttaque = true;
    GameObject PlayerCol;
    GameObject NPCCol;
    //EndGame EndGameManagerCD = new EndGame();
    GameObject Manager;

    public Image ImagePV;
    public bool NPCKO;
    Animator anime;




    void Start()
    {
        Manager = GameObject.Find("Manager");
        anime = GetComponent<Animator>();

    }

    void Update()
    {
        CombatInit();

        ImagePV.GetComponent<Image>().fillAmount = PointDeVie/100;
    }




    public void PertePV(int dgt)
    {

        if (gameObject.name == "NPC1")
        {
            anime.SetBool("DegatsNPC1", true);
        }
        if (gameObject.name == "NPC2")
        {
            anime.SetBool("DegatsNPC2", true);
        }
        StartCoroutine(animeDegat());


        PointDeVie -= dgt;
        if (PointDeVie <= 0)
        {
            MortDuNPC();
        }



    }
    public void NPCAttaque()
    {
        StartCoroutine(attente());
    }

    public void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {

            PlayerCol = col.gameObject;


            EngagementPlayer(PlayerCol, this.gameObject);



        }
        if (col.tag == "NPC")
        {
            NPCCol = col.gameObject;

        }
    }
    public void OnTriggerExit(Collider col)
    {

        if (col.tag == "NPC")
        {
            NPCCol = col.gameObject;
        }
        if (col.tag == "Player")
        {
            PlayerCol = col.gameObject;
            DesengagementPlayer(PlayerCol, this.gameObject);

        }
    }


    public void BarreDeVieNPC()
    {
        ImagePV.GetComponent<Image>().fillAmount = PointDeVie;
    }



    public void MortDuNPC()
    {
        NPCKO = true;
        anime.SetBool("MortDuNPC", true);
        if (gameObject.name == "NPC1")
        {
            NPC1EstMort = true;
            Manager.GetComponent<CombatManager>().NPC1EstMort = true;
        }
        if (gameObject.name == "NPC2")
        {
            NPC2EstMort = true;
            Manager.GetComponent<CombatManager>().NPC2EstMort = true;
        }
        FindDuJeu();

    }

    IEnumerator animeDegat()
    {
        yield return new WaitForSeconds(1);
        anime.SetBool("DegatsNPC1", false);
        anime.SetBool("DegatsNPC2", false);
    }


    IEnumerator attente()
    {
        yield return new WaitForSeconds(0);

        if (gameObject.name == "NPC1")
        {
            if (P1ContactAvecNPC1)
            {
                attaquerPlayer1();
                ActionAttaque = false;
            }
            if (P2ContactAvecNPC1)
            {
                attaquerPlayer2();
                ActionAttaque = false;
            }
        }
        if (gameObject.name == "NPC2")
        {
            if (P1ContactAvecNPC2)
            {
                attaquerPlayer1();
                ActionAttaque = false;
            }
            if (P2ContactAvecNPC2)
            {
                attaquerPlayer2();
                ActionAttaque = false;
            }
        }
        ActionAttaque = false;
    }
}