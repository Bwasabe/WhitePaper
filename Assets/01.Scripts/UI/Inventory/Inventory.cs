using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    //0~2번은 현재 사용중인 무기,
    //나머지는 그냥 보관중인 무기
    private List<InventoryGrid> _grids = new List<InventoryGrid>();


    [SerializeField]
    private GameObject _grid = null;

    [SerializeField]
    private InventoryGrid _weaponGrid;

    [SerializeField]
    private GameObject _itemExplainPanel;

    public GameObject ItemExplainPanel => _itemExplainPanel;

    private Transform _playerHand = null;

    private ExecuteSkill _executeSkill;

    public int CurrentLength { get; set; } = 0;
    public int InventorySize{ get; set; } = 20;

    public int CurrentSelect { get; set; }

    private PlayerController _playerCtrl = null;

    private void Start()
    {
        _playerCtrl = GameManager.Instance.PlayerCtrl;
        for (int i = 0; i < InventorySize; ++i)
        {
            AddGrid(i);
        }

        // AddItem(_teset);
        // AddItem(_tese1t);
        // AddItem(_tese1t2);
        _executeSkill = _playerCtrl.Skill;
        _playerHand = _playerCtrl.ItemTransform;
    }

    private void Update()
    {
        Debug.Log(CurrentSelect);
    }

    public void AddItem(BaseItem item)
    {
        SetItem(item, _grids[CurrentLength].transform);

        _grids[CurrentLength].SetItem(item.name);
        ++CurrentLength;
    }

    private void SetWeapon(BaseItem item)
    {
        SetItem(item, _weaponGrid.transform);

        _weaponGrid.SetItem(item.name);

        for (int i = CurrentSelect; i < CurrentLength -1 ; ++i)
        {
            SetItem(_grids[i + 1].Item, _grids[i].transform);
            _grids[i].SetItem(_grids[i + 1].Item.name);
        }
        CurrentLength--;

        _grids[CurrentLength].SetItem("null");
    }

    public void SetItem(BaseItem item, Transform parent)
    {
        item.transform.SetParent(parent);
        item.transform.localPosition = Vector3.zero;
        item.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        item.transform.localScale = item.InventoryScale;
        item.transform.localPosition = item.InventoryPos;
        item.gameObject.layer = LayerMask.NameToLayer("UI");
    }


    public void AddGrid(int id)
    {
        GameObject g = Instantiate(_grid, transform);
        g.SetActive(true);
        InventoryGrid grid = g.GetComponent<InventoryGrid>();
        grid.ID = id;
        _grids.Add(grid);
    }


    public void OnClickEquipItem()
    {
        if (CurrentSelect == -1) return;

        //TODO: 장착시 연출
        if (_weaponGrid.Item == null)
        {
            SetWeapon(_grids[CurrentSelect].Item);

            _executeSkill.ClearAction();
            _executeSkill.RegisterAction(_playerHand.Find(_weaponGrid.Item.name).GetComponent<BaseSkill>().Skill);
            //_executeSkill.RegisterAction(_weaponGrid.Item.GetComponent<BaseSkill>().Skill);
            Debug.Log(_weaponGrid.Item.name);
            _playerHand.Find(_weaponGrid.Item.name).gameObject.SetActive(true);
        }
        else
        {
            //TODO:스왑
            // 무기 변경
            _playerHand.Find(_grids[CurrentSelect].Item.name).gameObject.SetActive(true);
            _playerHand.Find(_weaponGrid.Item.name).gameObject.SetActive(false);

            // // 스킬 초기화 및 등록
            _executeSkill.ClearAction();
            _executeSkill.RegisterAction(_playerHand.Find(_grids[CurrentSelect].Item.name).GetComponent<BaseSkill>().Skill);

            // UI 아이템 스왑
            SetItem(_weaponGrid.Item, _grids[CurrentSelect].transform);
            SetItem(_grids[CurrentSelect].Item, _weaponGrid.transform);

            //각각의 그리드 마다 아이템 이름 설정
            string temp = _grids[CurrentSelect].Item.name;
            _grids[CurrentSelect].SetItem(_weaponGrid.Item.name);

            _weaponGrid.SetItem(temp);
        }
        CurrentSelect = -1;

    }

}
