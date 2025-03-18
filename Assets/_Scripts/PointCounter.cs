using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class PointCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private float duration;
    [SerializeField] private Transform PointTextContainer;
    [SerializeField] private Ease animationCurve;

    private float containerInitPosition;
    private float moveAmount;

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        toUpdate.SetText("0");
        containerInitPosition = PointTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }

    public void UpdatePoint(int point)
    {
        toUpdate.SetText($"{point}");
        PointTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetPointContainer(point));
    }

    private IEnumerator ResetPointContainer(int point)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{point}");
        Vector3 localPosition = PointTextContainer.localPosition;
        PointTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);



    }
}
