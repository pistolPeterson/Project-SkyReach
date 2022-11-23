using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Button changes sprite when hovered over
/// </summary>

public class ChangeSpriteOnHover : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = normalSprite;
    }

    public void ChangeToHoverSprite()
    {
        image.sprite = hoverSprite;
    }

    public void returnToNormalSprite()
    {
        image.sprite = normalSprite;
    }
}
