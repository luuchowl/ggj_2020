using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneLoader : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Final");
    }
}
