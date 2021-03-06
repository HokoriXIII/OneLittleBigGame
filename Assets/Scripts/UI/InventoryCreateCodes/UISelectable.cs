﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelectable : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler,GameUIActionInterFace, ISelectHandler
{
    public UISelect _UISelect;
    public Item _Item;

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        _UISelect.ComeToMe(GetComponent<RectTransform>());
        _UISelect._EventSystem.SetSelectedGameObject(gameObject);

    }
    public void OnPointerClick(PointerEventData eventData)
    {



        if (eventData.button == PointerEventData.InputButton.Left)
            UIPrimaryAction();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            UISecondaryAction();
    }

    void Start () {
        _UISelect = GameObject.FindGameObjectWithTag("UISelect").GetComponent<UISelect>();
        
	}
    //GameUIActionInterFace
    public void UIPrimaryAction()
    {
        if (_Item == null) {
            //Normal Click
            Debug.Log("Primary UI Click Called");
            GetComponent<Button>().onClick.Invoke();
        }else{
            //Call ItemPrimary Click
            _Item.UIPrimaryAction();
        }
    }
   
    public void UISecondaryAction()
    {
        if (_Item == null)
        {
            //Normal Secondry Click
            Debug.Log("Secondry UI Click Called");
        }
        else
        {
            //Call ItemPrimary Click
            _Item.UISecondaryAction();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _UISelect.ComeToMe(GetComponent<RectTransform>());
    }
}
