using System;
using NKM;

namespace NKC.BT.Office
{
	// Token: 0x0200081A RID: 2074
	public class BTOfficeIdle : BTOfficeActionBase
	{
		// Token: 0x060051F3 RID: 20979 RVA: 0x0018E2A0 File Offset: 0x0018C4A0
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			if (this.MinIdleTime == 0f && this.MaxIdleTime == 0f && !string.IsNullOrEmpty(this.IdleAnimName))
			{
				float animTime = this.m_Character.GetAnimTime(this.IdleAnimName);
				NKCAnimationInstance idleInstance = base.GetIdleInstance(animTime, this.m_Character.transform.localPosition, this.IdleAnimName);
				this.m_Character.EnqueueAnimation(idleInstance);
				return;
			}
			NKCAnimationInstance idleInstance2 = base.GetIdleInstance(NKMRandom.Range(this.MinIdleTime, this.MaxIdleTime), this.m_Character.transform.localPosition, this.IdleAnimName);
			this.m_Character.EnqueueAnimation(idleInstance2);
		}

		// Token: 0x04004233 RID: 16947
		public float MinIdleTime;

		// Token: 0x04004234 RID: 16948
		public float MaxIdleTime;

		// Token: 0x04004235 RID: 16949
		public string IdleAnimName;
	}
}
