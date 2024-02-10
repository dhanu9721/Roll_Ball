using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Health;
    [SerializeField] private Transform HealthContainer;
    private List<PlayerHealth> Healths = new List<PlayerHealth>();

    public void AddHealth(int healthCount)
    {
        for (int i = 0; i < healthCount; i++)
        {
            GameObject instance = Instantiate(Health, HealthContainer);
            instance.transform.parent = HealthContainer;
            PlayerHealth healthComponent = instance.GetComponent<PlayerHealth>();
            Healths.Add(healthComponent);
        }
    }
    public void ActivateHealth()
    {
        foreach (var item in Healths)
        {
            if (!item.isHealthActive())
            {
                item.ActivateHealth();
                break;
            }
        }
    }

    public void DeactivateHealth()
    {
        foreach (var item in Healths)
        {
            if (item.isHealthActive())
            {
                item.DeactivateHealth();
                break;
            }
        }
    }
}
