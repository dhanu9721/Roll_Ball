using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ActiveStar;
    [SerializeField] GameObject DeactiveStar;
    void Start()
    {
        
    }

    public void ActivateStar()
    {
        if(ActiveStar != null)
        {
            ActiveStar.SetActive(true);
            DeactiveStar.SetActive(false);
        }
    } 
    public void DeactivateStar()
    {
        if(ActiveStar != null)
        {
            ActiveStar.SetActive(false);
            DeactiveStar.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
