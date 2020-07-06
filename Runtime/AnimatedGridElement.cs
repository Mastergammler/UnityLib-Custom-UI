using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimatedGridElement : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private bool mIsSmall = false;
    private bool mIsLocked = false;

    public LeanTweenType type = LeanTweenType.easeInCubic;
    public AnimationCurve CustomCurve;

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 tweenTo = mIsSmall ? new Vector3(1f,1f,0f) : new Vector3(.5f,.5f,0f);
        mIsSmall = ! mIsSmall;
        mIsLocked = true;
        LTDescr desc = LeanTween.scale(gameObject,tweenTo,.5f)
                    .setOnComplete(() => mIsLocked = false);
        LTDescr finalDesc = type == LeanTweenType.animationCurve ? desc.setEase(CustomCurve) : desc.setEase(type);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(mIsSmall || mIsLocked) return;
        LeanTween.scale(gameObject,new Vector3(1.1f,1.1f,0),.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(mIsSmall || mIsLocked) return;
        LeanTween.scale(gameObject,new Vector3(1.0f,1.0f,0),.1f);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
