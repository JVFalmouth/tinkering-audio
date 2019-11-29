using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateKey : MonoBehaviour
{
    string key = "Major Key";
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        text.text = key;
    }

    public void SwitchKey()
    {
        if (key == "Major Key")
        {
            key = "Minor Key";
            text.text = key;
        }
        else
        {
            key = "Major Key";
            text.text = key;
        }

    }
}
