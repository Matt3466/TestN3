using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{// script qui permet de changer l'angle de la caméra de 90° à gauche ou à droite il y a donc 4 positions différente [0,1,2,3]
    // deux boutons dans la scène sont nécessaires pour faire fonctionner ce script, l'un doit appeler la fonction "RotateLeft" et l'autre la fonction "RotateRight"


    Animator anim;
    public int xcv = 0; // est en lien avec l'int d'animation, permet en lr modifiant de passer d'une animation à l'autre
    float timeRotat;    // permet de ne pas modifier xcv si une animation est en cours
    public bool freeze = false; // fonction identique
    public float TimeIni = 2f; // temps durant lequel le code de changement d'animation est bloqué




    private void Start()
    {
        anim = GetComponent<Animator>();
        timeRotat = TimeIni;
    }
    public void RotateLeft()
    {
        //transform.Rotate(Vector3.up, 90, Space.Self);
        if (!freeze) // si le code n'est pas bloquer, on incrémente XCV pour passer à l'animation suivante
        {

            if (xcv < 3) // si xcv est inférieur à 3, il passe à l'animation suivante
            {
                xcv += 1;
            }
            else xcv = 0; //  s'il est suppérieur à 3, il reboucle avec le début
            AnimCameVoid(); // lance l'animation en fonction de xcv


        }
    }
    public void RotateRight() // la logique est exactement la même que précédement
    {
        //transform.Rotate(Vector3.up, -90, Space.Self);

        if (!freeze)
        {

            if (xcv > 0)
            {
                xcv -= 1;
            }
            else xcv = 3;

            AnimCameVoid();
        }


    }
    public void AnimCameVoid()
    {
        timeRotat = TimeIni; // on réinitialise le compteur de la Couroutine
        anim.SetInteger("angleCam", xcv);
        freeze = true; // On bloque le code le temps que l'animation se termine
        StartCoroutine(CaR()); // on lance la corutine qui permettra de débloquer le code
    }

    IEnumerator CaR()
    {
        yield return new WaitForSeconds(TimeIni);
        freeze = false;

       /* while (timeRotat > 0)    // temps que le compteur est supérieur à 0, la Couroutine tourne
        {
            timeRotat -= 0.1f; // on le décrémente petit à petit
            yield return new WaitForSeconds(0.1f);
            Debug.Log(timeRotat);
            if (timeRotat <= 0) // lorsque la couroutine à fini de tourner, on débloque la rotation de la caméra
            {
                freeze = false;
            }
       */

        
    }
}