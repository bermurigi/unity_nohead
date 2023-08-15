using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Open : MonoBehaviour
{
    public bool[] RandomItem = new bool[5];
    public GameObject DoorObject;
    int rand;
    public int Keycount = 3;
    
    
    

    void RandItem()
    {
        
            for (int i = 0; i < 3; i++)
            {
                rand = Random.Range(0, 4);
                if (RandomItem[rand] == false)
                {
                    RandomItem[rand] = true;
                }
                else
                {
                    i--;
                }
            }
            
        
    }
   
   

    void Update()
    {
        
        if(Keycount == 0)
        {
            Door door = DoorObject.GetComponent<Door>();
            door.Opendoor = true;
            Keycount = -1;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key")
        {
            Item item = other.GetComponent<Item>();
            if (RandomItem[item.value] == true) 
            {
                RandomItem[item.value] = false;
                Keycount--;
            }
            Destroy(other.gameObject);
        }

    }
    
    
}
