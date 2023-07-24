using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownController : MonoBehaviour
{
    TMP_Dropdown options;

    private List<string> optionList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        options = this.GetComponent<TMP_Dropdown>();

        options.ClearOptions();

        optionList.Add("yellow");
        optionList.Add("black");
        optionList.Add("red");
        
        
        options.AddOptions(optionList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
