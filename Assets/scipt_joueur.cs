using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class scipt_joueur : MonoBehaviour
{
    public float bolle= 1;
    public float speed = 10.0f; // Vitesse de rotation
    public int maVariable = 10;
    private bool Flag_acseleration=false;
    private bool Flag_frein=false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 transform_forward = transform.forward;
        Vector3 transform_back = new Vector3(transform_forward.x,transform_forward.y*(-1),transform_forward.z);
        //new Vector3(transform_forward.x,transform_forward.y*-1,transform_forward.z);
        
        if (Flag_acseleration)
        {
            print("acseleration");
            GetComponent<Rigidbody>().AddForce(transform_forward* maVariable, ForceMode.Force);
        }
        if (Flag_frein)
        {
            print("frein");
            GetComponent<Rigidbody>().AddForce(transform_back * GetComponent<Rigidbody>().velocity.magnitude, ForceMode.Force);
        }
    }
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * maVariable, ForceMode.Force);

     void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * speed;
        transform.Rotate(0, horizontal, 0);        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { Flag_acseleration = true; }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        { Flag_acseleration = false; }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { Flag_frein = true; }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        { Flag_frein = false; }
    }
  /*  private void OnCollisionEnter(Collision other)
    {
        print("colision");
        Transform cible = other.transform;
        Vector3 toTarget = cible.transform.position - GetComponent<Transform>().position;
        Vector3 direction = toTarget.normalized;
        Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();

        float angle = Vector3.Angle(ballRigidbody.velocity, toTarget);
        if (angle > 180)
        {
            angle = angle - 180;
        }
        float ressor = (ballRigidbody.velocity.magnitude) * ((angle - 90) / 90 + 1) * bolle;
        print("valeur de la réaction du bumper : " + ressor);
        print("valeur de l'angle d'incidence : " + angle);
        ballRigidbody.AddForce(direction * ressor, ForceMode.Impulse);
    }*/
}