using System;
using System.Collections.Generic;
using Cs.Core.Util;
using NKM.Contract2;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x02000449 RID: 1097
	public static class NKMOpenTagManager
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0008E275 File Offset: 0x0008C475
		public static IEnumerable<string> Tags
		{
			get
			{
				return NKMOpenTagManager.openTagSet;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0008E27C File Offset: 0x0008C47C
		public static int TagCount
		{
			get
			{
				return NKMOpenTagManager.openTagSet.Count;
			}
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0008E288 File Offset: 0x0008C488
		static NKMOpenTagManager()
		{
			for (int i = 0; i < NKMOpenTagManager.SystemOpenTag.Length; i++)
			{
				string[] systemOpenTag = NKMOpenTagManager.SystemOpenTag;
				int num = i;
				SystemOpenTagType systemOpenTagType = (SystemOpenTagType)i;
				systemOpenTag[num] = systemOpenTagType.ToString();
			}
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0008E2E0 File Offset: 0x0008C4E0
		public static bool TryAddTag(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentException("[OpenTag] tag:" + tag);
			}
			return NKMOpenTagManager.openTagSet.Add(tag);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0008E306 File Offset: 0x0008C506
		public static void ClearOpenTag()
		{
			NKMOpenTagManager.openTagSet.Clear();
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0008E314 File Offset: 0x0008C514
		public static void SetTagList(IReadOnlyList<string> openTagList)
		{
			NKMOpenTagManager.ClearOpenTag();
			foreach (string tag in openTagList)
			{
				NKMOpenTagManager.TryAddTag(tag);
			}
			NKMOpenTagManager.RecalculateTemplets();
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0008E364 File Offset: 0x0008C564
		public static void AddRecalculateAction(string descKey, Action action)
		{
			if (!NKMOpenTagManager.recalculateActions.ContainsKey(descKey))
			{
				NKMOpenTagManager.recalculateActions.Add(descKey, action);
			}
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0008E380 File Offset: 0x0008C580
		public static void RecalculateTemplets()
		{
			foreach (Action action in NKMOpenTagManager.recalculateActions.Values)
			{
				if (action != null)
				{
					action();
				}
			}
			foreach (RandomUnitPoolTempletV2 randomUnitPoolTempletV in NKMTempletContainer<RandomUnitPoolTempletV2>.Values)
			{
				randomUnitPoolTempletV.RecalculateUnitTemplets();
			}
			foreach (ContractTempletV2 contractTempletV in ContractTempletV2.Values)
			{
				contractTempletV.RecalculateIntrusivePool();
			}
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0008E44C File Offset: 0x0008C64C
		public static bool IsOpened(string openTagID)
		{
			return string.IsNullOrWhiteSpace(openTagID) || NKMOpenTagManager.openTagSet.Contains(openTagID);
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0008E463 File Offset: 0x0008C663
		public static bool IsSystemOpened(SystemOpenTagType tagType)
		{
			return NKMOpenTagManager.IsOpened(NKMOpenTagManager.SystemOpenTag[(int)tagType]);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0008E471 File Offset: 0x0008C671
		public static void Drop()
		{
			NKMOpenTagManager.ClearOpenTag();
			NKMOpenTagManager.recalculateActions.Clear();
		}

		// Token: 0x04001E52 RID: 7762
		private static readonly HashSet<string> openTagSet = new HashSet<string>();

		// Token: 0x04001E53 RID: 7763
		private static readonly string[] SystemOpenTag = new string[EnumUtil<SystemOpenTagType>.Count];

		// Token: 0x04001E54 RID: 7764
		private static Dictionary<string, Action> recalculateActions = new Dictionary<string, Action>();
	}
}
