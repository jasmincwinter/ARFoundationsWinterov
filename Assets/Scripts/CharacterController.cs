using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public GameObject character;

    private int score;


    public TextMeshProUGUI scoreText;
    //public GameObject scoreText;
    //public TextMeshProUGUI scoreTextMesh;


    public Animation anim;
    public Animator animator;

    public Button startButton;
    public Button jumpButton; 

    private bool startClick;

    public Vector3 jump;
    public float jumpForce = 2.0f;
    public Rigidbody rb;

    public GameObject startbutton;


    private void Start()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("/Canvas/Score").GetComponent<TextMeshProUGUI>();
        }

        if (startButton == null)
        {
            //startButton = GameObject.Find("/Canvas/Start").GetComponent<Button>();
            startButton = GameObject.FindGameObjectWithTag("start").GetComponent<Button>();
            //startbutton = GameObject.FindGameObjectWithTag("start");
            Debug.Log("Start button is assigned");
            startButton.onClick.AddListener(StartOnClick);
            //startButton.onClick.
        }

        if (jumpButton == null)
        {
            //startButton = GameObject.Find("/Canvas/Jump")'
            jumpButton = GameObject.FindGameObjectWithTag("jump").GetComponent<Button>();
            Debug.Log("Jump button is assigned");
            jumpButton.onClick.AddListener(JumpOnClick);
        }

        //
        //

        animator = GetComponent<Animator>();
        //anim = GetComponent<Animation>();
        score = 0;

        scoreText.enabled = true; 
        
        SetScoreText();

        rb = GetComponent<Rigidbody>();
        jump = new Vector3(-0.0f, 1.5f, 0.0f); 

        //anim.Play("Idle");
    }

    void Update()
    {
        //character.transform.Translate(Vector3.forward * Time.deltaTime * 0.07f);

        //WaitForStart(); 

        //SetScoreText();

        //anim.Play("Running");

        if (startClick == true)
        {
            character.transform.Translate(Vector3.forward * Time.deltaTime * 0.07f);
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //}
    }

    public void StartOnClick()
    {
        //anim.Play("Running");
        animator.SetBool("IsRunning", true);
        //character.transform.Translate(Vector3.forward * Time.deltaTime * 0.07f);
        startClick = true;
    }



    public void JumpOnClick()
    {
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
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

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    //IEnumerator WaitForStart()
    //{
    //    yield return new WaitForSeconds(2);
    //    character.transform.Translate(Vector3.forward * Time.deltaTime * 0.07f);
    //}
}
