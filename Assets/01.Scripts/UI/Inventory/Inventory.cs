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
    private GameObject _itemExplainPanel;

    public GameObject ItemExplainPanel => _itemExplainPanel;

    [SerializeField]
    private BaseItem _test;
    [SerializeField]
    private BaseItem _test2;

    private int _currentLength = 0;
    private int _inventorySize = 20;


    private void Start()
    {
        for (int i = 0; i < _inventorySize; ++i)
        {
            AddGrid();
        }

        AddItem(_test);
        AddItem(_test2);
    }
    public void AddItem(BaseItem item)
    {
        item.transform.SetParent(_grids[_currentLength].transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        item.transform.localScale = item.InventoryScale;
        item.gameObject.layer = LayerMask.NameToLayer("UI");
        _grids[_currentLength].SetItem(item.name);
        ++_currentLength;
    }

    public void AddGrid()
    {
        GameObject g = Instantiate(_grid, transform);
        g.SetActive(true);

        _grids.Add(g.GetComponent<InventoryGrid>());
    }
}
