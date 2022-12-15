using System;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    public float size = 1f;  //todo
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float minSize = 0.5f;
    [SerializeField] private float maxSize = 1.5f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float maxLifetime = 30f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        transform.localScale = Vector3.one * size;

        _rigidbody.mass = size;
    }
    
    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            if (size * 0.5f >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid halfAsteroid = Instantiate(this, position, transform.rotation);
        halfAsteroid.size = size * 0.5f;
        halfAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
    
    public float getMinSize()
    {
        return minSize;
    }
    
    public float getMaxSize()
    {
        return maxSize;
    }
}
