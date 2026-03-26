using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] GameObject heartInstance;
    [SerializeField] Sprite PosHeart, NegHeart;

    List<Image> HeartsSpawned= new List<Image>();


    public void AddHearts(int added) 
    {
        foreach (var hearts in HeartsSpawned)
        {
            Destroy(hearts.gameObject);
            HeartsSpawned.Remove(hearts);
        }

        for (int i = 0; i < added; i++)
        {
           var heart= Instantiate(heartInstance, this.gameObject.transform) as GameObject;
            HeartsSpawned.Add(heart.GetComponent<Image>());
        }
    }

    public void CorrectHearts(int currentHealth) 
    {
        var fixedHearts = HeartsSpawned.AsEnumerable().Reverse().ToList();
        var diff = (HeartsSpawned.Count+1) - currentHealth;

        foreach (var hearts in fixedHearts)
        {
            diff -= 1;
            if (diff <= 0) 
            {
                hearts.sprite = PosHeart;
            }
            else 
            {
                hearts.sprite = NegHeart;

            }

        }       



    }

    
}
