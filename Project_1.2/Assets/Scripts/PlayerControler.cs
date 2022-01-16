using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{ 
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coins;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text coinsText;
    [SerializeField] private Score scoreScript;

    private int lineTomove = 1;
    public float lineDistance = 4;
    private float  maxSpeed = 100;

    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        StartCoroutine(SpeedIncrease());
        Time.timeScale = 1;
        
    }

    private void Update()
    {
        if (SwaipController.swipeRight)
        {
            if (lineTomove < 2)
                lineTomove++;
        }
        if (SwaipController.swipeLeft)
        {
            if (lineTomove > 0)
                lineTomove--;
        }

        if (SwaipController.swipeUp)
        {
            if (controller.isGrounded)
            Jump();
        }

        Vector3 targePosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineTomove == 0)
            targePosition += Vector3.left * lineDistance;
        else if (lineTomove == 2)
            targePosition += Vector3.right * lineDistance;

        if (transform.position == targePosition)
            return;
        Vector3 diff = targePosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        
    }
    // Update is called once per frame
    private void Jump()
    {
        dir.y = jumpForce;
    }
    private void FixedUpdate()
    {
        controller.Move(dir * Time.fixedDeltaTime);
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true);
            int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
            PlayerPrefs.SetInt("lastRunScore", lastRunScore);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Coin")
        {
            coins++;
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }
    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(10);
        if (speed < maxSpeed)
        {
            speed += 2;
            StartCoroutine(SpeedIncrease());
        }
        
    }
}
