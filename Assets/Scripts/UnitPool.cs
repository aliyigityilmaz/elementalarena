using System.Collections.Generic;
using UnityEngine;

public class UnitPool
{
    public int goldCost;
    public int totalUnits;
    public List<Unit> availableUnits = new List<Unit>();

    public UnitPool(int cost, int initialUnits)
    {
        goldCost = cost;
        totalUnits = initialUnits;
    }

    public Unit GetRandomUnit()
    {
        if (availableUnits.Count > 0)
        {
            int randomIndex = Random.Range(0, availableUnits.Count);
            Unit selectedUnit = availableUnits[randomIndex];
            availableUnits.RemoveAt(randomIndex);
            totalUnits--;
            return selectedUnit;
        }
        return null;
    }

    public bool IsEmpty()
    {
        return totalUnits <= 0;
    }
}