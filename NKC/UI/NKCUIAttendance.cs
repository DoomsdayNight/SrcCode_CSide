using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NKC.UI.Guide;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200096F RID: 2415
	public class NKCUIAttendance : NKCUIBase
	{
		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x0600615F RID: 24927 RVA: 0x001E8464 File Offset: 0x001E6664
		public static NKCUIAttendance Instance
		{
			get
			{
				if (NKCUIAttendance.m_Instance == null)
				{
					NKCUIAttendance.m_Instance = NKCUIManager.OpenNewInstance<NKCUIAttendance>("ab_ui_nkm_ui_attendance", "NKM_UI_ATTENDANCE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIAttendance.CleanupInstance)).GetInstance<NKCUIAttendance>();
					NKCUIAttendance.m_Instance.InitUI();
				}
				return NKCUIAttendance.m_Instance;
			}
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x001E84B3 File Offset: 0x001E66B3
		private static void CleanupInstance()
		{
			NKCUIAttendance.m_Instance = null;
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06006161 RID: 24929 RVA: 0x001E84BB File Offset: 0x001E66BB
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIAttendance.m_Instance != null && NKCUIAttendance.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x001E84D6 File Offset: 0x001E66D6
		private void OnDestroy()
		{
			NKCUIAttendance.m_Instance = null;
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06006163 RID: 24931 RVA: 0x001E84DE File Offset: 0x001E66DE
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x001E84E1 File Offset: 0x001E66E1
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_ATTENDANCE;
			}
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x001E84E8 File Offset: 0x001E66E8
		private void InitUI()
		{
			NKCUtil.SetLabelText(this.m_lbTitle, this.MenuName);
			NKCUIComStateButton btnClose = this.m_btnClose;
			if (btnClose != null)
			{
				btnClose.PointerDown.RemoveAllListeners();
			}
			NKCUIComStateButton btnClose2 = this.m_btnClose;
			if (btnClose2 != null)
			{
				btnClose2.PointerDown.AddListener(delegate(PointerEventData eventData)
				{
					base.Close();
				});
			}
			NKCUtil.SetGameobjectActive(this.m_objBlockLayer, false);
			NKCUIComStateButton btnBG = this.m_btnBG;
			if (btnBG != null)
			{
				btnBG.PointerUp.RemoveAllListeners();
			}
			NKCUIComStateButton btnBG2 = this.m_btnBG;
			if (btnBG2 != null)
			{
				btnBG2.PointerUp.AddListener(new UnityAction(base.Close));
			}
			this.m_currentSubUI = null;
			this.m_bSetDataComplete = false;
			this.m_bInitComplete = true;
			NKCUtil.SetBindFunction(this.m_HELP_BUTTON, new UnityAction(this.OnClickHelp));
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x001E85AE File Offset: 0x001E67AE
		private void OnClickHelp()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_SYSTEM_ATTENDANCE", 0);
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x001E85C0 File Offset: 0x001E67C0
		public override void Hide()
		{
			this.m_bHide = true;
			this.m_rtRoot.localScale = Vector3.zero;
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x001E85D9 File Offset: 0x001E67D9
		public override void UnHide()
		{
			this.m_bHide = false;
			this.m_rtRoot.localScale = Vector3.one;
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x001E85F4 File Offset: 0x001E67F4
		public override void CloseInternal()
		{
			base.StopAllCoroutines();
			NKCUtil.SetGameobjectActive(this.m_currentSubUI, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_objBlockLayer, false);
			if (this.m_bOpenNewsPopup)
			{
				NKCUINews.Instance.SetDataAndOpen(true, eNewsFilterType.NOTICE, -1);
				NKCUINews.Instance.SetCloseCallback(this.m_NewsCallback);
			}
			else
			{
				Action newsCallback = this.m_NewsCallback;
				if (newsCallback != null)
				{
					newsCallback();
				}
			}
			this.m_NewsCallback = null;
			this.m_bFirstOpen = true;
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x001E8674 File Offset: 0x001E6874
		public override void OnCloseInstance()
		{
			for (int i = 0; i < NKCUIAttendance.m_listNKCAssetResourceData.Count; i++)
			{
				NKCAssetResourceManager.CloseInstance(NKCUIAttendance.m_listNKCAssetResourceData[i]);
			}
			NKCUIAttendance.m_listNKCAssetResourceData.Clear();
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x001E86B0 File Offset: 0x001E68B0
		public void Open(List<int> lstNewAttendanceKey)
		{
			if (!this.m_bInitComplete)
			{
				this.InitUI();
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			this.m_tNextResetTime = NKMTime.GetNextResetTime(NKCSynchronizedTime.GetServerUTCTime(0.0), NKMTime.TimePeriod.Day).AddSeconds(1.0);
			NKMAttendanceManager.ResetNeedAttendanceKey();
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			if (nkmuserData != null && nkmuserData.m_AttendanceData.AttList.Count == 0)
			{
				Debug.LogError("현재 날짜/시간에 맞는 출석체크 데이터가 없음");
				NKCUtil.SetGameobjectActive(this.m_objEmpty, true);
				NKCUtil.SetLabelText(this.m_lbAttendanceDuration, "");
				base.UIOpened(true);
				return;
			}
			if (lstNewAttendanceKey == null)
			{
				lstNewAttendanceKey = new List<int>();
			}
			this.m_lstNeedAttendanceKey = lstNewAttendanceKey;
			this.m_bNeedAttendanceAniTab = (this.m_lstNeedAttendanceKey.Count > 0);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.SetData();
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x001E878C File Offset: 0x001E698C
		private void SetData()
		{
			NKMAttendanceData attendanceData = NKCScenManager.CurrentUserData().m_AttendanceData;
			this.m_currentSubUI = null;
			if (NKCNewsManager.CheckNeedNewsPopup(NKCSynchronizedTime.GetServerUTCTime(0.0)))
			{
				this.m_bOpenNewsPopup = true;
			}
			else
			{
				this.m_bOpenNewsPopup = false;
			}
			if (!this.m_bSetDataComplete || this.m_bNeedAttendanceAniTab)
			{
				this.ClearPrefabs();
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				for (int i = 0; i < attendanceData.AttList.Count; i++)
				{
					NKMAttendanceTabTemplet attendanceTabTamplet = NKMAttendanceManager.GetAttendanceTabTamplet(attendanceData.AttList[i].IDX);
					if (attendanceTabTamplet == null)
					{
						Debug.LogWarning(string.Format("tabTemplet is null - key : {0}", attendanceData.AttList[i].IDX));
					}
					else if ((!NKMContentsVersionManager.HasDFChangeTagType(DataFormatChangeTagType.OPEN_TAG_ATTENDANCE) || attendanceTabTamplet.EnableByTag) && !(attendanceData.AttList[i].EventEndDate < serverUTCTime) && attendanceData.AttList[i].Count != 0 && !this.m_dicTab.ContainsKey(attendanceTabTamplet.IDX))
					{
						NKCUIAttendanceTab component = UnityEngine.Object.Instantiate<NKCUIAttendanceTab>(this.m_pfbTab, this.m_trTabParent).GetComponent<NKCUIAttendanceTab>();
						if (component == null)
						{
							Debug.LogWarning("tabUI is null");
						}
						else
						{
							component.SetData(attendanceTabTamplet, this.m_tglGroup, this.m_lstNeedAttendanceKey.Contains(attendanceTabTamplet.IDX), new NKCUIAttendanceTab.OnClickEvent(this.OnClickTabUI));
							this.m_dicTab.Add(component.GetTabIDX(), component);
							NKCUIAttendanceSubUI nkcuiattendanceSubUI = NKCUIAttendance.OpenInstanceByAssetName<NKCUIAttendanceSubUI>("ab_ui_nkm_ui_attendance", attendanceTabTamplet.PrefabName, this.m_trSubUIParent);
							if (nkcuiattendanceSubUI == null)
							{
								Debug.LogWarning("subUI is null");
							}
							else
							{
								nkcuiattendanceSubUI.SetData(attendanceTabTamplet);
								this.m_dicSubUI.Add(component.GetTabIDX(), nkcuiattendanceSubUI);
							}
						}
					}
				}
				List<NKCUIAttendanceTab> list = this.m_dicTab.Values.ToList<NKCUIAttendanceTab>();
				list.Sort(new Comparison<NKCUIAttendanceTab>(this.CompTab));
				for (int j = 0; j < list.Count; j++)
				{
					list[j].transform.SetAsLastSibling();
				}
				this.m_bSetDataComplete = true;
			}
			if (this.m_bFirstOpen)
			{
				this.m_bFirstOpen = false;
				base.UIOpened(true);
			}
			this.OnClickTabUI(this.GetFirstTabID());
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x001E89E8 File Offset: 0x001E6BE8
		private int CompTab(NKCUIAttendanceTab rItem, NKCUIAttendanceTab lItem)
		{
			NKMAttendanceTabTemplet attendanceTabTamplet = NKMAttendanceManager.GetAttendanceTabTamplet(rItem.GetTabIDX());
			NKMAttendanceTabTemplet attendanceTabTamplet2 = NKMAttendanceManager.GetAttendanceTabTamplet(lItem.GetTabIDX());
			if (attendanceTabTamplet.TabID == attendanceTabTamplet2.TabID)
			{
				return rItem.GetTabIDX().CompareTo(lItem.GetTabIDX());
			}
			return attendanceTabTamplet.TabID.CompareTo(attendanceTabTamplet2.TabID);
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x001E8A44 File Offset: 0x001E6C44
		private IEnumerator Process()
		{
			NKCUtil.SetGameobjectActive(this.m_objBlockLayer, true);
			if (this.m_bNeedAttendanceAniTab)
			{
				this.m_bNeedAttendanceAniTab = false;
				List<NKCUIAttendanceTab> lstTab = this.m_dicTab.Values.ToList<NKCUIAttendanceTab>();
				lstTab.Sort(new Comparison<NKCUIAttendanceTab>(this.CompTab));
				int num;
				for (int i = 0; i < lstTab.Count; i = num + 1)
				{
					bool flag = this.m_lstNeedAttendanceKey.Contains(lstTab[i].GetTabIDX());
					if (flag)
					{
						NKCUtil.SetGameobjectActive(this.m_currentSubUI, false);
						lstTab[i].Select(true);
						this.m_currentSubUI = this.m_dicSubUI[lstTab[i].GetTabIDX()];
						this.SetAttendanceDuration(lstTab[i].GetTabIDX());
						yield return this.m_currentSubUI.ProcessSubUI(flag);
						this.m_lstNeedAttendanceKey.Remove(lstTab[i].GetTabIDX());
					}
					num = i;
				}
				lstTab = null;
			}
			else
			{
				yield return this.m_currentSubUI.ProcessSubUI(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objBlockLayer, false);
			yield break;
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x001E8A54 File Offset: 0x001E6C54
		private void SetAttendanceDuration(int tabID)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null || !this.m_dicTab.ContainsKey(tabID) || !this.m_dicSubUI.ContainsKey(tabID))
			{
				NKCUtil.SetGameobjectActive(this.m_lbAttendanceDuration, false);
				return;
			}
			NKMAttendanceTabTemplet tabTemplet = NKMAttendanceManager.GetAttendanceTabTamplet(this.m_dicTab[tabID].GetTabIDX());
			DateTime utcTime = default(DateTime);
			DateTime eventEndDate = nkmuserData.m_AttendanceData.AttList.Find((NKMAttendance x) => x.IDX == tabTemplet.IDX).EventEndDate;
			switch (tabTemplet.EventType)
			{
			case NKM_ATTENDANCE_EVENT_TYPE.NORMAL:
				utcTime = tabTemplet.StartDateUtc;
				break;
			case NKM_ATTENDANCE_EVENT_TYPE.RETURN:
				utcTime = eventEndDate.AddDays((double)(-(double)tabTemplet.LimitDayCount));
				break;
			case NKM_ATTENDANCE_EVENT_TYPE.NEW:
				utcTime = new DateTime(Math.Max(tabTemplet.StartDateUtc.Ticks, nkmuserData.m_NKMUserDateData.m_RegisterTime.Ticks));
				break;
			}
			NKCUtil.SetLabelText(this.m_lbAttendanceDuration, NKCUtilString.GetTimeIntervalString(NKMTime.UTCtoLocal(utcTime, 0), NKMTime.UTCtoLocal(eventEndDate, 0), NKMTime.INTERVAL_FROM_UTC, false));
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x001E8B7C File Offset: 0x001E6D7C
		private void OnClickTabUI(int tabID)
		{
			if (this.m_dicSubUI.ContainsKey(tabID))
			{
				if (this.m_currentSubUI == this.m_dicSubUI[tabID])
				{
					return;
				}
				if (this.m_currentSubUI != null)
				{
					NKCUtil.SetGameobjectActive(this.m_currentSubUI, false);
				}
				this.m_dicTab[tabID].Select(true);
				this.m_currentSubUI = this.m_dicSubUI[tabID];
				base.StopAllCoroutines();
				base.StartCoroutine(this.Process());
				this.SetAttendanceDuration(tabID);
			}
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x001E8C0C File Offset: 0x001E6E0C
		private void ClearPrefabs()
		{
			foreach (KeyValuePair<int, NKCUIAttendanceTab> keyValuePair in this.m_dicTab)
			{
				UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
			}
			this.m_dicTab.Clear();
			foreach (KeyValuePair<int, NKCUIAttendanceSubUI> keyValuePair2 in this.m_dicSubUI)
			{
				UnityEngine.Object.Destroy(keyValuePair2.Value.gameObject);
			}
			this.m_dicSubUI.Clear();
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x001E8CCC File Offset: 0x001E6ECC
		private int GetFirstTabID()
		{
			int num = int.MaxValue;
			foreach (KeyValuePair<int, NKCUIAttendanceTab> keyValuePair in this.m_dicTab)
			{
				if (keyValuePair.Value.GetTabIDX() < num)
				{
					num = keyValuePair.Value.GetTabIDX();
				}
			}
			return num;
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x001E8D3C File Offset: 0x001E6F3C
		public static T OpenInstanceByAssetName<T>(string BundleName, string AssetName, Transform parent) where T : MonoBehaviour
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(BundleName, AssetName, false, null);
			if (nkcassetInstanceData != null && nkcassetInstanceData.m_Instant != null)
			{
				NKCUIAttendance.m_listNKCAssetResourceData.Add(nkcassetInstanceData);
				return UnityEngine.Object.Instantiate<GameObject>(nkcassetInstanceData.m_Instant, parent).GetComponent<T>();
			}
			Debug.LogWarning("prefab is null - " + BundleName + "/" + AssetName);
			return default(T);
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x001E8DA0 File Offset: 0x001E6FA0
		public override void OnBackButton()
		{
			if (this.m_objBlockLayer.activeInHierarchy)
			{
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x001E8DB6 File Offset: 0x001E6FB6
		public void SetNewsCallback(Action action)
		{
			this.m_NewsCallback = action;
		}

		// Token: 0x04004D91 RID: 19857
		public const string UI_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_attendance";

		// Token: 0x04004D92 RID: 19858
		public const string UI_ASSET_NAME = "NKM_UI_ATTENDANCE";

		// Token: 0x04004D93 RID: 19859
		private static NKCUIAttendance m_Instance;

		// Token: 0x04004D94 RID: 19860
		[Header("Prefabs")]
		public NKCUIAttendanceTab m_pfbTab;

		// Token: 0x04004D95 RID: 19861
		[Header("TopBar")]
		public Text m_lbTitle;

		// Token: 0x04004D96 RID: 19862
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04004D97 RID: 19863
		[Header("Tab")]
		public Transform m_trTabParent;

		// Token: 0x04004D98 RID: 19864
		public NKCUIComToggleGroup m_tglGroup;

		// Token: 0x04004D99 RID: 19865
		[Header("SubUI")]
		public Transform m_trSubUIParent;

		// Token: 0x04004D9A RID: 19866
		public RectTransform m_rtRoot;

		// Token: 0x04004D9B RID: 19867
		public GameObject m_objBlockLayer;

		// Token: 0x04004D9C RID: 19868
		public NKCUIComStateButton m_btnBG;

		// Token: 0x04004D9D RID: 19869
		public NKCUIComStateButton m_HELP_BUTTON;

		// Token: 0x04004D9E RID: 19870
		public Text m_lbAttendanceDuration;

		// Token: 0x04004D9F RID: 19871
		public GameObject m_objEmpty;

		// Token: 0x04004DA0 RID: 19872
		private Dictionary<int, NKCUIAttendanceTab> m_dicTab = new Dictionary<int, NKCUIAttendanceTab>();

		// Token: 0x04004DA1 RID: 19873
		private Dictionary<int, NKCUIAttendanceSubUI> m_dicSubUI = new Dictionary<int, NKCUIAttendanceSubUI>();

		// Token: 0x04004DA2 RID: 19874
		private static List<NKCAssetInstanceData> m_listNKCAssetResourceData = new List<NKCAssetInstanceData>();

		// Token: 0x04004DA3 RID: 19875
		private DateTime m_tNextResetTime;

		// Token: 0x04004DA4 RID: 19876
		private NKCUIAttendanceSubUI m_currentSubUI;

		// Token: 0x04004DA5 RID: 19877
		private bool m_bInitComplete;

		// Token: 0x04004DA6 RID: 19878
		private bool m_bNeedAttendanceAniTab;

		// Token: 0x04004DA7 RID: 19879
		private bool m_bSetDataComplete;

		// Token: 0x04004DA8 RID: 19880
		private bool m_bOpenNewsPopup;

		// Token: 0x04004DA9 RID: 19881
		private List<int> m_lstNeedAttendanceKey = new List<int>();

		// Token: 0x04004DAA RID: 19882
		private Action m_NewsCallback;

		// Token: 0x04004DAB RID: 19883
		private bool m_bFirstOpen = true;
	}
}
