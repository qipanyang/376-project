using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minions
{
    public class WomanAttacker : Minion
    {
        public AudioClip attack;
        public override void beforeAttack()
        {
            GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(attack);
        }
    }
}