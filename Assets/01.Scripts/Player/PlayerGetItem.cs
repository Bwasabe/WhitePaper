using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetItem : MonoBehaviour
{
    [SerializeField]
    private float _itemRange = 3f;

    [SerializeField]
    private LayerMask _itemLayer;

    private Transform _camTransform = null;

    private PlayerController _playerCtrl = null;
    private void Start() {
        _camTransform = MainCam.transform;

        _playerCtrl = GameManager.Instance.PlayerCtrl;
    }
    private void Update() {
        GetItem();
    }

    private void GetItem(){
        Ray ray = new Ray(_camTransform.position, _camTransform.forward * _itemRange);

        RaycastHit raycastHit;

        Debug.DrawRay(ray.origin,ray.direction * _itemRange, Color.white, 1f);
        if(Physics.Raycast(ray,out raycastHit, _itemRange   , _itemLayer)){
            if(Input.GetKeyDown(KeyCode.F)){
                Debug.Log("아이템 획득");
                if(Inventory.Instance.CurrentLength >= Inventory.Instance.InventorySize){
                    Debug.Log("인벤토리가 꽉참");
                    return;
                }
                else
                {
                    GameObject item = Instantiate(raycastHit.transform.gameObject);
                    Inventory.Instance.AddItem(item.GetComponent<BaseItem>());

                    Transform hitTransform = raycastHit.transform;
                    _playerCtrl.Skill.ClearAction();
                    _playerCtrl.Skill.RegisterAction(hitTransform.GetComponent<BaseSkill>().Skill);


                    hitTransform.SetParent(GameManager.Instance.PlayerCtrl.ItemTransform);

                    hitTransform.localPosition = Vector3.zero;
                    hitTransform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                    hitTransform.name = item.name;

                    hitTransform.gameObject.SetActive(false);
                }
            }
            else{
                Debug.Log("F를 눌러 아이템 획득 가능");
            }
        }
        else{
            Debug.Log("레이 안닿음");
        }
    }
    
}
