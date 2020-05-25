using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD.Utils
{
    public class Billboard : MonoBehaviour
    {

        // Update is called once per frame
        void LateUpdate()
        {
            //transform.LookAt(Camera.main.transform, Vector3.up);
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up); 
        }
    }
}