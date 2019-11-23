using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;

    CharacterController charControl;
    PlayerAttributes playerAttributes;
    Animator animator;

    Vector3 up;

    [SerializeField] GameObject gameOverObject = null;
    [SerializeField] GameObject youWinObject = null;
    [SerializeField] GameObject scoreDisplay = null;
    [SerializeField] GameObject healthDisplay = null;
    [SerializeField] Text winningScoreDisplay = null;

    [SerializeField] float gameOverDelay = 1.5f;
    private bool leftBossHasBeenKilled = false;
    private bool rightBossHasBeenKilled = false;

    void Start()
    {
        charControl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerAttributes = GetComponent<PlayerAttributes>();
        up = transform.up;

        playerAttributes.onDeath += Die;
    }

    void Update()
    {
        // Player input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 movement = input * speed * playerAttributes.GetCurMoveSpeed();
        animator.SetFloat("MoveSpeed", movement.magnitude);

        // Gravity
        movement += Physics.gravity;

        // Apply movement
        charControl.Move(movement * Time.deltaTime);

        // Rotate player towards cursor
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 pointInWorld = ray.GetPoint(distance);
            Vector3 direction = pointInWorld - transform.position;
            transform.rotation = Quaternion.LookRotation(direction, up);
        }
        else
        {
            Debug.Log("Raycast from mouse pos failed in " + name);
        }

        if (leftBossHasBeenKilled && rightBossHasBeenKilled) Win();
    }

    void Die()
    {
        enabled = false;
        GetComponent<ShootingLogic>().enabled = false;
        GetComponent<PlayerAttributes>().enabled = false;
        animator.SetTrigger("Death");
        Invoke("ShowGameOver", gameOverDelay);
    }

    void Win()
    {
        enabled = false;
        GetComponent<ShootingLogic>().enabled = false;
        GetComponent<PlayerAttributes>().enabled = false;
        Invoke("ShowYouWin", gameOverDelay);
    }

    void ShowGameOver()
    {
        gameOverObject.SetActive(true);
        scoreDisplay.SetActive(false);
        healthDisplay.SetActive(false);
        GameObject.FindGameObjectWithTag("Music").SetActive(false);
    }

    void ShowYouWin()
    {
        youWinObject.SetActive(true);
        scoreDisplay.SetActive(false);
        winningScoreDisplay.text = "Your score: " + playerAttributes.GetCurPlayerScore();
        playerAttributes.PlayWinSound();
        healthDisplay.SetActive(false);
        GameObject.FindGameObjectWithTag("Music").SetActive(false);
    }

    public void LeftBossKilled() { leftBossHasBeenKilled = true; }
    public void RightBossKilled() { rightBossHasBeenKilled = true; }
}
