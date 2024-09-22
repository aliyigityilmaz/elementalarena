using UnityEngine;

public enum ElementType { Fire, Water, Earth, Air, Grass, Lightning }

public class Unit : MonoBehaviour
{
    public ElementType elementType;
    public string unitName;
    public int goldCost;
    public int level = 1;
    public bool isOnBoard = false;

    // Unit stats
    public float health;
    public float attackPower;
    public float spellPower;
    public float attackSpeed;
    public float attackRange;
    public float armor;
    public float magicResistance;

    // Class effects for bonuses
    public string[] unitClasses;

    // Add bonuses for having multiple of the same class
    public void ApplyClassBonus(string[] currentClasses)
    {
        // Logic for checking class synergies
    }

    // Combat interactions between elements
    public float CalculateElementalDamageMultiplier(Unit target)
    {
        // Example of elemental interaction logic
        if (this.elementType == ElementType.Fire && target.elementType == ElementType.Grass)
        {
            return 1.5f; // Fire deals 50% more to Grass
        }
        else if (this.elementType == ElementType.Water && target.elementType == ElementType.Fire)
        {
            return 2f; // Water deals double damage to Fire
        }
        return 1f; // Normal damage
    }

    // Example attack method
    public void Attack(Unit target)
    {
        float damage = attackPower * CalculateElementalDamageMultiplier(target);
        target.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle unit death
    }

    public void LevelUp()
    {
        level++;
        goldCost += 10;  // Example increase, adjust as needed
    }

    public void Sell()
    {
        // Give the player gold based on the unit's value
        // Example: Player.gold += goldCost;
    }
}