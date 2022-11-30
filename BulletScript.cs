using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

 private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    //Esto hara que la bala salga en alguna direccion ya sea izquierda o derecha
    public void SetDirection(Vector2 direction)
    {
        Direction = direction; 
    }

    // Fundion para destruir la bala
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
