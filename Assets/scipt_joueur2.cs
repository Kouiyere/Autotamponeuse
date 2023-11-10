using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class scipt_joueur2 : MonoBehaviour
{
    // T=push,R=acsélération,S=frein,souris"Y"=déviation;
    Vector3 back=new Vector3(1,-1,1);
    float timer_rember = -1;
    public float rebond= 1;
    public float speed_angle = 10.0f; 
    public int acsélération = 10;
    public int frein = 1;
    public int puch = 1;
    private bool Flag_acseleration=false;
    private bool Flag_frein=false;
    private Vector3 transform_forward;

    // Start is called before the first frame update
    void Start()
    {
        transform_forward = Vector3.forward;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        //transform_forward = Quaternion.AngleAxis(90, Vector3.back) * transform.forward;
        //Vector3 transform_back = Vector3.Dot(transform_forward, back);
        //new Vector3(transform_forward.x,transform_forward.y*-1,transform_forward.z);
        float horizontal = Input.GetAxis("Mouse Y") * speed_angle;
        transform.Rotate(0, horizontal, 0);
        transform_forward = Quaternion.AngleAxis(horizontal, Vector3.up) * transform_forward;
        if (Flag_acseleration)
        {
            print("tomacseleration");
            GetComponent<Rigidbody>().AddForce( transform.forward * acsélération, ForceMode.Acceleration);
        }
        if (Flag_frein)
        {
            print("frein");
            GetComponent<Rigidbody>().AddForce( transform.forward * GetComponent<Rigidbody>().velocity.magnitude*frein, ForceMode.Force);
        }
        Debug.DrawRay(transform.position, transform.forward,Color.red);
    }
    //GetComponent<Rigidbody>().AddForce(Vector3.forward * maVariable, ForceMode.Force);

     void Update()
    {
        // transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0); 
        if (timer_rember > -1)
        {
            if(timer_rember > 0.5f) { 
                timer_rember = -1;
                Gamepad.current.SetMotorSpeeds(0, 0);

            }
            timer_rember += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.R))
        { Flag_acseleration = true;
            print("puch_R");
        }
        if (Input.GetKeyUp(KeyCode.R))
        { Flag_acseleration = false; }
        if (Input.GetKeyDown(KeyCode.S))
        { Flag_frein = true;
            print("puch_S");  
        }
        if (Input.GetKeyUp(KeyCode.S))
        { Flag_frein = false; }
        if(Input.GetKeyDown(KeyCode.T))
        { GetComponent<Rigidbody>().AddForce(Quaternion.AngleAxis(90, Vector3.up) * transform.forward * puch, ForceMode.Impulse); }
    }
    private void OnCollisionEnter(Collision other)
    {
        print("touch");
        if (other.gameObject.tag=="wall")
        {
          //  Rigidbody Rigidbody = other.gameObject.GetComponent<Rigidbody>();
            print("vibration_2");
            timer_rember = 0;
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);

            // Vérifie si la balle a un Rigidbody
            /* if (Rigidbody != null)
             {
                 print("colision");
                 Transform cible = other.transform;
                 Vector3 toTarget = cible.transform.position - GetComponent<Transform>().position;
                 Vector3 direction = toTarget.normalized;

                 float angle = Vector3.Angle(Rigidbody.velocity, toTarget);
                 if (angle > 180)
                 {
                     angle = angle - 180;
                 }
                 float ressor = (Rigidbody.velocity.magnitude) * ((angle - 90) / 90 + 1) * rebond;
                 print("valeur de la réaction du bumper : " + ressor);
                 print("valeur de l'angle d'incidence : " + angle);
                 Rigidbody.AddForce(direction * ressor, ForceMode.Impulse);
             }*/
        }
    }
}