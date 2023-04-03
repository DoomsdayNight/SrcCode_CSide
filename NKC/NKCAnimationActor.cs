using System;
using NKC.UI.Component;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200063A RID: 1594
	public class NKCAnimationActor : MonoBehaviour, INKCAnimationActor
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000F69D0 File Offset: 0x000F4BD0
		public Animator Animator
		{
			get
			{
				return this.m_Ani;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000F69D8 File Offset: 0x000F4BD8
		public Transform Transform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000F69E0 File Offset: 0x000F4BE0
		public Transform SDParent
		{
			get
			{
				return this.m_trSDParent;
			}
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000F69E8 File Offset: 0x000F4BE8
		public void SetSpineIllust(NKCASUIUnitIllust illust, bool bSetParent = false)
		{
			if (bSetParent)
			{
				illust.SetParent(this.m_trSDParent, false);
			}
			this.m_UnitIllust = illust;
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000F6A01 File Offset: 0x000F4C01
		public NKCASUIUnitIllust GetSpineIllust()
		{
			return this.m_UnitIllust;
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000F6A0C File Offset: 0x000F4C0C
		public Vector3 GetBonePosition(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return base.transform.position;
			}
			if (this.m_UnitIllust != null)
			{
				return this.m_UnitIllust.GetBoneWorldPosition(name);
			}
			Transform transform = base.transform.Find(name);
			if (!(transform != null))
			{
				return base.transform.position;
			}
			return transform.position;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000F6A6A File Offset: 0x000F4C6A
		public void PlaySpineAnimation(string name, bool loop, float timeScale)
		{
			if (this.m_UnitIllust == null)
			{
				return;
			}
			this.m_UnitIllust.SetAnimation(name, loop, 0, true, 0f, false);
			this.m_UnitIllust.SetTimeScale(timeScale);
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000F6A98 File Offset: 0x000F4C98
		public void PlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, float timeScale, bool bDefaultAnim)
		{
			if (this.m_UnitIllust == null)
			{
				return;
			}
			if (bDefaultAnim)
			{
				this.m_UnitIllust.SetDefaultAnimation(eAnim, true, false, 0f);
			}
			else
			{
				this.m_UnitIllust.SetAnimation(eAnim, loop, 0, true, 0f, false);
			}
			this.m_UnitIllust.SetTimeScale(timeScale);
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000F6AE8 File Offset: 0x000F4CE8
		public bool IsSpineAnimationFinished(NKCASUIUnitIllust.eAnimation eAnim)
		{
			if (this.m_UnitIllust == null)
			{
				return true;
			}
			string animationName = NKCASUIUnitIllust.GetAnimationName(eAnim);
			return this.IsSpineAnimationFinished(animationName);
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000F6B0D File Offset: 0x000F4D0D
		public bool IsSpineAnimationFinished(string name)
		{
			return this.m_UnitIllust == null || !(this.m_UnitIllust.GetCurrentAnimationName(0) == name) || this.m_UnitIllust.GetAnimationTime(name) <= this.m_UnitIllust.GetCurrentAnimationTime(0);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000F6B4A File Offset: 0x000F4D4A
		public void PlayEmotion(string animName, float speed)
		{
			if (this.m_comEmotion != null)
			{
				this.m_comEmotion.Play(animName, speed);
			}
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000F6B67 File Offset: 0x000F4D67
		public bool CanPlaySpineAnimation(string name)
		{
			return this.m_UnitIllust != null && this.m_UnitIllust.HasAnimation(name);
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000F6B7F File Offset: 0x000F4D7F
		public bool CanPlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim)
		{
			return this.m_UnitIllust != null && this.m_UnitIllust.HasAnimation(eAnim);
		}

		// Token: 0x040030D0 RID: 12496
		public Animator m_Ani;

		// Token: 0x040030D1 RID: 12497
		public Transform m_trSDParent;

		// Token: 0x040030D2 RID: 12498
		public NKCUIComCharacterEmotion m_comEmotion;

		// Token: 0x040030D3 RID: 12499
		private NKCASUIUnitIllust m_UnitIllust;
	}
}
