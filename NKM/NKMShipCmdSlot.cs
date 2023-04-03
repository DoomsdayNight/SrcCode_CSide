using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000498 RID: 1176
	public class NKMShipCmdSlot : ISerializable
	{
		// Token: 0x060020ED RID: 8429 RVA: 0x000A7C00 File Offset: 0x000A5E00
		public NKMShipCmdSlot()
		{
			this.targetStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();
			this.targetRoleType = new HashSet<NKM_UNIT_ROLE_TYPE>();
			this.statType = NKM_STAT_TYPE.NST_RANDOM;
			this.statValue = 0f;
			this.statFactor = 0f;
			this.isLock = false;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000A7C4D File Offset: 0x000A5E4D
		public NKMShipCmdSlot(HashSet<NKM_UNIT_STYLE_TYPE> styleType, HashSet<NKM_UNIT_ROLE_TYPE> roleType, NKM_STAT_TYPE statType, float value, float factor, bool isLock)
		{
			this.targetStyleType = styleType;
			this.targetRoleType = roleType;
			this.statType = statType;
			this.statValue = value;
			this.statFactor = factor;
			this.isLock = isLock;
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000A7C84 File Offset: 0x000A5E84
		public bool CanApply(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return false;
			}
			NKMUnitTemplet unitTemplet = unitData.GetUnitTemplet();
			NKMUnitTempletBase nkmunitTempletBase = (unitTemplet != null) ? unitTemplet.m_UnitTempletBase : null;
			return nkmunitTempletBase != null && (this.targetStyleType.Count <= 0 || this.targetStyleType.Contains(nkmunitTempletBase.m_NKM_UNIT_STYLE_TYPE) || this.targetStyleType.Contains(nkmunitTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB)) && (this.targetRoleType.Count <= 0 || this.targetRoleType.Contains(nkmunitTempletBase.m_NKM_UNIT_ROLE_TYPE));
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000A7D08 File Offset: 0x000A5F08
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_UNIT_STYLE_TYPE>(ref this.targetStyleType);
			stream.PutOrGetEnum<NKM_UNIT_ROLE_TYPE>(ref this.targetRoleType);
			stream.PutOrGetEnum<NKM_STAT_TYPE>(ref this.statType);
			stream.PutOrGet(ref this.statValue);
			stream.PutOrGet(ref this.statFactor);
			stream.PutOrGet(ref this.isLock);
		}

		// Token: 0x04002170 RID: 8560
		public HashSet<NKM_UNIT_STYLE_TYPE> targetStyleType;

		// Token: 0x04002171 RID: 8561
		public HashSet<NKM_UNIT_ROLE_TYPE> targetRoleType;

		// Token: 0x04002172 RID: 8562
		public NKM_STAT_TYPE statType;

		// Token: 0x04002173 RID: 8563
		public float statValue;

		// Token: 0x04002174 RID: 8564
		public float statFactor;

		// Token: 0x04002175 RID: 8565
		public bool isLock;
	}
}
