using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CharacterController : MonoBehaviour
{
    public GameObject character;

    private int score;

    public TextMeshProUGUI scoreText; 

    private void Start()
    {
        score = 0;

        scoreText.enabled = true; 

        SetScoreText(); 
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); 
    }

    void Update()
    {
        character.transform.Translate(Vector3.forward * Time.deltaTime * 0.15f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("prize"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;

            SetScoreText(); 
        }
    }
}
