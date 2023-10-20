using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PickUp : MonoBehaviour
    {

        [SerializeField]
        private int _amount = 1;

        public int GetPickUp()
        {
            Destroy(gameObject);

            return _amount;
        }

    }

}