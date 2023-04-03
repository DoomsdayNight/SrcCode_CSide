using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientPacket.Event;
using NKM;
using NKM.Event;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BCF RID: 3023
	public class NKCUIEvent : NKCUIBase
	{
		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06008C05 RID: 35845 RVA: 0x002F9FA8 File Offset: 0x002F81A8
		public static NKCUIEvent Instance
		{
			get
			{
				if (NKCUIEvent.m_Instance == null)
				{
					NKCUIEvent.m_Instance = NKCUIManager.OpenNewInstance<NKCUIEvent>("ab_ui_nkm_ui_event", "NKM_UI_EVENT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIEvent.CleanupInstance)).GetInstance<NKCUIEvent>();
					NKCUIEvent.m_Instance.Init();
				}
				return NKCUIEvent.m_Instance;
			}
		}

		// Token: 0x06008C06 RID: 35846 RVA: 0x002F9FF7 File Offset: 0x002F81F7
		private static void CleanupInstance()
		{
			NKCUIEvent.m_Instance = null;
		}

		// Token: 0x06008C07 RID: 35847 RVA: 0x002F9FFF File Offset: 0x002F81FF
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			this.ClearPrefabs();
			NKCUIEvent.m_Instance = null;
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06008C08 RID: 35848 RVA: 0x002FA013 File Offset: 0x002F8213
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIEvent.m_Instance != null && NKCUIEvent.m_Instance.IsOpen;
			}
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06008C09 RID: 35849 RVA: 0x002FA02E File Offset: 0x002F822E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06008C0A RID: 35850 RVA: 0x002FA031 File Offset: 0x002F8231
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_EVENT_MENU;
			}
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06008C0B RID: 35851 RVA: 0x002FA038 File Offset: 0x002F8238
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06008C0C RID: 35852 RVA: 0x002FA03B File Offset: 0x002F823B
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06008C0D RID: 35853 RVA: 0x002FA043 File Offset: 0x002F8243
		public int SelectedTabId
		{
			get
			{
				return this.m_SelectedTabID;
			}
		}

		// Token: 0x06008C0E RID: 35854 RVA: 0x002FA04C File Offset: 0x002F824C
		private void Init()
		{
			this.m_loopScrollRect.dOnGetObject += this.GetObject;
			this.m_loopScrollRect.dOnReturnObject += this.ReturnObject;
			this.m_loopScrollRect.dOnProvideData += this.ProvideData;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(base.Close));
			this.m_loopScrollRect.PrepareCells(0);
		}

		// Token: 0x06008C0F RID: 35855 RVA: 0x002FA0C1 File Offset: 0x002F82C1
		private RectTransform GetObject(int index)
		{
			if (this.m_stkEventTabSlot.Count > 0)
			{
				NKCUIEventSlot nkcuieventSlot = this.m_stkEventTabSlot.Pop();
				NKCUtil.SetGameobjectActive(nkcuieventSlot, false);
				return nkcuieventSlot.GetComponent<RectTransform>();
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_pfbMenuSlot);
			NKCUtil.SetGameobjectActive(gameObject, false);
			return gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x06008C10 RID: 35856 RVA: 0x002FA100 File Offset: 0x002F8300
		private void ReturnObject(Transform rt)
		{
			NKCUtil.SetGameobjectActive(rt, false);
			rt.SetParent(base.transform);
			this.m_stkEventTabSlot.Push(rt.GetComponent<NKCUIEventSlot>());
		}

		// Token: 0x06008C11 RID: 35857 RVA: 0x002FA128 File Offset: 0x002F8328
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIEventSlot component = tr.GetComponent<NKCUIEventSlot>();
			if (component == null)
			{
				return;
			}
			if (!this.m_dicEventTab.ContainsKey(this.m_lstEventTabTemplet[idx].m_EventID))
			{
				this.m_dicEventTab.Add(this.m_lstEventTabTemplet[idx].m_EventID, component);
			}
			else
			{
				this.m_dicEventTab[this.m_lstEventTabTemplet[idx].m_EventID] = component;
			}
			bool bSelected = this.m_lstEventTabTemplet[idx].m_EventID == this.m_SelectedTabID;
			component.SetData(this.m_lstEventTabTemplet[idx], this.m_tglGroup, bSelected, new NKCUIEventSlot.OnSelect(this.OnSelectTab));
			component.CheckRedDot();
		}

		// Token: 0x06008C12 RID: 35858 RVA: 0x002FA1E8 File Offset: 0x002F83E8
		public override void CloseInternal()
		{
			this.CloseSubUI();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008C13 RID: 35859 RVA: 0x002FA1FC File Offset: 0x002F83FC
		public void Open(NKMEventTabTemplet reservedTabTemplet = null)
		{
			this.BuildTabs();
			if (this.m_lstEventTabTemplet.Count <= 0)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_EVENT_END, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, "");
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loopScrollRect.TotalCount = this.m_lstEventTabTemplet.Count;
			this.m_loopScrollRect.RefreshCells(false);
			if (reservedTabTemplet == null || !this.m_lstEventTabTemplet.Contains(reservedTabTemplet))
			{
				reservedTabTemplet = this.m_lstEventTabTemplet[0];
			}
			this.OnSelectTab(reservedTabTemplet);
			this.SaveEventPopupOpenInfo();
			base.UIOpened(true);
		}

		// Token: 0x06008C14 RID: 35860 RVA: 0x002FA2B9 File Offset: 0x002F84B9
		private void BuildTabs()
		{
			this.m_lstEventTabTemplet = NKCUIEvent.GetActiveEventTabList();
			this.m_lstEventTabTemplet.Sort(new Comparison<NKMEventTabTemplet>(this.Comparer));
		}

		// Token: 0x06008C15 RID: 35861 RVA: 0x002FA2DD File Offset: 0x002F84DD
		private int Comparer(NKMEventTabTemplet lItem, NKMEventTabTemplet rItem)
		{
			return lItem.m_OrderList.CompareTo(rItem.m_OrderList);
		}

		// Token: 0x06008C16 RID: 35862 RVA: 0x002FA2F0 File Offset: 0x002F84F0
		public void OnSelectTab(NKMEventTabTemplet tabTemplet)
		{
			this.m_SelectedTabID = tabTemplet.m_EventID;
			foreach (NKCUIEventSlot nkcuieventSlot in this.m_dicEventTab.Values)
			{
				nkcuieventSlot.SetToggle(nkcuieventSlot.EventID == tabTemplet.m_EventID, true, true);
			}
			if (this.m_dicSubUI.ContainsKey(tabTemplet.m_EventID) && this.m_dicSubUI[tabTemplet.m_EventID].gameObject.activeSelf)
			{
				this.m_dicSubUI[tabTemplet.m_EventID].Refresh();
				return;
			}
			foreach (NKCUIEventSubUI targetMono in this.m_dicSubUI.Values)
			{
				NKCUtil.SetGameobjectActive(targetMono, false);
			}
			if (this.m_dicSubUI.ContainsKey(tabTemplet.m_EventID))
			{
				NKCUIEventSubUI nkcuieventSubUI = this.m_dicSubUI[tabTemplet.m_EventID];
				NKCUtil.SetGameobjectActive(nkcuieventSubUI.gameObject, true);
				nkcuieventSubUI.Open(tabTemplet);
				return;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(this.GetBannerBundleName(tabTemplet), this.GetBannerAssetName(tabTemplet));
			NKCUIEventSubUI nkcuieventSubUI2 = NKCUIEvent.OpenInstanceByAssetName<NKCUIEventSubUI>(nkmassetName.m_BundleName, nkmassetName.m_AssetName, this.m_objSubUIParent);
			if (nkcuieventSubUI2 != null)
			{
				this.m_dicSubUI.Add(tabTemplet.m_EventID, nkcuieventSubUI2);
				nkcuieventSubUI2.Init();
				nkcuieventSubUI2.Open(tabTemplet);
			}
			this.RESOURCE_LIST = tabTemplet.m_lstResourceTypeID;
			NKCUIManager.UpdateUpsideMenu();
		}

		// Token: 0x06008C17 RID: 35863 RVA: 0x002FA48C File Offset: 0x002F868C
		private string GetBannerBundleName(NKMEventTabTemplet templet)
		{
			switch (templet.m_EventType)
			{
			default:
				return templet.m_EventBannerPrefabName;
			case NKM_EVENT_TYPE.ONTIME:
				return "AB_UI_NKM_UI_EVENT_PF_ONTIME";
			case NKM_EVENT_TYPE.CONTRACT:
				return "AB_UI_NKM_UI_EVENT_PF_CONTRACT";
			case NKM_EVENT_TYPE.RACE:
				return "AB_UI_NKM_UI_EVENT_PF_RACE";
			}
		}

		// Token: 0x06008C18 RID: 35864 RVA: 0x002FA4E4 File Offset: 0x002F86E4
		private string GetBannerAssetName(NKMEventTabTemplet templet)
		{
			switch (templet.m_EventType)
			{
			case NKM_EVENT_TYPE.CONTRACT:
				return "NKM_UI_EVENT_CONTRACT";
			}
			return templet.m_EventBannerPrefabName;
		}

		// Token: 0x06008C19 RID: 35865 RVA: 0x002FA530 File Offset: 0x002F8730
		public NKCUIEventSubUI GetEverOpenedEventSubUI(int eventID)
		{
			NKCUIEventSubUI result;
			this.m_dicSubUI.TryGetValue(eventID, out result);
			return result;
		}

		// Token: 0x06008C1A RID: 35866 RVA: 0x002FA550 File Offset: 0x002F8750
		public void RefreshUI(int eventId = 0)
		{
			foreach (KeyValuePair<int, NKCUIEventSubUI> keyValuePair in this.m_dicSubUI)
			{
				if (keyValuePair.Value.gameObject.activeSelf)
				{
					keyValuePair.Value.Refresh();
				}
			}
			foreach (KeyValuePair<int, NKCUIEventSlot> keyValuePair2 in this.m_dicEventTab)
			{
				keyValuePair2.Value.CheckRedDot();
			}
		}

		// Token: 0x06008C1B RID: 35867 RVA: 0x002FA604 File Offset: 0x002F8804
		private void ClearPrefabs()
		{
			foreach (KeyValuePair<int, NKCUIEventSlot> keyValuePair in this.m_dicEventTab)
			{
				UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
			}
			this.m_dicEventTab.Clear();
		}

		// Token: 0x06008C1C RID: 35868 RVA: 0x002FA66C File Offset: 0x002F886C
		public override void Hide()
		{
			base.Hide();
			foreach (KeyValuePair<int, NKCUIEventSubUI> keyValuePair in this.m_dicSubUI)
			{
				if (keyValuePair.Value.gameObject.activeSelf)
				{
					keyValuePair.Value.Hide();
				}
			}
		}

		// Token: 0x06008C1D RID: 35869 RVA: 0x002FA6E0 File Offset: 0x002F88E0
		public override void UnHide()
		{
			base.UnHide();
			foreach (KeyValuePair<int, NKCUIEventSubUI> keyValuePair in this.m_dicSubUI)
			{
				if (keyValuePair.Value.gameObject.activeSelf)
				{
					keyValuePair.Value.UnHide();
				}
			}
		}

		// Token: 0x06008C1E RID: 35870 RVA: 0x002FA754 File Offset: 0x002F8954
		public void CloseSubUI()
		{
			foreach (KeyValuePair<int, NKCUIEventSubUI> keyValuePair in this.m_dicSubUI)
			{
				if (keyValuePair.Value.gameObject.activeSelf)
				{
					keyValuePair.Value.Close();
				}
			}
		}

		// Token: 0x06008C1F RID: 35871 RVA: 0x002FA7C0 File Offset: 0x002F89C0
		public override void OnBackButton()
		{
			foreach (KeyValuePair<int, NKCUIEventSubUI> keyValuePair in this.m_dicSubUI)
			{
				if (keyValuePair.Value.gameObject.activeSelf && keyValuePair.Value.OnBackButton())
				{
					return;
				}
			}
			base.OnBackButton();
		}

		// Token: 0x06008C20 RID: 35872 RVA: 0x002FA838 File Offset: 0x002F8A38
		public static T OpenInstanceByAssetName<T>(string BundleName, string AssetName, Transform parent) where T : MonoBehaviour
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(BundleName, AssetName, false, parent);
			if (nkcassetInstanceData == null || !(nkcassetInstanceData.m_Instant != null))
			{
				Debug.LogWarning("prefab is null - " + BundleName + "/" + AssetName);
				return default(T);
			}
			GameObject instant = nkcassetInstanceData.m_Instant;
			T component = instant.GetComponent<T>();
			if (component == null)
			{
				UnityEngine.Object.Destroy(instant);
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				return default(T);
			}
			NKCUIEvent.m_listNKCAssetResourceData.Add(nkcassetInstanceData);
			return component;
		}

		// Token: 0x06008C21 RID: 35873 RVA: 0x002FA8C0 File Offset: 0x002F8AC0
		private void OnDestroy()
		{
			for (int i = 0; i < NKCUIEvent.m_listNKCAssetResourceData.Count; i++)
			{
				NKCAssetResourceManager.CloseInstance(NKCUIEvent.m_listNKCAssetResourceData[i]);
			}
			NKCUIEvent.m_listNKCAssetResourceData.Clear();
			NKCUIEvent.m_Instance = null;
		}

		// Token: 0x06008C22 RID: 35874 RVA: 0x002FA904 File Offset: 0x002F8B04
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			foreach (KeyValuePair<int, NKCUIEventSubUI> keyValuePair in this.m_dicSubUI)
			{
				if (keyValuePair.Value.gameObject.activeSelf)
				{
					keyValuePair.Value.OnInventoryChange(itemData);
				}
			}
			foreach (KeyValuePair<int, NKCUIEventSlot> keyValuePair2 in this.m_dicEventTab)
			{
				keyValuePair2.Value.CheckRedDot();
			}
		}

		// Token: 0x06008C23 RID: 35875 RVA: 0x002FA9B8 File Offset: 0x002F8BB8
		public void MarkBingo(int eventID, List<NKMBingoTile> bingoList, bool bRandom)
		{
			NKCUIEventSubUI nkcuieventSubUI;
			if (this.m_dicSubUI.TryGetValue(eventID, out nkcuieventSubUI))
			{
				foreach (NKCUIEventSubUIBingo nkcuieventSubUIBingo in nkcuieventSubUI.GetSubUIs<NKCUIEventSubUIBingo>())
				{
					nkcuieventSubUIBingo.MarkBingo(bingoList, bRandom);
				}
			}
		}

		// Token: 0x06008C24 RID: 35876 RVA: 0x002FAA14 File Offset: 0x002F8C14
		private static List<NKMEventTabTemplet> GetActiveEventTabList()
		{
			List<NKMEventTabTemplet> list = new List<NKMEventTabTemplet>();
			foreach (NKMEventTabTemplet nkmeventTabTemplet in NKMTempletContainer<NKMEventTabTemplet>.Values)
			{
				if (nkmeventTabTemplet.IsAvailable)
				{
					switch (nkmeventTabTemplet.m_EventType)
					{
					case NKM_EVENT_TYPE.BINGO:
						if (NKMEventManager.GetBingoData(nkmeventTabTemplet.m_EventID) != null)
						{
							list.Add(nkmeventTabTemplet);
							continue;
						}
						continue;
					case NKM_EVENT_TYPE.MISSION:
					case NKM_EVENT_TYPE.ONTIME:
						list.Add(nkmeventTabTemplet);
						continue;
					case NKM_EVENT_TYPE.KAKAOEMOTE:
						if (NKCScenManager.CurrentUserData().IsKakaoMissionOngoing())
						{
							list.Add(nkmeventTabTemplet);
							continue;
						}
						continue;
					}
					list.Add(nkmeventTabTemplet);
				}
			}
			return list;
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06008C25 RID: 35877 RVA: 0x002FAACC File Offset: 0x002F8CCC
		private static string LAST_VERSION_KEY
		{
			get
			{
				if (NKCScenManager.CurrentUserData() != null)
				{
					return "NKCUIEvent_LAST_VERSION_" + NKCScenManager.CurrentUserData().m_UserUID.ToString();
				}
				return "NKCUIEvent_LAST_VERSION";
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06008C26 RID: 35878 RVA: 0x002FAAF4 File Offset: 0x002F8CF4
		private static string LAST_EVENT_LIST_KEY
		{
			get
			{
				if (NKCScenManager.CurrentUserData() != null)
				{
					return "NKCUIEvent_LAST_EVENT_LIST_" + NKCScenManager.CurrentUserData().m_UserUID.ToString();
				}
				return "NKCUIEvent_LAST_EVENT_LIST";
			}
		}

		// Token: 0x06008C27 RID: 35879 RVA: 0x002FAB1C File Offset: 0x002F8D1C
		private void SaveEventPopupOpenInfo()
		{
			PlayerPrefs.SetString(NKCUIEvent.LAST_VERSION_KEY, NKMContentsVersionManager.CurrentVersion.Literal);
			PlayerPrefs.SetString(NKCUIEvent.LAST_EVENT_LIST_KEY, this.MakeLastEventListString(this.m_lstEventTabTemplet));
			PlayerPrefs.Save();
		}

		// Token: 0x06008C28 RID: 35880 RVA: 0x002FAB50 File Offset: 0x002F8D50
		private string MakeLastEventListString(IEnumerable<NKMEventTabTemplet> setEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (NKMEventTabTemplet nkmeventTabTemplet in setEvent)
			{
				stringBuilder.Append(nkmeventTabTemplet.m_EventID);
				stringBuilder.Append(';');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008C29 RID: 35881 RVA: 0x002FABB4 File Offset: 0x002F8DB4
		private static HashSet<int> LoadLastEventList()
		{
			string @string = PlayerPrefs.GetString(NKCUIEvent.LAST_EVENT_LIST_KEY, "");
			HashSet<int> hashSet = new HashSet<int>();
			string[] array = @string.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				int item;
				if (int.TryParse(array[i], out item))
				{
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x06008C2A RID: 35882 RVA: 0x002FAC0C File Offset: 0x002F8E0C
		public static NKMEventTabTemplet GetRequiredEventTemplet()
		{
			List<NKMEventTabTemplet> activeEventTabList = NKCUIEvent.GetActiveEventTabList();
			HashSet<int> hashSet = new HashSet<int>(from x in activeEventTabList
			select x.m_EventID);
			HashSet<int> other = NKCUIEvent.LoadLastEventList();
			hashSet.ExceptWith(other);
			if (hashSet.Count > 0)
			{
				return NKMEventTabTemplet.Find(hashSet.First<int>());
			}
			if (NKMContentsVersionManager.CurrentVersion.Literal != PlayerPrefs.GetString(NKCUIEvent.LAST_VERSION_KEY, "") && activeEventTabList.Count > 0)
			{
				return activeEventTabList[0];
			}
			return null;
		}

		// Token: 0x040078E3 RID: 30947
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_event";

		// Token: 0x040078E4 RID: 30948
		private const string UI_ASSET_NAME = "NKM_UI_EVENT";

		// Token: 0x040078E5 RID: 30949
		private static NKCUIEvent m_Instance;

		// Token: 0x040078E6 RID: 30950
		private List<int> RESOURCE_LIST = new List<int>();

		// Token: 0x040078E7 RID: 30951
		public GameObject m_pfbMenuSlot;

		// Token: 0x040078E8 RID: 30952
		public LoopScrollRect m_loopScrollRect;

		// Token: 0x040078E9 RID: 30953
		public NKCUIComToggleGroup m_tglGroup;

		// Token: 0x040078EA RID: 30954
		public Transform m_objSubUIParent;

		// Token: 0x040078EB RID: 30955
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x040078EC RID: 30956
		private List<NKMEventTabTemplet> m_lstEventTabTemplet = new List<NKMEventTabTemplet>();

		// Token: 0x040078ED RID: 30957
		private Stack<NKCUIEventSlot> m_stkEventTabSlot = new Stack<NKCUIEventSlot>();

		// Token: 0x040078EE RID: 30958
		private Dictionary<int, NKCUIEventSlot> m_dicEventTab = new Dictionary<int, NKCUIEventSlot>();

		// Token: 0x040078EF RID: 30959
		private Dictionary<int, NKCUIEventSubUI> m_dicSubUI = new Dictionary<int, NKCUIEventSubUI>();

		// Token: 0x040078F0 RID: 30960
		private static List<NKCAssetInstanceData> m_listNKCAssetResourceData = new List<NKCAssetInstanceData>();

		// Token: 0x040078F1 RID: 30961
		private int m_SelectedTabID;
	}
}
