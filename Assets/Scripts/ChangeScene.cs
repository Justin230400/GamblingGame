using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static void F_ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
