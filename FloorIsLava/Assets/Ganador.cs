using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ganador : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public Button winButton;

    private void Start()
    {
        winText.gameObject.SetActive(false);
        winButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winText.gameObject.SetActive(true);
            winButton.gameObject.SetActive(true);
            winText.text = "¡Has ganado!";
        }
    }
    public void NextSceneLoadLvl1()
    {
        SceneManager.LoadScene("lvl1");
    }
    public void NextSceneLoadLvl2()
    {
        SceneManager.LoadScene("lvl3");
    }
    public void NextSceneLoadLvl3()
    {
        SceneManager.LoadScene("lvl2");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
