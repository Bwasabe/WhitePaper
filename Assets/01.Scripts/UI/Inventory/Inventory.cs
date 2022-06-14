using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //0~2번은 현재 사용중인 무기,
    //나머지는 그냥 보관중인 무기
    private Dictionary<int, BaseItem> _inventoryDictionary = new Dictionary<int, BaseItem>();

    public void AddItem(){
        
    }
}
