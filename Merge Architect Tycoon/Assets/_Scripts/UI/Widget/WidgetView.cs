using UnityEngine;

public class WidgetView : MonoBehaviour
{
    public virtual void OnOpen()
    {
        gameObject.SetActive(true);
    }
    public virtual void OnClose()
    {
        gameObject.SetActive(false);
    }
}
