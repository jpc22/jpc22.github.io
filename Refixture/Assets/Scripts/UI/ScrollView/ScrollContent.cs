using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class ScrollContent : MonoBehaviour, IPointerClickHandler
{
    protected FixtureSO _contentSO;
    [SerializeField] protected GameObject _nameText;
    [SerializeField] protected VoidEventChannelSO _settingsUpdatedChannel;
    [SerializeField] protected SettingsSO _settingsSO;
    protected int _index;
    protected bool _useImperial;
    protected TextMeshProUGUI _nameTMP;

    public FixtureSO ContentSO
    {
        get => _contentSO; set
        {
            _contentSO = value;
            InitializeContent();
        }
    }
    protected virtual bool UseImperial { get => _useImperial; set => _useImperial = value; }
    public virtual int Index
    {
        get => _index; set
        {
            _index = value;
            SetTint();
        }
    }

    protected virtual void Awake()
    {
        _nameTMP = _nameText.GetComponent<TextMeshProUGUI>();
    }

    protected virtual void OnEnable()
    {
        UseImperial = _settingsSO.UseImperial;
        _settingsUpdatedChannel.OnEventRaised += UpdateSettings;
    }

    private void SetTint()
    {
        if((Index % 2) == 0)
        {
            var clb = gameObject.GetComponent<Selectable>().colors;
            clb.normalColor = new Color(clb.normalColor.r, clb.normalColor.g, clb.normalColor.b, 0.2f);
            gameObject.GetComponent<Selectable>().colors = clb;
        }
    }

    protected virtual void InitializeContent()
    {
        _nameTMP.text = ContentSO.Name;
    }

    protected virtual void UpdateSettings()
    {
        UseImperial = _settingsSO.UseImperial;
    }

    protected float ConvertToInches(float meters) => meters * 39.37007874f;
    protected float ConvertToMeters(float inches) => inches / 39.37007874f;

    protected virtual void OnDisable()
    {
        _settingsUpdatedChannel.OnEventRaised -= UpdateSettings;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
            OnDoubleSelect(eventData);
    }
    protected virtual void OnDoubleSelect(PointerEventData eventData)
    {

    }

    
}
