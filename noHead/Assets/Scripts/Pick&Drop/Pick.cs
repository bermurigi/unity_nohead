using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.Linq.Expressions;
using ExitGames.Client.Photon;
public class Pick : MonoBehaviourPunCallbacks, IPunObservable//이윤기
{
    public GameObject nearObject;
    public GameObject nowObject;
    public Transform PlayerTransform;
    bool ItemPick; bool MovingItem;
    private bool firstPick;//이윤기
    public PhotonView PV;
    
    
    
    
    
    void Update()
    {
        if (!PV.IsMine)
            return;
        GetInput();
        PV.RPC("PickUp", RpcTarget.AllBuffered);
        
        
        
        
        if (MovingItem)
        {
            
            
            nowObject.transform.position = PlayerTransform.position;
        }

        
    }

    void GetInput()
    {
        ItemPick = Input.GetButtonDown("PickUp");
        
    }

   

    
    [PunRPC] void PickUp()
    {
        if (ItemPick && nearObject != null)
        {
            nowObject = nearObject;
            Item item = nowObject.GetComponent<Item>();
            if (nearObject.tag == "Key" && MovingItem == false && item.PickingItem == false)
            {
                nowObject.transform.position = PlayerTransform.position;
                nowObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                firstPick = true;//이윤기
                if (firstPick)//이윤기
                {
                    nowObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer); //조종권한바꾸기
                    firstPick = false;
                }
                
                MovingItem = true;
                item.rb.isKinematic = true;
                item.PickingItem = true;
                
                item.photonView.RPC("UpdatePickingItem", RpcTarget.AllBuffered, item.PickingItem);
                item.photonView.RPC("UpdateIsKinematic", RpcTarget.AllBuffered, item.rb.isKinematic);
            
                
                
                
                

            }
            else if (MovingItem)
            {
                MovingItem = false;
                item.rb.isKinematic = false;
                nowObject = null;
                item.PickingItem = false;
                
                item.photonView.RPC("UpdatePickingItem", RpcTarget.AllBuffered, item.PickingItem);
                item.photonView.RPC("UpdateIsKinematic", RpcTarget.AllBuffered, item.rb.isKinematic);
            }
                
                
               
               
            
        }
    }
    
    
   
   
   

    

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Key" && nearObject == null)
        {
            nearObject = other.gameObject;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key")
        {
            nearObject = null;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)  //이윤기
    {
       /* if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            Vector3 receivedPosition = (Vector3)stream.ReceiveNext();
            if (!photonView.IsMine && !nowObject)
            {
                transform.position = receivedPosition;
            }
        }
        */
        
    }
    
    
}
