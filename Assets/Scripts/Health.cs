using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;

    public Image[] hearts;
    public Sprite heart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;  // Enable the heart if its index is less than the health
            }
            else
            {
                hearts[i].enabled = false; // Disable the heart otherwise
            }
        }
    }
}
