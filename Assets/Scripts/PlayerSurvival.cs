using UnityEngine;
using UnityEngine.UI;

public class PlayerSurvival : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float health;

    public float maxHunger = 100f;
    public float hunger;

    public float maxStamina = 100f;
    public float stamina;

    [Header("Drain Rates")]
    public float hungerDrain = 2f;
    public float staminaDrain = 10f;
    public float staminaRegen = 15f;

    [Header("UI")]
    public Slider healthBar;
    public Slider hungerBar;
    public Slider staminaBar;

    private bool isRunning;

    void Start()
    {
        health = maxHealth;
        hunger = maxHunger;
        stamina = maxStamina;
    }

    void Update()
    {
        HandleHunger();
        HandleStamina();
        UpdateUI();
    }

    void HandleHunger()
    {
        hunger -= hungerDrain * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);

        if (hunger <= 0)
        {
            health -= 5f * Time.deltaTime;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void HandleStamina()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning && stamina > 0)
        {
            stamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            stamina += staminaRegen * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    void UpdateUI()
    {
        if (healthBar != null)
            healthBar.value = health / maxHealth;
        if (hungerBar != null)
            hungerBar.value = hunger / maxHunger;
        if (staminaBar != null)
            staminaBar.value = stamina / maxStamina;
    }

    public void EatFood(float foodAmount)
    {
        hunger += foodAmount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        // Add respawn or game over logic here.
    }
}
