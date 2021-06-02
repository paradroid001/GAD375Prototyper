using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{
    
    public class ObjectMover : MonoBehaviour
    {
        [System.Serializable]
        public class PositionInfo : NamedData<Transform>
        {
        }

        public float speed;
        public PositionInfo[] positions;
        public float distanceThreshold = 0.1f;
        Vector3 destination;
        bool moving = false;

        [YarnCommand("move")]
        public void MoveObject(string posname)
        {
            //Make pos so we don't overwrite destination on bad lookups.
            Transform pos;
            if ( PositionInfo.FindByName(positions, posname, out pos) )
            {
                moving = true;
                destination = pos.position;
            }
        }

        [YarnCommand("teleport")]
        public void TeleportObject(string posname)
        {
            //Make pos so we don't overwrite destination on bad lookups.
            Transform pos;
            if ( PositionInfo.FindByName(positions, posname, out pos) )
            {
                destination = pos.position;
                transform.position = destination;
            }
        }

        /*
        public void TeleportObject(GameObject mover, GameObject g, string posname)
        {
            //Make pos so we don't overwrite destination on bad lookups.
            Transform pos;
            if ( PositionInfo.FindByName(positions, posname, out pos) )
            {
                destination = pos.position;
                g.transform.position = destination;
            }
        }
        */

        //Object mover invoked with dynamic args (from say, collision) will move the collided object
        //To the FIRST position
        public void TeleportObject(GameObject mover, GameObject g)//, string posname = "")
        {
            string posname = "";
            if (posname == "")
            {
                if (positions != null && positions.Length > 0)
                    posname = positions[0].name;
                else
                    return;
            }
            //Make pos so we don't overwrite destination on bad lookups.
            Transform pos;
            if ( PositionInfo.FindByName(positions, posname, out pos) )
            {
                Debug.Log("Teleport found named position");
                destination = pos.position;
                g.transform.position = destination;
            }
        }



        /// Draw the position
        public void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;
            if (positions == null)
                return;
            foreach (PositionInfo p in positions)
            {
                if (p.data != null)
                    Gizmos.DrawWireSphere(p.data.position, 0.2f);
            }
        }

        void Update()
        {
            if (moving)
            {
                Vector3 difference = transform.position - destination;
                if (difference.magnitude < distanceThreshold)
                {
                    //We have arrived
                    //TODO: a better way of doing this might be to have the agent
                    //give up if you arent getting any closer to your destination
                    OnMoveFinish();
                    moving = false;
                }
                else
                {
                    Vector3 movement = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                    transform.position = movement;
                }
            }
        }

        public void OnMoveFinish()
        {
            Debug.Log("Reached Destination");
        }

    }
}