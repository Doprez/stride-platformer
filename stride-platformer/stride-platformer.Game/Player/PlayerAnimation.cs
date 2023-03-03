// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using Stride.Core;
using Stride.Core.Annotations;
using Stride.Core.Collections;
using Stride.Core.Mathematics;
using Stride.Animations;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Physics;

namespace StridePlatformer.Player;

public class PlayerAnimation : SyncScript
{

	public CharacterComponent Character;
	public AnimationComponent Animations;


	private double _currentTime = 0;
	private bool _isGrounded = false;
	private float _runSpeed = 0;
	private AnimationState _state = AnimationState.Airborne;
	private readonly EventReceiver<float> runSpeedEvent = new EventReceiver<float>(PlayerController.RunSpeedEventKey);
	private readonly EventReceiver<bool> isGroundedEvent = new EventReceiver<bool>(PlayerController.IsGroundedEventKey);

	public override void Start()
	{
		
	}

	public void PlayRun()
	{
		if(!Animations.IsPlaying("Run"))
		{
			var playingClip = Animations.Play("Run");
			playingClip.RepeatMode = AnimationRepeatMode.PlayOnceHold;
		}
	}

	public void PlayJump()
	{
		if (!Animations.IsPlaying("Jump"))
		{
			var playingClip = Animations.Play("Jump");
			playingClip.RepeatMode = AnimationRepeatMode.PlayOnce;
		}
	}

	public void PlayLand()
	{
		if (!Animations.IsPlaying("Land"))
		{
			var playingClip = Animations.Play("Land");
			playingClip.RepeatMode = AnimationRepeatMode.PlayOnce;
		}
	}

	public void PlayIdle()
	{
		if (!Animations.IsPlaying("Idle"))
		{
			var playingClip = Animations.Play("Idle");
			playingClip.RepeatMode = AnimationRepeatMode.PlayOnce;
		}
	}

	public override void Update()
	{
		// State control
		runSpeedEvent.TryReceive(out _runSpeed);
		bool isGroundedNewValue;
		isGroundedEvent.TryReceive(out isGroundedNewValue);
		if (_isGrounded != isGroundedNewValue)
		{
			_currentTime = 0;
			_isGrounded = isGroundedNewValue;
			_state = (_isGrounded) ? AnimationState.Landing : AnimationState.Jumping;
		}

		if(_isGrounded && _runSpeed == 0)
		{
			_state = AnimationState.Idle;
		}

		if (_isGrounded && _runSpeed > 0.001f)
		{
			_state = AnimationState.Walking;
		}

		switch (_state)
		{
			case AnimationState.Idle: PlayIdle(); break;
			case AnimationState.Walking: PlayRun(); break;
			case AnimationState.Jumping: PlayJump(); break;
			case AnimationState.Landing: PlayLand(); break;
		}

		_runSpeed = 0;
	}
}


public enum AnimationState
{
	Walking,
	Jumping,
	Airborne,
	Landing,
	Idle,
}