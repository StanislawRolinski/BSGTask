using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public List<GameObject> enemies;
    public static SceneController sceneController;
    [SerializeField] Text enemyCountText;
    [SerializeField] Text winText;

    private void Awake()
    {
        sceneController = this;
        
    }
    private void Start()
    {
        enemies = new List<GameObject>();
        winText.enabled = false;
        Invoke("UpdateText", .1f);
    }
    

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        UpdateText();
        if (enemies.Count == 0)
        {
            StartCoroutine(RestartGame());
        }
    }
    private IEnumerator RestartGame()
    {
        winText.enabled = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
    private void UpdateText()
    {
        enemyCountText.text = "Enemies left: " + enemies.Count;
    }
}
