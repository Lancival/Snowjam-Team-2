using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class LetterManager : MonoBehaviour
{

	[SerializeField] private Sprite[] letterSprites;
	private int letters = 0;

	private Image image;
	private Animator animator;
	private AudioSource source;

    void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void ShowLetter()
    {
    	letters++;
    	if (letters < letterSprites.Length)
    	{
    		image.sprite = letterSprites[letters];
	    	source.Play();
	    	animator.ResetTrigger("FadeOut");
	    	animator.SetTrigger("FadeIn");
	    }
    }

    public void HideLetter()
    {
    	animator.ResetTrigger("FadeIn");
    	animator.SetTrigger("FadeOut");
    }
}
