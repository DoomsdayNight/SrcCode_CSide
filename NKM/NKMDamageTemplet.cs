using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003D1 RID: 977
	public class NKMDamageTemplet
	{
		// Token: 0x060019C8 RID: 6600 RVA: 0x0006DFE4 File Offset: 0x0006C1E4
		public NKMDamageTemplet()
		{
			this.m_HitEffect = "AB_fx_hit_b_blue_small";
			this.m_HitEffectAir = "AB_fx_hit_b_blue_small";
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0006E170 File Offset: 0x0006C370
		public void Validate()
		{
			if (!string.IsNullOrEmpty(this.m_AttackerBuff) && NKMBuffManager.GetBuffTempletByStrID(this.m_AttackerBuff) == null)
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[NKMDamageTemplet] m_AttackerBuff is invalid. DamageTempletStrID [",
					this.m_DamageTempletBase.m_DamageTempletName,
					"], m_AttackerBuff [",
					this.m_AttackerBuff,
					"]"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 361);
			}
			if (!string.IsNullOrEmpty(this.m_DefenderBuff) && NKMBuffManager.GetBuffTempletByStrID(this.m_DefenderBuff) == null)
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[NKMDamageTemplet] m_DefenderBuff is invalid. DamageTempletStrID [",
					this.m_DamageTempletBase.m_DamageTempletName,
					"], m_DefenderBuff [",
					this.m_DefenderBuff,
					"]"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 369);
			}
			if (this.m_listAttackerHitBuff != null)
			{
				foreach (NKMHitBuff nkmhitBuff in this.m_listAttackerHitBuff)
				{
					if (!nkmhitBuff.Validate())
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[NKMDamageTemplet] m_listAttackerHitBuff is invalid. DamageTempletStrID [",
							this.m_DamageTempletBase.m_DamageTempletName,
							"], m_HitBuff [",
							nkmhitBuff.m_HitBuff,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 379);
					}
				}
			}
			if (this.m_listDefenderHitBuff != null)
			{
				foreach (NKMHitBuff nkmhitBuff2 in this.m_listDefenderHitBuff)
				{
					if (!nkmhitBuff2.Validate())
					{
						Log.ErrorAndExit(string.Concat(new string[]
						{
							"[NKMDamageTemplet] m_listDefenderHitBuff is invalid. DamageTempletStrID [",
							this.m_DamageTempletBase.m_DamageTempletName,
							"], m_HitBuff [",
							nkmhitBuff2.m_HitBuff,
							"]"
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 389);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.m_ExtraHitDamageTempletID) && this.m_ExtraHitDamageTemplet == null)
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"[NKMDamageTemplet] ",
					this.m_DamageTempletBase.m_DamageTempletName,
					" : m_ExtraHitDamageTempletID  ",
					this.m_ExtraHitDamageTempletID,
					" not found"
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageManager.cs", 399);
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0006E3D4 File Offset: 0x0006C5D4
		public void JoinExtraHitDT()
		{
			if (!string.IsNullOrEmpty(this.m_ExtraHitDamageTempletID))
			{
				this.m_ExtraHitDamageTemplet = NKMDamageManager.GetTempletByStrID(this.m_ExtraHitDamageTempletID);
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0006E3F4 File Offset: 0x0006C5F4
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			NKMDamageTemplet.<>c__DisplayClass68_0 CS$<>8__locals1;
			CS$<>8__locals1.cNKMLua = cNKMLua;
			this.luaDataloaded = true;
			CS$<>8__locals1.cNKMLua.GetData<NKM_REACT_TYPE>("m_ReActType", ref this.m_ReActType);
			CS$<>8__locals1.cNKMLua.GetData<NKM_SUPER_ARMOR_LEVEL>("m_CrashSuperArmorLevel", ref this.m_CrashSuperArmorLevel);
			CS$<>8__locals1.cNKMLua.GetData("m_bCanRevenge", ref this.m_bCanRevenge);
			CS$<>8__locals1.cNKMLua.GetData("m_BackSpeedKeepTimeX", ref this.m_BackSpeedKeepTimeX);
			CS$<>8__locals1.cNKMLua.GetData("m_BackSpeedKeepTimeZ", ref this.m_BackSpeedKeepTimeZ);
			CS$<>8__locals1.cNKMLua.GetData("m_BackSpeedKeepTimeJumpY", ref this.m_BackSpeedKeepTimeJumpY);
			CS$<>8__locals1.cNKMLua.GetData("m_BackSpeedX", ref this.m_BackSpeedX);
			CS$<>8__locals1.cNKMLua.GetData("m_BackSpeedZ", ref this.m_BackSpeedZ);
			CS$<>8__locals1.cNKMLua.GetData("m_BackSpeedJumpY", ref this.m_BackSpeedJumpY);
			CS$<>8__locals1.cNKMLua.GetData("m_ReAttackCount", ref this.m_ReAttackCount);
			CS$<>8__locals1.cNKMLua.GetData("m_fReAttackGap", ref this.m_fReAttackGap);
			CS$<>8__locals1.cNKMLua.GetData("m_fStopReserveTimeAtk", ref this.m_fStopReserveTimeAtk);
			CS$<>8__locals1.cNKMLua.GetData("m_fStopReserveTimeDef", ref this.m_fStopReserveTimeDef);
			CS$<>8__locals1.cNKMLua.GetData("m_fStopTimeAtk", ref this.m_fStopTimeAtk);
			CS$<>8__locals1.cNKMLua.GetData("m_fStopTimeDef", ref this.m_fStopTimeDef);
			CS$<>8__locals1.cNKMLua.GetData("m_fCameraCrashGap", ref this.m_fCameraCrashGap);
			CS$<>8__locals1.cNKMLua.GetData("m_fCameraCrashTime", ref this.m_fCameraCrashTime);
			CS$<>8__locals1.cNKMLua.GetData("m_bCrashBarrier", ref this.m_bCrashBarrier);
			CS$<>8__locals1.cNKMLua.GetData("m_fFeedbackBarrier", ref this.m_fFeedbackBarrier);
			CS$<>8__locals1.cNKMLua.GetData("m_fStunTime", ref this.m_fStunTime);
			this.m_StunIgnoreStyleType.Clear();
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_StunIgnoreStyleType"))
			{
				bool flag = true;
				int num = 1;
				NKM_UNIT_STYLE_TYPE item = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (flag)
				{
					flag = CS$<>8__locals1.cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num, ref item);
					if (flag)
					{
						this.m_StunIgnoreStyleType.Add(item);
					}
					num++;
				}
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			this.m_StunAllowStyleType.Clear();
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_StunAllowStyleType"))
			{
				bool flag2 = true;
				int num2 = 1;
				NKM_UNIT_STYLE_TYPE item2 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (flag2)
				{
					flag2 = CS$<>8__locals1.cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num2, ref item2);
					if (flag2)
					{
						this.m_StunAllowStyleType.Add(item2);
					}
					num2++;
				}
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			CS$<>8__locals1.cNKMLua.GetData("m_fCoolTimeDamage", ref this.m_fCoolTimeDamage);
			this.m_CoolTimeDamageIgnoreStyleType.Clear();
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_CoolTimeDamageIgnoreStyleType"))
			{
				bool flag3 = true;
				int num3 = 1;
				NKM_UNIT_STYLE_TYPE item3 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (flag3)
				{
					flag3 = CS$<>8__locals1.cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num3, ref item3);
					if (flag3)
					{
						this.m_CoolTimeDamageIgnoreStyleType.Add(item3);
					}
					num3++;
				}
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			this.m_CoolTimeDamageAllowStyleType.Clear();
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_CoolTimeDamageAllowStyleType"))
			{
				bool flag4 = true;
				int num4 = 1;
				NKM_UNIT_STYLE_TYPE item4 = NKM_UNIT_STYLE_TYPE.NUST_INVALID;
				while (flag4)
				{
					flag4 = CS$<>8__locals1.cNKMLua.GetData<NKM_UNIT_STYLE_TYPE>(num4, ref item4);
					if (flag4)
					{
						this.m_CoolTimeDamageAllowStyleType.Add(item4);
					}
					num4++;
				}
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			CS$<>8__locals1.cNKMLua.GetDataEnum<NKM_UNIT_STATUS_EFFECT>("m_ApplyStatusEffect", out this.m_ApplyStatusEffect);
			CS$<>8__locals1.cNKMLua.GetData("m_fApplyStatusTime", ref this.m_fApplyStatusTime);
			CS$<>8__locals1.cNKMLua.GetDataListEnum<NKM_UNIT_STYLE_TYPE>("m_StatusIgnoreStyleType", this.m_StatusIgnoreStyleType, true);
			CS$<>8__locals1.cNKMLua.GetDataListEnum<NKM_UNIT_STYLE_TYPE>("m_StatusAllowStyleType", this.m_StatusAllowStyleType, true);
			CS$<>8__locals1.cNKMLua.GetData("m_fInstantKillHPRate", ref this.m_fInstantKillHPRate);
			CS$<>8__locals1.cNKMLua.GetData("m_fInstantKillAwaken", ref this.m_fInstantKillAwaken);
			CS$<>8__locals1.cNKMLua.GetData("m_HitSoundName", ref this.m_HitSoundName);
			CS$<>8__locals1.cNKMLua.GetData("m_fLocalVol", ref this.m_fLocalVol);
			CS$<>8__locals1.cNKMLua.GetData("m_HitEffect", ref this.m_HitEffect);
			CS$<>8__locals1.cNKMLua.GetData("m_HitEffectAnimName", ref this.m_HitEffectAnimName);
			CS$<>8__locals1.cNKMLua.GetData("m_fHitEffectRange", ref this.m_fHitEffectRange);
			CS$<>8__locals1.cNKMLua.GetData("m_fHitEffectOffsetZ", ref this.m_fHitEffectOffsetZ);
			CS$<>8__locals1.cNKMLua.GetData("m_bHitEffectLand", ref this.m_bHitEffectLand);
			CS$<>8__locals1.cNKMLua.GetData("m_HitEffectAir", ref this.m_HitEffectAir);
			CS$<>8__locals1.cNKMLua.GetData("m_HitEffectAirAnimName", ref this.m_HitEffectAirAnimName);
			CS$<>8__locals1.cNKMLua.GetData("m_fHitEffectAirRange", ref this.m_fHitEffectAirRange);
			CS$<>8__locals1.cNKMLua.GetData("m_fHitEffectAirOffsetZ", ref this.m_fHitEffectAirOffsetZ);
			CS$<>8__locals1.cNKMLua.GetData("m_AttackerStateChange", ref this.m_AttackerStateChange);
			NKMDamageTemplet.<LoadFromLUA>g__LowerCompability|68_0("m_AttackerBuffBaseLevel", ref this.m_AttackerBuffStatBaseLevel, ref this.m_AttackerBuffTimeBaseLevel, ref CS$<>8__locals1);
			NKMDamageTemplet.<LoadFromLUA>g__LowerCompability|68_0("m_AttackerBuffAddLVBySkillLV", ref this.m_AttackerBuffStatAddLVBySkillLV, ref this.m_AttackerBuffTimeAddLVBySkillLV, ref CS$<>8__locals1);
			CS$<>8__locals1.cNKMLua.GetData("m_AttackerBuff", ref this.m_AttackerBuff);
			CS$<>8__locals1.cNKMLua.GetData("m_AttackerBuffStatBaseLevel", ref this.m_AttackerBuffStatBaseLevel);
			CS$<>8__locals1.cNKMLua.GetData("m_AttackerBuffStatAddLVBySkillLV", ref this.m_AttackerBuffStatAddLVBySkillLV);
			CS$<>8__locals1.cNKMLua.GetData("m_AttackerBuffTimeBaseLevel", ref this.m_AttackerBuffTimeBaseLevel);
			CS$<>8__locals1.cNKMLua.GetData("m_AttackerBuffTimeAddLVBySkillLV", ref this.m_AttackerBuffTimeAddLVBySkillLV);
			NKMDamageTemplet.<LoadFromLUA>g__LowerCompability|68_0("m_DefenderBuffBaseLevel", ref this.m_DefenderBuffStatBaseLevel, ref this.m_DefenderBuffTimeBaseLevel, ref CS$<>8__locals1);
			NKMDamageTemplet.<LoadFromLUA>g__LowerCompability|68_0("m_DefenderBuffAddLVBySkillLV", ref this.m_DefenderBuffStatAddLVBySkillLV, ref this.m_DefenderBuffTimeAddLVBySkillLV, ref CS$<>8__locals1);
			CS$<>8__locals1.cNKMLua.GetData("m_DefenderBuff", ref this.m_DefenderBuff);
			CS$<>8__locals1.cNKMLua.GetData("m_DefenderBuffStatBaseLevel", ref this.m_DefenderBuffStatBaseLevel);
			CS$<>8__locals1.cNKMLua.GetData("m_DefenderBuffStatAddLVBySkillLV", ref this.m_DefenderBuffStatAddLVBySkillLV);
			CS$<>8__locals1.cNKMLua.GetData("m_DefenderBuffTimeBaseLevel", ref this.m_DefenderBuffTimeBaseLevel);
			CS$<>8__locals1.cNKMLua.GetData("m_DefenderBuffTimeAddLVBySkillLV", ref this.m_DefenderBuffTimeAddLVBySkillLV);
			CS$<>8__locals1.cNKMLua.GetData("m_DeleteBuffCount", ref this.m_DeleteBuffCount);
			CS$<>8__locals1.cNKMLua.GetData("m_DeleteConfuseBuff", ref this.m_DeleteConfuseBuff);
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_NKMKillFeedBack"))
			{
				this.m_NKMKillFeedBack.LoadFromLUA(CS$<>8__locals1.cNKMLua);
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_NKMEventMove"))
			{
				this.m_EventMove = new NKMEventMove();
				this.m_EventMove.LoadFromLUA(CS$<>8__locals1.cNKMLua);
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_listAttackerHitBuff"))
			{
				int num5 = 1;
				while (CS$<>8__locals1.cNKMLua.OpenTable(num5))
				{
					NKMHitBuff nkmhitBuff;
					if (this.m_listAttackerHitBuff.Count >= num5)
					{
						nkmhitBuff = this.m_listAttackerHitBuff[num5 - 1];
					}
					else
					{
						nkmhitBuff = new NKMHitBuff();
						this.m_listAttackerHitBuff.Add(nkmhitBuff);
					}
					nkmhitBuff.LoadFromLUA(CS$<>8__locals1.cNKMLua);
					CS$<>8__locals1.cNKMLua.CloseTable();
					num5++;
				}
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			if (CS$<>8__locals1.cNKMLua.OpenTable("m_listDefenderHitBuff"))
			{
				int num6 = 1;
				while (CS$<>8__locals1.cNKMLua.OpenTable(num6))
				{
					NKMHitBuff nkmhitBuff2;
					if (this.m_listDefenderHitBuff.Count >= num6)
					{
						nkmhitBuff2 = this.m_listDefenderHitBuff[num6 - 1];
					}
					else
					{
						nkmhitBuff2 = new NKMHitBuff();
						this.m_listDefenderHitBuff.Add(nkmhitBuff2);
					}
					nkmhitBuff2.LoadFromLUA(CS$<>8__locals1.cNKMLua);
					CS$<>8__locals1.cNKMLua.CloseTable();
					num6++;
				}
				CS$<>8__locals1.cNKMLua.CloseTable();
			}
			CS$<>8__locals1.cNKMLua.GetData("m_ExtraHitDamageTempletID", ref this.m_ExtraHitDamageTempletID);
			this.m_ExtraHitCountRange.LoadFromLua(CS$<>8__locals1.cNKMLua, "m_ExtraHitCountRange");
			this.m_ExtraHitAttribute = NKMDamageAttribute.LoadFromLua(CS$<>8__locals1.cNKMLua, "m_ExtraHitAttribute");
			return true;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006EC47 File Offset: 0x0006CE47
		public bool CanApplyStun(NKMUnitTempletBase defenderUnitTempletBase)
		{
			return this.m_fStunTime > 0f && this.CanApply(defenderUnitTempletBase, this.m_StunAllowStyleType, this.m_StunIgnoreStyleType);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006EC6B File Offset: 0x0006CE6B
		public bool CanApplyCooltimeDamage(NKMUnitTempletBase defenderUnitTempletBase)
		{
			return this.m_fCoolTimeDamage > 0f && this.CanApply(defenderUnitTempletBase, this.m_CoolTimeDamageAllowStyleType, this.m_CoolTimeDamageIgnoreStyleType);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0006EC8F File Offset: 0x0006CE8F
		public bool CanApplyStatus(NKMUnitTempletBase targetUnit)
		{
			return this.m_ApplyStatusEffect != NKM_UNIT_STATUS_EFFECT.NUSE_NONE && this.m_fApplyStatusTime > 0f && this.CanApply(targetUnit, this.m_StatusAllowStyleType, this.m_StatusIgnoreStyleType);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0006ECBD File Offset: 0x0006CEBD
		private bool CanApply(NKMUnitTempletBase targetUnit, HashSet<NKM_UNIT_STYLE_TYPE> hsAllowStyle, HashSet<NKM_UNIT_STYLE_TYPE> hsIgnoreStyle)
		{
			return targetUnit == null || targetUnit.IsAllowUnitStyleType(hsAllowStyle, hsIgnoreStyle);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0006ECD4 File Offset: 0x0006CED4
		[CompilerGenerated]
		internal static void <LoadFromLUA>g__LowerCompability|68_0(string oldName, ref byte statLevel, ref byte timeLevel, ref NKMDamageTemplet.<>c__DisplayClass68_0 A_3)
		{
			byte b = 0;
			if (A_3.cNKMLua.GetData(oldName, ref b))
			{
				statLevel = b;
				timeLevel = b;
			}
		}

		// Token: 0x0400128C RID: 4748
		public NKMDamageTempletBase m_DamageTempletBase = new NKMDamageTempletBase();

		// Token: 0x0400128D RID: 4749
		public bool luaDataloaded;

		// Token: 0x0400128E RID: 4750
		public NKM_REACT_TYPE m_ReActType = NKM_REACT_TYPE.NRT_NO_ACTION;

		// Token: 0x0400128F RID: 4751
		public NKM_SUPER_ARMOR_LEVEL m_CrashSuperArmorLevel = NKM_SUPER_ARMOR_LEVEL.NSAL_NO;

		// Token: 0x04001290 RID: 4752
		public bool m_bCanRevenge;

		// Token: 0x04001291 RID: 4753
		public float m_BackSpeedKeepTimeX;

		// Token: 0x04001292 RID: 4754
		public float m_BackSpeedKeepTimeZ;

		// Token: 0x04001293 RID: 4755
		public float m_BackSpeedKeepTimeJumpY;

		// Token: 0x04001294 RID: 4756
		public float m_BackSpeedX = -1f;

		// Token: 0x04001295 RID: 4757
		public float m_BackSpeedZ = -1f;

		// Token: 0x04001296 RID: 4758
		public float m_BackSpeedJumpY = -1f;

		// Token: 0x04001297 RID: 4759
		public int m_ReAttackCount = 1;

		// Token: 0x04001298 RID: 4760
		public float m_fReAttackGap;

		// Token: 0x04001299 RID: 4761
		public float m_fStopReserveTimeAtk;

		// Token: 0x0400129A RID: 4762
		public float m_fStopReserveTimeDef;

		// Token: 0x0400129B RID: 4763
		public float m_fStopTimeAtk;

		// Token: 0x0400129C RID: 4764
		public float m_fStopTimeDef;

		// Token: 0x0400129D RID: 4765
		public float m_fCameraCrashGap;

		// Token: 0x0400129E RID: 4766
		public float m_fCameraCrashTime;

		// Token: 0x0400129F RID: 4767
		public bool m_bCrashBarrier;

		// Token: 0x040012A0 RID: 4768
		public float m_fFeedbackBarrier;

		// Token: 0x040012A1 RID: 4769
		public float m_fStunTime;

		// Token: 0x040012A2 RID: 4770
		public HashSet<NKM_UNIT_STYLE_TYPE> m_StunIgnoreStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040012A3 RID: 4771
		public HashSet<NKM_UNIT_STYLE_TYPE> m_StunAllowStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040012A4 RID: 4772
		public float m_fCoolTimeDamage;

		// Token: 0x040012A5 RID: 4773
		public HashSet<NKM_UNIT_STYLE_TYPE> m_CoolTimeDamageIgnoreStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040012A6 RID: 4774
		public HashSet<NKM_UNIT_STYLE_TYPE> m_CoolTimeDamageAllowStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040012A7 RID: 4775
		public NKM_UNIT_STATUS_EFFECT m_ApplyStatusEffect;

		// Token: 0x040012A8 RID: 4776
		public float m_fApplyStatusTime;

		// Token: 0x040012A9 RID: 4777
		public HashSet<NKM_UNIT_STYLE_TYPE> m_StatusIgnoreStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040012AA RID: 4778
		public HashSet<NKM_UNIT_STYLE_TYPE> m_StatusAllowStyleType = new HashSet<NKM_UNIT_STYLE_TYPE>();

		// Token: 0x040012AB RID: 4779
		public float m_fInstantKillHPRate;

		// Token: 0x040012AC RID: 4780
		public bool m_fInstantKillAwaken = true;

		// Token: 0x040012AD RID: 4781
		public string m_HitSoundName = "";

		// Token: 0x040012AE RID: 4782
		public float m_fLocalVol = 1f;

		// Token: 0x040012AF RID: 4783
		public string m_HitEffect = "";

		// Token: 0x040012B0 RID: 4784
		public string m_HitEffectAnimName = "BASE";

		// Token: 0x040012B1 RID: 4785
		public float m_fHitEffectRange = 50f;

		// Token: 0x040012B2 RID: 4786
		public float m_fHitEffectOffsetZ = 50f;

		// Token: 0x040012B3 RID: 4787
		public bool m_bHitEffectLand;

		// Token: 0x040012B4 RID: 4788
		public string m_HitEffectAir = "";

		// Token: 0x040012B5 RID: 4789
		public string m_HitEffectAirAnimName = "BASE";

		// Token: 0x040012B6 RID: 4790
		public float m_fHitEffectAirRange = 50f;

		// Token: 0x040012B7 RID: 4791
		public float m_fHitEffectAirOffsetZ;

		// Token: 0x040012B8 RID: 4792
		public string m_AttackerStateChange = "";

		// Token: 0x040012B9 RID: 4793
		public string m_AttackerBuff = "";

		// Token: 0x040012BA RID: 4794
		public byte m_AttackerBuffStatBaseLevel = 1;

		// Token: 0x040012BB RID: 4795
		public byte m_AttackerBuffStatAddLVBySkillLV;

		// Token: 0x040012BC RID: 4796
		public byte m_AttackerBuffTimeBaseLevel = 1;

		// Token: 0x040012BD RID: 4797
		public byte m_AttackerBuffTimeAddLVBySkillLV;

		// Token: 0x040012BE RID: 4798
		public string m_DefenderBuff = "";

		// Token: 0x040012BF RID: 4799
		public byte m_DefenderBuffStatBaseLevel = 1;

		// Token: 0x040012C0 RID: 4800
		public byte m_DefenderBuffStatAddLVBySkillLV;

		// Token: 0x040012C1 RID: 4801
		public byte m_DefenderBuffTimeBaseLevel = 1;

		// Token: 0x040012C2 RID: 4802
		public byte m_DefenderBuffTimeAddLVBySkillLV;

		// Token: 0x040012C3 RID: 4803
		public byte m_DeleteBuffCount;

		// Token: 0x040012C4 RID: 4804
		public bool m_DeleteConfuseBuff;

		// Token: 0x040012C5 RID: 4805
		public List<NKMHitBuff> m_listAttackerHitBuff = new List<NKMHitBuff>();

		// Token: 0x040012C6 RID: 4806
		public List<NKMHitBuff> m_listDefenderHitBuff = new List<NKMHitBuff>();

		// Token: 0x040012C7 RID: 4807
		public NKMKillFeedBack m_NKMKillFeedBack = new NKMKillFeedBack();

		// Token: 0x040012C8 RID: 4808
		public NKMEventMove m_EventMove;

		// Token: 0x040012C9 RID: 4809
		public string m_ExtraHitDamageTempletID = "";

		// Token: 0x040012CA RID: 4810
		public NKMDamageTemplet m_ExtraHitDamageTemplet;

		// Token: 0x040012CB RID: 4811
		public NKMMinMaxInt m_ExtraHitCountRange = new NKMMinMaxInt(1, 1);

		// Token: 0x040012CC RID: 4812
		public NKMDamageAttribute m_ExtraHitAttribute;
	}
}
