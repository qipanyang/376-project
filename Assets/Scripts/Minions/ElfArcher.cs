using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minions
{
    public class ElfArcher : Minion
    {
        public override void beforeAttack()
        {
            GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(attack);
        }
    }
}