using System;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000635 RID: 1589
	public class NKCASUnitViewerSpineSprite : NKCASUnitSpineSprite
	{
		// Token: 0x06003173 RID: 12659 RVA: 0x000F5473 File Offset: 0x000F3673
		public NKCASUnitViewerSpineSprite(string bundleName, string name, bool bAsync = false) : base(bundleName, name, bAsync)
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitViewerSpineSprite;
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000F5488 File Offset: 0x000F3688
		public override bool LoadComplete()
		{
			if (!base.LoadComplete())
			{
				return false;
			}
			if (this.m_UnitSpineSpriteInstant != null && this.m_UnitSpineSpriteInstant.m_Instant != null)
			{
				NKCComSpineSkeletonAnimationEvent componentInChildren = this.m_UnitSpineSpriteInstant.m_Instant.GetComponentInChildren<NKCComSpineSkeletonAnimationEvent>();
				if (componentInChildren != null)
				{
					UnityEngine.Object.Destroy(componentInChildren);
				}
				Transform transform = this.m_UnitSpineSpriteInstant.m_Instant.transform.Find("VFX");
				if (transform != null)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
				Transform transform2 = this.m_UnitSpineSpriteInstant.m_Instant.transform.Find("VFX_STATIC");
				if (transform2 != null)
				{
					UnityEngine.Object.Destroy(transform2.gameObject);
				}
			}
			else
			{
				Debug.LogError("NKCASUnitSpineSprite 로드 실패. ObjectPoolBundlename : " + this.m_ObjectPoolBundleName + " / ObjectPoolName : " + this.m_ObjectPoolName);
			}
			if (this.m_SPINE_SkeletonAnimation != null)
			{
				this.m_NKCComSpineSkeletonAnimationEvent = this.m_SPINE_SkeletonAnimation.GetComponent<NKCComSpineSkeletonAnimationEvent>();
				if (this.m_NKCComSpineSkeletonAnimationEvent != null)
				{
					this.m_NKCComSpineSkeletonAnimationEvent.SetActiveEvent(false);
				}
				else
				{
					Debug.LogError("NKCComSpineSkeletonAnimationEvent를 찾지 못했습니다. ObjectPoolBundlename : " + this.m_ObjectPoolBundleName + " / ObjectPoolName : " + this.m_ObjectPoolName);
				}
			}
			return true;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000F55BC File Offset: 0x000F37BC
		public override void Unload()
		{
			this.m_NKCComSpineSkeletonAnimationEvent = null;
			base.Unload();
		}

		// Token: 0x0400309E RID: 12446
		private NKCComSpineSkeletonAnimationEvent m_NKCComSpineSkeletonAnimationEvent;
	}
}
