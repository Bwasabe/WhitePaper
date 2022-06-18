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
    private void Start() {
        _camTransform = MainCam.transform;

        Debug.Log("인벤토리를 켯다 꺼주세요");
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
                BaseItem item = Instantiate(raycastHit.transform.GetComponent<BaseItem>());
                Inventory.Instance.AddItem(item);
                raycastHit.transform.gameObject.SetActive(false);
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
