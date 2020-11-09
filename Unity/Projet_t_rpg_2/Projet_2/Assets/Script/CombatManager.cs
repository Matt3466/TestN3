using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    /*GameObject FenetrePlayer1;
    GameObject FenetrePlayer2;*/
    public GameObject NPC1;
    public GameObject NPC2;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject EndGameManager;
    public GameObject BulleInfo1;
    public GameObject BulleInfo2;
    public GameObject BulleInfo3;

    public Text TxtBulleInfo1;
    public Text TxtBulleInfo2;
    public Text TxtBulleInfo3;

    public int DegatDeBase = 10;
    public int DGTCombo = 20;


    public bool NPC1EstMort;
    public bool NPC2EstMort;
    public bool Player1EstMort;
    public bool Player2EstMort;





    public bool P1ContactAvecNPC1 = false;
    public bool P1ContactAvecNPC2 = false;

    public bool P2ContactAvecNPC1 = false;
    public bool P2ContactAvecNPC2 = false;



    void Update()
    {
        FindDuJeu();
        CombatInit();
        TxtBulleInfo1.GetComponent<Text>().color = BulleInfo1.GetComponent<Image>().color;
        TxtBulleInfo2.GetComponent<Text>().color = BulleInfo2.GetComponent<Image>().color;
        TxtBulleInfo3.GetComponent<Text>().color = BulleInfo3.GetComponent<Image>().color;

    }



    public void CombatInit()
    {

        NPC1EstMort = NPC1.GetComponent<CombatNPC>().NPC1EstMort;
        NPC2EstMort = NPC2.GetComponent<CombatNPC>().NPC2EstMort;
        Player1EstMort = Player1.GetComponent<CombatPlayer>().Player1EstMort;
        Player2EstMort = Player2.GetComponent<CombatPlayer>().Player2EstMort;

        P1ContactAvecNPC1 = Player1.GetComponent<CombatPlayer>().P1ContactAvecNPC1;
        P1ContactAvecNPC2 = Player1.GetComponent<CombatPlayer>().P1ContactAvecNPC2;
        P2ContactAvecNPC1 = Player2.GetComponent<CombatPlayer>().P2ContactAvecNPC1;
        P2ContactAvecNPC2 = Player2.GetComponent<CombatPlayer>().P2ContactAvecNPC2;


    }


    public void EngagementPlayer(GameObject Player, GameObject NPCEngage)
    {

        if (Player.name == "Player1")
        {
            if (NPCEngage.name == "NPC1")
            {
                P1ContactAvecNPC1 = true;
            }
            if (NPCEngage.name == "NPC2")
            {
                P1ContactAvecNPC2 = true;
            }
        }
        if (Player.name == "Player2")
        {
            if (NPCEngage.name == "NPC1")
            {
                P2ContactAvecNPC1 = true;
            }
            if (NPCEngage.name == "NPC2")
            {
                P2ContactAvecNPC2 = true;
            }
        }



    }
    public void DesengagementPlayer(GameObject Player, GameObject NPCEngage)
    {
        if (Player.name == "Player1")
        {
            if (NPCEngage.name == "NPC1")
            {
                P1ContactAvecNPC1 = false;
            }
            if (NPCEngage.name == "NPC2")
            {
                P1ContactAvecNPC2 = false;
            }
        }
        if (Player.name == "Player2")
        {
            if (NPCEngage.name == "NPC1")
            {
                P2ContactAvecNPC1 = false;
            }
            if (NPCEngage.name == "NPC2")
            {
                P2ContactAvecNPC2 = false;
            }
        }

        /*if (!P1ContactAvecNPC1 && !P1ContactAvecNPC2)
        {
            ContactAvecEnnemi = false;
        }*/
    }

    public void attaquerNPC1()
    { if (!NPC1EstMort)
        {
            if (P1ContactAvecNPC1 && P2ContactAvecNPC1)
            {
                NPC1.GetComponent<CombatNPC>().PertePV(DGTCombo);
                gestionBulle("Ennemi 1", DGTCombo);
            }
            if (!P1ContactAvecNPC1 || !P2ContactAvecNPC1)
            {
                NPC1.GetComponent<CombatNPC>().PertePV(DegatDeBase);
                gestionBulle("Ennemi 1", DegatDeBase);

            }
        }
    }
    public void attaquerNPC2()
    {
        if (!NPC2EstMort)
        {
            if (P1ContactAvecNPC2 && P2ContactAvecNPC2)
            {
                NPC2.GetComponent<CombatNPC>().PertePV(DGTCombo);
                gestionBulle("Ennemi 2", DGTCombo);

            }
            if (!P1ContactAvecNPC2 || !P2ContactAvecNPC2)
            {
                NPC2.GetComponent<CombatNPC>().PertePV(DegatDeBase);
                gestionBulle("Ennemi 2", DegatDeBase);

            }
        }
    }
    public void attaquerPlayer1()
    {
        if (!Player1EstMort)
        {
            if (P1ContactAvecNPC1 && P1ContactAvecNPC2)
            {
                Player1.GetComponent<CombatPlayer>().PertePV(DGTCombo);
                gestionBulle("Personnage 1", DGTCombo);

            }
            else if (!P1ContactAvecNPC1 || !P1ContactAvecNPC2)
            {
                Player1.GetComponent<CombatPlayer>().PertePV(DegatDeBase);
                gestionBulle("Personnage 1", DegatDeBase);

            }
        }
    }
    public void attaquerPlayer2()
    {
        if (!Player2EstMort)
        {
            if (P2ContactAvecNPC1 && P2ContactAvecNPC2)
            {
                Player2.GetComponent<CombatPlayer>().PertePV(DGTCombo);
                gestionBulle("Personnage 2", DGTCombo);

            }
            else if (!P2ContactAvecNPC1 || !P2ContactAvecNPC2)
            {
                Player2.GetComponent<CombatPlayer>().PertePV(DegatDeBase);
                gestionBulle("Personnage 2", DegatDeBase);

            }
        }
    }




    public void gestionBulle(string victime, int degatSubit)
    {
        if (!BulleInfo1.GetComponent<Animator>().GetBool("BulleActive"))
        {
            BulleInfo1.GetComponent<Animator>().SetBool("BulleActive", true);
            TxtBulleInfo1.text = victime + " a perdu " + degatSubit + " Points de Vie";
            StartCoroutine(animBulle());
        }
        else if(!BulleInfo2.GetComponent<Animator>().GetBool("BulleActive"))
        {
            BulleInfo2.GetComponent<Animator>().SetBool("BulleActive", true);

            TxtBulleInfo2.text = victime + " a perdu " + degatSubit + " Points de Vie";
            StartCoroutine(animBulle2());
        }
        else if (!BulleInfo3.GetComponent<Animator>().GetBool("BulleActive"))
        {
            BulleInfo3.GetComponent<Animator>().SetBool("BulleActive", true);

            TxtBulleInfo3.text = victime + " a perdu " + degatSubit + " Points de Vie";
            StartCoroutine(animBulle3());
        }

    }

    IEnumerator animBulle()
    {
        yield return new WaitForSeconds(2);
        BulleInfo1.GetComponent<Animator>().SetBool("BulleActive", false);

    }
    IEnumerator animBulle2()
    {
        yield return new WaitForSeconds(2);
        BulleInfo2.GetComponent<Animator>().SetBool("BulleActive", false);

    }
    IEnumerator animBulle3()
    {
        yield return new WaitForSeconds(2);
        BulleInfo3.GetComponent<Animator>().SetBool("BulleActive", false);

    }



    public void FindDuJeu()
    {
        if (NPC1EstMort && NPC2EstMort)
        {
            EndGameManager.GetComponent<EndGame>().GameWin();

        }
        if (Player1EstMort && Player2EstMort)
        {
            EndGameManager.GetComponent<EndGame>().GameOver();
        }
    }



}
