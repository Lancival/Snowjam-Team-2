using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(RawImage))]

/*
	IMPORTANT: The texture used with this script MUST have Wrap Mode set to repeat in its import settings.
*/

public class ScrollingBackground : MonoBehaviour
{

	[Tooltip("Speed at which the background scrolls.")]
	[SerializeField] private float speed = 2.0f;

	// RectTransform attached to this GameObject.
	private RectTransform rectTransform;

	// The texture being displayed as the background.
	private RawImage image;

	// How much to offset the texture. Used for scrolling.
	private Vector2 offset = Vector2.zero;

	// Number of times to repeat the texture.
	private Vector2 repeat;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();

		image = GetComponent<RawImage>();

		repeat = new Vector2(rectTransform.sizeDelta.x / image.mainTexture.width, rectTransform.sizeDelta.y / image.mainTexture.height);
		image.uvRect = new Rect(offset, repeat);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 offset = new Vector2(Time.time * speed, Time.time * speed);
        image.uvRect = new Rect(offset, repeat);
    }
}
