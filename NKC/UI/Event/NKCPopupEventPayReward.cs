using System;
using System.Collections.Generic;
using NKC.Templet;
using NKC.UI.NPC;
using NKC.UI.Shop;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BC8 RID: 3016
	public class NKCPopupEventPayReward : NKCUIBase
	{
		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06008B8C RID: 35724 RVA: 0x002F6F20 File Offset: 0x002F5120
		public static NKCPopupEventPayReward Instance
		{
			get
			{
				if (NKCPopupEventPayReward.m_Instance == null)
				{
					NKCPopupEventPayReward.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventPayReward>(NKCPopupEventPayReward.ASSET_BUNDLE_NAME, NKCPopupEventPayReward.UI_ASSET_NAME, NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventPayReward.CleanupInstance)).GetInstance<NKCPopupEventPayReward>();
					NKCPopupEventPayReward.m_Instance.InitUI();
				}
				return NKCPopupEventPayReward.m_Instance;
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06008B8D RID: 35725 RVA: 0x002F6F6F File Offset: 0x002F516F
		public static bool HasInstance
		{
			get
			{
				return NKCPopupEventPayReward.m_Instance != null;
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06008B8E RID: 35726 RVA: 0x002F6F7C File Offset: 0x002F517C
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventPayReward.m_Instance != null && NKCPopupEventPayReward.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008B8F RID: 35727 RVA: 0x002F6F97 File Offset: 0x002F5197
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventPayReward.m_Instance != null && NKCPopupEventPayReward.m_Instance.IsOpen)
			{
				NKCPopupEventPayReward.m_Instance.Close();
			}
		}

		// Token: 0x06008B90 RID: 35728 RVA: 0x002F6FBC File Offset: 0x002F51BC
		private static void CleanupInstance()
		{
			NKCPopupEventPayReward.m_Instance = null;
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06008B91 RID: 35729 RVA: 0x002F6FC4 File Offset: 0x002F51C4
		public override string MenuName
		{
			get
			{
				return "Payback Mission";
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06008B92 RID: 35730 RVA: 0x002F6FCB File Offset: 0x002F51CB
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06008B93 RID: 35731 RVA: 0x002F6FCE File Offset: 0x002F51CE
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x06008B94 RID: 35732 RVA: 0x002F6FD4 File Offset: 0x002F51D4
		private void InitUI()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnUnitInfo, new UnityAction(this.OnClickUnitInfo));
			NKCPopupEventPayRewardSlot finalRewardSlot = this.m_finalRewardSlot;
			if (finalRewardSlot != null)
			{
				finalRewardSlot.Init();
			}
			this.m_scrollRectInit = false;
		}

		// Token: 0x06008B95 RID: 35733 RVA: 0x002F7028 File Offset: 0x002F5228
		public override void CloseInternal()
		{
			if (this.m_npcSpineIllust != null)
			{
				this.m_npcSpineIllust.m_dOnTouch = null;
			}
			this.m_paybackTemplet = null;
			List<NKMMissionTemplet> missionTempletList = this.m_missionTempletList;
			if (missionTempletList != null)
			{
				missionTempletList.Clear();
			}
			this.m_missionTempletList = null;
			NKCUIVoiceManager.StopVoice();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06008B96 RID: 35734 RVA: 0x002F707F File Offset: 0x002F527F
		public static void SetAssetName(string bundleName, string assetName)
		{
			NKCPopupEventPayReward.ASSET_BUNDLE_NAME = bundleName;
			NKCPopupEventPayReward.UI_ASSET_NAME = assetName;
		}

		// Token: 0x06008B97 RID: 35735 RVA: 0x002F7090 File Offset: 0x002F5290
		public void Open(int eventId, int missionTabId)
		{
			this.m_missionTabId = missionTabId;
			this.m_paybackTemplet = NKCEventPaybackTemplet.Find(eventId);
			if (this.m_paybackTemplet == null)
			{
				return;
			}
			base.gameObject.SetActive(true);
			if (!this.m_scrollRectInit && this.m_loopScrollRect != null)
			{
				this.m_loopScrollRect.dOnGetObject += this.GetSlot;
				this.m_loopScrollRect.dOnReturnObject += this.ReturnSlot;
				this.m_loopScrollRect.dOnProvideData += this.ProvideData;
				this.m_loopScrollRect.ContentConstraintCount = 1;
				this.m_loopScrollRect.PrepareCells(0);
				this.m_loopScrollRect.TotalCount = 0;
				this.m_loopScrollRect.RefreshCells(false);
				this.m_scrollRectInit = true;
			}
			if (this.m_npcSpineIllust != null)
			{
				this.m_npcSpineIllust.m_dOnTouch = new NKCUINPCSpineIllust.OnTouch(this.OnTouchSpineIllust);
			}
			if (string.IsNullOrEmpty(this.m_paybackTemplet.UnitStrId))
			{
				NKCUtil.SetGameobjectActive(this.m_objUnitInfoRoot, false);
			}
			else
			{
				this.SetUnitInfo(this.m_paybackTemplet);
			}
			this.Refresh();
			base.UIOpened(true);
		}

		// Token: 0x06008B98 RID: 35736 RVA: 0x002F71B8 File Offset: 0x002F53B8
		public void Refresh()
		{
			if (this.m_missionTempletList == null)
			{
				this.m_missionTempletList = this.GetMissionTempletList(this.m_missionTabId);
			}
			int indexPosition = 0;
			int count = this.m_missionTempletList.Count;
			for (int i = 0; i < count; i++)
			{
				NKMMissionManager.MissionState state = NKMMissionManager.GetMissionStateData(this.m_missionTempletList[i]).state;
				if (state == NKMMissionManager.MissionState.REPEAT_COMPLETED || state == NKMMissionManager.MissionState.COMPLETED)
				{
					indexPosition = i;
				}
			}
			this.SetConsumeAmount(this.m_missionTempletList);
			if (this.m_loopScrollRect != null)
			{
				this.m_loopScrollRect.TotalCount = this.m_missionTempletList.Count;
				this.m_loopScrollRect.SetIndexPosition(indexPosition);
			}
			if (this.m_missionTempletList.Count > 0)
			{
				this.SetFinalMissionInfo(this.m_missionTempletList[this.m_missionTempletList.Count - 1]);
				this.SetPayIcon(this.m_missionTempletList[0]);
			}
			else
			{
				this.SetFinalMissionInfo(null);
				this.SetPayIcon(null);
			}
			this.SetTimeLeft(this.m_paybackTemplet);
		}

		// Token: 0x06008B99 RID: 35737 RVA: 0x002F72B5 File Offset: 0x002F54B5
		public override void UnHide()
		{
			base.UnHide();
			this.Refresh();
		}

		// Token: 0x06008B9A RID: 35738 RVA: 0x002F72C4 File Offset: 0x002F54C4
		private void SetUnitInfo(NKCEventPaybackTemplet paybackTemplet)
		{
			NKCUtil.SetGameobjectActive(this.m_objUnitInfoRoot, true);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(paybackTemplet.UnitStrId);
			if (unitTempletBase != null)
			{
				NKCUtil.SetLabelText(this.m_lbUnitName, unitTempletBase.GetUnitName());
				NKCUtil.SetLabelText(this.m_lbUnitTitle, unitTempletBase.GetUnitTitle());
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbUnitName, "");
				NKCUtil.SetLabelText(this.m_lbUnitTitle, "");
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(paybackTemplet.SkinId);
			if (skinTemplet != null)
			{
				NKCUtil.SetImageSprite(this.m_imgUnitIcon, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, skinTemplet.m_SkinEquipUnitID, 0), false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgUnitIcon, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase), false);
		}

		// Token: 0x06008B9B RID: 35739 RVA: 0x002F736C File Offset: 0x002F556C
		private List<NKMMissionTemplet> GetMissionTempletList(int missionTabId)
		{
			List<NKMMissionTemplet> list = NKMMissionManager.GetMissionTempletListByType(missionTabId);
			if (list == null)
			{
				list = new List<NKMMissionTemplet>();
			}
			list.Sort(delegate(NKMMissionTemplet e1, NKMMissionTemplet e2)
			{
				if (e1.m_MissionID < e2.m_MissionID)
				{
					return -1;
				}
				if (e1.m_MissionID > e2.m_MissionID)
				{
					return 1;
				}
				return 0;
			});
			return list;
		}

		// Token: 0x06008B9C RID: 35740 RVA: 0x002F73B0 File Offset: 0x002F55B0
		private void SetConsumeAmount(List<NKMMissionTemplet> missionTempletList)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			this.m_consumeAmount = 0L;
			if (nkmuserData != null && missionTempletList != null)
			{
				int count = missionTempletList.Count;
				for (int i = 0; i < count; i++)
				{
					if (missionTempletList[i] != null)
					{
						NKMMissionData missionDataByGroupId = nkmuserData.m_MissionData.GetMissionDataByGroupId(missionTempletList[i].m_GroupId);
						if (missionDataByGroupId != null)
						{
							long num = Math.Min(missionTempletList[i].m_Times, missionDataByGroupId.times);
							if (this.m_consumeAmount < num)
							{
								this.m_consumeAmount = num;
							}
						}
					}
				}
			}
			NKCUtil.SetLabelText(this.m_lbConsumeAmount, string.Format("{0:#,0}", this.m_consumeAmount));
		}

		// Token: 0x06008B9D RID: 35741 RVA: 0x002F7454 File Offset: 0x002F5654
		private void SetFinalMissionInfo(NKMMissionTemplet finalMissionTemplet)
		{
			if (finalMissionTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_finalRewardSlot, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_finalRewardSlot, true);
			if (finalMissionTemplet.m_MissionReward.Count > 0 && finalMissionTemplet.m_MissionReward[0].reward_type == NKM_REWARD_TYPE.RT_SKIN)
			{
				NKCPopupEventPayRewardSlot finalRewardSlot = this.m_finalRewardSlot;
				if (finalRewardSlot == null)
				{
					return;
				}
				finalRewardSlot.SetData(finalMissionTemplet, 0f, new NKCPopupEventPayRewardSlot.OnSetMissionState(this.SetFinalMissionDeco), new NKCPopupEventPayRewardSlot.OnMissionComplete(this.OnMissionComplete), new NKCUISlot.OnClick(this.OnClickRewardIcon));
				return;
			}
			else
			{
				NKCPopupEventPayRewardSlot finalRewardSlot2 = this.m_finalRewardSlot;
				if (finalRewardSlot2 == null)
				{
					return;
				}
				finalRewardSlot2.SetData(finalMissionTemplet, 0f, new NKCPopupEventPayRewardSlot.OnSetMissionState(this.SetFinalMissionDeco), new NKCPopupEventPayRewardSlot.OnMissionComplete(this.OnMissionComplete), null);
				return;
			}
		}

		// Token: 0x06008B9E RID: 35742 RVA: 0x002F750C File Offset: 0x002F570C
		private void SetPayIcon(NKMMissionTemplet missionTemplet)
		{
			if (missionTemplet != null && missionTemplet.m_MissionCond.value1 != null && missionTemplet.m_MissionCond.value1.Count > 0)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(missionTemplet.m_MissionCond.value1[0]);
				NKCUtil.SetImageSprite(this.m_payIcon, orLoadMiscItemSmallIcon, false);
			}
		}

		// Token: 0x06008B9F RID: 35743 RVA: 0x002F7560 File Offset: 0x002F5760
		private void SetTimeLeft(NKCEventPaybackTemplet paybackTemplet)
		{
			NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(paybackTemplet.IntervalTag);
			if (nkmintervalTemplet == null)
			{
				NKCUtil.SetLabelText(this.m_lbEventInterval, "");
				return;
			}
			string remainTimeStringOneParam = NKCUtilString.GetRemainTimeStringOneParam(NKCSynchronizedTime.ToUtcTime(nkmintervalTemplet.EndDate));
			NKCUtil.SetLabelText(this.m_lbEventInterval, remainTimeStringOneParam);
		}

		// Token: 0x06008BA0 RID: 35744 RVA: 0x002F75AA File Offset: 0x002F57AA
		private void SetFinalMissionDeco(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objFinalMissionDeco, !value);
		}

		// Token: 0x06008BA1 RID: 35745 RVA: 0x002F75BC File Offset: 0x002F57BC
		private void OnTouchSpineIllust()
		{
			if (string.IsNullOrEmpty(this.m_paybackTemplet.UnitStrId) || this.m_paybackTemplet.SkinId <= 0)
			{
				return;
			}
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_TOUCH, this.m_paybackTemplet.UnitStrId, this.m_paybackTemplet.SkinId, false, true);
		}

		// Token: 0x06008BA2 RID: 35746 RVA: 0x002F760C File Offset: 0x002F580C
		private void OnClickUnitInfo()
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(this.m_paybackTemplet.SkinId);
			NKCUIShopSkinPopup.Instance.OpenForSkinInfo(skinTemplet, 0);
		}

		// Token: 0x06008BA3 RID: 35747 RVA: 0x002F7638 File Offset: 0x002F5838
		private void OnClickRewardIcon(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			if (slotData.eType == NKCUISlot.eSlotMode.Skin)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(slotData.ID);
				NKCUIShopSkinPopup.Instance.OpenForSkinInfo(skinTemplet, 0);
			}
		}

		// Token: 0x06008BA4 RID: 35748 RVA: 0x002F766A File Offset: 0x002F586A
		private void OnMissionComplete()
		{
			NKCPacketSender.Send_NKMPacket_MISSION_COMPLETE_ALL_REQ(this.m_missionTabId);
		}

		// Token: 0x06008BA5 RID: 35749 RVA: 0x002F7677 File Offset: 0x002F5877
		private RectTransform GetSlot(int index)
		{
			NKCPopupEventPayRewardSlot newInstance = NKCPopupEventPayRewardSlot.GetNewInstance(null, this.m_paybackTemplet.BannerPrefabId, this.m_paybackTemplet.BannerPrefabId + "_SLOT");
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06008BA6 RID: 35750 RVA: 0x002F76AC File Offset: 0x002F58AC
		private void ReturnSlot(Transform tr)
		{
			NKCPopupEventPayRewardSlot component = tr.GetComponent<NKCPopupEventPayRewardSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06008BA7 RID: 35751 RVA: 0x002F76E4 File Offset: 0x002F58E4
		private void ProvideData(Transform tr, int index)
		{
			NKCPopupEventPayRewardSlot component = tr.GetComponent<NKCPopupEventPayRewardSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_missionTempletList.Count <= 0 || this.m_missionTempletList.Count <= index)
			{
				component.SetData(null, 0f, null, null, null);
				return;
			}
			long num = 0L;
			if (index > 0)
			{
				num = this.m_missionTempletList[index - 1].m_Times;
			}
			float num2 = (float)(this.m_consumeAmount - num) / (float)(this.m_missionTempletList[index].m_Times - num);
			num2 = Mathf.Max(num2, 0f);
			if (index == this.m_missionTempletList.Count - 1 && this.m_missionTempletList[index].m_MissionReward.Count > 0 && this.m_missionTempletList[index].m_MissionReward[0].reward_type == NKM_REWARD_TYPE.RT_SKIN)
			{
				component.SetData(this.m_missionTempletList[index], num2, null, new NKCPopupEventPayRewardSlot.OnMissionComplete(this.OnMissionComplete), new NKCUISlot.OnClick(this.OnClickRewardIcon));
				return;
			}
			component.SetData(this.m_missionTempletList[index], num2, null, new NKCPopupEventPayRewardSlot.OnMissionComplete(this.OnMissionComplete), null);
		}

		// Token: 0x0400785F RID: 30815
		private static string ASSET_BUNDLE_NAME = "EVENT_PF_PAYBACK_001";

		// Token: 0x04007860 RID: 30816
		private static string UI_ASSET_NAME = "EVENT_PF_PAYBACK_001";

		// Token: 0x04007861 RID: 30817
		private static NKCPopupEventPayReward m_Instance;

		// Token: 0x04007862 RID: 30818
		public NKCUINPCSpineIllust m_npcSpineIllust;

		// Token: 0x04007863 RID: 30819
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04007864 RID: 30820
		public Text m_lbConsumeAmount;

		// Token: 0x04007865 RID: 30821
		public Text m_lbEventInterval;

		// Token: 0x04007866 RID: 30822
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x04007867 RID: 30823
		public Image m_payIcon;

		// Token: 0x04007868 RID: 30824
		public GameObject m_objFinalMissionDeco;

		// Token: 0x04007869 RID: 30825
		[Header("ĳ���� ����")]
		public GameObject m_objUnitInfoRoot;

		// Token: 0x0400786A RID: 30826
		public NKCUIComStateButton m_csbtnUnitInfo;

		// Token: 0x0400786B RID: 30827
		public Image m_imgUnitIcon;

		// Token: 0x0400786C RID: 30828
		public Text m_lbUnitName;

		// Token: 0x0400786D RID: 30829
		public Text m_lbUnitTitle;

		// Token: 0x0400786E RID: 30830
		[Header("���� ���� ����")]
		public NKCPopupEventPayRewardSlot m_finalRewardSlot;

		// Token: 0x0400786F RID: 30831
		private NKCEventPaybackTemplet m_paybackTemplet;

		// Token: 0x04007870 RID: 30832
		private List<NKMMissionTemplet> m_missionTempletList;

		// Token: 0x04007871 RID: 30833
		private int m_missionTabId;

		// Token: 0x04007872 RID: 30834
		private long m_consumeAmount;

		// Token: 0x04007873 RID: 30835
		private bool m_scrollRectInit;
	}
}
