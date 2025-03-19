using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private float duration;
    [SerializeField] private Transform LifeTextContainer;
    [SerializeField] private Ease animationCurve;

    private float containerInitPosition;
    private float moveAmount;

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("3");
        toUpdate.SetText("3");
        containerInitPosition = LifeTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }

    public void UpdateLife(int life)
    {
        toUpdate.SetText($"{life}");
        LifeTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetLifeContainer(life));
    }

    private IEnumerator ResetLifeContainer(int life)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{life}");
        Vector3 localPosition = LifeTextContainer.localPosition;
        LifeTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);



    }
}
