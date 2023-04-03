using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cs.Logging;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000668 RID: 1640
	public static class NKCDebugUtil
	{
		// Token: 0x06003394 RID: 13204 RVA: 0x00103C55 File Offset: 0x00101E55
		public static void DebugDrawRect(Rect rect, Vector2 vOffset, Vector2 vEnlarge, Color color)
		{
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x00103C57 File Offset: 0x00101E57
		public static void DebugDrawCircle(Transform rect, Vector3 center, float radius, Color color, int segments = 32)
		{
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x00103C59 File Offset: 0x00101E59
		public static string GetObjectPath(GameObject go, bool StopAtClone = true)
		{
			if (go == null)
			{
				return "null";
			}
			return NKCDebugUtil.GetObjectPath(go.transform, StopAtClone);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x00103C78 File Offset: 0x00101E78
		public static string GetObjectPath(Transform t, bool StopAtClone = true)
		{
			if (t == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(t.name);
			Transform parent = t.parent;
			while (parent != null)
			{
				stringBuilder.Insert(0, parent.name + "/");
				if (StopAtClone && parent.name.EndsWith("(Clone)"))
				{
					break;
				}
				parent = parent.parent;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x00103CF4 File Offset: 0x00101EF4
		public static string ToDebugString<T>(IEnumerable<T> target)
		{
			if (target == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(target.GetType().ToString());
			stringBuilder.Append("[");
			bool flag = false;
			foreach (T t in target)
			{
				stringBuilder.Append(t.ToString());
				stringBuilder.Append(", ");
				flag = true;
			}
			if (flag)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x00103DAC File Offset: 0x00101FAC
		public static void LogBehaivorTree(BehaviorTree behaivor)
		{
			if (behaivor == null)
			{
				return;
			}
			Cs.Logging.Log.Info("Behaivor name : " + behaivor.BehaviorName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 118);
			if (behaivor.GetAllVariables() == null)
			{
				Cs.Logging.Log.Info("Variables not initialized, or have no variables", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 122);
			}
			else
			{
				Cs.Logging.Log.Info("Behaivor Variables", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 126);
				foreach (SharedVariable sharedVariable in behaivor.GetAllVariables())
				{
					if (sharedVariable == null)
					{
						Cs.Logging.Log.Error("null variable found!!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 131);
					}
					else
					{
						string text = sharedVariable.GetType().ToString();
						Cs.Logging.Log.Info(string.Concat(new string[]
						{
							"Variable ",
							sharedVariable.Name,
							"(",
							text,
							") : ",
							sharedVariable.ToString()
						}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 135);
					}
				}
			}
			Cs.Logging.Log.Info("Behaivor nodes", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 139);
			foreach (Task task in behaivor.FindTasks<Task>())
			{
				string text2 = task.GetType().ToString();
				if (text2.Contains("UnknownTask"))
				{
					Cs.Logging.Log.Error(string.Format("Task ID : {0}, Type : {1}", task.ID, text2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 146);
				}
				else
				{
					Cs.Logging.Log.Info(string.Format("Task ID : {0}, Type : {1}", task.ID, text2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCDebugUtil.cs", 150);
				}
			}
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x00103F78 File Offset: 0x00102178
		public static Color GetAttackBoxColor(NKMEventAttack cNKMEventAttack, Color defaultcolor)
		{
			if (cNKMEventAttack.m_bTrueDamage)
			{
				return Color.white;
			}
			if (cNKMEventAttack.m_bForceCritical)
			{
				return Color.red;
			}
			if (cNKMEventAttack.m_bCleanHit)
			{
				return Color.green;
			}
			if (cNKMEventAttack.m_bNoCritical)
			{
				return Color.yellow;
			}
			return defaultcolor;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x00103FB4 File Offset: 0x001021B4
		public static string BuildAttackText(NKMEventAttack cNKMEventAttack)
		{
			NKCDebugUtil.sb.Clear();
			NKCDebugUtil.<>c__DisplayClass8_0 CS$<>8__locals1;
			CS$<>8__locals1.bAppend = false;
			NKMDamageTemplet nkmdamageTemplet = null;
			if (!string.IsNullOrEmpty(cNKMEventAttack.m_DamageTempletName))
			{
				NKCDebugUtil.sb.Append("<size=30>");
				NKCDebugUtil.sb.Append(cNKMEventAttack.m_DamageTempletName);
				nkmdamageTemplet = NKMDamageManager.GetTempletByStrID(cNKMEventAttack.m_DamageTempletName);
				if (nkmdamageTemplet != null)
				{
					NKCDebugUtil.sb.Append('(');
					NKCDebugUtil.sb.Append(nkmdamageTemplet.m_DamageTempletBase.m_fAtkFactor);
					if (nkmdamageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactor > 0f)
					{
						NKCDebugUtil.sb.Append("·");
						NKCDebugUtil.sb.Append("MaxHP ");
						NKCDebugUtil.sb.Append(nkmdamageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactor);
					}
					if (nkmdamageTemplet.m_DamageTempletBase.m_fAtkHPRateFactor > 0f)
					{
						NKCDebugUtil.sb.Append("·");
						NKCDebugUtil.sb.Append("HP ");
						NKCDebugUtil.sb.Append(nkmdamageTemplet.m_DamageTempletBase.m_fAtkHPRateFactor);
					}
					NKCDebugUtil.sb.Append(')');
				}
				NKCDebugUtil.sb.AppendLine("</size>");
				if (nkmdamageTemplet.m_ApplyStatusEffect != NKM_UNIT_STATUS_EFFECT.NUSE_NONE)
				{
					NKCDebugUtil.sb.Append(nkmdamageTemplet.m_ApplyStatusEffect);
					NKCDebugUtil.sb.Append('(');
					NKCDebugUtil.sb.Append(nkmdamageTemplet.m_fApplyStatusTime);
					NKCDebugUtil.sb.Append(')');
					CS$<>8__locals1.bAppend = true;
				}
			}
			NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bTrueDamage", cNKMEventAttack.m_bTrueDamage, ref CS$<>8__locals1);
			NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bForceHit", cNKMEventAttack.m_bForceHit, ref CS$<>8__locals1);
			NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bCleanHit", cNKMEventAttack.m_bCleanHit, ref CS$<>8__locals1);
			NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bForceCritical", cNKMEventAttack.m_bForceCritical, ref CS$<>8__locals1);
			NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bNoCritical", cNKMEventAttack.m_bNoCritical, ref CS$<>8__locals1);
			NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bDamageSpeedDependRight", cNKMEventAttack.m_bDamageSpeedDependRight, ref CS$<>8__locals1);
			if (cNKMEventAttack.m_fGetAgroTime > 0f)
			{
				if (CS$<>8__locals1.bAppend)
				{
					NKCDebugUtil.sb.Append("·");
				}
				else
				{
					CS$<>8__locals1.bAppend = true;
				}
				NKCDebugUtil.sb.Append("m_fGetAgroTime = ");
				NKCDebugUtil.sb.Append(cNKMEventAttack.m_fGetAgroTime);
			}
			if (nkmdamageTemplet != null && nkmdamageTemplet.m_ExtraHitDamageTemplet != null)
			{
				NKMDamageTemplet extraHitDamageTemplet = nkmdamageTemplet.m_ExtraHitDamageTemplet;
				NKCDebugUtil.sb.AppendLine();
				NKCDebugUtil.sb.Append("ExtraHitDT");
				NKCDebugUtil.sb.Append('[');
				NKCDebugUtil.sb.Append(extraHitDamageTemplet.m_ExtraHitCountRange.m_Min);
				if (extraHitDamageTemplet.m_ExtraHitCountRange.m_Max > extraHitDamageTemplet.m_ExtraHitCountRange.m_Min)
				{
					NKCDebugUtil.sb.Append('-');
					NKCDebugUtil.sb.Append(extraHitDamageTemplet.m_ExtraHitCountRange.m_Max);
				}
				NKCDebugUtil.sb.Append("] : ");
				NKCDebugUtil.sb.Append(nkmdamageTemplet.m_ExtraHitDamageTempletID);
				NKCDebugUtil.sb.Append('(');
				NKCDebugUtil.sb.Append(extraHitDamageTemplet.m_DamageTempletBase.m_fAtkFactor);
				if (extraHitDamageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactor > 0f)
				{
					NKCDebugUtil.sb.Append("·");
					NKCDebugUtil.sb.Append("MaxHP ");
					NKCDebugUtil.sb.Append(extraHitDamageTemplet.m_DamageTempletBase.m_fAtkMaxHPRateFactor);
				}
				if (extraHitDamageTemplet.m_DamageTempletBase.m_fAtkHPRateFactor > 0f)
				{
					NKCDebugUtil.sb.Append("·");
					NKCDebugUtil.sb.Append("HP ");
					NKCDebugUtil.sb.Append(extraHitDamageTemplet.m_DamageTempletBase.m_fAtkHPRateFactor);
				}
				NKCDebugUtil.sb.Append(')');
				if (nkmdamageTemplet.m_ExtraHitAttribute != null)
				{
					NKCDebugUtil.sb.AppendLine();
					CS$<>8__locals1.bAppend = false;
					NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bTrueDamage", nkmdamageTemplet.m_ExtraHitAttribute.m_bTrueDamage, ref CS$<>8__locals1);
					NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bForceCritical", nkmdamageTemplet.m_ExtraHitAttribute.m_bForceCritical, ref CS$<>8__locals1);
					NKCDebugUtil.<BuildAttackText>g__ApplyBool|8_0("m_bNoCritical", nkmdamageTemplet.m_ExtraHitAttribute.m_bNoCritical, ref CS$<>8__locals1);
				}
			}
			return NKCDebugUtil.sb.ToString();
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x001043DE File Offset: 0x001025DE
		[CompilerGenerated]
		internal static void <BuildAttackText>g__ApplyBool|8_0(string name, bool value, ref NKCDebugUtil.<>c__DisplayClass8_0 A_2)
		{
			if (value)
			{
				if (A_2.bAppend)
				{
					NKCDebugUtil.sb.Append("·");
				}
				else
				{
					A_2.bAppend = true;
				}
				NKCDebugUtil.sb.Append(name);
			}
		}

		// Token: 0x0400324B RID: 12875
		private static StringBuilder sb = new StringBuilder();
	}
}
