using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    float mass = 10f;
    float force = 20f;
    float acceleration;
    float gravity = -9.8f;
    float gAcceleration;
    float speedY;
    float speedZ;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        acceleration = force / mass;
        speedZ += acceleration * Time.deltaTime;

        gAcceleration = gravity / mass;
        speedY += gAcceleration * Time.deltaTime;
        //if (speedZ > 0)
            this.transform.Translate(0, speedY, speedZ);

        force -= 0.5f;
    }
}
