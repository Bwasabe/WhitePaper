using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int ID { get; set; } = 0;

    
    private RectTransform _rect;

    private Outline _outline;

    private BaseItem _item;

    public BaseItem Item => _item;

    private const string OUTLINE = "GridOutline";
    private const string NAMETEXT = "NameText";
    private const string EXPLAINTEXT = "ExplainText";
    private const string SKILLNAMETEXT = "SkillNameText";
    private const string SKILLEXPLAINTEXT = "SkillExplainText";

    protected GameObject _panel;
    private RectTransform _panelRect;

    private TMP_Text _nameText;

    private TMP_Text _statusText;

    private TMP_Text _skillNameText;
    private TMP_Text _skillExplainText;


    private void Start()
    {
        _panel = Inventory.Instance.ItemExplainPanel;

        _rect = GetComponent<RectTransform>();
        _panelRect = _panel.GetComponent<RectTransform>();

        _outline = transform.Find(OUTLINE).GetComponent<Outline>();
        _nameText = _panel.transform.Find(NAMETEXT).GetComponent<TMP_Text>();
        _statusText = _panel.transform.Find(EXPLAINTEXT).GetComponent<TMP_Text>();
        _skillNameText = _panel.transform.Find(SKILLNAMETEXT).GetComponent<TMP_Text>();
        _skillExplainText = _panel.transform.Find(SKILLEXPLAINTEXT).GetComponent<TMP_Text>();
    }

    public void SetItem(string name)
    {
        if(name == "null"){
            _item = null;
        }
        else{   
            _item = transform.Find(name).GetComponent<BaseItem>();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _outline.enabled = true;
    }

    public void SetValue()
    {

        _nameText.text = _item.Name;
        _nameText.color = _item.NameColor;

        _statusText.text = $"힘 {_item.ItemStatus.Str} \n민첩 {_item.ItemStatus.Agi} \n체력 {_item.ItemStatus.Hp} \n공격속도 {_item.ItemStatus.AttackSpeed}";

        _skillNameText.text = _item.SkillName;
        _skillNameText.color = _item.SkillNameColor;

        _skillExplainText.text = _item.SkillExplain;

        _nameText.ForceMeshUpdate();
        _statusText.ForceMeshUpdate();
        _skillNameText.ForceMeshUpdate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _outline.enabled = false;
        //_panel.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (_item)
        {
            SetValue();
            _panel.SetActive(true);
        }
        else
        {
            _panel.SetActive(false);

        }
        Inventory.Instance.CurrentSelect = ID;
        //TODO: 아이템 위치와 id에 따라 설명 위치 바꿔주기 

    }
}
