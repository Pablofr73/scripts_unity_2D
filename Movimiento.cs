using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Esto hace que el personaje se gire cuando estemos regresando
        if(Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //ESto nos ayuda a saber si el valor de la animacion y si se esta moviendo o no
        Animator.SetBool("Run", Horizontal != 0.0f);

        // Esto nos permite hacer que el pesonaje no salte infinitas veces y tenga que detectar algo a bajo para saltar
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        // Programacion del Salto
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;
        //Ahora aparte de solo apretar la W pedira verificar que este tocando el piso para volver a saltar
        if (Input.GetKeyDown(KeyCode.W) && Grounded) 
        {
            Jump();
        }
         
        //Programando la accion de disparar
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }
    // Programacion del salto, "En unity se modifica la fuerza" aproximadamente 150
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
    // Esto hace que multiplique el prefab de la bala en la posision que estemos
    private void Shoot()
    {
    // Definir la direccion de la bala
    Vector3 direction;
    if (transform.localScale.x == 1.0f) direction = Vector3.right;
    else direction = Vector3.left;
       GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
       bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
    // Programacion del movimiento en X
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

}
