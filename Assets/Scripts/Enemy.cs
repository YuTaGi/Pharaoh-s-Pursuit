using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;         
    public float speed = 3.5f;         
    public float damage = 10f;         
    public float attackCooldown = 1f;  
    public float detectRange = 10f;    

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectRange)
        {
            agent.SetDestination(player.position); 

            if (distance <= 1.5f && Time.time > lastAttackTime + attackCooldown)
            {
                
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, detectRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Enemy â¨ÁµÕ Player!");
                        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                        if (playerHealth != null)
                        {
                            playerHealth.TakeDamage(damage);
                            lastAttackTime = Time.time;
                        }
                    }
                }
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ScoreManager.instance.AddScore(1);
        Destroy(gameObject);
    }
}

