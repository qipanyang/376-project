using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minions
{
    public class WomanArcher : Minion
    {
        public override void beforeAttack()
        {
            GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(attack);
        }
    }
}