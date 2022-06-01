using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TMP_Text scoreText;
    private Vector3 startEnemy = new Vector3(2.49f, -0.67f, 0.0f);
    private Vector3 startMario = new Vector3(-0.09f, -0.15f, 0.0f) ;
    void Awake()
    {
        Time.timeScale = 0.0f;
    }
    public void StartButtonClicked()
    {   
        
        scoreText.text = "Score: 0";
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
