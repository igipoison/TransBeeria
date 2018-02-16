using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevelEndClick : MonoBehaviour {

    public void Start()
    {
        Button buttonEndLevel = this.GetComponent<Button>();
        buttonEndLevel.onClick.AddListener(() =>
        {
            GameManager.GameFinished();
        });
    }

}
