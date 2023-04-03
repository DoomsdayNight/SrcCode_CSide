using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200061C RID: 1564
	[DisallowMultipleComponent]
	public class NKCComSpineAnimControl : MonoBehaviour
	{
		// Token: 0x06003053 RID: 12371 RVA: 0x000EE054 File Offset: 0x000EC254
		private void Awake()
		{
			Transform transform = base.gameObject.transform.Find("SPINE_SkeletonAnimation");
			if (transform == null)
			{
				return;
			}
			this.m_SPINE_SkeletonAnimation = transform.gameObject;
			this.m_NKCAnimSpine.SetAnimObj(this.m_SPINE_SkeletonAnimation, base.gameObject, false);
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000EE0A5 File Offset: 0x000EC2A5
		private void Update()
		{
			if (this.m_SPINE_SkeletonAnimation == null)
			{
				return;
			}
			this.m_NKCAnimSpine.Update(Time.deltaTime);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000EE0C6 File Offset: 0x000EC2C6
		public void Play(string animName)
		{
			if (this.m_SPINE_SkeletonAnimation == null)
			{
				return;
			}
			this.m_NKCAnimSpine.SetPlaySpeed(this.timeScale);
			this.m_NKCAnimSpine.Play(animName, this.loop, 0f);
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000EE0FF File Offset: 0x000EC2FF
		public void Stop()
		{
		}

		// Token: 0x04002FB6 RID: 12214
		private GameObject m_SPINE_SkeletonAnimation;

		// Token: 0x04002FB7 RID: 12215
		private NKCAnimSpine m_NKCAnimSpine = new NKCAnimSpine();

		// Token: 0x04002FB8 RID: 12216
		public string animationName;

		// Token: 0x04002FB9 RID: 12217
		public bool loop;

		// Token: 0x04002FBA RID: 12218
		public float timeScale = 1f;
	}
}
