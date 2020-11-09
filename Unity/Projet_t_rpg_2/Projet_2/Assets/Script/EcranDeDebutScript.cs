using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcranDeDebutScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(desactivation());
    }

    // Update is called once per frame

    IEnumerator desactivation()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
