using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Menu,
    Playing,
    Dead
}

public class GameDirector : MonoBehaviour
{
    public Player player;
    public PivotController pivot;
    public GameStateHolder state;

    void Awake()
    {
        state.state = GameState.Playing;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state.state == GameState.Playing) {
            if (player.dead) {
                state.state = GameState.Dead;
                pivot.linearSpeed = 0f;
            }
        }
    }
}
