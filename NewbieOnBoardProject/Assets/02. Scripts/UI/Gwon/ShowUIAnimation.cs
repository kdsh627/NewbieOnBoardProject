using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public class UIAnimationInfo
{
    public RectTransform UI;
    public Vector3 start;
    public Vector3 end;
    public float duration;
    public Ease type;
}

namespace UIAnimation.ShowUI
{
    public class ShowUIAnimation : MonoBehaviour
    {
        [SerializeField] private UIAnimationInfo info;

        private void OnEnable()
        {
            Sequence seq = DOTween.Sequence();

            ((RectTransform)transform).localPosition = info.start;
            gameObject.GetComponent<TMP_Text>().alpha = 1f;

            //위로 올라가면서 사라지는 애니메이션
            seq.Append(info.UI.DOAnchorPos(info.end, info.duration).SetEase(info.type))
                .Join(info.UI.GetComponent<TMP_Text>().DOFade(0.0f, info.duration).SetEase(info.type)).OnComplete(() => { gameObject.SetActive(false); });
        }
    }

}