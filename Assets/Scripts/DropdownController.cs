using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownController : MonoBehaviour
{
    TMP_Dropdown options;

    private List<string> optionList = new List<string>();
    // Start is called before the first frame update
    public SkinnedMeshRenderer choiceChracter;
    public Material[] choicemat = new Material[3];
    void Start()
    {
        options = this.GetComponent<TMP_Dropdown>();

        options.ClearOptions();

        optionList.Add("yellow");
        optionList.Add("black");
        optionList.Add("red");
        
        
        options.AddOptions(optionList);
        options.value = 0;
    }

    public void onDropEvent()
    {
        if (options.value == 0)
        {
            choiceChracter.material = choicemat[0];
            Debug.Log("0");
        }
        else if (options.value==1)
        {
            choiceChracter.material = choicemat[1];
            Debug.Log("1");
        }
        else if (options.value == 2)
        {
            choiceChracter.material = choicemat[2];
            Debug.Log("2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
