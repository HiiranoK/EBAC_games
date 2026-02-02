using System;
using UnityEngine;

public class EnemyPaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D _rb;

    [SerializeField] private GameObject _ball;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_ball)
        {
            float targetY = Mathf.Clamp(_ball.transform.position.y, -4f, 4f);
            Vector2 targetPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
