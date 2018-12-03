﻿using System;
using System.Collections;
using System.Collections.Generic;
using FarrokhGames.SpriteAnimation;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	IAnimator _animator;
	AudioSource _audioSource;

	[SerializeField] AudioClip[] _footsteps;

	ISpriteAnimator _spriteAnimator;
	IImageAnimator _imageAnimator;

	void Awake()
	{
		_animator = GetComponent<IAnimator>();
		_animator.OnTrigger += HandleTrigger;

		_audioSource = GetComponent<AudioSource>();

		_spriteAnimator = _animator as ISpriteAnimator;
		_imageAnimator = _animator as IImageAnimator;
	}

	void Update()
	{
		var walkAxis = Input.GetAxis("Horizontal");

		if (walkAxis == 0)
		{
			_animator.Play("Idle");
		}
		else
		{
			_animator.Play("Walk");
			if (_spriteAnimator != null) { _spriteAnimator.Flip = walkAxis < 0; }
			if (_imageAnimator != null) { _imageAnimator.Flip = walkAxis < 0; }
		}
	}

	void HandleTrigger(string trigger)
	{
		if (trigger == "footstep")
		{
			if (_footsteps.Length > 0)
			{
				var clip = _footsteps[UnityEngine.Random.Range(0, _footsteps.Length - 1)];
				_audioSource.PlayOneShot(clip);
			}
		}
	}
}