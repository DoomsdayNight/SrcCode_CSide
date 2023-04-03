using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC.UI.Component
{
	// Token: 0x02000C52 RID: 3154
	public class NKCUIComCharacterEmotion : MonoBehaviour
	{
		// Token: 0x060092DE RID: 37598 RVA: 0x003222F2 File Offset: 0x003204F2
		public void Init()
		{
			if (this.m_Spine != null)
			{
				this.m_Spine.Initialize(true);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060092DF RID: 37599 RVA: 0x0032231A File Offset: 0x0032051A
		public void Play(string animName, float speed = 1f)
		{
			if (string.IsNullOrEmpty(animName))
			{
				this.Stop();
				return;
			}
			if (string.Equals(animName, "NONE", StringComparison.InvariantCultureIgnoreCase) || string.Equals(animName, "STOP", StringComparison.InvariantCultureIgnoreCase))
			{
				this.Stop();
				return;
			}
			this.SetAnimation(animName, speed);
		}

		// Token: 0x060092E0 RID: 37600 RVA: 0x00322356 File Offset: 0x00320556
		public void Play(NKCUIComCharacterEmotion.Type animType, float speed = 1f)
		{
			if (animType == NKCUIComCharacterEmotion.Type.NONE)
			{
				this.Stop();
				return;
			}
			this.SetAnimation(this.GetAnimname(animType), speed);
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x00322370 File Offset: 0x00320570
		public void Stop()
		{
			if (this.m_CanvasGroup != null)
			{
				this.m_CanvasGroup.DOKill(false);
				this.m_CanvasGroup.alpha = 1f;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060092E2 RID: 37602 RVA: 0x003223A9 File Offset: 0x003205A9
		private string GetAnimname(NKCUIComCharacterEmotion.Type animType)
		{
			return string.Format("EMO_{0}", animType.ToString().ToUpper());
		}

		// Token: 0x060092E3 RID: 37603 RVA: 0x003223C7 File Offset: 0x003205C7
		protected bool HasAnimation(string AnimName)
		{
			return !(this.m_Spine == null) && this.m_Spine.SkeletonData.FindAnimation(AnimName) != null;
		}

		// Token: 0x060092E4 RID: 37604 RVA: 0x003223F0 File Offset: 0x003205F0
		protected void SetAnimation(string animName, float speed = 1f)
		{
			if (this.HasAnimation(animName))
			{
				base.gameObject.SetActive(true);
				Skeleton skeleton = this.m_Spine.Skeleton;
				if (skeleton != null)
				{
					skeleton.SetToSetupPose();
				}
				this.m_Spine.AnimationState.SetAnimation(0, animName, true).TimeScale = speed;
				if (this.m_CanvasGroup != null)
				{
					this.m_CanvasGroup.DOKill(false);
					this.m_CanvasGroup.alpha = 1f;
					this.m_CanvasGroup.DOFade(0f, this.m_fAnimFadeTime).SetDelay(this.m_fAnimPlayTime).OnComplete(new TweenCallback(this.Stop));
					return;
				}
			}
			else
			{
				Debug.LogError("Has no emotion : " + animName);
				this.Stop();
			}
		}

		// Token: 0x060092E5 RID: 37605 RVA: 0x003224B9 File Offset: 0x003206B9
		protected void AddAnimation(string AnimName, bool loop)
		{
			if (this.HasAnimation(AnimName))
			{
				this.m_Spine.AnimationState.AddAnimation(0, AnimName, loop, 0f);
			}
		}

		// Token: 0x060092E6 RID: 37606 RVA: 0x003224E0 File Offset: 0x003206E0
		private void Update()
		{
			if (base.transform.lossyScale.x < 0f)
			{
				base.transform.localScale = new Vector3(-base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
			}
		}

		// Token: 0x04007FF1 RID: 32753
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04007FF2 RID: 32754
		public SkeletonGraphic m_Spine;

		// Token: 0x04007FF3 RID: 32755
		public float m_fAnimFadeTime = 0.5f;

		// Token: 0x04007FF4 RID: 32756
		public float m_fAnimPlayTime = 2.5f;

		// Token: 0x02001A0D RID: 6669
		public enum Type
		{
			// Token: 0x0400AD9F RID: 44447
			NONE,
			// Token: 0x0400ADA0 RID: 44448
			Angry,
			// Token: 0x0400ADA1 RID: 44449
			Annoy,
			// Token: 0x0400ADA2 RID: 44450
			Exclamation,
			// Token: 0x0400ADA3 RID: 44451
			Flower,
			// Token: 0x0400ADA4 RID: 44452
			Gloomy,
			// Token: 0x0400ADA5 RID: 44453
			Heart,
			// Token: 0x0400ADA6 RID: 44454
			Imagine,
			// Token: 0x0400ADA7 RID: 44455
			Laugh,
			// Token: 0x0400ADA8 RID: 44456
			Music,
			// Token: 0x0400ADA9 RID: 44457
			Question,
			// Token: 0x0400ADAA RID: 44458
			Star,
			// Token: 0x0400ADAB RID: 44459
			Stress,
			// Token: 0x0400ADAC RID: 44460
			Surprise,
			// Token: 0x0400ADAD RID: 44461
			Sweat,
			// Token: 0x0400ADAE RID: 44462
			Talk,
			// Token: 0x0400ADAF RID: 44463
			Warm
		}
	}
}
