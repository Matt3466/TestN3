using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    void Start()
    {
        
    }

public void changeScene()
    {
        SceneManager.LoadScene("SampleScene"); // on change la scene
    }
}
