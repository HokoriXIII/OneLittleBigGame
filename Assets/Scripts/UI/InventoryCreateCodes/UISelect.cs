﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelect : MonoBehaviour
{

    public RectTransform _ActiveItem;
    public UIController _UIController;
    public ControlMethod _ControlMethod;
    public EventSystem _EventSystem;

    RectTransform selfTransform;
    Vector2 targetSize;
    Vector2 targetAnchor;
    float sizeTreshold;
    float anchorTreshold;
    float sizeAnimationSpeed;
    float moveAnimationSpeed;
    public bool go = false;
	// Use this for initialization 
	void Start () {
        _UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        _ControlMethod = Camera.main.GetComponent<ControlMethod>();
        targetSize = GetComponent<RectTransform>().sizeDelta;
        targetAnchor= GetComponent<RectTransform>().anchoredPosition;
        selfTransform = GetComponent<RectTransform>();
        sizeTreshold = 0.2f;
        anchorTreshold = 0.2f;
        sizeAnimationSpeed = 0.1f;
        moveAnimationSpeed = 0.1f;
        _EventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();




    }
  
    // Update is called once per frame
    public void MakeTopOfEveryThing()
    {
        transform.SetParent(_UIController.transform);
    }

    public void MoveSelection(Vector2 Size,Vector2 Anchor)
    {
        if (_ActiveItem != null) { transform.SetParent(_ActiveItem.transform); }
        selfTransform.sizeDelta = Size;
        selfTransform.anchoredPosition = Anchor;
       

    }
	void Update () {
        AnimatedMove();
        ControlWithJoyStick();
    }
    public bool canGo = true;
    IEnumerator DelayUIResponse()
    {
        canGo = false;
        yield return new WaitForSeconds(0.2f);
        canGo = true;
    }

    

    void ControlWithJoyStick()
    {
        if (_ControlMethod.joystick&&canGo)
        {
           
            if(
                Input.GetAxis("Horizontal_Pad") != 0f ||
                Input.GetAxis("Vertical_Pad") != 0f
              )
            {
               
                if (_EventSystem.currentSelectedGameObject == null)
                {
                    if (_ActiveItem != null)
                        _EventSystem.SetSelectedGameObject(_ActiveItem.gameObject);
                    else
                        _EventSystem.SetSelectedGameObject(_EventSystem.firstSelectedGameObject);
                }

                
               if (Input.GetAxis("Horizontal_Pad") > 0f)
                {
                    //Debug.Log("Right");
                    
                }
                else if (Input.GetAxis("Horizontal_Pad") < 0f)
                {
                    //Debug.Log("Left");
                }else if(Input.GetAxis("Vertical_Pad") > 0f)
                {
                    //Debug.Log("Up");
                }
                else if (Input.GetAxis("Vertical_Pad") < 0f)
                {
                  

                }
                StartCoroutine(DelayUIResponse());
            }
           
        }
    }


    void AnimatedMove()
    {
        if (!go)
        {
            MoveSelection(targetSize, targetAnchor);
            MakeTopOfEveryThing();
            return;
        }
        if (Mathf.Abs(targetSize.x - selfTransform.sizeDelta.x) > sizeTreshold ||
           Mathf.Abs(targetSize.y - selfTransform.sizeDelta.y) > sizeTreshold ||
           Mathf.Abs(targetAnchor.x - selfTransform.anchoredPosition.x) > anchorTreshold ||
           Mathf.Abs(targetAnchor.y - selfTransform.anchoredPosition.y) > anchorTreshold
          )
        {
            Vector2 next_step_size = Vector2.Lerp(selfTransform.sizeDelta, targetSize, sizeAnimationSpeed);
            Vector2 next_step_anchor = Vector2.Lerp(selfTransform.anchoredPosition, targetAnchor, moveAnimationSpeed);
            MoveSelection(next_step_size, next_step_anchor);
        }
        else
        {
            MoveSelection(targetSize, targetAnchor);
            MakeTopOfEveryThing();
            go = false;
        }
    }
    public void SetStatus(bool status)
    {
        gameObject.SetActive(status);
        canGo = true;
    }
   
    public void ComeToMe(RectTransform item)
    {
        
        RectTransform targetTransform = item.gameObject.GetComponent<RectTransform>();
        _ActiveItem = item;
        targetSize= new Vector2(targetTransform.rect.width, targetTransform.rect.height);
        targetAnchor = Vector2.zero;
        go = true;
        transform.SetParent(_ActiveItem.transform);
        if (_ControlMethod.joystick)
        {
            if(_ActiveItem.parent!=null && _ActiveItem.parent.GetComponent<ItemCreateInScrollView>() != null)
            {
                TranslateScroolView(_ActiveItem.parent.GetComponent<RectTransform>(), _ActiveItem);
            }
        }
    }
    void TranslateScroolView(RectTransform content,RectTransform item)
    {
        IsItInContentView(content.parent.GetComponent<RectTransform>(), item);
    }
    bool IsItInContentView(RectTransform content, RectTransform item)
    {
        Vector3[] corners = new Vector3[4];
        item.GetWorldCorners(corners);
        for(int i = 0; i < 4; i++)
        {
           // Debug.Log(corners[i]);
        }
        if(RectTransformUtility.RectangleContainsScreenPoint(content,corners[0]+new Vector3(1,1,0))
            && RectTransformUtility.RectangleContainsScreenPoint(content, corners[1] + new Vector3(1, -1, 0))
            && RectTransformUtility.RectangleContainsScreenPoint(content, corners[2] + new Vector3(-1, -1, 0))
            && RectTransformUtility.RectangleContainsScreenPoint(content, corners[3] + new Vector3(-1, 1, 0))
          )
        {
            Debug.Log("icinde");
        }
        else
        {
            Debug.Log("dısında");
        }
        /*if (RectTransformUtility.RectangleContainsScreenPoint(invContainers[c].obj.transform.GetChild(2).gameObject.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Vector3[] corners = new Vector3[4];
            obj.transform.GetChild(1).gameObject.GetComponent<RectTransform>().GetWorldCorners(corners);

            if (RectTransformUtility.RectangleContainsScreenPoint(invContainers[c].obj.transform.GetChild(2).gameObject.GetComponent<RectTransform>(), corners[0])
                && RectTransformUtility.RectangleContainsScreenPoint(invContainers[c].obj.transform.GetChild(2).gameObject.GetComponent<RectTransform>(), corners[1])
                && RectTransformUtility.RectangleContainsScreenPoint(invContainers[c].obj.transform.GetChild(2).gameObject.GetComponent<RectTransform>(), corners[2])
                && RectTransformUtility.RectangleContainsScreenPoint(invContainers[c].obj.transform.GetChild(2).gameObject.GetComponent<RectTransform>(), corners[3]))
            {

                Debug.Log("Can drop the item in ID " + c);
                obj.transform.SetParent(invContainers[c].obj.transform.GetChild(2));
                invContainers[c].items.Add(obj.GetComponent<InventoryItem>().item);
                obj.GetComponent<RectTransform>().localScale = Vector2.one;
                obj.GetComponent<RectTransform>().position = tempCoords;
                obj.GetComponent<InventoryItem>().containerID = c;


            }

        }*/


        return false;
    }
}