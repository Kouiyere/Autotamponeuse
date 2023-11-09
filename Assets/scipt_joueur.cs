using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class scipt_joueur : MonoBehaviour
{
    Vector3 back=new Vector3(1,-1,1);
    public float rebond= 1;
    public float speed_angle = 10.0f; // Vitesse de rotation
    public int acsélération = 10;
    public int frein = 1;
    private bool Flag_acseleration=false;
    private bool Flag_frein=false;
    private Vector3 transform_forward;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 transform_forward = new Vector3(0,-1,0);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform_forward = Quaternion.AngleAxis(90, Vector3.up) * transform_forward;
        //Vector3 transform_back = Vector3.Dot(transform_forward, back);
        //new Vector3(transform_forward.x,transform_forward.y*-1,transform_forward.z);
        float horizontal = Input.GetAxis("Mouse X") * speed_angle;
        transform.Rotate(0, 0, horizontal);
        transform_forward = Quaternion.AngleAxis(horizontal, Vector3.up) * transform_forward;
        if (Flag_acseleration)
        {
            print("acseleration");
            GetComponent<Rigidbody>().AddForce(transform_forward * acsélération, ForceMode.Acceleration);
        }
        if (Flag_frein)
        {
            print("frein");
            GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(180, Vector3.up) * transform_forward * GetComponent<Rigidbody>().velocity.magnitude*frein, ForceMode.Force);
        }
    }
    //GetComponent<Rigidbody>().AddForce(Vector3.forward * maVariable, ForceMode.Force);

     void Update()
    {
       // transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0); 

        if (Input.GetKeyDown(KeyCode.UpArrow))
        { Flag_acseleration = true; }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        { Flag_acseleration = false; }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { Flag_frein = true; }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        { Flag_frein = false; }
    }
    private void OnCollisionEnter(Collision other)
    {
       /* print("colision");
        Transform cible = other.transform;
        Vector3 toTarget = cible.transform.position - GetComponent<Transform>().position;
        Vector3 direction = toTarget.normalized;
        Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();

        float angle = Vector3.Angle(ballRigidbody.velocity, toTarget);
        if (angle > 180)
        {
            angle = angle - 180;
        }
        float ressor = (ballRigidbody.velocity.magnitude) * ((angle - 90) / 90 + 1) * rebond;
        print("valeur de la réaction du bumper : " + ressor);
        print("valeur de l'angle d'incidence : " + angle);
        ballRigidbody.AddForce(direction * ressor, ForceMode.Impulse);*/
    }
}