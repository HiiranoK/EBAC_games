using UnityEngine;
public class GameManager : MonoBehaviour
{
    
    
    
    [SerializeField] private Transform playerPaddle;
    [SerializeField] private Transform enemyPaddle;
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private BallController ballController;
    
    [SerializeField]private GameScore _score;
    
    [SerializeField]private int winPoints = 5;
    private int playerScore = 0;
    private int enemyScore = 0;

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
        
        playerScore = 0;
        enemyScore = 0;
        _score.UpdatePlayerScore(playerScore);
        _score.UpdateEnemyScore(enemyScore);
    }

    private void ScorePlayer()
    {
        playerScore++;
        _score.UpdatePlayerScore(playerScore);
        gameInfo.CheckSetPlayerHighScore(playerScore);
        CheckWin();
    }

    private void ScoreEnemy()
    {
        enemyScore++;
        _score.UpdateEnemyScore(enemyScore);
        gameInfo.CheckSetEnemyHighScore(enemyScore);
        CheckWin();
    }
    
    private void CheckWin()
    {
        if (playerScore >= winPoints)
        {
            _score.UpdateEndGame(gameInfo.playerName);
            ResetGame();
        }
        else if (enemyScore >= winPoints)
        {
            _score.UpdateEndGame("Enemy");
            ResetGame();
        }
    }
}
