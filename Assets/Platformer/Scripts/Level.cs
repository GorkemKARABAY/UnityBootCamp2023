using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Level : MonoBehaviour
    {
        private GameManager _gameManager;
        public int MinimumCoinCollectAmount;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
    }
}