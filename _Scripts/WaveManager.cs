using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    float timer;
    int seconds;
    int wave;
    public bool prometheaVisible;
    public GameObject[] arloWaypoints;
    public GameObject promethea;

    public int killCount;

    public int playerHealth;

    public GameObject scoreTextObj;
    TextMesh scoreText;
    public GameObject healthTextObj;
    TextMesh healthText;

    public Color healthColor1;
    public Color healthColor2;
    public Color healthColor3;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 10;
        prometheaVisible = false;
        
        wave = 1;
        print("Starting Wave " + wave + ".");
        scoreText = scoreTextObj.GetComponent<TextMesh>();
        healthText = healthTextObj.GetComponent<TextMesh>();
    }

    void HealthBar()
    {
        healthText.text = playerHealth.ToString();
        if(playerHealth >= 7)
        {
            healthText.color = healthColor1;
        }
        else if (playerHealth > 4 && playerHealth < 7)
        {
            healthText.color = healthColor2;
        }
        else if(playerHealth <= 4)
        {
            healthText.color = healthColor3;
        }
    }
    // Update is called once per frame
    void Update()
    {
        WaveClock();

        Wave();

        UpdateText();

        HealthBar();
    }

    void UpdateText()
    {
        scoreText.text = killCount.ToString();
    }

    private void WaveClock()
    {
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
    }

    private void SpawnPromethea()
    {
        GameObject arloCurrWaypoint = arloWaypoints[Random.Range(0, 3)];
        GameObject prometheaObj = Instantiate(promethea, arloCurrWaypoint.transform.position, Quaternion.identity);
        prometheaObj.GetComponent<PrometheaAi>().GetCurrentWaypoint(arloCurrWaypoint);
    }

    private void Wave()
    {
        if (seconds >= 30 && seconds <= 50)
        {
            //Promethea Appears
            if (prometheaVisible != true)
            {
                prometheaVisible = true;
                print("Promethea is visible");
                SpawnPromethea();

            }
        }
        else if (seconds >= 50)
        {
            //Promethea disappears and the timer resets.
            prometheaVisible = false;

            Destroy(GameObject.FindGameObjectWithTag("Promethea"));
            print("Promethea is invisible");
            if (wave == 3)
            {
                //Game Over
                print("Game Over");
            }
            else
            {
                print("Wave " + wave + " is over. Starting Wave " + (wave + 1) + ".");
                wave++;
            }
            timer = 0;
            seconds = 0;
        }
    }
}
