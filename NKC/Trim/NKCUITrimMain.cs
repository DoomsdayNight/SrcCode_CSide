using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AAE RID: 2734
	public class NKCUITrimMain : NKCUIBase
	{
		// Token: 0x060079AB RID: 31147 RVA: 0x00287DD2 File Offset: 0x00285FD2
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			if (!NKCUIManager.IsValid(NKCUITrimMain.s_LoadedUIData))
			{
				NKCUITrimMain.s_LoadedUIData = NKCUIManager.OpenNewInstanceAsync<NKCUITrimMain>("ab_ui_trim", "AB_UI_TRIM", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUITrimMain.CleanupInstance));
			}
			return NKCUITrimMain.s_LoadedUIData;
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060079AC RID: 31148 RVA: 0x00287E06 File Offset: 0x00286006
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUITrimMain.s_LoadedUIData != null && NKCUITrimMain.s_LoadedUIData.IsUIOpen;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060079AD RID: 31149 RVA: 0x00287E1B File Offset: 0x0028601B
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUITrimMain.s_LoadedUIData != null && NKCUITrimMain.s_LoadedUIData.IsLoadComplete;
			}
		}

		// Token: 0x060079AE RID: 31150 RVA: 0x00287E30 File Offset: 0x00286030
		public static NKCUITrimMain GetInstance()
		{
			if (NKCUITrimMain.s_LoadedUIData != null && NKCUITrimMain.s_LoadedUIData.IsLoadComplete)
			{
				return NKCUITrimMain.s_LoadedUIData.GetInstance<NKCUITrimMain>();
			}
			return null;
		}

		// Token: 0x060079AF RID: 31151 RVA: 0x00287E51 File Offset: 0x00286051
		public static void CleanupInstance()
		{
			NKCUITrimMain.s_LoadedUIData = null;
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060079B0 RID: 31152 RVA: 0x00287E59 File Offset: 0x00286059
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x060079B1 RID: 31153 RVA: 0x00287E5C File Offset: 0x0028605C
		public override string MenuName
		{
			get
			{
				return "TRIM";
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x060079B2 RID: 31154 RVA: 0x00287E63 File Offset: 0x00286063
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x060079B3 RID: 31155 RVA: 0x00287E66 File Offset: 0x00286066
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_TRIM_INFO";
			}
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x00287E6D File Offset: 0x0028606D
		public override void CloseInternal()
		{
			NKCSoundManager.PlayScenMusic();
			this.m_trimIdList.Clear();
			base.gameObject.SetActive(false);
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x00287E8B File Offset: 0x0028608B
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x00287E94 File Offset: 0x00286094
		public override void OnBackButton()
		{
			if (this.m_eTrimState == NKCUITrimMain.TrimState.Selected)
			{
				this.SetToEntryState();
				return;
			}
			base.OnBackButton();
			NKCScenManager scenManager = NKCScenManager.GetScenManager();
			if (scenManager != null)
			{
				scenManager.Get_SCEN_OPERATION().SetReservedEpisodeCategory(EPISODE_CATEGORY.EC_TRIM);
			}
			NKCScenManager scenManager2 = NKCScenManager.GetScenManager();
			if (scenManager2 == null)
			{
				return;
			}
			scenManager2.ScenChangeFade(NKM_SCEN_ID.NSI_OPERATION, false);
		}

		// Token: 0x060079B7 RID: 31159 RVA: 0x00287EE0 File Offset: 0x002860E0
		public void Init()
		{
			if (this.m_trimSlot != null)
			{
				int num = this.m_trimSlot.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUITrimSlot nkcuitrimSlot = this.m_trimSlot[i];
					if (nkcuitrimSlot != null)
					{
						nkcuitrimSlot.Init(i, new NKCUITrimSlot.OnClick(this.OnClickTrimSlot));
					}
				}
			}
			NKCUITrimMainInfo trimMainInfo = this.m_TrimMainInfo;
			if (trimMainInfo != null)
			{
				trimMainInfo.Init();
			}
			this.m_aniEnter.keepAnimatorControllerStateOnDisable = true;
			this.m_aniInfo.keepAnimatorControllerStateOnDisable = true;
		}

		// Token: 0x060079B8 RID: 31160 RVA: 0x00287F54 File Offset: 0x00286154
		public void Open(NKMTrimIntervalTemplet trimIntervalTemplet, int prevTrimId)
		{
			if (trimIntervalTemplet == null)
			{
				return;
			}
			this.m_trimIdList.Clear();
			base.gameObject.SetActive(true);
			int[] array = null;
			if (trimIntervalTemplet != null)
			{
				array = trimIntervalTemplet.TrimSlot;
			}
			if (array != null)
			{
				int num = this.m_trimSlot.Length;
				for (int i = 0; i < num; i++)
				{
					if (array.Length <= i || array[i] <= 0)
					{
						this.m_trimSlot[i].SetActive(false);
					}
					else
					{
						this.m_trimSlot[i].SetActive(true);
						this.m_trimSlot[i].SetData(array[i]);
						this.m_trimSlot[i].SetSlotState(NKCUITrimSlot.SlotState.Default);
						this.m_trimIdList.Add(array[i]);
					}
				}
			}
			this.m_eTrimState = NKCUITrimMain.TrimState.Entry;
			this.m_selectedIndex = -1;
			this.m_dateUpdateTimerSec = 0f;
			this.m_dateUpdateTimerMin = 0f;
			string remainTimeStringExWithoutEnd = NKCUtilString.GetRemainTimeStringExWithoutEnd(NKCUITrimUtility.GetRemainDateMsg());
			NKCUtil.SetLabelText(this.m_lbRemainData, remainTimeStringExWithoutEnd);
			NKCSoundManager.PlayMusic(this.m_bgmName, true, 1f, false, 0f, 0f);
			base.UIOpened(true);
			NKCTutorialManager.TutorialRequired(TutorialPoint.TrimEntry, true);
			this.SetTrimSelectState(prevTrimId);
		}

		// Token: 0x060079B9 RID: 31161 RVA: 0x00288064 File Offset: 0x00286264
		public override bool OnHotkey(HotkeyEventType hotkey)
		{
			int count = this.m_trimIdList.Count;
			if (count == 0)
			{
				return false;
			}
			if (hotkey != HotkeyEventType.PrevTab)
			{
				if (hotkey != HotkeyEventType.NextTab)
				{
					return false;
				}
				if (this.m_eTrimState == NKCUITrimMain.TrimState.Entry)
				{
					this.m_selectedIndex = 0;
					if (this.m_selectedIndex < this.m_trimIdList.Count)
					{
						this.OnClickTrimSlot(this.m_selectedIndex, this.m_trimIdList[this.m_selectedIndex]);
					}
					return true;
				}
				this.m_selectedIndex = (this.m_selectedIndex + 1) % count;
				if (this.m_selectedIndex < this.m_trimIdList.Count)
				{
					this.OnClickTrimSlot(this.m_selectedIndex, this.m_trimIdList[this.m_selectedIndex]);
				}
				return true;
			}
			else
			{
				if (this.m_eTrimState == NKCUITrimMain.TrimState.Entry)
				{
					this.m_selectedIndex = 0;
					if (this.m_selectedIndex < this.m_trimIdList.Count)
					{
						this.OnClickTrimSlot(this.m_selectedIndex, this.m_trimIdList[this.m_selectedIndex]);
					}
					return true;
				}
				this.m_selectedIndex = (this.m_selectedIndex - 1 + count) % count;
				if (this.m_selectedIndex < this.m_trimIdList.Count)
				{
					this.OnClickTrimSlot(this.m_selectedIndex, this.m_trimIdList[this.m_selectedIndex]);
				}
				return true;
			}
		}

		// Token: 0x060079BA RID: 31162 RVA: 0x0028819C File Offset: 0x0028639C
		public void RefreshUI()
		{
			if (base.IsHidden)
			{
				return;
			}
			if (this.m_trimSlot != null)
			{
				int count = this.m_trimIdList.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.m_trimSlot.Length > i)
					{
						this.m_trimSlot[i].SetData(this.m_trimIdList[i]);
					}
				}
			}
			if (this.m_selectedIndex >= 0 && this.m_trimIdList.Count > this.m_selectedIndex)
			{
				NKCUITrimMainInfo trimMainInfo = this.m_TrimMainInfo;
				if (trimMainInfo == null)
				{
					return;
				}
				trimMainInfo.SetData(this.m_trimIdList[this.m_selectedIndex]);
			}
		}

		// Token: 0x060079BB RID: 31163 RVA: 0x00288234 File Offset: 0x00286434
		public override void UnHide()
		{
			base.UnHide();
			this.RefreshUI();
			if (this.m_bShowIntervalTime)
			{
				string remainTimeStringExWithoutEnd = NKCUtilString.GetRemainTimeStringExWithoutEnd(NKCUITrimUtility.GetRemainDateMsg());
				NKCUtil.SetLabelText(this.m_lbRemainData, remainTimeStringExWithoutEnd);
			}
		}

		// Token: 0x060079BC RID: 31164 RVA: 0x0028826C File Offset: 0x0028646C
		private void Update()
		{
			if (!this.m_bShowIntervalTime)
			{
				return;
			}
			if (this.m_dateUpdateTimerSec > 1f)
			{
				DateTime remainDateMsg = NKCUITrimUtility.GetRemainDateMsg();
				if (NKCSynchronizedTime.GetTimeLeft(remainDateMsg).TotalMinutes >= 1.0 && this.m_dateUpdateTimerMin < 60f)
				{
					this.m_dateUpdateTimerSec = 0f;
					return;
				}
				string remainTimeStringExWithoutEnd = NKCUtilString.GetRemainTimeStringExWithoutEnd(remainDateMsg);
				NKCUtil.SetLabelText(this.m_lbRemainData, remainTimeStringExWithoutEnd);
				this.m_dateUpdateTimerSec = 0f;
				this.m_dateUpdateTimerMin = 0f;
			}
			this.m_dateUpdateTimerSec += Time.deltaTime;
			this.m_dateUpdateTimerMin += Time.deltaTime;
		}

		// Token: 0x060079BD RID: 31165 RVA: 0x00288314 File Offset: 0x00286514
		private void SetToEntryState()
		{
			if (this.m_eTrimState == NKCUITrimMain.TrimState.Entry)
			{
				return;
			}
			if (this.m_trimSlot != null)
			{
				int num = this.m_trimSlot.Length;
				for (int i = 0; i < num; i++)
				{
					NKCUITrimSlot nkcuitrimSlot = this.m_trimSlot[i];
					if (nkcuitrimSlot != null)
					{
						nkcuitrimSlot.SetSlotState(NKCUITrimSlot.SlotState.Default);
					}
				}
			}
			this.m_aniInfo.Play(this.m_aniInfo_02_to_01);
			this.m_eTrimState = NKCUITrimMain.TrimState.Entry;
			this.m_selectedIndex = -1;
		}

		// Token: 0x060079BE RID: 31166 RVA: 0x0028837A File Offset: 0x0028657A
		private void SetToSelectedState(int slotIndex, int trimId)
		{
			this.SetTrimInfo(slotIndex, trimId);
			this.m_aniInfo.Play(this.m_aniInfo_01_to_02);
			this.m_eTrimState = NKCUITrimMain.TrimState.Selected;
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x0028839C File Offset: 0x0028659C
		private void SetTrimInfo(int slotIndex, int trimId)
		{
			NKCUITrimMainInfo trimMainInfo = this.m_TrimMainInfo;
			if (trimMainInfo != null)
			{
				trimMainInfo.SetData(trimId);
			}
			if (this.m_trimSlot != null)
			{
				int num = this.m_trimSlot.Length;
				for (int i = 0; i < num; i++)
				{
					bool flag = slotIndex == i;
					NKCUITrimSlot nkcuitrimSlot = this.m_trimSlot[i];
					if (nkcuitrimSlot != null)
					{
						nkcuitrimSlot.SetSlotState(flag ? NKCUITrimSlot.SlotState.Selected : NKCUITrimSlot.SlotState.Disable);
					}
				}
			}
		}

		// Token: 0x060079C0 RID: 31168 RVA: 0x002883F8 File Offset: 0x002865F8
		private void SetTrimSelectState(int prevTrimId)
		{
			if (prevTrimId <= 0 || this.m_trimSlot == null)
			{
				return;
			}
			int num = this.m_trimSlot.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_trimSlot[i].IsActive())
				{
					this.m_trimSlot[i].LetChangeClickState(prevTrimId);
				}
			}
			this.m_aniEnter.Play(this.m_aniEnterIdle);
			this.m_aniInfo.Play(this.m_aniInfo_02);
			if (this.m_eTrimState == NKCUITrimMain.TrimState.Selected)
			{
				NKCUITrimMainInfo trimMainInfo = this.m_TrimMainInfo;
				if (trimMainInfo == null)
				{
					return;
				}
				trimMainInfo.OnClickEnter();
			}
		}

		// Token: 0x060079C1 RID: 31169 RVA: 0x0028847F File Offset: 0x0028667F
		private IEnumerator WaitingTrimInfoOpenSequece()
		{
			while (this.m_aniInfo.GetCurrentAnimatorStateInfo(0).IsName(this.m_aniInfo_01_to_02))
			{
				yield return null;
			}
			this.m_TrimMainInfo.OnClickEnter();
			yield break;
		}

		// Token: 0x060079C2 RID: 31170 RVA: 0x00288490 File Offset: 0x00286690
		private void OnClickTrimSlot(int slotIndex, int trimId)
		{
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimId);
			if (nkmtrimTemplet == null)
			{
				this.SetToEntryState();
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				this.SetToEntryState();
				return;
			}
			if (!NKMContentUnlockManager.IsContentUnlocked(nkmuserData, nkmtrimTemplet.m_UnlockInfo, false))
			{
				string message = NKCContentManager.MakeUnlockConditionString(nkmtrimTemplet.m_UnlockInfo, false);
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(message, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				this.SetToEntryState();
				return;
			}
			this.m_bShowIntervalTime = nkmtrimTemplet.ShowInterval;
			NKCUtil.SetGameobjectActive(this.m_objRemainTime, this.m_bShowIntervalTime);
			this.m_selectedIndex = slotIndex;
			if (this.m_eTrimState == NKCUITrimMain.TrimState.Entry)
			{
				this.SetToSelectedState(slotIndex, trimId);
				return;
			}
			if (this.m_eTrimState == NKCUITrimMain.TrimState.Selected)
			{
				if (this.m_trimSlot[slotIndex] != null && this.m_trimSlot[slotIndex].TrimSlotState == NKCUITrimSlot.SlotState.Selected)
				{
					this.SetToEntryState();
					return;
				}
				this.SetTrimInfo(slotIndex, trimId);
			}
		}

		// Token: 0x04006668 RID: 26216
		private const string ASSET_BUNDLE_NAME = "ab_ui_trim";

		// Token: 0x04006669 RID: 26217
		private const string UI_ASSET_NAME = "AB_UI_TRIM";

		// Token: 0x0400666A RID: 26218
		private static NKCUIManager.LoadedUIData s_LoadedUIData;

		// Token: 0x0400666B RID: 26219
		public Animator m_aniEnter;

		// Token: 0x0400666C RID: 26220
		public Animator m_aniInfo;

		// Token: 0x0400666D RID: 26221
		public string m_aniEnterIdle;

		// Token: 0x0400666E RID: 26222
		public string m_aniInfo_01_to_02;

		// Token: 0x0400666F RID: 26223
		public string m_aniInfo_02_to_01;

		// Token: 0x04006670 RID: 26224
		public string m_aniInfo_02;

		// Token: 0x04006671 RID: 26225
		public NKCUITrimSlot[] m_trimSlot;

		// Token: 0x04006672 RID: 26226
		public NKCUITrimMainInfo m_TrimMainInfo;

		// Token: 0x04006673 RID: 26227
		public GameObject m_objRemainTime;

		// Token: 0x04006674 RID: 26228
		public Text m_lbRemainData;

		// Token: 0x04006675 RID: 26229
		public string m_bgmName;

		// Token: 0x04006676 RID: 26230
		private NKCUITrimMain.TrimState m_eTrimState;

		// Token: 0x04006677 RID: 26231
		private int m_selectedIndex;

		// Token: 0x04006678 RID: 26232
		private List<int> m_trimIdList = new List<int>();

		// Token: 0x04006679 RID: 26233
		private float m_dateUpdateTimerSec;

		// Token: 0x0400667A RID: 26234
		private float m_dateUpdateTimerMin;

		// Token: 0x0400667B RID: 26235
		private bool m_bShowIntervalTime;

		// Token: 0x02001803 RID: 6147
		public enum TrimState
		{
			// Token: 0x0400A7E2 RID: 42978
			Entry,
			// Token: 0x0400A7E3 RID: 42979
			Selected
		}
	}
}
