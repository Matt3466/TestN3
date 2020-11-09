using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimeBulleInfo : MonoBehaviour
{
    /* public GameObject BulleInfo1;
     public GameObject BulleInfo2;
     public GameObject BulleInfo3;
     public GameObject BulleInfo4;


     Text text1;
     Text text2;
     Text text3;
     Text text4;

     Animator anim1;
     Animator anim2;
     Animator anim3;
     Animator anim4;*/


    // Start is called before the first frame update
    /*void Start()
    {
        text1 = BulleInfo1.GetComponentInChildren<Text>();
        text2 = BulleInfo2.GetComponentInChildren<Text>();
        text3 = BulleInfo3.GetComponentInChildren<Text>();
        text4 = BulleInfo4.GetComponentInChildren<Text>();

        anim1 = BulleInfo1.GetComponent<Animator>();
        anim2 = BulleInfo2.GetComponent<Animator>();
        anim3 = BulleInfo3.GetComponent<Animator>();
        anim4 = BulleInfo4.GetComponent<Animator>();

        anim1.SetBool("Apparition", true);

        anim3.SetBool("Apparition", true);
        anim4.SetBool("Apparition", false);



        anim1.SetBool("BulleN1", true );
        anim1.SetBool("BulleN2", true);
        anim3.SetBool("BulleN3", true);
        anim4.SetBool("BulleN4", true);




    }
 
    void Update()
    {
        text1.GetComponent<Text>().color = BulleInfo1.GetComponent<Image>().color;
        text2.GetComponent<Text>().color = BulleInfo2.GetComponent<Image>().color;

    }*/














    /*public void lancemenDesAnime()
    {
        ManagerAnim(anim1);
        ManagerAnim(anim2);
        ManagerAnim(anim3);
        ManagerAnim(anim4);
        
        if (anim1.GetBool("BulleN1") == true)
        {
            anim1.SetBool("BulleN1", false);
        }
        if (anim2.GetBool("BulleN2") == true)
        {
            anim2.SetBool("BulleN2", false);
        }
        if (anim3.GetBool("BulleN3") == true)
        {
            anim3.SetBool("BulleN3", false);
        }
        if (anim4.GetBool("BulleN4") == true)
        {
            anim4.SetBool("BulleN4", false);
        }
    }
    public void ManagerAnim(Animator animBI)
    {
        animBI.SetBool("Apparition", !animBI.GetBool("Apparition"));


        if (!animBI.GetBool("Suivant"))
        {
            animBI.SetBool("Suivant", true);
        }


        if (animBI.GetBool("Suivant") && !animBI.GetBool("Apparition"))
        {
            //anim1.SetBool("Apparition", false);
            animBI.SetBool("Suivant", false);
        }


        if (animBI.GetBool("Suivant") && animBI.GetBool("Apparition"))
        {
            animBI.SetBool("Apparition", false);
        }


        if (!animBI.GetBool("Suivant") && !animBI.GetBool("Apparition"))
        {
            animBI.SetBool("Apparition", true);
        }*/








}