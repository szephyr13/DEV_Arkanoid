using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. free movement
        float inputH = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(inputH, 0, 0) * speed * Time.deltaTime);

        //2. delimited movement (boundaries)
        float xDelimited = Mathf.Clamp(transform.position.x, -4.25f, 4.49f);
        transform.position = new Vector3(xDelimited, transform.position.y, transform.position.z);

    }
}
