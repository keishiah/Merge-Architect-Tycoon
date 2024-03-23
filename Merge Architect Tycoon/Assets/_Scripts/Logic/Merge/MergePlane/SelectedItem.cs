using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectedItem : MonoBehaviour
{
    public Slot selectedSlot;

    public float approachSpeed = 0.02f;
    public float growthBound = 2f;
    public float shrinkBound = 0.5f;

    private float currentRatio = 1;
    private Image image;
    private bool keepGoing = true;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        StartCoroutine(_PulseAnimation());
    }
    private void OnDisable()
    {
        StopCoroutine(_PulseAnimation());
    }

    public void SelectSlot(Slot slot)
    {
        selectedSlot = slot;
        transform.transform.position = (slot.transform.position);
        gameObject.SetActive(true);
    }

    private IEnumerator _PulseAnimation()
    {
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (currentRatio != growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                // Update our text element
                image.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (currentRatio != shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                // Update our text element
                image.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}