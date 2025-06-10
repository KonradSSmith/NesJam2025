using System.Collections;
using UnityEngine;

public class EnemyJousting : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AgentDriving agentDriving;
    [SerializeField] PlacementChecker placementChecker;
    [SerializeField] Sprite walkSprite1;
    [SerializeField] Sprite walkSprite2;
    [SerializeField] Sprite hitSprite;
    [SerializeField] float hitAnimationTime;

    bool invincible;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetJousted(float damage)
    {
        if (!invincible)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                StartCoroutine(EnemyDead());
            }
            placementChecker.health = health;
            spriteRenderer.sprite = hitSprite;
            agentDriving.SlowDown();
            StartCoroutine(invincibilityTime());
        }

    }
    IEnumerator invincibilityTime()
    {
        invincible = true;
        yield return new WaitForSeconds(hitAnimationTime);
        invincible = false;
        spriteRenderer.sprite = walkSprite1;
    }

    IEnumerator EnemyDead()
    {
        agentDriving.StopDriving();
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        PlacementManager.instance.amountAlive -= 1;

        Destroy(gameObject);
    }
}
