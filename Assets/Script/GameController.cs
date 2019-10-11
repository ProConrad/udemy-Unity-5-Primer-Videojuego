using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Range(0, 0.2f)]
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage platform;
    // Start is called before the first frame update
    public GameObject uiTexts;

    public GameObject uiTextsEnd;

    public GameObject player;

    public GameObject enemyGenerator;

    public EnumGameState gameState = EnumGameState.IDLE;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == EnumGameState.IDLE && UserAction())
        {
            gameState = EnumGameState.PLAYING;
            uiTexts.SetActive(false);
            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGeneration");
        }
        else if (gameState == EnumGameState.PLAYING)
        {
            Parallax();
        }
        else if (gameState == EnumGameState.DYING)
        {
            enemyGenerator.SendMessage("StopGeneration", true);
            //gameState = EnumGameState.ENDED;
        }
        else if (gameState == EnumGameState.ENDED)
        {
            uiTextsEnd.SetActive(true);
            if (UserAction())
            {
                RestartGame();
                uiTextsEnd.SetActive(false);
            }
        }
    }

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, background.uvRect.y, background.uvRect.width, background.uvRect.height);
        platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 5, platform.uvRect.y, platform.uvRect.width, platform.uvRect.height);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    bool UserAction()
    {
        return Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);
    }
}
