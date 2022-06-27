using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGetItem : MonoBehaviour
{
    [SerializeField]
    private float _itemRange = 3f;

    [SerializeField]
    private LayerMask _itemLayer;

    [SerializeField]
    private Text _textObject;

    private Transform _camTransform = null;

    private PlayerController _playerCtrl = null;
    private void Start()
    {
        _camTransform = MainCam.transform;

        _playerCtrl = GameManager.Instance.PlayerCtrl;
    }
    private void Update()
    {
        GetItem();

    }

    private void GetItem()
    {
        Ray ray = new Ray(_camTransform.position, _camTransform.forward * _itemRange);

        RaycastHit raycastHit;

        Debug.DrawRay(ray.origin, ray.direction * _itemRange, Color.white, 1f);
        if (Physics.Raycast(ray, out raycastHit, _itemRange, _itemLayer))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("아이템 획득");
                if (Inventory.Instance.CurrentLength >= Inventory.Instance.InventorySize)
                {
                    _textObject.text = "아이템이 가득 차 더는 획득이 불가능 합니다";
                    _textObject.gameObject.SetActive(true);
                    return;
                }
                else
                {
                    GameObject g = Instantiate(raycastHit.transform.gameObject);
                    BaseItem item = g.GetComponent<BaseItem>();
                    Inventory.Instance.AddItem(item);

                    Transform hitTransform = raycastHit.transform;

                    hitTransform.GetComponent<Collider>().enabled = false;

                    hitTransform.SetParent(GameManager.Instance.PlayerCtrl.ItemTransform);

                    hitTransform.localPosition = item.HandPos;

                    hitTransform.localRotation = Quaternion.Euler(item.HandRotate);

                    hitTransform.name = g.name;
                    hitTransform.gameObject.SetActive(false);
                }
            }
            else
            {
                _textObject.gameObject.SetActive(true);
            }
        }
        else
        {
            _textObject.gameObject.SetActive(false);

        }
    }

}
