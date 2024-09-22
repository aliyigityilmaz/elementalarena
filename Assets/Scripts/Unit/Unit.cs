using UnityEngine;

public enum ElementType { Fire, Water, Earth, Air }

public class Unit : MonoBehaviour
{
    public Sprite unitSprite; // Unit'in g�rseli
    public ElementType elementType; // Element tipi
    public string unitName;
    public int goldCost;
    public int level = 1;
    public bool isOnBoard = false;

    // Unit'in istatistikleri
    public float health;
    public float attackPower;
    public float attackSpeed;
    public float attackRange;
    public float armor;
    public float magicResistance;

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
        // Unit'in �l�m� i�in gereken i�lemler
        Destroy(gameObject);
    }
}