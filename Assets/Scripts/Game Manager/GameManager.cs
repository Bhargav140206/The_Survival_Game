using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float gameTime = 300f; // 5 Minutes

    private bool gameEnded = false;

    [Header("Kill Targets")]
    public int humanKills = 0;
    public int pigKills = 0;

    public int requiredHumanKills = 10;
    public int requiredPigKills = 10;

    [Header("UI References")]
    public TMP_Text timerText;

    public TMP_Text humanKillText;
    public TMP_Text pigKillText;

    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

       
        Time.timeScale = 1f;

        UpdateKillUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (gameEnded)
            return;

        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            gameTime = 0;

            LoseGame();
        }

        UpdateTimerUI();

        if (humanKills >= requiredHumanKills &&
            pigKills >= requiredPigKills)
        {
            WinGame();
        }
    }


    void UpdateTimerUI()
    {
        int minutes =
            Mathf.FloorToInt(gameTime / 60);

        int seconds =
            Mathf.FloorToInt(gameTime % 60);

        if (timerText != null)
        {
            timerText.text =
                string.Format("{0:00}:{1:00}",
                minutes,
                seconds);
        }
    }


    void UpdateKillUI()
    {
        if (humanKillText != null)
        {
            humanKillText.text =
                "Humans: " +
                humanKills +
                " / " +
                requiredHumanKills;
        }

        if (pigKillText != null)
        {
            pigKillText.text =
                "Pigs: " +
                pigKills +
                " / " +
                requiredPigKills;
        }
    }

    public void AddKill(string enemyTag)
    {
        if (enemyTag == "HumanEnemy")
        {
            humanKills++;
        }

        if (enemyTag == "PigEnemy")
        {
            pigKills++;
        }

        UpdateKillUI();
    }

    void WinGame()
    {
        gameEnded = true;

        Debug.Log("YOU WIN");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

 
    void LoseGame()
    {
        gameEnded = true;

        Debug.Log("YOU LOSE");

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}