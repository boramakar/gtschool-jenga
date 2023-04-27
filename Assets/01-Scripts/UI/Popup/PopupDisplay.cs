using DG.Tweening;
using UnityEngine;

public abstract class PopupDisplay : MonoBehaviour
{
    public abstract void ShowMessage(string message);

    protected virtual void Open()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, GameManager.Instance.gameSettings.popupAnimationDuration);
    }

    public virtual void Close()
    {
        transform.DOScale(Vector3.zero, GameManager.Instance.gameSettings.popupAnimationDuration)
            .OnComplete(() => gameObject.SetActive(false));
    }
}