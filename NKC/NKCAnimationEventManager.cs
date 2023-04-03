using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200069B RID: 1691
	public static class NKCAnimationEventManager
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x0011F177 File Offset: 0x0011D377
		public static bool DataExist
		{
			get
			{
				return NKCAnimationEventManager.m_dicEventSet.Count > 0;
			}
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x0011F188 File Offset: 0x0011D388
		public static bool LoadFromLua()
		{
			NKCAnimationEventManager.m_dicEventSet.Clear();
			IEnumerable<NKCAnimationEventTemplet> enumerable = NKMTempletLoader.LoadCommonPath<NKCAnimationEventTemplet>("AB_SCRIPT", "LUA_ANIMATION_EVENT_TEMPLET", "ANIMATION_EVENT_TEMPLET", new Func<NKMLua, NKCAnimationEventTemplet>(NKCAnimationEventTemplet.LoadFromLua));
			if (NKCAnimationEventManager.m_dicEventSet == null)
			{
				Log.Error("[NKCAnimationEventTemplet] m_dicEventSet is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCAnimationEventManager.cs", 43);
				return false;
			}
			foreach (NKCAnimationEventTemplet nkcanimationEventTemplet in enumerable)
			{
				if (!NKCAnimationEventManager.m_dicEventSet.ContainsKey(nkcanimationEventTemplet.Key))
				{
					NKCAnimationEventManager.m_dicEventSet.Add(nkcanimationEventTemplet.Key, new List<NKCAnimationEventTemplet>());
				}
				NKCAnimationEventManager.m_dicEventSet[nkcanimationEventTemplet.Key].Add(nkcanimationEventTemplet);
			}
			return true;
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x0011F24C File Offset: 0x0011D44C
		public static List<NKCAnimationEventTemplet> Find(string strID)
		{
			List<NKCAnimationEventTemplet> result;
			if (!NKCAnimationEventManager.m_dicEventSet.TryGetValue(strID, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x0011F26B File Offset: 0x0011D46B
		public static bool IsEmotionOnly(List<NKCAnimationEventTemplet> lstAnim)
		{
			return lstAnim.TrueForAll((NKCAnimationEventTemplet x) => x.m_AniEventType == AnimationEventType.PLAY_EMOTION);
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x0011F294 File Offset: 0x0011D494
		public static bool CanPlayAnimEvent(INKCAnimationActor actor, string animEventID)
		{
			List<NKCAnimationEventTemplet> lstAnim = NKCAnimationEventManager.Find(animEventID);
			return NKCAnimationEventManager.CanPlayAnimEvent(actor, lstAnim);
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x0011F2B0 File Offset: 0x0011D4B0
		public static bool CanPlayAnimEvent(INKCAnimationActor actor, List<NKCAnimationEventTemplet> lstAnim)
		{
			if (actor == null || lstAnim == null)
			{
				return false;
			}
			foreach (NKCAnimationEventTemplet nkcanimationEventTemplet in lstAnim)
			{
				switch (nkcanimationEventTemplet.m_AniEventType)
				{
				case AnimationEventType.ANIMATION_SPINE:
					if (!actor.CanPlaySpineAnimation(NKCAnimationEventManager.GetAnimationType(nkcanimationEventTemplet.m_StrValue)))
					{
						return false;
					}
					break;
				case AnimationEventType.ANIMATION_NAME_SPINE:
					if (!actor.CanPlaySpineAnimation(nkcanimationEventTemplet.m_StrValue))
					{
						return false;
					}
					break;
				case AnimationEventType.ANIMATION_UNITY:
					if (actor.Animator == null)
					{
						return false;
					}
					if (!actor.Animator.HasState(0, Animator.StringToHash(nkcanimationEventTemplet.m_StrValue)))
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x0011F37C File Offset: 0x0011D57C
		public static NKCASUIUnitIllust.eAnimation GetAnimationType(string aniName)
		{
			return (NKCASUIUnitIllust.eAnimation)Enum.Parse(typeof(NKCASUIUnitIllust.eAnimation), aniName);
		}

		// Token: 0x04003443 RID: 13379
		private static Dictionary<string, List<NKCAnimationEventTemplet>> m_dicEventSet = new Dictionary<string, List<NKCAnimationEventTemplet>>();
	}
}
