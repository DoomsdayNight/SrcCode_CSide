using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Fierce
{
	// Token: 0x02000BC1 RID: 3009
	public class NKCPopupFierceBattleSelfPenalty : NKCUIBase
	{
		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06008B11 RID: 35601 RVA: 0x002F4B6C File Offset: 0x002F2D6C
		public static NKCPopupFierceBattleSelfPenalty Instance
		{
			get
			{
				if (NKCPopupFierceBattleSelfPenalty.m_Instance == null)
				{
					NKCPopupFierceBattleSelfPenalty.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupFierceBattleSelfPenalty>("ab_ui_nkm_ui_fierce_battle", "NKM_UI_POPUP_FIERCE_BATTLE_PEN", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupFierceBattleSelfPenalty.CleanupInstance)).GetInstance<NKCPopupFierceBattleSelfPenalty>();
					NKCPopupFierceBattleSelfPenalty.m_Instance.Init();
				}
				return NKCPopupFierceBattleSelfPenalty.m_Instance;
			}
		}

		// Token: 0x06008B12 RID: 35602 RVA: 0x002F4BBB File Offset: 0x002F2DBB
		private static void CleanupInstance()
		{
			NKCPopupFierceBattleSelfPenalty.m_Instance = null;
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06008B13 RID: 35603 RVA: 0x002F4BC3 File Offset: 0x002F2DC3
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupFierceBattleSelfPenalty.m_Instance != null && NKCPopupFierceBattleSelfPenalty.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008B14 RID: 35604 RVA: 0x002F4BDE File Offset: 0x002F2DDE
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupFierceBattleSelfPenalty.m_Instance != null && NKCPopupFierceBattleSelfPenalty.m_Instance.IsOpen)
			{
				NKCPopupFierceBattleSelfPenalty.m_Instance.Close();
			}
		}

		// Token: 0x06008B15 RID: 35605 RVA: 0x002F4C03 File Offset: 0x002F2E03
		private void OnDestroy()
		{
			NKCPopupFierceBattleSelfPenalty.m_Instance = null;
		}

		// Token: 0x06008B16 RID: 35606 RVA: 0x002F4C0B File Offset: 0x002F2E0B
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008B17 RID: 35607 RVA: 0x002F4C19 File Offset: 0x002F2E19
		public override void OnBackButton()
		{
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06008B18 RID: 35608 RVA: 0x002F4C1B File Offset: 0x002F2E1B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06008B19 RID: 35609 RVA: 0x002F4C1E File Offset: 0x002F2E1E
		public override string MenuName
		{
			get
			{
				return "FIERCE_BATTLE_SELF_PENALTY_POPUP";
			}
		}

		// Token: 0x06008B1A RID: 35610 RVA: 0x002F4C28 File Offset: 0x002F2E28
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_csbtnConfirm, new UnityAction(this.OnClickConfirm));
			NKCUtil.SetBindFunction(this.m_csbtnReset, new UnityAction(this.OnClickReset));
			NKCUtil.SetBindFunction(this.m_csbtnCancel, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_csbtnConfirm, HotkeyEventType.Confirm, null, false);
			this.InitFiercePenaltyData();
		}

		// Token: 0x06008B1B RID: 35611 RVA: 0x002F4C90 File Offset: 0x002F2E90
		private void InitFiercePenaltyData()
		{
			Dictionary<int, List<NKMFiercePenaltyTemplet>> dictionary = new Dictionary<int, List<NKMFiercePenaltyTemplet>>();
			foreach (KeyValuePair<int, NKMBattleConditionTemplet> keyValuePair in NKMBattleConditionManager.Dic)
			{
				if (keyValuePair.Value.UseContentsType == NKMBattleConditionTemplet.USE_CONTENT_TYPE.UCT_FIERCE_PENALTY)
				{
					using (IEnumerator<NKMFiercePenaltyTemplet> enumerator2 = NKMTempletContainer<NKMFiercePenaltyTemplet>.Values.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							NKMFiercePenaltyTemplet penaltyTemplet = enumerator2.Current;
							if (penaltyTemplet != null && keyValuePair.Value == penaltyTemplet.battleCondition && penaltyTemplet.FiercePenaltyType == FiercePenalty.DEBUFF)
							{
								if (dictionary.ContainsKey(penaltyTemplet.PenaltyGroupID))
								{
									if (dictionary[penaltyTemplet.PenaltyGroupID].Find((NKMFiercePenaltyTemplet e) => e.battleCondition == penaltyTemplet.battleCondition) == null)
									{
										dictionary[penaltyTemplet.PenaltyGroupID].Add(penaltyTemplet);
									}
								}
								else
								{
									dictionary.Add(penaltyTemplet.PenaltyGroupID, new List<NKMFiercePenaltyTemplet>
									{
										penaltyTemplet
									});
								}
							}
						}
					}
				}
			}
			foreach (KeyValuePair<int, List<NKMFiercePenaltyTemplet>> keyValuePair2 in dictionary)
			{
				NKCUIFierceBattleSelfPenaltyContent penaltyContentSlot = this.GetPenaltyContentSlot(FiercePenalty.DEBUFF);
				if (penaltyContentSlot != null)
				{
					penaltyContentSlot.SetData(keyValuePair2.Value, new OnClickPenalty(this.OnClickPenaltySlot));
					this.m_lstPenaltyContests.Add(penaltyContentSlot);
				}
			}
		}

		// Token: 0x06008B1C RID: 35612 RVA: 0x002F4E90 File Offset: 0x002F3090
		private NKCUIFierceBattleSelfPenaltyContent GetPenaltyContentSlot(FiercePenalty type)
		{
			NKCUIFierceBattleSelfPenaltyContent nkcuifierceBattleSelfPenaltyContent = UnityEngine.Object.Instantiate<NKCUIFierceBattleSelfPenaltyContent>(this.m_pfbSlot);
			if (null != nkcuifierceBattleSelfPenaltyContent)
			{
				nkcuifierceBattleSelfPenaltyContent.Init(this.m_rtDebuffSlotsParents);
			}
			return nkcuifierceBattleSelfPenaltyContent;
		}

		// Token: 0x06008B1D RID: 35613 RVA: 0x002F4EC0 File Offset: 0x002F30C0
		public void OnClickPenaltySlot(NKMFiercePenaltyTemplet penaltyTemplet)
		{
			if (penaltyTemplet == null)
			{
				return;
			}
			bool flag = false;
			if (this.m_lstPenaltySum.Count > 0)
			{
				for (int i = 0; i < this.m_lstPenaltySum.Count; i++)
				{
					if (this.m_lstPenaltySum[i].PenaltyTemplet.Key == penaltyTemplet.Key)
					{
						UnityEngine.Object.Destroy(this.m_lstPenaltySum[i].gameObject);
						this.m_lstPenaltySum[i] = null;
						this.m_lstPenaltySum.RemoveAt(i);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					for (int j = 0; j < this.m_lstPenaltySum.Count; j++)
					{
						if (this.m_lstPenaltySum[j].PenaltyTemplet.PenaltyGroupID == penaltyTemplet.PenaltyGroupID)
						{
							this.m_lstPenaltySum[j].SetData(penaltyTemplet);
							flag = true;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				NKCUIFierceBattleSelfPenaltySumSlot nkcuifierceBattleSelfPenaltySumSlot = this.AddPenaltySumSlot(penaltyTemplet);
				if (null != nkcuifierceBattleSelfPenaltySumSlot)
				{
					this.m_lstPenaltySum.Add(nkcuifierceBattleSelfPenaltySumSlot);
				}
			}
			this.UpdateRightUI();
		}

		// Token: 0x06008B1E RID: 35614 RVA: 0x002F4FC0 File Offset: 0x002F31C0
		private void ClearCurResultSlots()
		{
			for (int i = 0; i < this.m_lstPenaltySum.Count; i++)
			{
				if (null != this.m_lstPenaltySum[i])
				{
					UnityEngine.Object.Destroy(this.m_lstPenaltySum[i].gameObject);
					this.m_lstPenaltySum[i] = null;
				}
			}
			this.m_lstPenaltySum.Clear();
		}

		// Token: 0x06008B1F RID: 35615 RVA: 0x002F5028 File Offset: 0x002F3228
		private NKCUIFierceBattleSelfPenaltySumSlot AddPenaltySumSlot(NKMFiercePenaltyTemplet penaltyTemplet)
		{
			if (penaltyTemplet != null)
			{
				NKCUIFierceBattleSelfPenaltySumSlot nkcuifierceBattleSelfPenaltySumSlot = UnityEngine.Object.Instantiate<NKCUIFierceBattleSelfPenaltySumSlot>(this.m_pfbResultSlot);
				if (null != nkcuifierceBattleSelfPenaltySumSlot)
				{
					nkcuifierceBattleSelfPenaltySumSlot.SetData(penaltyTemplet);
					nkcuifierceBattleSelfPenaltySumSlot.transform.SetParent(this.m_rtResultSlotParent);
					return nkcuifierceBattleSelfPenaltySumSlot;
				}
			}
			Debug.LogError("<color=red>Fail - NKCPopupFierceBattleSelfPenalty::AddPenaltySumSlot</color>");
			return null;
		}

		// Token: 0x06008B20 RID: 35616 RVA: 0x002F5074 File Offset: 0x002F3274
		private void UpdateRightUI()
		{
			float num = 0f;
			foreach (NKCUIFierceBattleSelfPenaltySumSlot nkcuifierceBattleSelfPenaltySumSlot in this.m_lstPenaltySum)
			{
				num += nkcuifierceBattleSelfPenaltySumSlot.PenaltyTemplet.FierceScoreRate;
			}
			NKCUtil.SetGameobjectActive(this.m_lbTotalDesc.gameObject, num != 0f);
			num *= 0.01f;
			if (num >= 0f)
			{
				NKCUtil.SetLabelText(this.m_lbTotalDesc, NKCUtilString.GET_STRING_FIERCE_PENALTY_BUFF_POINT);
				NKCUtil.SetLabelText(this.m_lbTotalPercent, string.Format(NKCUtilString.GET_STRING_FIERCE_PENALTY_SCORE_PLUS, num));
			}
			else
			{
				num *= -1f;
				NKCUtil.SetLabelText(this.m_lbTotalPercent, string.Format(NKCUtilString.GET_STRING_FIERCE_PENALTY_SCORE_MINUS, num));
				NKCUtil.SetLabelText(this.m_lbTotalDesc, NKCUtilString.GET_STRING_FIERCE_PENALTY_DEBUFF_POINT);
			}
			NKCUtil.SetGameobjectActive(this.m_objNoneResult, this.m_lstPenaltySum.Count <= 0);
		}

		// Token: 0x06008B21 RID: 35617 RVA: 0x002F517C File Offset: 0x002F337C
		public void Open(int FierceID, UnityAction callback)
		{
			this.m_dCallBack = callback;
			this.UpdateUIWhenOpend();
			this.UpdateUI();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (!NKCPopupFierceBattleSelfPenalty.m_Instance.IsOpen)
			{
				base.UIOpened(true);
			}
		}

		// Token: 0x06008B22 RID: 35618 RVA: 0x002F51B0 File Offset: 0x002F33B0
		private void UpdateUIWhenOpend()
		{
			List<NKMFiercePenaltyTemplet> list = this.LoadSelfPenaltyData();
			if (list != null)
			{
				foreach (NKCUIFierceBattleSelfPenaltyContent nkcuifierceBattleSelfPenaltyContent in this.m_lstPenaltyContests)
				{
					nkcuifierceBattleSelfPenaltyContent.UnCheckChildSlots();
					foreach (NKMFiercePenaltyTemplet nkmfiercePenaltyTemplet in list)
					{
						if (nkcuifierceBattleSelfPenaltyContent.PenaltyGroupID == nkmfiercePenaltyTemplet.PenaltyGroupID)
						{
							nkcuifierceBattleSelfPenaltyContent.SelectChildSlot(nkmfiercePenaltyTemplet.Key);
						}
					}
				}
				this.ClearCurResultSlots();
				foreach (NKMFiercePenaltyTemplet penaltyTemplet in list)
				{
					NKCUIFierceBattleSelfPenaltySumSlot nkcuifierceBattleSelfPenaltySumSlot = this.AddPenaltySumSlot(penaltyTemplet);
					if (null != nkcuifierceBattleSelfPenaltySumSlot)
					{
						this.m_lstPenaltySum.Add(nkcuifierceBattleSelfPenaltySumSlot);
					}
				}
			}
			this.UpdateBossData();
		}

		// Token: 0x06008B23 RID: 35619 RVA: 0x002F52C8 File Offset: 0x002F34C8
		private void UpdateBossData()
		{
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr == null)
			{
				return;
			}
			int curBossGroupID = nkcfierceBattleSupportDataMgr.CurBossGroupID;
			int curSelectedBossLv = nkcfierceBattleSupportDataMgr.GetCurSelectedBossLv();
			if (NKMFierceBossGroupTemplet.Groups.ContainsKey(curBossGroupID))
			{
				foreach (NKMFierceBossGroupTemplet nkmfierceBossGroupTemplet in NKMFierceBossGroupTemplet.Groups[curBossGroupID])
				{
					if (nkmfierceBossGroupTemplet.Level == curSelectedBossLv)
					{
						Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_fierce_battle_boss_thumbnail", nkmfierceBossGroupTemplet.UI_BossFaceSlot, false);
						NKCUtil.SetImageSprite(this.m_imgBoss, orLoadAssetResource, false);
						break;
					}
				}
			}
			NKCUtil.SetLabelText(this.m_lbBossName, nkcfierceBattleSupportDataMgr.GetCurBossName());
			int curSelectedBossLv2 = nkcfierceBattleSupportDataMgr.GetCurSelectedBossLv(curBossGroupID);
			NKCUtil.SetLabelText(this.m_lbBossLv, string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, curSelectedBossLv2));
		}

		// Token: 0x06008B24 RID: 35620 RVA: 0x002F53A8 File Offset: 0x002F35A8
		private List<NKMFiercePenaltyTemplet> LoadSelfPenaltyData()
		{
			List<NKMFiercePenaltyTemplet> list = new List<NKMFiercePenaltyTemplet>();
			List<int> selfPenalty = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr().GetSelfPenalty();
			if (selfPenalty != null && selfPenalty.Count > 0)
			{
				foreach (int key in selfPenalty)
				{
					NKMFiercePenaltyTemplet nkmfiercePenaltyTemplet = NKMTempletContainer<NKMFiercePenaltyTemplet>.Find(key);
					if (nkmfiercePenaltyTemplet != null)
					{
						list.Add(nkmfiercePenaltyTemplet);
					}
				}
			}
			return list;
		}

		// Token: 0x06008B25 RID: 35621 RVA: 0x002F5424 File Offset: 0x002F3624
		private void UnSelectAllSotUI()
		{
			foreach (NKCUIFierceBattleSelfPenaltyContent nkcuifierceBattleSelfPenaltyContent in this.m_lstPenaltyContests)
			{
				nkcuifierceBattleSelfPenaltyContent.UnCheckChildSlots();
			}
			this.ClearCurResultSlots();
		}

		// Token: 0x06008B26 RID: 35622 RVA: 0x002F547C File Offset: 0x002F367C
		private void UpdateUI()
		{
			NKCUtil.SetLabelText(this.m_lbSelectDesc, NKCUtilString.GET_STRING_FIERCE_POPUP_SELF_PENALTY_DEBUFF);
			this.UpdateSlotUI();
			this.UpdateRightUI();
		}

		// Token: 0x06008B27 RID: 35623 RVA: 0x002F549C File Offset: 0x002F369C
		private void UpdateSlotUI()
		{
			foreach (NKCUIFierceBattleSelfPenaltySumSlot nkcuifierceBattleSelfPenaltySumSlot in this.m_lstPenaltySum)
			{
				NKCUtil.SetGameobjectActive(nkcuifierceBattleSelfPenaltySumSlot.gameObject, true);
			}
		}

		// Token: 0x06008B28 RID: 35624 RVA: 0x002F54F4 File Offset: 0x002F36F4
		private void OnClickConfirm()
		{
			this.SavePenaltyData();
			UnityAction dCallBack = this.m_dCallBack;
			if (dCallBack != null)
			{
				dCallBack();
			}
			base.Close();
		}

		// Token: 0x06008B29 RID: 35625 RVA: 0x002F5514 File Offset: 0x002F3714
		private void SavePenaltyData()
		{
			List<int> list = new List<int>();
			foreach (NKCUIFierceBattleSelfPenaltySumSlot nkcuifierceBattleSelfPenaltySumSlot in this.m_lstPenaltySum)
			{
				list.Add(nkcuifierceBattleSelfPenaltySumSlot.PenaltyTemplet.Key);
			}
			NKCFierceBattleSupportDataMgr nkcfierceBattleSupportDataMgr = NKCScenManager.GetScenManager().GetNKCFierceBattleSupportDataMgr();
			if (nkcfierceBattleSupportDataMgr != null)
			{
				nkcfierceBattleSupportDataMgr.SetSelfPenalty(list);
			}
		}

		// Token: 0x06008B2A RID: 35626 RVA: 0x002F558C File Offset: 0x002F378C
		private void OnClickReset()
		{
			if (this.m_lstPenaltyContests != null)
			{
				foreach (NKCUIFierceBattleSelfPenaltyContent nkcuifierceBattleSelfPenaltyContent in this.m_lstPenaltyContests)
				{
					nkcuifierceBattleSelfPenaltyContent.UnCheckChildSlots();
				}
			}
			this.ClearCurResultSlots();
			this.UpdateRightUI();
		}

		// Token: 0x040077F4 RID: 30708
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_fierce_battle";

		// Token: 0x040077F5 RID: 30709
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_FIERCE_BATTLE_PEN";

		// Token: 0x040077F6 RID: 30710
		private static NKCPopupFierceBattleSelfPenalty m_Instance;

		// Token: 0x040077F7 RID: 30711
		[Header("Left Menu")]
		public Image m_imgBoss;

		// Token: 0x040077F8 RID: 30712
		public Text m_lbBossLv;

		// Token: 0x040077F9 RID: 30713
		public Text m_lbBossName;

		// Token: 0x040077FA RID: 30714
		public Text m_lbSelectDesc;

		// Token: 0x040077FB RID: 30715
		public NKCUIComStateButton m_csbtnReset;

		// Token: 0x040077FC RID: 30716
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x040077FD RID: 30717
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x040077FE RID: 30718
		[Header("옵션")]
		public NKCUIFierceBattleSelfPenaltyContent m_pfbSlot;

		// Token: 0x040077FF RID: 30719
		public RectTransform m_rtDebuffSlotsParents;

		// Token: 0x04007800 RID: 30720
		[Header("결과")]
		public RectTransform m_rtResultSlotParent;

		// Token: 0x04007801 RID: 30721
		public GameObject m_objNoneResult;

		// Token: 0x04007802 RID: 30722
		public Text m_lbTotalPercent;

		// Token: 0x04007803 RID: 30723
		public Text m_lbTotalDesc;

		// Token: 0x04007804 RID: 30724
		public NKCUIFierceBattleSelfPenaltySumSlot m_pfbResultSlot;

		// Token: 0x04007805 RID: 30725
		private List<NKCUIFierceBattleSelfPenaltyContent> m_lstPenaltyContests = new List<NKCUIFierceBattleSelfPenaltyContent>();

		// Token: 0x04007806 RID: 30726
		private List<NKCUIFierceBattleSelfPenaltySumSlot> m_lstPenaltySum = new List<NKCUIFierceBattleSelfPenaltySumSlot>();

		// Token: 0x04007807 RID: 30727
		private UnityAction m_dCallBack;
	}
}
