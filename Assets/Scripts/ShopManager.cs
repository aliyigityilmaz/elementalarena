using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<UnitPool> unitPools = new List<UnitPool>();
    public int playerLevel = 1;

    // Example probabilities per level (adjustable)
    private Dictionary<int, float[]> levelProbabilities = new Dictionary<int, float[]>()
    {
        { 1, new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f } }, // 100% 1-cost units at level 1
        { 2, new float[] { 0.80f, 0.20f, 0.0f, 0.0f , 0.0f } }, // 80% 1-cost, 20% 2-cost at level 2
        { 3, new float[] { 0.70f, 0.25f, 0.05f, 0.00f, 0.0f } }, // 70% 1-cost, 25% 2-cost, 5% 3-cost at level 3
        { 4, new float[] { 0.60f, 0.30f, 0.09f, 0.01f, 0.0f } },
        { 5, new float[] { 0.40f, 0.35f, 0.12f, 0.03f, 0.0f } },
        { 6, new float[] { 0.27f, 0.40f, 0.20f, 0.10f, 0.03f } },
        { 7, new float[] { 0.18f, 0.30f, 0.25f, 0.20f, 0.07f } },
        { 8, new float[] { 0.10f, 0.20f, 0.30f, 0.28f, 0.12f } },
        { 9, new float[] { 0.0f, 0.15f, 0.35f, 0.35f, 0.15f } },
        { 10, new float[] { 0.0f, 0.10f, 0.25f, 0.40f, 0.25f } }
    };

    // Unit prefabs categorized by gold cost
    public Dictionary<int, GameObject> unitPrefabs = new Dictionary<int, GameObject>();

    void Start()
    {
        // Example: Load unit prefabs for each cost (replace with your actual prefab references)
        unitPrefabs[1] = Resources.Load<GameObject>("UnitPrefabs/1CostUnit");
        unitPrefabs[2] = Resources.Load<GameObject>("UnitPrefabs/2CostUnit");
        unitPrefabs[3] = Resources.Load<GameObject>("UnitPrefabs/3CostUnit");
        unitPrefabs[4] = Resources.Load<GameObject>("UnitPrefabs/4CostUnit");
        unitPrefabs[5] = Resources.Load<GameObject>("UnitPrefabs/5CostUnit");

        // Initialize unit pools (example: 1-cost units = 30, etc.)
        unitPools.Add(new UnitPool(1, 30)); // 1-cost units
        unitPools.Add(new UnitPool(2, 25)); // 2-cost units
        unitPools.Add(new UnitPool(3, 15)); // 3-cost units
        unitPools.Add(new UnitPool(4, 16)); // 4-cost units
        unitPools.Add(new UnitPool(5, 10)); // 5-cost units

        // Fill pools with unit objects (populate these based on your game design)
        PopulatePools();
    }

    void PopulatePools()
    {
        foreach (var pool in unitPools)
        {
            for (int i = 0; i < pool.totalUnits; i++)
            {
                Unit newUnit = InstantiateUnit(pool.goldCost); // Replace with actual instantiation logic
                pool.availableUnits.Add(newUnit);
            }
        }
    }

    public List<Unit> RefreshShop()
    {
        List<Unit> shopUnits = new List<Unit>();
        float[] probabilities = levelProbabilities[playerLevel];

        // For each slot in the shop (assuming 5 units)
        for (int i = 0; i < 5; i++)
        {
            Unit selectedUnit = SelectRandomUnitBasedOnProbability(probabilities);
            if (selectedUnit != null)
            {
                shopUnits.Add(selectedUnit);
            }
        }

        return shopUnits; // Display these in your UI
    }

    Unit SelectRandomUnitBasedOnProbability(float[] probabilities)
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulative = 0f;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulative += probabilities[i];
            if (randomValue <= cumulative && !unitPools[i].IsEmpty())
            {
                return unitPools[i].GetRandomUnit();
            }
        }

        return null;
    }

    // Instantiate the unit based on gold cost
    Unit InstantiateUnit(int goldCost)
    {
        if (unitPrefabs.ContainsKey(goldCost))
        {
            GameObject unitPrefab = unitPrefabs[goldCost];
            GameObject unitInstance = Instantiate(unitPrefab);
            return unitInstance.GetComponent<Unit>(); // Ensure the prefab has a Unit script
        }

        return null;
    }
}
