using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeSpriteOnClick : MonoBehaviour
{
   [SerializeField] private Image img;
   [SerializeField] private Sprite normalSprite; 
   [SerializeField] private Sprite pressedSprite;


   private void Start()
   {
      img = GetComponent<Image>();
      img.sprite = normalSprite;
   }

   public void OnPress()
   {
      img.sprite = pressedSprite; 
   }

   public void OnRelease()
   {
      img.sprite = normalSprite;
   }
}
