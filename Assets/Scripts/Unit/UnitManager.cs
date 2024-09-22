using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab; // Birim prefabý
    public Transform unitContainer; // Tahtadaki birimler için container

    public void SpawnUnit(Vector3 position)
    {
        GameObject newUnit = Instantiate(unitPrefab, position, Quaternion.identity, unitContainer);
        newUnit.name = "Unit";
    }
}