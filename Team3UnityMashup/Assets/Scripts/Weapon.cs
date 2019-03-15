using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Damage & Range")]
    public float Damage = 10f;
    public float Range = 10f;

    [Header("Weapon position & bullet prefab")]
    public Transform WeaponFirePoint;
    public GameObject BulletPrefab;

    [Header("Controller2D")]
    public Controller2D Controller;

    private Vector2 Dir;
    Vector2 WeaponRight = new Vector2(1.2f, 0f);
    Vector2 WeaponLeft = new Vector2(-1.2f, 0f);
    Vector2 WeaponUp = new Vector2(0f, 1.6f);

    bool Right, Left, Up;

    void Update()
    {
        Dir = Controller.GetFaceDirection();

        if (Dir.x > 0)
        {
            WeaponFirePoint.localPosition = WeaponRight;
            Right = true;
            Left = !Right;
        }

        else if (Dir.x < 0)
        {
            WeaponFirePoint.localPosition = WeaponLeft;
            Left = true;
            Right = !Left;
        }

        if (Dir == new Vector2(0f, 1f))
        {
            WeaponFirePoint.localPosition = WeaponUp;
            Up = true;
        }
        else
        {
            WeaponFirePoint.localPosition = Right ? WeaponRight : WeaponLeft;
            Up = false;
        }

        if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("XButton"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Up)
        {
            Instantiate(BulletPrefab, WeaponFirePoint.position, Quaternion.LookRotation(Vector3.forward, Vector3.left));
        }

        else if (Right)
        {
            Instantiate(BulletPrefab, WeaponFirePoint.position, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        }

        else if (Left)
        {
            Instantiate(BulletPrefab, WeaponFirePoint.position, Quaternion.LookRotation(Vector3.forward, Vector3.down));
        }
    }
}
