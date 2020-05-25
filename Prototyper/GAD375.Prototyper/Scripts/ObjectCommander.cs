using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD375.Prototyper
{
    [System.Serializable]
    public class NamedData<T>
    {
        public string name;
        public T data;

        public static bool FindByName(NamedData<T>[] list, string itemname, out T result)
        {
            bool found = false;
            result = default(T);
            foreach(NamedData<T> item in list)
            {
                if (item.name == itemname)
                {
                    result = item.data;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Debug.Log("Item named " + itemname + "of type " + typeof(T) + " could not be found");
            }
            return found;
        }
    }

    interface IObjectCommander
    {
        void RunCommand(string namedata);

        void OnCommandFinished();

        void OnDrawGizmosSelected();
    }
}
