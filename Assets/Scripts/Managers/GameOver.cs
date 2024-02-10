using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject Loose;
    [SerializeField] private Ground _Ground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnLoose()
    {
        win.SetActive(false);
        Loose.SetActive(true);
    }  
    public void OnWin()
    {
        Loose.SetActive(false);
        win.SetActive(true);
    } 

    public void OnReplay()
    {
        Loose.SetActive(false);
        win.SetActive(false);
        _Ground.Restart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
