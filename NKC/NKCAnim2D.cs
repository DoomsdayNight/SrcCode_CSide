using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000637 RID: 1591
	public class NKCAnim2D
	{
		// Token: 0x06003177 RID: 12663 RVA: 0x000F55E9 File Offset: 0x000F37E9
		public string GetAnimName()
		{
			return this.m_AnimName;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000F5624 File Offset: 0x000F3824
		public void Init()
		{
			this.m_Sprite = null;
			this.m_Animator = null;
			this.m_AnimatorStateInfo = default(AnimatorStateInfo);
			this.m_AnimatorClipInfos = null;
			this.m_bSpriteActive = false;
			this.m_AnimName = "";
			this.m_bLoop = false;
			this.m_fPlaySpeed = 1f;
			this.m_bAnimationEnd = false;
			this.m_bAnimStartThisFrame = false;
			this.m_AnimTimeNow = 0f;
			this.m_AnimTimeBefore = 0f;
			this.m_dicAnimAutoChange.Clear();
			this.m_bShow = true;
			this.m_bUpdateThisFrame = false;
			this.m_fUpdateDeltaTime = 0f;
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000F56C0 File Offset: 0x000F38C0
		public bool IsClipLoop()
		{
			for (int i = 0; i < this.m_AnimatorClipInfos.Length; i++)
			{
				AnimatorClipInfo animatorClipInfo = this.m_AnimatorClipInfos[i];
				if (animatorClipInfo.clip.isLooping)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000F5700 File Offset: 0x000F3900
		public void SetAnimObj(GameObject sprite)
		{
			this.Init();
			this.m_Sprite = sprite;
			this.m_Animator = this.m_Sprite.GetComponentInChildren<Animator>(true);
			if (this.m_Animator == null)
			{
				this.Init();
				return;
			}
			if (this.m_Animator.enabled)
			{
				this.m_Animator.enabled = false;
			}
			this.SetSpriteActive();
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000F5760 File Offset: 0x000F3960
		private void SetSpriteActive()
		{
			if (!this.m_Animator.gameObject.activeSelf || !this.m_Animator.gameObject.activeInHierarchy)
			{
				this.m_bSpriteActive = false;
				return;
			}
			this.m_bSpriteActive = true;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000F5798 File Offset: 0x000F3998
		public AnimationClip GetAnimClipByName(string animClipName)
		{
			if (this.m_Animator == null)
			{
				Debug.LogError("Animator Null!");
				return null;
			}
			if (this.m_Animator.runtimeAnimatorController == null)
			{
				Debug.LogError(this.m_Animator.gameObject.name + " has no animation controller!!");
				return null;
			}
			foreach (AnimationClip animationClip in this.m_Animator.runtimeAnimatorController.animationClips)
			{
				if (animationClip == null)
				{
					Debug.LogError("Animation null : " + this.m_Animator.gameObject.name);
				}
				else if (animClipName.CompareTo(animationClip.name) == 0)
				{
					return animationClip;
				}
			}
			return null;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000F5854 File Offset: 0x000F3A54
		public void Update(float deltaTime)
		{
			if (this.m_Sprite == null || this.m_Animator == null)
			{
				return;
			}
			if (!this.m_bSpriteActive)
			{
				this.SetSpriteActive();
				if (this.m_bSpriteActive)
				{
					this.Play(this.m_AnimName, this.m_bLoop, this.m_AnimTimeNormal);
				}
			}
			else
			{
				this.SetSpriteActive();
			}
			if (!this.m_bSpriteActive)
			{
				return;
			}
			this.m_bAnimStartThisFrame = false;
			this.m_AnimTimeBefore = this.m_AnimTimeNow;
			this.m_AnimTimeNow = this.GetAnimTimeNow(false);
			this.m_AnimTimeNormal = this.GetAnimTimeNow(true);
			this.m_AnimatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
			if (!this.m_AnimatorStateInfo.IsName(this.m_AnimName))
			{
				this.UpdateAnimator(deltaTime);
				return;
			}
			if (this.IsAnimationEnd())
			{
				if (this.m_dicAnimAutoChange.ContainsKey(this.m_AnimName))
				{
					AnimAutoChange animAutoChange = this.m_dicAnimAutoChange[this.m_AnimName];
					this.SetPlaySpeed(animAutoChange.m_fAnimSpeed);
					this.Play(animAutoChange.m_AnimName, animAutoChange.m_bLoop, 0f);
				}
				else if (this.m_bLoop && this.m_bAnimationEnd)
				{
					this.Play(this.m_AnimName, this.m_bLoop, 0f);
				}
				this.m_bAnimationEnd = true;
				return;
			}
			this.UpdateAnimator(deltaTime);
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000F59A0 File Offset: 0x000F3BA0
		public void UpdateAnimator(float deltaTime)
		{
			NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
			if (gameOptionData != null && gameOptionData.AnimationQuality == NKCGameOptionDataSt.GraphicOptionAnimationQuality.Normal)
			{
				this.m_bHalfUpdate = true;
			}
			if (this.m_bHalfUpdate)
			{
				this.m_fUpdateDeltaTime += deltaTime;
				this.m_bUpdateThisFrame = !this.m_bUpdateThisFrame;
				if (this.m_bUpdateThisFrame)
				{
					this.m_Animator.Update(this.m_fUpdateDeltaTime);
					this.m_fUpdateDeltaTime = 0f;
					return;
				}
			}
			else
			{
				this.m_Animator.Update(deltaTime);
			}
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000F5A20 File Offset: 0x000F3C20
		public void SetAnimAutoChange(string animName, string nextAnimName, bool bLoop, float fAnimSpeed)
		{
			AnimAutoChange animAutoChange;
			if (!this.m_dicAnimAutoChange.ContainsKey(animName))
			{
				animAutoChange = new AnimAutoChange();
				this.m_dicAnimAutoChange.Add(animName, animAutoChange);
			}
			else
			{
				animAutoChange = this.m_dicAnimAutoChange[animName];
			}
			animAutoChange.m_AnimName = nextAnimName;
			animAutoChange.m_bLoop = bLoop;
			animAutoChange.m_fAnimSpeed = fAnimSpeed;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000F5A78 File Offset: 0x000F3C78
		public void Play(string animName, bool bLoop, float fNormalTime = 0f)
		{
			if (this.m_Animator == null)
			{
				return;
			}
			this.m_AnimName = animName;
			this.m_bLoop = bLoop;
			this.m_Animator.speed = this.m_fPlaySpeed;
			this.m_bAnimationEnd = false;
			this.m_bAnimStartThisFrame = true;
			this.m_AnimTimeNow = 0f;
			this.m_AnimTimeBefore = 0f;
			this.m_AnimTimeNormal = fNormalTime;
			if (!this.m_bSpriteActive)
			{
				this.SetSpriteActive();
			}
			if (!this.m_bSpriteActive)
			{
				return;
			}
			this.m_Animator.Play(this.m_AnimName, -1, this.m_AnimTimeNormal);
			this.m_Animator.Update(0.001f);
			this.m_AnimatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
			this.m_AnimatorClipInfos = this.m_Animator.GetCurrentAnimatorClipInfo(0);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000F5B41 File Offset: 0x000F3D41
		public float GetAnimTimeNow(bool bNormalTime)
		{
			if (bNormalTime)
			{
				return this.m_AnimatorStateInfo.normalizedTime;
			}
			return this.m_AnimatorStateInfo.length * this.m_AnimatorStateInfo.normalizedTime;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000F5B69 File Offset: 0x000F3D69
		public bool IsAnimationEnd()
		{
			return this.m_AnimatorStateInfo.normalizedTime >= 1f;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000F5B80 File Offset: 0x000F3D80
		public void SetShow(bool bShow)
		{
			if (this.m_bShow == bShow)
			{
				return;
			}
			if (this.m_Sprite != null)
			{
				this.m_Sprite.SetActive(bShow);
			}
			this.m_bShow = bShow;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000F5BAD File Offset: 0x000F3DAD
		public void SetPlaySpeed(float fSpeed)
		{
			this.m_fPlaySpeed = fSpeed;
			this.m_Animator.speed = this.m_fPlaySpeed;
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000F5BC7 File Offset: 0x000F3DC7
		public bool EventTimer(float fTime)
		{
			return (fTime == 0f && this.m_bAnimStartThisFrame) || (fTime > this.m_AnimTimeBefore && fTime <= this.m_AnimTimeNow);
		}

		// Token: 0x040030A2 RID: 12450
		private GameObject m_Sprite;

		// Token: 0x040030A3 RID: 12451
		private Animator m_Animator;

		// Token: 0x040030A4 RID: 12452
		private AnimatorStateInfo m_AnimatorStateInfo;

		// Token: 0x040030A5 RID: 12453
		private AnimatorClipInfo[] m_AnimatorClipInfos;

		// Token: 0x040030A6 RID: 12454
		private bool m_bSpriteActive;

		// Token: 0x040030A7 RID: 12455
		private string m_AnimName = "";

		// Token: 0x040030A8 RID: 12456
		private bool m_bLoop;

		// Token: 0x040030A9 RID: 12457
		private float m_fPlaySpeed = 1f;

		// Token: 0x040030AA RID: 12458
		private bool m_bAnimationEnd;

		// Token: 0x040030AB RID: 12459
		private bool m_bAnimStartThisFrame;

		// Token: 0x040030AC RID: 12460
		private float m_AnimTimeNow;

		// Token: 0x040030AD RID: 12461
		private float m_AnimTimeBefore;

		// Token: 0x040030AE RID: 12462
		private float m_AnimTimeNormal;

		// Token: 0x040030AF RID: 12463
		private bool m_bShow = true;

		// Token: 0x040030B0 RID: 12464
		private bool m_bHalfUpdate;

		// Token: 0x040030B1 RID: 12465
		private bool m_bUpdateThisFrame;

		// Token: 0x040030B2 RID: 12466
		private float m_fUpdateDeltaTime;

		// Token: 0x040030B3 RID: 12467
		private Dictionary<string, AnimAutoChange> m_dicAnimAutoChange = new Dictionary<string, AnimAutoChange>();
	}
}
