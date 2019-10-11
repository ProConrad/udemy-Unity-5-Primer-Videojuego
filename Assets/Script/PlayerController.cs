using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = game.GetComponent<GameController>().gameState == EnumGameState.PLAYING;
        if (gamePlaying && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0)))
        {
            UpdateState("PlayerJump");
        }
    }

    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }

    public void GameEnded(){
        game.GetComponent<GameController>().gameState = EnumGameState.ENDED;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "enemy"){
            UpdateState("PlayerDie");
            game.GetComponent<GameController>().gameState = EnumGameState.DYING;
        }
    }
}
