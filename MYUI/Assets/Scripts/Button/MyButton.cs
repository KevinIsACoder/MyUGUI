using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Selectable, IPointerClickHandler, ISubmitHandler
{
    public delegate void CickAction<T>(T handler, PointerEventData eventData);

    public CickAction<IPointerClickHandler> clickHandler;
    public Image image;
    void Start()
    {
        clickHandler = OnClick;
       // image.rectTransform.SetSizeWithCurrentAnchors(0, 500);
       SetSizeWithCurrentAnchors(0, 200, image);
       StartCoroutine(TestCoroutine());
    }

    IEnumerator TestCoroutine()
    {
        while (true)
        {
            yield return null;
        }
    }
    
    public void OnClick(IPointerClickHandler handler, PointerEventData eventData)
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnSubmit(BaseEventData eventData)
    {
        
    }
    
    public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size, Image img)
    {
        int index = (int) axis;
        var tt = img.GetComponent<RectTransform>();
        Vector2 sizeDelta = tt.sizeDelta;
        sizeDelta[index] = size - this.GetParentSize()[index] * (tt.anchorMax[index] - tt.anchorMin[index]);
        Debug.LogFormat("parentSize++++++++ {0} +++++++++ tt.anchorMax+++++++ {1} tt.anchorMin+++++++ {2} ", this.GetParentSize()[index], tt.anchorMax[index], tt.anchorMin[index]);
        Debug.Log(sizeDelta[index]);
        tt.sizeDelta = sizeDelta;
    }

    private Vector2 GetParentSize()
    {
        RectTransform parent = transform.parent as RectTransform;
        return !(bool) (Object) parent ? Vector2.zero : parent.rect.size;
    }
}
