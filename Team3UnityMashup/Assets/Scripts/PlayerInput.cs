using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    Player _Player;

    private void Start()
    {
        _Player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        Vector2 DirectionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _Player.SetDirectionalInput(DirectionalInput);

        if (Input.GetButtonDown("Jump"))
        {
            _Player.OnJumpInputDown();
        }

        if (Input.GetButtonUp("Jump"))
        {
            _Player.OnJumpInputUp();
        }
    }
}
