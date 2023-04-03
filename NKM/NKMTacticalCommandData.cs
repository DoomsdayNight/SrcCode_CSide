using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000477 RID: 1143
	public class NKMTacticalCommandData : ISerializable
	{
		// Token: 0x06001F1F RID: 7967 RVA: 0x00093CE8 File Offset: 0x00091EE8
		public virtual void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_TCID);
			stream.PutOrGet(ref this.m_Level);
			stream.PutOrGet(ref this.m_fCoolTimeNow);
			stream.PutOrGet(ref this.m_UseCount);
			stream.PutOrGet(ref this.m_ComboCount);
			stream.PutOrGet(ref this.m_fComboResetCoolTimeNow);
			stream.PutOrGet(ref this.m_bCoolTimeOn);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00093D4C File Offset: 0x00091F4C
		public void DeepCopyFromSource(NKMTacticalCommandData source)
		{
			this.m_TCID = source.m_TCID;
			this.m_Level = source.m_Level;
			this.m_fCoolTimeNow = source.m_fCoolTimeNow;
			this.m_UseCount = source.m_UseCount;
			this.m_ComboCount = source.m_ComboCount;
			this.m_fComboResetCoolTimeNow = source.m_fComboResetCoolTimeNow;
			this.m_bCoolTimeOn = source.m_bCoolTimeOn;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00093DAD File Offset: 0x00091FAD
		public void AddComboCount()
		{
			this.m_ComboCount += 1;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00093DC0 File Offset: 0x00091FC0
		public void Update(float fDeltaTime)
		{
			this.m_fCoolTimeNow -= fDeltaTime;
			if (this.m_fCoolTimeNow < 0f)
			{
				this.m_fCoolTimeNow = 0f;
			}
			this.m_fComboResetCoolTimeNow -= fDeltaTime;
			if (this.m_fComboResetCoolTimeNow < 0f)
			{
				this.m_fComboResetCoolTimeNow = 0f;
			}
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00093E1C File Offset: 0x0009201C
		public NKMTacticalCombo GetNKMTacticalComboGoal()
		{
			NKMTacticalCommandTemplet tacticalCommandTempletByID = NKMTacticalCommandManager.GetTacticalCommandTempletByID((int)this.m_TCID);
			if (tacticalCommandTempletByID == null)
			{
				return null;
			}
			if (this.m_ComboCount < 0 || (int)this.m_ComboCount >= tacticalCommandTempletByID.m_listComboType.Count)
			{
				return null;
			}
			return tacticalCommandTempletByID.m_listComboType[(int)this.m_ComboCount];
		}

		// Token: 0x04001F98 RID: 8088
		public short m_TCID;

		// Token: 0x04001F99 RID: 8089
		public byte m_Level = 1;

		// Token: 0x04001F9A RID: 8090
		public float m_fCoolTimeNow;

		// Token: 0x04001F9B RID: 8091
		public byte m_UseCount;

		// Token: 0x04001F9C RID: 8092
		public byte m_ComboCount;

		// Token: 0x04001F9D RID: 8093
		public float m_fComboResetCoolTimeNow;

		// Token: 0x04001F9E RID: 8094
		public bool m_bCoolTimeOn = true;
	}
}
