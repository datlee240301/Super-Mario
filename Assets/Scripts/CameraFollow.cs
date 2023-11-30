namespace io.lockedroom.Games.SuperMario
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        private Transform player;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Mario").transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 camera = transform.position;
            camera.x = player.position.x;
            transform.position = camera;
        }
    }
}