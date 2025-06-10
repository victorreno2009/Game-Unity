using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Player))]
public class ConstantDamage : MonoBehaviour
{
    public float damageInterval = 0.3f; // tempo entre danos
    public int damagePerTick = 1;

    private float damageTimer = 0f;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        damageTimer -= Time.deltaTime;

        if (damageTimer <= 0f)
        {
            ApplyDamage();
            damageTimer = damageInterval;
        }
    }

    void ApplyDamage()
    {
        if (player.entity.dead) return;

        player.entity.currentHealth -= damagePerTick;

        Debug.Log("Vida restante: " + player.entity.currentHealth);

        if (player.entity.currentHealth <= 0)
        {
            player.entity.dead = true;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Morreu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // troca de cena
    }
}
