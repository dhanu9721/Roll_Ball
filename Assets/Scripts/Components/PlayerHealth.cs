using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject ActivatedHealth;
    // Start is called before the first frame update

    public void ActivateHealth()
    {
       ActivatedHealth.SetActive(true);
    }

    public bool isHealthActive()
    {
        return ActivatedHealth.activeSelf;
    }

    public void DeactivateHealth()
    {
        ActivatedHealth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
