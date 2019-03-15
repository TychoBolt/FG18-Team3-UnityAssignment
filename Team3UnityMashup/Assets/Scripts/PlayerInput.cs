using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    Player _Player;

    void Start()
    {
        _Player = GetComponent<Player>();
    }

    void Update()
    {
        Vector2 DirectionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _Player.SetDirectionalInput(DirectionalInput);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton"))
        {
            _Player.OnJumpInputDown();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("AButton"))
        {
            _Player.OnJumpInputUp();
        }
    }
}
