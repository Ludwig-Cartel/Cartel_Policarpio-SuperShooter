using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level instance;
    uint enemyNum = 0;
    bool nextLevelStart = false;
    float nextLevelTimer = 0;

    int score = 0;
    Text scoretext;

    string[] levels = {"Level1","Level2","Level3","Complete"};
    int currentLevel = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoretext = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        else {
            Destroy(gameObject);
        }
             
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextLevelStart) {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                if (currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                }
                else {
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                nextLevelTimer = 3;
                nextLevelStart = false;
            }
            else {
                nextLevelTimer -= Time.deltaTime;
            }
        }   
    }

    public void resetLevel() {
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>()) {
            Destroy(b.gameObject);
        }
        enemyNum = 0;
        score = 0;
        addScore(score);    
        string sceneName = levels[currentLevel - 1];
        SceneManager.LoadScene(sceneName);
    }
    public void addScore(int scoreToAdd) {
        score += scoreToAdd;
        scoretext.text = score.ToString();
    }
    public void addEnemy() {
        enemyNum++;
    }

    public void removeEnemy() {
        enemyNum--;

        if (enemyNum == 0) {
            nextLevelStart = true;
        }
    }
}
