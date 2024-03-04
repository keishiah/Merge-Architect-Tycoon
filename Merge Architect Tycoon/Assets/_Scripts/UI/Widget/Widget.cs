using UnityEngine;

public class Widget : MonoBehaviour
{
    public virtual void OnEnable()
    {
        gameObject.SetActive(true);
    }
    public virtual void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
