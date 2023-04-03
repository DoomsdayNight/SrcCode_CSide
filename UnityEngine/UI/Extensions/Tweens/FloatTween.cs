using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.Tweens
{
	// Token: 0x0200035F RID: 863
	public struct FloatTween : ITweenValue
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0004DA23 File Offset: 0x0004BC23
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x0004DA2B File Offset: 0x0004BC2B
		public float startFloat
		{
			get
			{
				return this.m_StartFloat;
			}
			set
			{
				this.m_StartFloat = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0004DA34 File Offset: 0x0004BC34
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0004DA3C File Offset: 0x0004BC3C
		public float targetFloat
		{
			get
			{
				return this.m_TargetFloat;
			}
			set
			{
				this.m_TargetFloat = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0004DA45 File Offset: 0x0004BC45
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0004DA4D File Offset: 0x0004BC4D
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0004DA56 File Offset: 0x0004BC56
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0004DA5E File Offset: 0x0004BC5E
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0004DA67 File Offset: 0x0004BC67
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			this.m_Target.Invoke(Mathf.Lerp(this.m_StartFloat, this.m_TargetFloat, floatPercentage));
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0004DA8F File Offset: 0x0004BC8F
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0004DAB0 File Offset: 0x0004BCB0
		public void AddOnFinishCallback(UnityAction callback)
		{
			if (this.m_Finish == null)
			{
				this.m_Finish = new FloatTween.FloatFinishCallback();
			}
			this.m_Finish.AddListener(callback);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0004DAD1 File Offset: 0x0004BCD1
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0004DAD9 File Offset: 0x0004BCD9
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0004DAE1 File Offset: 0x0004BCE1
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0004DAEC File Offset: 0x0004BCEC
		public void Finished()
		{
			if (this.m_Finish != null)
			{
				this.m_Finish.Invoke();
			}
		}

		// Token: 0x04000E50 RID: 3664
		private float m_StartFloat;

		// Token: 0x04000E51 RID: 3665
		private float m_TargetFloat;

		// Token: 0x04000E52 RID: 3666
		private float m_Duration;

		// Token: 0x04000E53 RID: 3667
		private bool m_IgnoreTimeScale;

		// Token: 0x04000E54 RID: 3668
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x04000E55 RID: 3669
		private FloatTween.FloatFinishCallback m_Finish;

		// Token: 0x0200117B RID: 4475
		public class FloatTweenCallback : UnityEvent<float>
		{
		}

		// Token: 0x0200117C RID: 4476
		public class FloatFinishCallback : UnityEvent
		{
		}
	}
}
