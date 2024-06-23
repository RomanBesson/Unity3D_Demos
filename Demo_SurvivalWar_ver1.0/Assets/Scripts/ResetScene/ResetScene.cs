using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 重新开始场景的脚本
/// </summary>
public class ResetScene : MonoBehaviour {

    private bool isEnter = false;       //标志位.

	void Update () {
        //按下enter后重新加载游戏场景
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (isEnter == false)
            {
                isEnter = true;
                SceneManager.LoadScene("Game");
            }
        }


	}
}
