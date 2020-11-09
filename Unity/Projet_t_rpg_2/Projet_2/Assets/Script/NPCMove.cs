using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{
    GameObject target;
    int NombreDeMort = 0;
    //bool attenteBool = false;

    void Start()
    {
        Init(); // méthode de la classe TacticsMove
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn) // si le tour de ce personnage est fini
        {
            return; // Met un terme à la méthode actuelle, cela permet lorsque ce sera a nouveau le tour de se personnge de relancer la méthode
        }

        if (!moving) // si le PNJ n'est pas en mouvement
        {

                FindNearestTarget(); // il doit calculer l'emplacement de sa cible
                CalculatePath(); //
                FindSelectableTiles();
                actualTargetTile.target = true;
        }
        else
        {
            Move();
        }
    }

    void CalculatePath() // Calcule le chemin
    {
        Tile targetTile = GetTargetTile(target);

        FindPath(targetTile);       //FindPath est en substence l'algorthme A*, ici on lui introduit donc la tuile cible et on lui demande de calculer le meilleur chemin


    }

    void FindNearestTarget() // permet de definir la tuile sur laquel le PNJ va se déplacer
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player"); // Crée un liste avec tous les gameObjects portant le tag "Player" 

        GameObject nearest = null; //nearest = le plus proche
        float distance = Mathf.Infinity; // calcule la distance avec les objects les plus proches
        //int NombreDeMort = 0;
        foreach (GameObject obj in targets) // pour chaque personnage dans la scène
        {
            if (obj.GetComponent<CombatPlayer>().PlayerKO == true)
            {
                NombreDeMort += 1;
                if (NombreDeMort == 2)
                {
                    return;
                }
            }
            if (obj.GetComponent<CombatPlayer>().PlayerKO == false)
            {
                float d = Vector3.Distance(transform.position, obj.transform.position); // Distance permet de calculer la distance euclidienne entre le PNJ et ses cible potentiels

                if (d < distance)// si la distance à parcourir est trop grande, distance devient d
                {
                    distance = d;
                    nearest = obj;

                }
            }


        }

        target = nearest; //la cible est défini
    }

    /*IEnumerator attente()
    {
        yield return new WaitForSeconds(0);
        attenteBool = true;
    }*/

}



/*public class NPCMove : TacticsMove
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }


        if (!moving)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;

        }
        else
        {
            Move();
        }
    }
    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }
    void FindNearestTarget() // permet de définir la cible
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        
        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);
            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }
}*/
