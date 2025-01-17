using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
