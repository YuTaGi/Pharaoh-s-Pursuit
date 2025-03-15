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
    private Animator anim;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();  // ดึง Component Animator
        agent.speed = speed;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectRange)
        {
            agent.SetDestination(player.position);
            anim.SetBool("isWalking", true); // เล่นอนิเมชันเดิน

            if (distance <= 1.5f && Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
            }
        }
        else
        {
            agent.ResetPath();
            anim.SetBool("isWalking", false); // หยุดเดิน
        }
    }

    void AttackPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, detectRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                anim.SetTrigger("Attack"); // เล่นอนิเมชันโจมตี
                Debug.Log("Trigger Attack Animation!");
                Debug.Log("Enemy โจมตี Player!");

                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    //anim.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }
            }
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
