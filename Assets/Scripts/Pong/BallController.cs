using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pong
{
    public class BallController : MonoBehaviour
    {
        [SerializeField]  private float initialVelocity= 5f;
        private Rigidbody2D _rb;
        private GameManager _gameManager;
    
        public static event Action OnPlayerScored;
        public static event Action OnEnemyScored;
    
        public void ResetBall(float direction)
        {
            transform.position = Vector3.zero;

            if (!_rb) _rb = GetComponent<Rigidbody2D>();
        
            _rb.linearVelocity = new Vector2(Random.Range(3, 6) * direction, initialVelocity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                Vector2 newVelocity = _rb.linearVelocity;
                newVelocity.y = -newVelocity.y;
                _rb.linearVelocity = newVelocity;
            }
            else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            {
                _rb.linearVelocity = new Vector2(-_rb.linearVelocity.x, _rb.linearVelocity.y);
            }
            else if (other.gameObject.CompareTag("WallPlayer"))
            {
                OnPlayerScored?.Invoke();
                ResetBall(-1f);
            }
            else if (other.gameObject.CompareTag("WallEnemy"))
            {
                OnEnemyScored?.Invoke();
                ResetBall(1f);
            }
        }
    }
}
