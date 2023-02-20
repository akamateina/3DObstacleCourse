using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI minusTime;
    [SerializeField] TextMeshProUGUI endText;
    private float currentTime;
    bool finish = false;

    void Start()
    {
        currentTime = Time.deltaTime;
    }

    void Update()
    {
        if (finish == false)
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("Time: 0");
        }
        else 
        {
            GameObject.Find("Rival").GetComponent<WaypointFollower>().enabled = false;   //stops rival when finish
            endText.text = "You Won!\nYour time was " + currentTime.ToString("0.00") + " seconds.";
            hideText();
            //Invoke("nextLevel", 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            currentTime -= 2;
            showText();
            Invoke("hideText", 1.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            finish = true;
        }
    }

    private void showText()
    {
        minusTime.text = "-2 secs";
    }

    private void hideText()
    {
        minusTime.text = "";

        if (finish == true)
        {
            timerText.text = "";
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void lastLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
