using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);

        if(transform.position.x > 100)
            transform.position = new Vector3(-100, transform.position.y, transform.position.z);
    }
}
