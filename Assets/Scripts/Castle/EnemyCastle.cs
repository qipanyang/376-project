﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Castle
{
    public class EnemyCastle : Castle
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            GameManager.Ctx.TextManager.UpdateEnemyCastleHealth(Health);
        }
    }
}