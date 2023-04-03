using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Event;
using NKM.Templet;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Module
{
	// Token: 0x02000B1F RID: 2847
	public class NKCUIModuleHome : NKCUIBase
	{
		// Token: 0x060081AC RID: 33196 RVA: 0x002BB812 File Offset: 0x002B9A12
		public static NKCUIModuleHome MakeInstance(string bundleName, string assetName)
		{
			NKCUIModuleHome instance = NKCUIManager.OpenNewInstance<NKCUIModuleHome>(bundleName, assetName, NKCUIManager.eUIBaseRect.UIFrontCommon, null).GetInstance<NKCUIModuleHome>();
			instance.Init();
			return instance;
		}

		// Token: 0x060081AD RID: 33197 RVA: 0x002BB828 File Offset: 0x002B9A28
		public static NKCUIModuleHome OpenEventModule(NKMEventCollectionIndexTemplet eventCollectionIndexTemplet)
		{
			if (eventCollectionIndexTemplet == null || !eventCollectionIndexTemplet.IsOpen)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_MENU_EXCEPTION_EVENT_EXPIRED_POPUP", false), null, "");
				return null;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(eventCollectionIndexTemplet.EventPrefabId, eventCollectionIndexTemplet.EventPrefabId);
			NKCUIManager.LoadedUIData instance = NKCUIManager.GetInstance(nkmassetName.m_AssetName.ToLower(), nkmassetName.m_AssetName, false);
			NKCUIModuleHome nkcuimoduleHome;
			if (instance == null)
			{
				nkcuimoduleHome = NKCUIModuleHome.MakeInstance(nkmassetName.m_AssetName, nkmassetName.m_AssetName);
			}
			else
			{
				nkcuimoduleHome = (instance.GetInstance() as NKCUIModuleHome);
			}
			if (nkcuimoduleHome == null)
			{
				if (instance != null)
				{
					instance.CloseInstance();
				}
				return null;
			}
			nkcuimoduleHome.Open(eventCollectionIndexTemplet, 0);
			return nkcuimoduleHome;
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x060081AE RID: 33198 RVA: 0x002BB8C9 File Offset: 0x002B9AC9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return this.m_eUIType;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x060081AF RID: 33199 RVA: 0x002BB8D1 File Offset: 0x002B9AD1
		public override string MenuName
		{
			get
			{
				return this.m_menuName;
			}
		}

		// Token: 0x060081B0 RID: 33200 RVA: 0x002BB8DC File Offset: 0x002B9ADC
		public override void CloseInternal()
		{
			foreach (KeyValuePair<int, NKCUIModuleSubUIBase> keyValuePair in this.m_dicSubUI)
			{
				if (null != keyValuePair.Value)
				{
					keyValuePair.Value.OnClose();
				}
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x002BB950 File Offset: 0x002B9B50
		public override void UnHide()
		{
			base.UnHide();
			foreach (KeyValuePair<int, NKCUIModuleSubUIBase> keyValuePair in this.m_dicSubUI)
			{
				keyValuePair.Value.UnHide();
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x060081B2 RID: 33202 RVA: 0x002BB9B0 File Offset: 0x002B9BB0
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x060081B3 RID: 33203 RVA: 0x002BB9B3 File Offset: 0x002B9BB3
		public NKMEventCollectionIndexTemplet EventCollectionIndexTemplet
		{
			get
			{
				return this.m_eventCollectionIndexTemplet;
			}
		}

		// Token: 0x060081B4 RID: 33204 RVA: 0x002BB9BB File Offset: 0x002B9BBB
		public override void Initialize()
		{
			this.Init();
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x002BB9C4 File Offset: 0x002B9BC4
		private void Init()
		{
			foreach (NKCUIModuleSubUIBase nkcuimoduleSubUIBase in base.GetComponentsInChildren<NKCUIModuleSubUIBase>(true))
			{
				if (this.m_dicSubUI.ContainsKey(nkcuimoduleSubUIBase.ModuleID))
				{
					NKCUtil.SetGameobjectActive(nkcuimoduleSubUIBase, false);
					Log.Error(string.Format("Module of same ID {0} exist!", nkcuimoduleSubUIBase.ModuleID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Module/NKCUIModuleHome.cs", 117);
				}
				else
				{
					nkcuimoduleSubUIBase.Init();
					this.m_dicSubUI.Add(nkcuimoduleSubUIBase.ModuleID, nkcuimoduleSubUIBase);
				}
			}
			foreach (NKCUIComToggle nkcuicomToggle in this.m_lstTabButtons)
			{
				if (!(nkcuicomToggle == null))
				{
					nkcuicomToggle.OnValueChangedWithData = new NKCUIComToggle.ValueChangedWithData(this.OnSelectTab);
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x002BBAB4 File Offset: 0x002B9CB4
		private void OnSelectTab(bool value, int tabID)
		{
			if (!value)
			{
				return;
			}
			this.SelectTab(tabID);
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x002BBAC4 File Offset: 0x002B9CC4
		private void SelectTab(int tabID)
		{
			foreach (KeyValuePair<int, NKCUIModuleSubUIBase> keyValuePair in this.m_dicSubUI)
			{
				bool flag = keyValuePair.Key == tabID;
				NKCUIModuleSubUIBase value = keyValuePair.Value;
				if (!flag && value.gameObject.activeInHierarchy)
				{
					value.OnClose();
				}
				NKCUtil.SetGameobjectActive(value, flag);
				if (flag)
				{
					value.OnOpen(this.m_eventCollectionIndexTemplet);
				}
			}
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x002BBB50 File Offset: 0x002B9D50
		public override void OpenByShortcut(Dictionary<string, string> dicParam)
		{
			int intValue = NKCUtil.GetIntValue(dicParam, "ID", -1);
			NKMEventCollectionIndexTemplet templet;
			if (intValue > 0)
			{
				templet = NKMEventCollectionIndexTemplet.Find(intValue);
			}
			else
			{
				templet = NKMEventCollectionIndexTemplet.GetEventCollectionIndexTemplet(NKCSynchronizedTime.ServiceTime);
			}
			int intValue2 = NKCUtil.GetIntValue(dicParam, "TabID", 0);
			this.Open(templet, intValue2);
		}

		// Token: 0x060081B9 RID: 33209 RVA: 0x002BBB98 File Offset: 0x002B9D98
		public void Open(NKMEventCollectionIndexTemplet templet, int tabID = 0)
		{
			this.m_eventCollectionIndexTemplet = templet;
			List<NKCUIComToggle> lstTabButtons = this.m_lstTabButtons;
			NKCUIComToggle nkcuicomToggle = (lstTabButtons != null) ? lstTabButtons.Find((NKCUIComToggle e) => e.m_DataInt == tabID) : null;
			if (nkcuicomToggle != null)
			{
				nkcuicomToggle.Select(true, false, false);
			}
			this.PlayEventBGM();
			NKMIntervalTemplet nkmintervalTemplet = null;
			if (templet != null)
			{
				nkmintervalTemplet = NKMIntervalTemplet.Find(templet.DateStrId);
			}
			if (nkmintervalTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_lbEventTimeLeft, true);
				if (NKCSynchronizedTime.GetTimeLeft(NKMTime.LocalToUTC(nkmintervalTemplet.EndDate, 0)).TotalDays > (double)NKCSynchronizedTime.UNLIMITD_REMAIN_DAYS)
				{
					NKCUtil.SetLabelText(this.m_lbEventTimeLeft, NKCUtilString.GET_STRING_EVENT_DATE_UNLIMITED_TEXT);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbEventTimeLeft, NKCUtilString.GetTimeIntervalString(nkmintervalTemplet.StartDate, nkmintervalTemplet.EndDate, NKMTime.INTERVAL_FROM_UTC, false));
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_lbEventTimeLeft, false);
			}
			base.UIOpened(true);
		}

		// Token: 0x060081BA RID: 33210 RVA: 0x002BBC78 File Offset: 0x002B9E78
		public void PlayEventBGM()
		{
			if (this.m_eventCollectionIndexTemplet == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.m_eventCollectionIndexTemplet.BgmAssetId))
			{
				NKCSoundManager.PlayMusic(this.m_eventCollectionIndexTemplet.BgmAssetId, true, (float)this.m_eventCollectionIndexTemplet.BgmVolume * 0.01f, true, 0f, 0f);
			}
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x002BBCCE File Offset: 0x002B9ECE
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData == null)
			{
				return;
			}
			this.UpdateUI();
		}

		// Token: 0x060081BC RID: 33212 RVA: 0x002BBCDC File Offset: 0x002B9EDC
		public void UpdateUI()
		{
			foreach (KeyValuePair<int, NKCUIModuleSubUIBase> keyValuePair in this.m_dicSubUI)
			{
				keyValuePair.Value.Refresh();
			}
		}

		// Token: 0x060081BD RID: 33213 RVA: 0x002BBD34 File Offset: 0x002B9F34
		public IEnumerable<T> GetSubUIs<T>() where T : NKCUIModuleSubUIBase
		{
			foreach (KeyValuePair<int, NKCUIModuleSubUIBase> keyValuePair in this.m_dicSubUI)
			{
				if (keyValuePair.Value is T)
				{
					yield return keyValuePair.Value as T;
				}
			}
			Dictionary<int, NKCUIModuleSubUIBase>.Enumerator enumerator = default(Dictionary<int, NKCUIModuleSubUIBase>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060081BE RID: 33214 RVA: 0x002BBD44 File Offset: 0x002B9F44
		public static void PlayBGMMusic()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_HOME)
			{
				using (List<NKCUIModuleHome>.Enumerator enumerator = NKCUIManager.GetOpenedUIsByType<NKCUIModuleHome>().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						enumerator.Current.PlayEventBGM();
					}
				}
			}
		}

		// Token: 0x04006DE2 RID: 28130
		private NKMEventCollectionIndexTemplet m_eventCollectionIndexTemplet;

		// Token: 0x04006DE3 RID: 28131
		private NKCUIBase.eMenutype m_eUIType;

		// Token: 0x04006DE4 RID: 28132
		private string m_menuName = string.Empty;

		// Token: 0x04006DE5 RID: 28133
		private Dictionary<int, NKCUIModuleSubUIBase> m_dicSubUI = new Dictionary<int, NKCUIModuleSubUIBase>();

		// Token: 0x04006DE6 RID: 28134
		public List<NKCUIComToggle> m_lstTabButtons;

		// Token: 0x04006DE7 RID: 28135
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006DE8 RID: 28136
		public Text m_lbEventTimeLeft;
	}
}
