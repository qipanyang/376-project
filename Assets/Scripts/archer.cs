using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
	rb.velocity = new Vector2(2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
