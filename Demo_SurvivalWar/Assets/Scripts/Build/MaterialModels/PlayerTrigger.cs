using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 门形墙壁检查玩家进入
/// </summary>
public class PlayerTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        //玩家进入
        if (coll.gameObject.name == "FPSController")
        {
            //如果门存在
            if(gameObject.GetComponent<Transform>().parent.Find("Door(Clone)") != null)
            {
                //开门
                gameObject.GetComponent<Transform>().parent.Find("Door(Clone)").GetComponent<DoorCtr>().OpenDoor();
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        //离开门
        if (coll.gameObject.name == "FPSController")
        {
            //门存在
            if (gameObject.GetComponent<Transform>().parent.Find("Door(Clone)") != null)
            {
                //关门
                gameObject.GetComponent<Transform>().parent.Find("Door(Clone)").GetComponent<DoorCtr>().CloseDoor();
            }
        }
    }
}
