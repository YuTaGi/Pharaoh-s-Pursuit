using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;         // เลือดของศัตรู
    public float speed = 3.5f;         // ความเร็วการเคลื่อนที่
    public float damage = 10f;         // ดาเมจที่ทำต่อผู้เล่น
    public float attackCooldown = 1f;  // เวลาหน่วงก่อนโจมตีอีกครั้ง
    public float detectRange = 10f;    // ระยะตรวจจับผู้เล่น

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

        if (distance <= detectRange)  // ถ้าผู้เล่นอยู่ในระยะตรวจจับ
        {
            agent.SetDestination(player.position); // เดินไปหาผู้เล่น
        }
        else
        {
            agent.ResetPath(); // หยุดเมื่ออยู่นอกระยะ
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastAttackTime = Time.time; // อัปเดตเวลาการโจมตี
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
        Destroy(gameObject); // ลบศัตรูออกจากเกม
    }
}

