using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AC4 RID: 2756
	public class NKCUIWorldMapMissionSlot : MonoBehaviour
	{
		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06007B3A RID: 31546 RVA: 0x0029194B File Offset: 0x0028FB4B
		// (set) Token: 0x06007B3B RID: 31547 RVA: 0x00291953 File Offset: 0x0028FB53
		public int MissionID { get; private set; }

		// Token: 0x06007B3C RID: 31548 RVA: 0x0029195C File Offset: 0x0028FB5C
		public void Init(NKCUIWorldMapMissionSlot.OnClickSlot onClick)
		{
			this.m_cbtnMissionSelect.PointerClick.AddListener(new UnityAction(this.OnBtnClicked));
			this.dOnClickSlot = onClick;
			foreach (NKCUISlot nkcuislot in this.m_lstItemSlot)
			{
				nkcuislot.Init();
			}
			this.m_CompleteRewardItemSlot.Init();
		}

		// Token: 0x06007B3D RID: 31549 RVA: 0x002919DC File Offset: 0x0028FBDC
		public void SetData(NKMWorldMapMissionTemplet missionTemplet, int leaderLevel = -1)
		{
			if (missionTemplet == null)
			{
				this.MissionID = -1;
				return;
			}
			this.MissionID = missionTemplet.m_ID;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL_MISSION_THUMBNAIL", missionTemplet.m_WorldMapMissionThumbnailFile, false);
			if (orLoadAssetResource != null)
			{
				NKCUtil.SetImageSprite(this.m_imgMissionBG, orLoadAssetResource, false);
			}
			else
			{
				Debug.LogWarning(string.Format("MissionBG sprite not found. mission id {0}, spriteName {1}", this.MissionID, missionTemplet.m_WorldMapMissionThumbnailFile));
			}
			NKCUtil.SetImageSprite(this.m_imgMissionType, this.GetMissionTypeSprite(missionTemplet.m_eMissionType), true);
			NKCUtil.SetLabelText(this.m_lbMissionType, NKCUtilString.GetWorldMapMissionType(missionTemplet.m_eMissionType));
			NKCUtil.SetImageSprite(this.m_imgMissinRank, this.GetMissionRankSprite(missionTemplet.m_eMissionRank), true);
			NKCUtil.SetLabelText(this.m_lbName, missionTemplet.GetMissionName());
			NKCUtil.SetLabelText(this.m_lbTime, NKCUtilString.GetTimeStringFromMinutes(missionTemplet.m_MissionTimeInMinutes));
			NKCUtil.SetLabelText(this.m_lbRequiredLevel, string.Format(NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_REQ_LEVEL_ONE_PARAM, missionTemplet.m_ReqManagerLevel));
			if (this.m_lbRequiredLevel != null)
			{
				if (leaderLevel >= 0 && leaderLevel < missionTemplet.m_ReqManagerLevel)
				{
					this.m_lbRequiredLevel.color = Color.red;
				}
				else
				{
					this.m_lbRequiredLevel.color = new Color(0.6037f, 0.6037f, 0.6037f);
				}
			}
			this.SetMissionReward(missionTemplet);
		}

		// Token: 0x06007B3E RID: 31550 RVA: 0x00291B2C File Offset: 0x0028FD2C
		private void SetMissionReward(NKMWorldMapMissionTemplet missionTemplet)
		{
			List<NKCUISlot.SlotData> list = this.MakeSlotDataListFromMissionTemplet(missionTemplet);
			for (int i = 0; i < this.m_lstItemSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstItemSlot[i];
				if (i < list.Count)
				{
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					nkcuislot.SetData(list[i], true, null);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
			NKCUISlot.SlotData slotData = this.MakeCompleteRewardSlotData(missionTemplet);
			if (slotData != null)
			{
				NKCUtil.SetGameobjectActive(this.m_CompleteRewardItemSlot, true);
				this.m_CompleteRewardItemSlot.SetData(slotData, false, true, true, null);
				this.m_CompleteRewardItemSlot.SetAdditionalText(NKCUtilString.GET_STRING_WORLDMAP_CITY_MISSION_REWARD_ADD_TEXT, TextAnchor.MiddleCenter);
				this.m_CompleteRewardItemSlot.SetOpenItemBoxOnClick();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_CompleteRewardItemSlot, false);
		}

		// Token: 0x06007B3F RID: 31551 RVA: 0x00291BDC File Offset: 0x0028FDDC
		private List<NKCUISlot.SlotData> MakeSlotDataListFromMissionTemplet(NKMWorldMapMissionTemplet missionTemplet)
		{
			List<NKCUISlot.SlotData> list = new List<NKCUISlot.SlotData>();
			if (missionTemplet.m_RewardUnitExp > 0)
			{
				NKCUISlot.SlotData item = NKCUISlot.SlotData.MakeMiscItemData(502, (long)missionTemplet.m_RewardUnitExp, 0);
				list.Add(item);
			}
			if (missionTemplet.m_RewardCredit > 0)
			{
				NKCUISlot.SlotData item2 = NKCUISlot.SlotData.MakeMiscItemData(1, (long)missionTemplet.m_RewardCredit, 0);
				list.Add(item2);
			}
			if (missionTemplet.m_RewardEternium > 0)
			{
				NKCUISlot.SlotData item3 = NKCUISlot.SlotData.MakeMiscItemData(2, (long)missionTemplet.m_RewardEternium, 0);
				list.Add(item3);
			}
			if (missionTemplet.m_RewardInformation > 0)
			{
				NKCUISlot.SlotData item4 = NKCUISlot.SlotData.MakeMiscItemData(3, (long)missionTemplet.m_RewardInformation, 0);
				list.Add(item4);
			}
			return list;
		}

		// Token: 0x06007B40 RID: 31552 RVA: 0x00291C72 File Offset: 0x0028FE72
		private NKCUISlot.SlotData MakeCompleteRewardSlotData(NKMWorldMapMissionTemplet missionTemplet)
		{
			if (missionTemplet.m_CompleteRewardQuantity > 0)
			{
				return NKCUISlot.SlotData.MakeRewardTypeData(missionTemplet.m_CompleteRewardType, missionTemplet.m_CompleteRewardID, missionTemplet.m_CompleteRewardQuantity, 0);
			}
			return null;
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x00291C97 File Offset: 0x0028FE97
		private void SetSlotData(NKCUISlot slot, int itemID, int count)
		{
			slot.SetMiscItemData(itemID, (long)count, false, true, false, null);
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x00291CA6 File Offset: 0x0028FEA6
		private Sprite GetMissionTypeSprite(NKMWorldMapMissionTemplet.WorldMapMissionType type)
		{
			switch (type)
			{
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_EXPLORE:
				return this.m_spMissionExplore;
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_DEFENCE:
				return this.m_spMissionDefence;
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_MINING:
				return this.m_spMissionMining;
			case NKMWorldMapMissionTemplet.WorldMapMissionType.WMT_OFFICE:
				return this.m_spMissionOffice;
			default:
				return this.m_spMissionDefence;
			}
		}

		// Token: 0x06007B43 RID: 31555 RVA: 0x00291CE4 File Offset: 0x0028FEE4
		private Sprite GetMissionRankSprite(NKMWorldMapMissionTemplet.WorldMapMissionRank rank)
		{
			switch (rank)
			{
			case NKMWorldMapMissionTemplet.WorldMapMissionRank.WMMR_S:
				return this.m_spMissionRankS;
			case NKMWorldMapMissionTemplet.WorldMapMissionRank.WMMR_A:
				return this.m_spMissionRankA;
			case NKMWorldMapMissionTemplet.WorldMapMissionRank.WMMR_B:
				return this.m_spMissionRankB;
			case NKMWorldMapMissionTemplet.WorldMapMissionRank.WMMR_C:
				return this.m_spMissionRankC;
			default:
				return this.m_spMissionRankC;
			}
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x00291D20 File Offset: 0x0028FF20
		private void OnBtnClicked()
		{
			if (this.m_cbtnMissionSelect.m_bLock)
			{
				return;
			}
			if (this.dOnClickSlot != null)
			{
				this.dOnClickSlot(this.MissionID);
			}
		}

		// Token: 0x040067F2 RID: 26610
		[Header("Object")]
		public Image m_imgMissionBG;

		// Token: 0x040067F3 RID: 26611
		public Image m_imgMissinRank;

		// Token: 0x040067F4 RID: 26612
		public Text m_lbName;

		// Token: 0x040067F5 RID: 26613
		public Text m_lbRequiredLevel;

		// Token: 0x040067F6 RID: 26614
		public Image m_imgMissionType;

		// Token: 0x040067F7 RID: 26615
		public Text m_lbMissionType;

		// Token: 0x040067F8 RID: 26616
		public Text m_lbTime;

		// Token: 0x040067F9 RID: 26617
		public List<NKCUISlot> m_lstItemSlot;

		// Token: 0x040067FA RID: 26618
		public NKCUISlot m_CompleteRewardItemSlot;

		// Token: 0x040067FB RID: 26619
		[Header("Resource_MissionType")]
		public Sprite m_spMissionExplore;

		// Token: 0x040067FC RID: 26620
		public Sprite m_spMissionDefence;

		// Token: 0x040067FD RID: 26621
		public Sprite m_spMissionMining;

		// Token: 0x040067FE RID: 26622
		public Sprite m_spMissionOffice;

		// Token: 0x040067FF RID: 26623
		[Header("Resource_MissionRank")]
		public Sprite m_spMissionRankS;

		// Token: 0x04006800 RID: 26624
		public Sprite m_spMissionRankA;

		// Token: 0x04006801 RID: 26625
		public Sprite m_spMissionRankB;

		// Token: 0x04006802 RID: 26626
		public Sprite m_spMissionRankC;

		// Token: 0x04006803 RID: 26627
		public NKCUIComStateButton m_cbtnMissionSelect;

		// Token: 0x04006804 RID: 26628
		private NKCUIWorldMapMissionSlot.OnClickSlot dOnClickSlot;

		// Token: 0x02001839 RID: 6201
		// (Invoke) Token: 0x0600B564 RID: 46436
		public delegate void OnClickSlot(int missionID);
	}
}
