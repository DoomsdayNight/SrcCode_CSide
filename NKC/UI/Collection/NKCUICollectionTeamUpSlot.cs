using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C2E RID: 3118
	public class NKCUICollectionTeamUpSlot : MonoBehaviour
	{
		// Token: 0x0600908B RID: 37003 RVA: 0x00313ACD File Offset: 0x00311CCD
		public int GetTeamID()
		{
			return this.m_iTeamID;
		}

		// Token: 0x0600908C RID: 37004 RVA: 0x00313AD5 File Offset: 0x00311CD5
		public void Init()
		{
			this.m_RewardSlot.Init();
		}

		// Token: 0x0600908D RID: 37005 RVA: 0x00313AE2 File Offset: 0x00311CE2
		public List<RectTransform> GetRentalSlot()
		{
			return this.m_lstRentalSlot;
		}

		// Token: 0x0600908E RID: 37006 RVA: 0x00313AEA File Offset: 0x00311CEA
		public void ClearRentalList()
		{
			this.m_lstRentalSlot.Clear();
		}

		// Token: 0x0600908F RID: 37007 RVA: 0x00313AF8 File Offset: 0x00311CF8
		public void SetData(NKCUICollectionTeamUp.TeamUpSlotData slotData, NKCUICollectionTeamUpSlot.OnClicked click = null, bool bSetUnitList = true, NKCUISlot.OnClick SlotClick = null, List<RectTransform> lstSlot = null)
		{
			if (slotData == null)
			{
				Debug.Log("NKCUICollectionTeamUpSlot.SetData - wrong slot data");
				return;
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_TEAM_UP_SLOT_BG_TEXT_TITLE, slotData.m_TeamName.ToString());
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_TEAM_UP_SLOT_BG_TEXT_COUNT, string.Format("{0}/{1}", slotData.m_HasUnitCount, slotData.m_RewardCriteria));
			if (lstSlot != null)
			{
				List<int> lstUnit = slotData.m_lstUnit;
				for (int i = 0; i < lstSlot.Count; i++)
				{
					lstSlot[i].SetParent(this.m_rtSlotParent, false);
					lstSlot[i].GetComponent<RectTransform>().localScale = Vector3.one;
					NKCUISlot component = lstSlot[i].GetComponent<NKCUISlot>();
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeUnitData(lstUnit[i], 1, 0, slotData.m_TeamID);
					component.SetData(data, true, SlotClick);
					NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
					if (armyData != null)
					{
						component.SetDenied(armyData.IsFirstGetUnit(lstUnit[i]));
					}
					NKCUtil.SetGameobjectActive(component.m_imgUpperRightIcon, false);
					this.m_lstRentalSlot.Add(lstSlot[i]);
				}
			}
			bool openTagCollectionTeamUp = NKCUnitMissionManager.GetOpenTagCollectionTeamUp();
			NKCUtil.SetGameobjectActive(this.m_objTeamUpReward, openTagCollectionTeamUp);
			if (null != this.m_RewardSlot && openTagCollectionTeamUp)
			{
				this.m_bReadyReward = false;
				NKCUISlot.SlotData slotData2 = NKCUISlot.SlotData.MakeRewardTypeData(slotData.m_RewardType, slotData.m_RewardID, slotData.m_RewardValue, 0);
				if (slotData2 != null)
				{
					this.m_RewardSlot.SetData(slotData2, true, new NKCUISlot.OnClick(this.OnRewardClick));
					switch (slotData.m_RewardState)
					{
					case NKCUICollectionTeamUp.eTeamUpRewardState.RS_NOT_READY:
						this.m_RewardSlot.SetDisable(true, "");
						this.m_RewardSlot.SetClear(false);
						break;
					case NKCUICollectionTeamUp.eTeamUpRewardState.RS_READY:
						this.m_RewardSlot.SetDisable(false, "");
						this.m_RewardSlot.SetClear(false);
						this.m_bReadyReward = true;
						break;
					case NKCUICollectionTeamUp.eTeamUpRewardState.RS_COMPLETE:
						this.m_RewardSlot.SetDisable(false, "");
						this.m_RewardSlot.SetClear(true);
						break;
					}
					NKCUtil.SetGameobjectActive(this.m_AB_ICON_SLOT_REWARD_FX, slotData.m_RewardState == NKCUICollectionTeamUp.eTeamUpRewardState.RS_READY);
				}
			}
			this.m_iTeamID = slotData.m_TeamID;
			this.m_OnRewardClicked = click;
			if (null != this.m_rtBackground)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.m_rtBackground);
				if (base.gameObject.activeInHierarchy && base.gameObject.activeSelf)
				{
					base.StartCoroutine(this.DelayLayout());
				}
			}
		}

		// Token: 0x06009090 RID: 37008 RVA: 0x00313D64 File Offset: 0x00311F64
		private IEnumerator DelayLayout()
		{
			yield return new WaitForEndOfFrame();
			this.m_LayoutElement.minHeight = this.m_rtBackground.GetHeight();
			this.m_LayoutElement.preferredHeight = this.m_rtBackground.GetHeight();
			yield return null;
			yield break;
		}

		// Token: 0x06009091 RID: 37009 RVA: 0x00313D73 File Offset: 0x00311F73
		public void OnRewardClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (!this.m_bReadyReward)
			{
				return;
			}
			if (this.m_OnRewardClicked != null)
			{
				this.m_OnRewardClicked(this.m_iTeamID);
			}
		}

		// Token: 0x04007DB2 RID: 32178
		public Text m_NKM_UI_COLLECTION_TEAM_UP_SLOT_BG_TEXT_TITLE;

		// Token: 0x04007DB3 RID: 32179
		public Text m_NKM_UI_COLLECTION_TEAM_UP_SLOT_BG_TEXT_COUNT;

		// Token: 0x04007DB4 RID: 32180
		public RectTransform m_rtBackground;

		// Token: 0x04007DB5 RID: 32181
		public LayoutElement m_LayoutElement;

		// Token: 0x04007DB6 RID: 32182
		public RectTransform m_rtSlotParent;

		// Token: 0x04007DB7 RID: 32183
		public NKCUISlot m_RewardSlot;

		// Token: 0x04007DB8 RID: 32184
		public GameObject m_AB_ICON_SLOT_REWARD_FX;

		// Token: 0x04007DB9 RID: 32185
		public GameObject m_objTeamUpReward;

		// Token: 0x04007DBA RID: 32186
		private int m_iTeamID;

		// Token: 0x04007DBB RID: 32187
		private bool m_bReadyReward;

		// Token: 0x04007DBC RID: 32188
		private List<RectTransform> m_lstRentalSlot = new List<RectTransform>();

		// Token: 0x04007DBD RID: 32189
		private NKCUICollectionTeamUpSlot.OnClicked m_OnRewardClicked;

		// Token: 0x020019F4 RID: 6644
		// (Invoke) Token: 0x0600BAA5 RID: 47781
		public delegate void OnClicked(int teamID);
	}
}
