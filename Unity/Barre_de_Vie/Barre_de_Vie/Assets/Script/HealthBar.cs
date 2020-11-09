using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    static Image Barre;
    static Text txt;
    public float max { get; set; }
    private float Valeur;
    public float valeur
    {
        get { return Valeur; }
        set
        {
            Valeur = Mathf.Clamp(value, 0, max); // par exemple: Valeur = Mathf.Clamp(10,0,9) veux dire que Valeur sera égale à 10 et sera forcément compris entre 0 et 9
            if (Barre != null) // permet d'aviter une erreur si l'image de la barre ne se charge pas au moment du Start
            {
                Barre.fillAmount = (1 / max) * Valeur; // fillAmount est une valeur entre 0 et 1, il faut donc faire un produit en croix pour ajuster le nombre de PV avec le remplissage de la barre de vie
                txt.text = Barre.fillAmount * 100 + "%";
            }

        }
    }

    void Start()
    {
        Barre = GetComponent<Image>();
        txt = transform.GetComponentInChildren<Text>();
        txt.text = Barre.fillAmount * 100 + "%";


    }



}
