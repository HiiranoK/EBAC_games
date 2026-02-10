using System;
using UnityEngine;

namespace Pong
{
    public class GameManager : MonoBehaviour
    {
    
    
    
        [SerializeField] private Transform playerPaddle;
        [SerializeField] private Transform enemyPaddle;
        [SerializeField] private GameInfo gameInfo;
        [SerializeField] private BallController ballController;
    
        [SerializeField]private GameScore score;
        [SerializeField]private int winPoints = 5;
        private int _playerScore = 0;
        private int _enemyScore = 0;

        void Start()
        {
            ResetGame();
        }

        private void OnEnable()
        {
            BallController.OnPlayerScored += ScorePlayer;
            BallController.OnEnemyScored += ScoreEnemy;
            playerPaddle.gameObject.GetComponent<SpriteRenderer>().color = gameInfo.playerColor;
            enemyPaddle.gameObject.GetComponent<SpriteRenderer>().color = gameInfo.enemyColor;
        }

        private void OnDisable()
        {
            BallController.OnPlayerScored -= ScorePlayer;
            BallController.OnEnemyScored -= ScoreEnemy;
        }
        
        private void ResetGame()
        {
            playerPaddle.position = new Vector3(-7f, 0f, 0f);
            enemyPaddle.position = new Vector3(7f, 0f, 0f);
            ballController.ResetBall(1f);
            _playerScore = 0;
            _enemyScore = 0;
            score.UpdatePlayerScore(_playerScore);
            score.UpdateEnemyScore(_enemyScore);
        }

        private void ScorePlayer()
        {
            _playerScore++;
            score.UpdatePlayerScore(_playerScore);
            gameInfo.CheckSetPlayerHighScore(_playerScore);
            CheckWin();
        }

        private void ScoreEnemy()
        {
            _enemyScore++;
            score.UpdateEnemyScore(_enemyScore);
            gameInfo.CheckSetEnemyHighScore(_enemyScore);
            CheckWin();
        }
    
        private void CheckWin()
        {
            if (_playerScore >= winPoints)
            {
                score.UpdateEndGame(gameInfo.playerName);
                ResetGame();
            }
            else if (_enemyScore >= winPoints)
            {
                score.UpdateEndGame(gameInfo.enemyName);
                ResetGame();
            }
        }
    }
}
