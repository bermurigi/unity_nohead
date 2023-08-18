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
    public Transform PlayerTransform;
    bool ItemPick; bool MovingItem; public bool rayItem;
    public GameObject cameraObject;
    [SerializeField] LayerMask layermask;
    RaycastHit hit;

    private bool firstPick;//이윤기
    public PhotonView PV;
    
    
    
    
    
    void Update()
    {
        if (!PV.IsMine)
            return;
        ItemPick = Input.GetButtonDown("PickUp");
        PV.RPC("PickUp", RpcTarget.AllBuffered);


        if (MovingItem)
        {
            nearObject.transform.position = PlayerTransform.position;
        }

        CameraController camera = cameraObject.GetComponent<CameraController>();
        Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.red);

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 20,layermask) && MovingItem != true)
        {
            nearObject = hit.collider.gameObject;
            rayItem = true;
        }
        else
        {
            rayItem = false;
            if(MovingItem != true)
            {
                nearObject = null;
            }
        }
    }

    
    [PunRPC] void PickUp()
    {
        if (ItemPick && nearObject != null)
        {
            Item item = nearObject.GetComponent<Item>();
            if (MovingItem == false)
            {
                nearObject.transform.position = PlayerTransform.position;
                nearObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                firstPick = true;//이윤기
                if (firstPick)//이윤기
                {
                    nearObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer); //조종권한바꾸기
                    firstPick = false;
                }
                
                MovingItem = true;
                item.rb.isKinematic = true;
                Cursor.lockState = CursorLockMode.Locked;

                item.photonView.RPC("UpdatePickingItem", RpcTarget.AllBuffered, item.PickingItem);
                item.photonView.RPC("UpdateIsKinematic", RpcTarget.AllBuffered, item.rb.isKinematic);
            
                
                
                
                

            }
            else if (MovingItem)
            {
                MovingItem = false;
                item.rb.isKinematic = false;
                Cursor.lockState = CursorLockMode.None;

                item.photonView.RPC("UpdatePickingItem", RpcTarget.AllBuffered, item.PickingItem);
                item.photonView.RPC("UpdateIsKinematic", RpcTarget.AllBuffered, item.rb.isKinematic);
            }
             
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
