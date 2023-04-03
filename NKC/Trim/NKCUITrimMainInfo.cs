using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AAF RID: 2735
	public class NKCUITrimMainInfo : MonoBehaviour
	{
		// Token: 0x060079C4 RID: 31172 RVA: 0x0028857C File Offset: 0x0028677C
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEnter, new UnityAction(this.OnClickEnter));
			NKCUtil.SetHotkey(this.m_csbtnEnter, HotkeyEventType.Confirm, null, false);
			NKCUITrimReward trimReward = this.m_trimReward;
			if (trimReward != null)
			{
				trimReward.Init();
			}
			NKCUITrimUtility.InitBattleCondition(this.m_battleCondParent, true);
		}

		// Token: 0x060079C5 RID: 31173 RVA: 0x002885CC File Offset: 0x002867CC
		public void SetData(int trimId)
		{
			this.m_trimId = trimId;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimId);
			Sprite sp = null;
			int a = 0;
			int trimGroup = 0;
			string msg;
			string msg2;
			if (nkmtrimTemplet != null)
			{
				msg = NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false);
				msg2 = NKCStringTable.GetString(nkmtrimTemplet.TirmGroupDesc, false);
				sp = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_TRIM_MAP_IMG", nkmtrimTemplet.TrimGroupBGPrefab, false);
				a = nkmtrimTemplet.MaxTrimLevel;
				trimGroup = nkmtrimTemplet.TrimPointGroup;
				Color color;
				ColorUtility.TryParseHtmlString(nkmtrimTemplet.TrimBGColor, out color);
				NKCUtil.SetImageColor(this.m_imgColor, color);
			}
			else
			{
				msg = " - ";
				msg2 = " - ";
			}
			int clearedTrimLevel = NKCUITrimUtility.GetClearedTrimLevel(NKCScenManager.CurrentUserData(), trimId);
			int trimLevel = Mathf.Min(a, clearedTrimLevel + 1);
			NKCUITrimUtility.SetBattleCondition(this.m_battleCondParent, nkmtrimTemplet, trimLevel, true);
			NKCUtil.SetLabelText(this.m_lbTrimName, msg);
			NKCUtil.SetLabelText(this.m_lbTrimDesc, msg2);
			NKCUtil.SetLabelText(this.m_lbTrimLevel, trimLevel.ToString());
			NKCUtil.SetImageSprite(this.m_imgMapLarge, sp, true);
			NKCUtil.SetImageSprite(this.m_imgMapSmall, sp, true);
			NKMTrimIntervalTemplet trimInterval = NKMTrimIntervalTemplet.Find(NKCSynchronizedTime.ServiceTime);
			NKCUtil.SetGameobjectActive(this.m_objEnterLimitRoot, NKCUITrimUtility.IsEnterCountLimited(trimInterval));
			if (NKCUITrimUtility.IsEnterCountLimited(trimInterval))
			{
				string enterLimitMsg = NKCUITrimUtility.GetEnterLimitMsg(trimInterval);
				NKCUtil.SetLabelText(this.m_lbEnterLimit, enterLimitMsg);
			}
			int recommendedPower = NKCUITrimUtility.GetRecommendedPower(trimGroup, trimLevel);
			NKCUtil.SetLabelText(this.m_lbRecommendedPower, recommendedPower.ToString());
			NKCUITrimReward trimReward = this.m_trimReward;
			if (trimReward == null)
			{
				return;
			}
			trimReward.SetData(trimId, trimLevel);
		}

		// Token: 0x060079C6 RID: 31174 RVA: 0x00288735 File Offset: 0x00286935
		public void OnClickEnter()
		{
			NKCUIPopupTrimDungeon.Instance.Open(this.m_trimId);
		}

		// Token: 0x0400667C RID: 26236
		public Transform m_battleCondParent;

		// Token: 0x0400667D RID: 26237
		public NKCUIComStateButton m_csbtnEnter;

		// Token: 0x0400667E RID: 26238
		public NKCUITrimReward m_trimReward;

		// Token: 0x0400667F RID: 26239
		public Text m_lbTrimLevel;

		// Token: 0x04006680 RID: 26240
		public Text m_lbTrimName;

		// Token: 0x04006681 RID: 26241
		public Text m_lbTrimDesc;

		// Token: 0x04006682 RID: 26242
		public Text m_lbEnterLimit;

		// Token: 0x04006683 RID: 26243
		public Text m_lbRecommendedPower;

		// Token: 0x04006684 RID: 26244
		public Image m_imgMapLarge;

		// Token: 0x04006685 RID: 26245
		public Image m_imgMapSmall;

		// Token: 0x04006686 RID: 26246
		public Image m_imgColor;

		// Token: 0x04006687 RID: 26247
		public GameObject m_objEnterLimitRoot;

		// Token: 0x04006688 RID: 26248
		private int m_trimId;
	}
}
