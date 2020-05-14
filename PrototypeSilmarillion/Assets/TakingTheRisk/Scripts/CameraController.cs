using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Watikita
{


    public class CameraController : MonoBehaviour
    {

        public GameObject player;
        [Range(0, 100)]
        public int varX;
        [Range(0, 100)]
        public int varY;
        [Range(0, 100)]
        public int varZ;
        // Update is called once per frame
        private void Start()
        {
            varX = 12;
            varY = 11;
            varZ = 5;
        }

        void Update()
        {
            transform.position = new Vector3(player.transform.position.x + varX, player.transform.position.y + varY, player.transform.position.z + varZ);

        }
    }
}