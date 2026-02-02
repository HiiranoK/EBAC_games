using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameScore : MonoBehaviour
{
    private VisualElement m_Root;
    private Label m_PlayerScore;
    private Label m_EnemyScore;
    private Label m_EndGame;
    
    private void Awake()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_PlayerScore = m_Root.Q<Label>("PlayerScore");
        m_EnemyScore = m_Root.Q<Label>("EnemyScore");
        m_EndGame = m_Root.Q<Label>("EndGame");
    }

    private void OnEnable()
    {
        DisableWinnerText();
    }

    private void DisableWinnerText()
    {
        m_EndGame.style.display = DisplayStyle.None;
    }
    
    public void UpdatePlayerScore(int playerScore)
    {
        if(m_PlayerScore != null) m_PlayerScore.text = playerScore.ToString("D2");
    }
    public void UpdateEnemyScore(int enemyScore)
    {
        if(m_EnemyScore!= null) m_EnemyScore.text = enemyScore.ToString("D2");
    }

    public void UpdateEndGame(string winner)
    {
        m_EndGame.style.display = DisplayStyle.Flex;
        if (m_EndGame != null) m_EndGame.text = $"{winner} Wins!";
        Invoke("DisableWinnerText",3f);
    }
}
