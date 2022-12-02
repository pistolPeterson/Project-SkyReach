using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = SkyReach.Input;

public class PlayerSpriteScript : MonoBehaviour, Input.ICheatActions
{
    private bool _isJPressed = false;
    private Input _input;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject body;

    public List<Sprite> characterSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnEnable()
    {
        if (_input == null)
        {
            _input = new Input();
            _input.Cheat.SetCallbacks(this);
        }
        _input.Enable();

    }

    public void OnDisable()
    {
        _input.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_isJPressed)
        {
            body.transform.localScale = new Vector3(5, 5, 5);
            anim.enabled = false;
            ChangeSprite();

        }
    }

    public void ChangeSprite()
    {
        sr.sprite = SpriteBingo();
    }

    private Sprite SpriteBingo()
    {
       return characterSprite[ Random.Range(0, characterSprite.Count)];
    }

    public void OnSpriteswitch(InputAction.CallbackContext context)
    {
        _isJPressed = context.ReadValueAsButton();
       
    }
}
