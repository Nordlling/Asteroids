using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float respawnTime = 3f;
    [SerializeField] private int lives = 3;
    [SerializeField] private float respawnInvulnerabilityTime = 3f;
    [SerializeField] private int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if (asteroid.size < 0.75f)
        {
            score += 100;
        } else if (asteroid.size < 1.2f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
    }
    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
    }

    public void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void GameOver()
    {
        
    }
}
