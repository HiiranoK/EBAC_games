using UnityEngine;

[CreateAssetMenu(fileName = "GameInfo", menuName = "Scriptable Objects/GameInfo")]
public class GameInfo : ScriptableObject
{
    [SerializeField] public string playerName;
    [SerializeField] public string enemyName;
    [SerializeField] public int playerHighScore;
    [SerializeField] public int enemyHighScore;
    [SerializeField] public Color playerColor = Color.white;
    [SerializeField] public Color enemyColor = Color.white;

    public void ResetInfo()
    {
        playerName = string.Empty;
        enemyName = string.Empty;
        playerHighScore = 0;
        enemyHighScore = 0;
        playerColor = Color.white;
        enemyColor = Color.white;
    }

    public void CheckSetPlayerHighScore(int score)
    {
        if(score > playerHighScore)
        {
            playerHighScore = score;
        }
    }

    public void CheckSetEnemyHighScore(int score)
    {
        if (score > enemyHighScore)
        {
            enemyHighScore = score;
        }
    }
}
