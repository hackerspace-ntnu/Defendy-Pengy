using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager{
    void GameLost();
    void GameStart();
    void GamePause();
    void GameRestart();
    void GameWin();
}
