using System;
using System.Collections.Generic;

namespace NKM
{
	// Token: 0x020003D3 RID: 979
	public class NKMDamageInst : NKMObjectPoolData
	{
		// Token: 0x060019D1 RID: 6609 RVA: 0x0006ECF9 File Offset: 0x0006CEF9
		public NKMDamageInst()
		{
			this.m_NKM_OBJECT_POOL_TYPE = NKM_OBJECT_POOL_TYPE.NOPT_NKMDamageInst;
			this.Init();
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0006ED1C File Offset: 0x0006CF1C
		public void Init()
		{
			this.m_Templet = null;
			this.m_bEvade = false;
			this.m_ReActResult = NKM_REACT_TYPE.NRT_INVALID;
			this.m_EventAttack = null;
			this.m_listHitUnit.Clear();
			this.m_AttackCount = 0;
			this.m_bReAttackCountOver = false;
			this.m_ReAttackCount = 0;
			this.m_fReAttackGap = 0f;
			this.m_AttackerTeamType = NKM_TEAM_TYPE.NTT_INVALID;
			this.m_AttackerType = NKM_REACTOR_TYPE.NRT_INVALID;
			this.m_AttackerGameUnitUID = 0;
			this.m_AttackerEffectUID = 0;
			this.m_DefenderUID = 0;
			this.m_AttackerPosX = 0f;
			this.m_AttackerPosZ = 0f;
			this.m_AttackerPosJumpY = 0f;
			this.m_bAttackerRight = false;
			this.m_bAttackerAwaken = false;
			this.m_AttackerAddAttackUnitCount = 0;
			this.m_AttackerUnitSkillTemplet = null;
			this.m_AtkBuffCount = 0;
			this.m_DefBuffCount = 0;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0006EDE0 File Offset: 0x0006CFE0
		public void DeepCopyFromSource(NKMDamageInst source)
		{
			this.m_Templet = source.m_Templet;
			this.m_bEvade = source.m_bEvade;
			this.m_ReActResult = source.m_ReActResult;
			this.m_EventAttack = source.m_EventAttack;
			this.m_listHitUnit.Clear();
			for (int i = 0; i < source.m_listHitUnit.Count; i++)
			{
				this.m_listHitUnit.Add(source.m_listHitUnit[i]);
			}
			this.m_AttackCount = source.m_AttackCount;
			this.m_bReAttackCountOver = source.m_bReAttackCountOver;
			this.m_ReAttackCount = source.m_ReAttackCount;
			this.m_fReAttackGap = source.m_fReAttackGap;
			this.m_AttackerTeamType = source.m_AttackerTeamType;
			this.m_AttackerType = source.m_AttackerType;
			this.m_AttackerGameUnitUID = source.m_AttackerGameUnitUID;
			this.m_AttackerEffectUID = source.m_AttackerEffectUID;
			this.m_DefenderUID = source.m_DefenderUID;
			this.m_AttackerPosX = source.m_AttackerPosX;
			this.m_AttackerPosZ = source.m_AttackerPosZ;
			this.m_AttackerPosJumpY = source.m_AttackerPosJumpY;
			this.m_bAttackerRight = source.m_bAttackerRight;
			this.m_bAttackerAwaken = source.m_bAttackerAwaken;
			this.m_AttackerAddAttackUnitCount = source.m_AttackerAddAttackUnitCount;
			this.m_AttackerUnitSkillTemplet = source.m_AttackerUnitSkillTemplet;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0006EF15 File Offset: 0x0006D115
		public override void Close()
		{
			this.Init();
		}

		// Token: 0x040012D1 RID: 4817
		public NKMDamageTemplet m_Templet;

		// Token: 0x040012D2 RID: 4818
		public NKMEventAttack m_EventAttack;

		// Token: 0x040012D3 RID: 4819
		public bool m_bEvade;

		// Token: 0x040012D4 RID: 4820
		public NKM_REACT_TYPE m_ReActResult;

		// Token: 0x040012D5 RID: 4821
		public List<short> m_listHitUnit = new List<short>();

		// Token: 0x040012D6 RID: 4822
		public int m_AttackCount;

		// Token: 0x040012D7 RID: 4823
		public bool m_bReAttackCountOver;

		// Token: 0x040012D8 RID: 4824
		public int m_ReAttackCount;

		// Token: 0x040012D9 RID: 4825
		public float m_fReAttackGap;

		// Token: 0x040012DA RID: 4826
		public NKM_TEAM_TYPE m_AttackerTeamType;

		// Token: 0x040012DB RID: 4827
		public NKM_REACTOR_TYPE m_AttackerType;

		// Token: 0x040012DC RID: 4828
		public short m_AttackerGameUnitUID;

		// Token: 0x040012DD RID: 4829
		public short m_AttackerEffectUID;

		// Token: 0x040012DE RID: 4830
		public short m_DefenderUID;

		// Token: 0x040012DF RID: 4831
		public float m_AttackerPosX;

		// Token: 0x040012E0 RID: 4832
		public float m_AttackerPosZ;

		// Token: 0x040012E1 RID: 4833
		public float m_AttackerPosJumpY;

		// Token: 0x040012E2 RID: 4834
		public bool m_bAttackerRight;

		// Token: 0x040012E3 RID: 4835
		public bool m_bAttackerAwaken;

		// Token: 0x040012E4 RID: 4836
		public byte m_AttackerAddAttackUnitCount;

		// Token: 0x040012E5 RID: 4837
		public byte m_AtkBuffCount;

		// Token: 0x040012E6 RID: 4838
		public byte m_DefBuffCount;

		// Token: 0x040012E7 RID: 4839
		public NKMUnitSkillTemplet m_AttackerUnitSkillTemplet;
	}
}
