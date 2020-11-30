using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minions
{
    public class Archer : Minion
    {
        // Start is called before the first frame update
        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(speed, 0f);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}