using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003CA RID: 970
	public class NKMDamageEffectTemplet
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x0006CFD8 File Offset: 0x0006B1D8
		public bool LoadFromLUA(NKMLua cNKMLua)
		{
			cNKMLua.GetData("m_BASE_ID", ref this.m_BASE_ID);
			cNKMLua.GetData("m_DamageEffectID", ref this.m_DamageEffectID);
			cNKMLua.GetData("m_MainEffectName", ref this.m_MainEffectName);
			cNKMLua.GetData("m_bSpine", ref this.m_bSpine);
			cNKMLua.GetData("m_fScaleFactor", ref this.m_fScaleFactor);
			cNKMLua.GetData("m_bUseTargetBuffScaleFactor", ref this.m_bUseTargetBuffScaleFactor);
			cNKMLua.GetData("m_DamageCountMax", ref this.m_DamageCountMax);
			cNKMLua.GetData("m_bDamageCountMaxDie", ref this.m_bDamageCountMaxDie);
			cNKMLua.GetData("m_fTargetDistDie", ref this.m_fTargetDistDie);
			cNKMLua.GetData("m_bTargetDistDieOnlyTargetDie", ref this.m_bTargetDistDieOnlyTargetDie);
			cNKMLua.GetData("m_fFindTargetTime", ref this.m_fFindTargetTime);
			cNKMLua.GetData("m_fSeeRange", ref this.m_fSeeRange);
			cNKMLua.GetData<NKM_FIND_TARGET_TYPE>("m_NKM_FIND_TARGET_TYPE", ref this.m_NKM_FIND_TARGET_TYPE);
			cNKMLua.GetDataListEnum<NKM_UNIT_ROLE_TYPE>("m_hsFindTargetRolePriority", this.m_hsFindTargetRolePriority, true);
			cNKMLua.GetData("m_bTargetNoChange", ref this.m_bTargetNoChange);
			cNKMLua.GetData("m_bNoBackTarget", ref this.m_bNoBackTarget);
			cNKMLua.GetData("m_fSeeTargetTime", ref this.m_fSeeTargetTime);
			cNKMLua.GetData("m_bSeeTarget", ref this.m_bSeeTarget);
			cNKMLua.GetData("m_bSeeTargetSpeed", ref this.m_bSeeTargetSpeed);
			cNKMLua.GetData("m_bUseTargetDir", ref this.m_bUseTargetDir);
			cNKMLua.GetData("m_fTargetDirSpeed", ref this.m_fTargetDirSpeed);
			cNKMLua.GetData("m_bLookDir", ref this.m_bLookDir);
			cNKMLua.GetData("m_bNoMove", ref this.m_bNoMove);
			cNKMLua.GetData("m_bLandStruck", ref this.m_bLandStruck);
			cNKMLua.GetData("m_bLandBind", ref this.m_bLandBind);
			cNKMLua.GetData("m_bLandEdge", ref this.m_bLandEdge);
			cNKMLua.GetData("m_bLandConnect", ref this.m_bLandConnect);
			cNKMLua.GetData("m_fEffectSize", ref this.m_fEffectSize);
			cNKMLua.GetData("m_bDamageSpeedDependMaster", ref this.m_bDamageSpeedDependMaster);
			cNKMLua.GetData("m_fReloadAccel", ref this.m_fReloadAccel);
			cNKMLua.GetData("m_fGAccel", ref this.m_fGAccel);
			cNKMLua.GetData("m_fMaxGSpeed", ref this.m_fMaxGSpeed);
			cNKMLua.GetData<NKC_TEAM_COLOR_TYPE>("m_NKC_TEAM_COLOR_TYPE", ref this.m_NKC_TEAM_COLOR_TYPE);
			cNKMLua.GetData("m_fShadowScaleX", ref this.m_fShadowScaleX);
			cNKMLua.GetData("m_fShadowScaleY", ref this.m_fShadowScaleY);
			cNKMLua.GetData("m_fShadowRotateX", ref this.m_fShadowRotateX);
			cNKMLua.GetData("m_fShadowRotateZ", ref this.m_fShadowRotateZ);
			cNKMLua.GetData("m_CanIgnoreStopTime", ref this.m_CanIgnoreStopTime);
			cNKMLua.GetData("m_UseMasterAnimSpeed", ref this.m_UseMasterAnimSpeed);
			NKMUnitState.LoadAndMergeEventList<NKMEventAttack>(cNKMLua, "m_listNKMDieEventAttack", ref this.m_listNKMDieEventAttack, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventEffect>(cNKMLua, "m_listNKMDieEventEffect", ref this.m_listNKMDieEventEffect, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventDamageEffect>(cNKMLua, "m_listNKMDieEventDamageEffect", ref this.m_listNKMDieEventDamageEffect, null);
			NKMUnitState.LoadAndMergeEventList<NKMEventSound>(cNKMLua, "m_listNKMDieEventSound", ref this.m_listNKMDieEventSound, null);
			if (cNKMLua.OpenTable("m_dicNKMState"))
			{
				int num = 1;
				while (cNKMLua.OpenTable(num))
				{
					string key = "";
					cNKMLua.GetData("m_StateName", ref key);
					NKMDamageEffectState nkmdamageEffectState;
					if (this.m_dicNKMState.ContainsKey(key))
					{
						nkmdamageEffectState = this.m_dicNKMState[key];
					}
					else
					{
						nkmdamageEffectState = new NKMDamageEffectState();
					}
					nkmdamageEffectState.LoadFromLUA(cNKMLua);
					if (!this.m_dicNKMState.ContainsKey(nkmdamageEffectState.m_StateName))
					{
						this.m_dicNKMState.Add(nkmdamageEffectState.m_StateName, nkmdamageEffectState);
					}
					num++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			if (cNKMLua.OpenTable("m_dicSkinEffect"))
			{
				if (this.m_dicSkinMainEffect == null)
				{
					this.m_dicSkinMainEffect = new Dictionary<int, string>();
				}
				int num2 = 1;
				while (cNKMLua.OpenTable(num2))
				{
					int key2 = -1;
					string value = "AB_FX_DUMMY";
					cNKMLua.GetData("m_SkinID", ref key2);
					cNKMLua.GetData("m_MainEffectName", ref value);
					this.m_dicSkinMainEffect.Add(key2, value);
					num2++;
					cNKMLua.CloseTable();
				}
				cNKMLua.CloseTable();
			}
			foreach (NKMDamageEffectState nkmdamageEffectState2 in this.m_dicNKMState.Values)
			{
				foreach (NKMEventMove nkmeventMove in nkmdamageEffectState2.m_listNKMEventMove)
				{
					if (nkmeventMove.m_MoveBase == NKMEventMove.MoveBase.ME && (nkmeventMove.m_MoveOffset == NKMEventMove.MoveOffset.ME || nkmeventMove.m_MoveOffset == NKMEventMove.MoveOffset.ME_INV))
					{
						Log.Error(this.m_DamageEffectID + " : " + nkmdamageEffectState2.m_StateName + " Base and offset is same", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffectTemplet.cs", 336);
					}
					if (nkmeventMove.m_MoveBase == NKMEventMove.MoveBase.TARGET_UNIT && (nkmeventMove.m_MoveOffset == NKMEventMove.MoveOffset.TARGET_UNIT || nkmeventMove.m_MoveOffset == NKMEventMove.MoveOffset.TARGET_UNIT_INV))
					{
						Log.Error(this.m_DamageEffectID + " : " + nkmdamageEffectState2.m_StateName + " Base and offset is same", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDamageEffectTemplet.cs", 345);
					}
				}
			}
			return true;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0006D518 File Offset: 0x0006B718
		public void DeepCopyFromSource(NKMDamageEffectTemplet source)
		{
			this.m_MainEffectName = source.m_MainEffectName;
			this.m_bSpine = source.m_bSpine;
			this.m_fScaleFactor = source.m_fScaleFactor;
			this.m_bUseTargetBuffScaleFactor = source.m_bUseTargetBuffScaleFactor;
			this.m_DamageCountMax = source.m_DamageCountMax;
			this.m_bDamageCountMaxDie = source.m_bDamageCountMaxDie;
			this.m_fTargetDistDie = source.m_fTargetDistDie;
			this.m_bTargetDistDieOnlyTargetDie = source.m_bTargetDistDieOnlyTargetDie;
			this.m_fFindTargetTime = source.m_fFindTargetTime;
			this.m_fSeeRange = source.m_fSeeRange;
			this.m_NKM_FIND_TARGET_TYPE = source.m_NKM_FIND_TARGET_TYPE;
			this.m_hsFindTargetRolePriority.Clear();
			this.m_hsFindTargetRolePriority.UnionWith(source.m_hsFindTargetRolePriority);
			this.m_bTargetNoChange = source.m_bTargetNoChange;
			this.m_bNoBackTarget = source.m_bNoBackTarget;
			this.m_fSeeTargetTime = source.m_fSeeTargetTime;
			this.m_bSeeTarget = source.m_bSeeTarget;
			this.m_bSeeTargetSpeed = source.m_bSeeTargetSpeed;
			this.m_bUseTargetDir = source.m_bUseTargetDir;
			this.m_fTargetDirSpeed = source.m_fTargetDirSpeed;
			this.m_bLookDir = source.m_bLookDir;
			this.m_fEffectSize = source.m_fEffectSize;
			this.m_bNoMove = source.m_bNoMove;
			this.m_bLandStruck = source.m_bLandStruck;
			this.m_bLandBind = source.m_bLandBind;
			this.m_bLandEdge = source.m_bLandEdge;
			this.m_bLandConnect = source.m_bLandConnect;
			this.m_bDamageSpeedDependMaster = source.m_bDamageSpeedDependMaster;
			this.m_fReloadAccel = source.m_fReloadAccel;
			this.m_fGAccel = source.m_fGAccel;
			this.m_fMaxGSpeed = source.m_fMaxGSpeed;
			this.m_NKC_TEAM_COLOR_TYPE = source.m_NKC_TEAM_COLOR_TYPE;
			this.m_fShadowScaleX = source.m_fShadowScaleX;
			this.m_fShadowScaleY = source.m_fShadowScaleY;
			this.m_fShadowRotateX = source.m_fShadowRotateX;
			this.m_fShadowRotateZ = source.m_fShadowRotateZ;
			this.m_CanIgnoreStopTime = source.m_CanIgnoreStopTime;
			this.m_UseMasterAnimSpeed = source.m_UseMasterAnimSpeed;
			NKMUnitState.DeepCopy<NKMEventAttack>(source.m_listNKMDieEventAttack, ref this.m_listNKMDieEventAttack, delegate(NKMEventAttack t, NKMEventAttack s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventEffect>(source.m_listNKMDieEventEffect, ref this.m_listNKMDieEventEffect, delegate(NKMEventEffect t, NKMEventEffect s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventDamageEffect>(source.m_listNKMDieEventDamageEffect, ref this.m_listNKMDieEventDamageEffect, delegate(NKMEventDamageEffect t, NKMEventDamageEffect s)
			{
				t.DeepCopyFromSource(s);
			});
			NKMUnitState.DeepCopy<NKMEventSound>(source.m_listNKMDieEventSound, ref this.m_listNKMDieEventSound, delegate(NKMEventSound t, NKMEventSound s)
			{
				t.DeepCopyFromSource(s);
			});
			foreach (KeyValuePair<string, NKMDamageEffectState> keyValuePair in source.m_dicNKMState)
			{
				NKMDamageEffectState nkmdamageEffectState;
				if (!this.m_dicNKMState.ContainsKey(keyValuePair.Key))
				{
					nkmdamageEffectState = new NKMDamageEffectState();
					this.m_dicNKMState.Add(keyValuePair.Key, nkmdamageEffectState);
				}
				else
				{
					nkmdamageEffectState = this.m_dicNKMState[keyValuePair.Key];
				}
				nkmdamageEffectState.DeepCopyFromSource(keyValuePair.Value);
			}
			if (source.m_dicSkinMainEffect == null)
			{
				this.m_dicSkinMainEffect = null;
				return;
			}
			this.m_dicSkinMainEffect = new Dictionary<int, string>();
			foreach (KeyValuePair<int, string> keyValuePair2 in source.m_dicSkinMainEffect)
			{
				this.m_dicSkinMainEffect.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0006D8B4 File Offset: 0x0006BAB4
		public NKMDamageEffectState GetState(string stateName)
		{
			if (this.m_dicNKMState.ContainsKey(stateName))
			{
				return this.m_dicNKMState[stateName];
			}
			return null;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0006D8D4 File Offset: 0x0006BAD4
		public string GetMainEffectName(int skinID)
		{
			if (skinID < 0)
			{
				return this.m_MainEffectName;
			}
			if (this.m_dicSkinMainEffect == null)
			{
				return this.m_MainEffectName;
			}
			string result;
			if (this.m_dicSkinMainEffect.TryGetValue(skinID, out result))
			{
				return result;
			}
			return this.m_MainEffectName;
		}

		// Token: 0x04001236 RID: 4662
		public string m_BASE_ID = "";

		// Token: 0x04001237 RID: 4663
		public string m_DamageEffectID = "";

		// Token: 0x04001238 RID: 4664
		public string m_MainEffectName = "";

		// Token: 0x04001239 RID: 4665
		public bool m_bSpine;

		// Token: 0x0400123A RID: 4666
		public float m_fScaleFactor = 1f;

		// Token: 0x0400123B RID: 4667
		public bool m_bUseTargetBuffScaleFactor;

		// Token: 0x0400123C RID: 4668
		public int m_DamageCountMax;

		// Token: 0x0400123D RID: 4669
		public bool m_bDamageCountMaxDie;

		// Token: 0x0400123E RID: 4670
		public float m_fTargetDistDie;

		// Token: 0x0400123F RID: 4671
		public bool m_bTargetDistDieOnlyTargetDie = true;

		// Token: 0x04001240 RID: 4672
		public float m_fFindTargetTime;

		// Token: 0x04001241 RID: 4673
		public float m_fSeeRange;

		// Token: 0x04001242 RID: 4674
		public NKM_FIND_TARGET_TYPE m_NKM_FIND_TARGET_TYPE;

		// Token: 0x04001243 RID: 4675
		public HashSet<NKM_UNIT_ROLE_TYPE> m_hsFindTargetRolePriority = new HashSet<NKM_UNIT_ROLE_TYPE>();

		// Token: 0x04001244 RID: 4676
		public bool m_bTargetNoChange = true;

		// Token: 0x04001245 RID: 4677
		public bool m_bNoBackTarget;

		// Token: 0x04001246 RID: 4678
		public float m_fSeeTargetTime = 0.5f;

		// Token: 0x04001247 RID: 4679
		public bool m_bSeeTarget = true;

		// Token: 0x04001248 RID: 4680
		public bool m_bSeeTargetSpeed = true;

		// Token: 0x04001249 RID: 4681
		public bool m_bUseTargetDir;

		// Token: 0x0400124A RID: 4682
		public float m_fTargetDirSpeed;

		// Token: 0x0400124B RID: 4683
		public bool m_bLookDir;

		// Token: 0x0400124C RID: 4684
		public float m_fEffectSize;

		// Token: 0x0400124D RID: 4685
		public bool m_bNoMove;

		// Token: 0x0400124E RID: 4686
		public bool m_bLandStruck;

		// Token: 0x0400124F RID: 4687
		public bool m_bLandBind;

		// Token: 0x04001250 RID: 4688
		public bool m_bLandEdge;

		// Token: 0x04001251 RID: 4689
		public bool m_bLandConnect;

		// Token: 0x04001252 RID: 4690
		public bool m_bDamageSpeedDependMaster;

		// Token: 0x04001253 RID: 4691
		public float m_fReloadAccel;

		// Token: 0x04001254 RID: 4692
		public float m_fGAccel;

		// Token: 0x04001255 RID: 4693
		public float m_fMaxGSpeed = -3000f;

		// Token: 0x04001256 RID: 4694
		public NKC_TEAM_COLOR_TYPE m_NKC_TEAM_COLOR_TYPE;

		// Token: 0x04001257 RID: 4695
		public float m_fShadowScaleX;

		// Token: 0x04001258 RID: 4696
		public float m_fShadowScaleY;

		// Token: 0x04001259 RID: 4697
		public float m_fShadowRotateX;

		// Token: 0x0400125A RID: 4698
		public float m_fShadowRotateZ;

		// Token: 0x0400125B RID: 4699
		public bool m_CanIgnoreStopTime;

		// Token: 0x0400125C RID: 4700
		public bool m_UseMasterAnimSpeed;

		// Token: 0x0400125D RID: 4701
		public List<NKMEventAttack> m_listNKMDieEventAttack = new List<NKMEventAttack>();

		// Token: 0x0400125E RID: 4702
		public List<NKMEventEffect> m_listNKMDieEventEffect = new List<NKMEventEffect>();

		// Token: 0x0400125F RID: 4703
		public List<NKMEventDamageEffect> m_listNKMDieEventDamageEffect = new List<NKMEventDamageEffect>();

		// Token: 0x04001260 RID: 4704
		public List<NKMEventSound> m_listNKMDieEventSound = new List<NKMEventSound>();

		// Token: 0x04001261 RID: 4705
		public Dictionary<string, NKMDamageEffectState> m_dicNKMState = new Dictionary<string, NKMDamageEffectState>();

		// Token: 0x04001262 RID: 4706
		public Dictionary<int, string> m_dicSkinMainEffect;
	}
}
