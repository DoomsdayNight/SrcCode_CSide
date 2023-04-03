using System;
using NKM;

namespace NKC
{
	// Token: 0x02000746 RID: 1862
	public class NKCEventRaceAnimationTemplet
	{
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06004A6B RID: 19051 RVA: 0x001653CC File Offset: 0x001635CC
		public RaceEventType Key
		{
			get
			{
				return this.m_RaceEventType;
			}
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x001653D4 File Offset: 0x001635D4
		public static NKCEventRaceAnimationTemplet LoadFromLua(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCEventRaceAnimationTemplet.cs", 48))
			{
				return null;
			}
			NKCEventRaceAnimationTemplet nkceventRaceAnimationTemplet = new NKCEventRaceAnimationTemplet();
			bool flag = true & cNKMLua.GetData<RaceEventType>("m_RaceEventType", ref nkceventRaceAnimationTemplet.m_RaceEventType) & cNKMLua.GetData("m_AnimationEventSetID", ref nkceventRaceAnimationTemplet.m_AnimationEventSetID) & cNKMLua.GetData("m_SlotCapacity", ref nkceventRaceAnimationTemplet.m_SlotCapacity);
			cNKMLua.GetData("m_TargetObjName", ref nkceventRaceAnimationTemplet.m_TargetObjName);
			cNKMLua.GetData("m_SpawnPosX", ref nkceventRaceAnimationTemplet.m_SpawnPosX);
			cNKMLua.GetData("m_Size", ref nkceventRaceAnimationTemplet.m_Size);
			cNKMLua.GetData("m_MaxCount", ref nkceventRaceAnimationTemplet.m_MaxCount);
			cNKMLua.GetData("m_MinIndex", ref nkceventRaceAnimationTemplet.m_MinIndex);
			cNKMLua.GetData("m_MaxIndex", ref nkceventRaceAnimationTemplet.m_MaxIndex);
			if (!flag)
			{
				return null;
			}
			return nkceventRaceAnimationTemplet;
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x001654A0 File Offset: 0x001636A0
		public void Join()
		{
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x001654A2 File Offset: 0x001636A2
		public void Validate()
		{
		}

		// Token: 0x04003941 RID: 14657
		public RaceEventType m_RaceEventType;

		// Token: 0x04003942 RID: 14658
		public string m_AnimationEventSetID;

		// Token: 0x04003943 RID: 14659
		public int m_SlotCapacity;

		// Token: 0x04003944 RID: 14660
		public string m_TargetObjName = string.Empty;

		// Token: 0x04003945 RID: 14661
		public float m_SpawnPosX;

		// Token: 0x04003946 RID: 14662
		public float m_Size = 1f;

		// Token: 0x04003947 RID: 14663
		public int m_MaxCount;

		// Token: 0x04003948 RID: 14664
		public int m_MinIndex;

		// Token: 0x04003949 RID: 14665
		public int m_MaxIndex;
	}
}
