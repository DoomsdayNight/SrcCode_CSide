using System;
using ClientPacket.Unit;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x02000803 RID: 2051
	public class NKCUIRearmament : NKCUIBase
	{
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x00189FAC File Offset: 0x001881AC
		public static NKCUIRearmament Instance
		{
			get
			{
				if (NKCUIRearmament.m_Instance == null)
				{
					NKCUIRearmament.m_Instance = NKCUIManager.OpenNewInstance<NKCUIRearmament>("ab_ui_rearm", "AB_UI_REARM_UI", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIRearmament.OnCleanupInstance)).GetInstance<NKCUIRearmament>();
					NKCUIRearmament.m_Instance.InitUI();
				}
				return NKCUIRearmament.m_Instance;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x00189FFB File Offset: 0x001881FB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x00189FFE File Offset: 0x001881FE
		public override string MenuName
		{
			get
			{
				return this.GetCurrentMenuName();
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x0600512F RID: 20783 RVA: 0x0018A006 File Offset: 0x00188206
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIRearmament.m_Instance != null && NKCUIRearmament.m_Instance.IsOpen;
			}
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x0018A021 File Offset: 0x00188221
		public static void CheckInstanceAndClose()
		{
			if (NKCUIRearmament.m_Instance != null && NKCUIRearmament.m_Instance.IsOpen)
			{
				NKCUIRearmament.m_Instance.Close();
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x0018A046 File Offset: 0x00188246
		public static bool IsInstanceLoaded
		{
			get
			{
				return NKCUIRearmament.m_Instance != null;
			}
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x0018A053 File Offset: 0x00188253
		public override void CloseInternal()
		{
			this.m_preUIState = NKCUIRearmament.REARM_TYPE.RT_NONE;
			this.m_RearmExtract.Clear();
			this.m_RearmProcess.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005133 RID: 20787 RVA: 0x0018A07E File Offset: 0x0018827E
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x0018A081 File Offset: 0x00188281
		public static void OnCleanupInstance()
		{
			NKCUIRearmament.m_Instance = null;
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005135 RID: 20789 RVA: 0x0018A089 File Offset: 0x00188289
		public override string GuideTempletID
		{
			get
			{
				return this.GetCurrentGuideName();
			}
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x0018A091 File Offset: 0x00188291
		public override void UnHide()
		{
			base.UnHide();
			this.UpdateAni(this.m_curUIType);
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x0018A0A5 File Offset: 0x001882A5
		public override void OnBackButton()
		{
			if (this.m_curUIType != NKCUIRearmament.REARM_TYPE.RT_PROCESS)
			{
				base.OnBackButton();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_UNIT_LIST)
			{
				base.Close();
				return;
			}
			this.ChangeState(NKCUIRearmament.REARM_TYPE.RT_LIST);
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x0018A0D3 File Offset: 0x001882D3
		private string GetCurrentMenuName()
		{
			if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT)
			{
				return NKCUtilString.GET_STRING_REARM_EXTRACT_TITLE;
			}
			return NKCUtilString.GET_STRING_REARM_PROCESS_TITLE;
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x0018A0EC File Offset: 0x001882EC
		private string GetCurrentGuideName()
		{
			switch (this.m_curUIType)
			{
			case NKCUIRearmament.REARM_TYPE.RT_LIST:
				return this.REARM_LIST_ID;
			case NKCUIRearmament.REARM_TYPE.RT_PROCESS:
				return this.REARM_PROCESS_ID;
			default:
				return this.REARM_EXTRACT_ID;
			}
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x0018A128 File Offset: 0x00188328
		private void InitUI()
		{
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctgRearm, new UnityAction<bool>(this.OnClickToggleRearm));
			NKCUtil.SetToggleValueChangedDelegate(this.m_ctgExtract, new UnityAction<bool>(this.OnClickToggleExtract));
			NKCUIRearmamentExtract rearmExtract = this.m_RearmExtract;
			if (rearmExtract != null)
			{
				rearmExtract.Init();
			}
			NKCUIRearmamentProcess rearmProcess = this.m_RearmProcess;
			if (rearmProcess == null)
			{
				return;
			}
			rearmProcess.Init(new NKCUIRearmamentProcess.ChangeState(this.ChangeState));
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x0018A190 File Offset: 0x00188390
		private void OnClickToggleRearm(bool bVal)
		{
			if (bVal)
			{
				if (this.m_preUIState != NKCUIRearmament.REARM_TYPE.RT_NONE)
				{
					this.ChangeState(this.m_preUIState);
					return;
				}
				this.ChangeState(NKCUIRearmament.REARM_TYPE.RT_LIST);
			}
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x0018A1B1 File Offset: 0x001883B1
		private void OnClickToggleExtract(bool bVal)
		{
			if (bVal)
			{
				this.m_preUIState = this.m_curUIType;
				this.ChangeState(NKCUIRearmament.REARM_TYPE.RT_EXTRACT);
			}
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x0018A1C9 File Offset: 0x001883C9
		public void SetReserveRearmData(int iTargetRearmTypeUnitID, long iResourceRearmUnitUID)
		{
			this.m_RearmProcess.SetReserveRearmData(iTargetRearmTypeUnitID, iResourceRearmUnitUID);
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x0018A1D8 File Offset: 0x001883D8
		public void Open(NKCUIRearmament.REARM_TYPE type = NKCUIRearmament.REARM_TYPE.RT_LIST)
		{
			this.m_curUIType = NKCUIRearmament.REARM_TYPE.RT_NONE;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.ChangeState(type);
			if (type == NKCUIRearmament.REARM_TYPE.RT_EXTRACT)
			{
				NKCUIComToggle ctgExtract = this.m_ctgExtract;
				if (ctgExtract != null)
				{
					ctgExtract.Select(true, true, false);
				}
			}
			else
			{
				NKCUIComToggle ctgRearm = this.m_ctgRearm;
				if (ctgRearm != null)
				{
					ctgRearm.Select(true, true, false);
				}
			}
			this.m_ctgRearm.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.REARM, 0, 0), false);
			this.m_ctgExtract.SetLock(!NKCContentManager.IsContentsUnlocked(ContentsType.EXTRACT, 0, 0), false);
			base.UIOpened(true);
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x0018A268 File Offset: 0x00188468
		public void ChangeState(NKCUIRearmament.REARM_TYPE newType)
		{
			if (this.m_curUIType == newType)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRearm, newType != NKCUIRearmament.REARM_TYPE.RT_EXTRACT);
			NKCUtil.SetGameobjectActive(this.m_objExtract, newType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT);
			if (newType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT)
			{
				this.m_RearmExtract.Open();
			}
			else
			{
				this.m_RearmProcess.Open(newType);
			}
			this.UpdateAni(newType);
			this.m_curUIType = newType;
			base.UpdateUpsideMenu();
			this.CheckTutorial();
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x0018A2D8 File Offset: 0x001884D8
		private void UpdateAni(NKCUIRearmament.REARM_TYPE newType)
		{
			string text = "";
			if (newType == NKCUIRearmament.REARM_TYPE.RT_LIST && this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_PROCESS)
			{
				text = "PROCESS_TO_LIST";
			}
			else if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_LIST && newType == NKCUIRearmament.REARM_TYPE.RT_PROCESS)
			{
				text = "LIST_TO_PROCESS";
			}
			else if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_NONE && newType == NKCUIRearmament.REARM_TYPE.RT_LIST)
			{
				text = "LIST_INTRO";
			}
			else if ((this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_NONE && newType == NKCUIRearmament.REARM_TYPE.RT_PROCESS) || (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT && newType == NKCUIRearmament.REARM_TYPE.RT_PROCESS) || (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_PROCESS && newType == NKCUIRearmament.REARM_TYPE.RT_PROCESS))
			{
				text = "DIRECT_IN_PROCESS";
			}
			else if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT && newType == NKCUIRearmament.REARM_TYPE.RT_LIST)
			{
				text = "LIST_INTRO";
			}
			else if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_LIST && newType == NKCUIRearmament.REARM_TYPE.RT_LIST)
			{
				text = "DIRECT_IN_LIST";
			}
			else
			{
				if (newType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT)
				{
					return;
				}
				Debug.LogError(string.Format("<color=red>모르는 애니메이션 !!! : m_curUIType : {0}, newType : {1}  </color>", this.m_curUIType, newType));
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.m_AniRearmProcess.SetTrigger(text);
			}
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0018A3C0 File Offset: 0x001885C0
		public void OnRecv(NKMPacket_EXTRACT_UNIT_ACK sPacket)
		{
			if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT)
			{
				this.m_RearmExtract.OnRecv(sPacket);
			}
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x0018A3D7 File Offset: 0x001885D7
		public void CheckTutorial()
		{
			if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT)
			{
				NKCTutorialManager.TutorialRequired(TutorialPoint.Extract, true);
				return;
			}
			NKCTutorialManager.TutorialRequired(TutorialPoint.Rearm, true);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x0018A3F5 File Offset: 0x001885F5
		public RectTransform GetRearmSlotRectTransform(int rearmUnitID)
		{
			if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_LIST)
			{
				return this.m_RearmProcess.GetRearmSlotRectTransform(rearmUnitID);
			}
			Debug.LogError("재무장 리스트 슬롯 데이터 확인 불가");
			return null;
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x0018A418 File Offset: 0x00188618
		public RectTransform GetExtractSlotRectTransform(int extractSlotIdx)
		{
			if (this.m_curUIType == NKCUIRearmament.REARM_TYPE.RT_EXTRACT)
			{
				return this.m_RearmExtract.GetExtractSlotRectTransform(extractSlotIdx);
			}
			Debug.LogError("재무장 추출 슬롯 데이터 확인 불가");
			return null;
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x0018A43B File Offset: 0x0018863B
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			this.m_RearmProcess.OnInventoryChange();
		}

		// Token: 0x0400419B RID: 16795
		private const string ASSET_BUNDLE_NAME = "ab_ui_rearm";

		// Token: 0x0400419C RID: 16796
		private const string UI_ASSET_NAME = "AB_UI_REARM_UI";

		// Token: 0x0400419D RID: 16797
		private static NKCUIRearmament m_Instance;

		// Token: 0x0400419E RID: 16798
		public GameObject m_objRearm;

		// Token: 0x0400419F RID: 16799
		public GameObject m_objExtract;

		// Token: 0x040041A0 RID: 16800
		public GameObject m_objShortCut;

		// Token: 0x040041A1 RID: 16801
		[Header("숏컷")]
		public NKCUIComToggle m_ctgRearm;

		// Token: 0x040041A2 RID: 16802
		public NKCUIComToggle m_ctgExtract;

		// Token: 0x040041A3 RID: 16803
		public NKCUIRearmamentExtract m_RearmExtract;

		// Token: 0x040041A4 RID: 16804
		public NKCUIRearmamentProcess m_RearmProcess;

		// Token: 0x040041A5 RID: 16805
		[Header("Guide Templet ID")]
		public string REARM_EXTRACT_ID = "ARTICLE_EXTRACT_INFO";

		// Token: 0x040041A6 RID: 16806
		public string REARM_LIST_ID = "ARTICLE_REARM_INFO";

		// Token: 0x040041A7 RID: 16807
		public string REARM_PROCESS_ID = "ARTICLE_REARM_INFO";

		// Token: 0x040041A8 RID: 16808
		private NKCUIRearmament.REARM_TYPE m_preUIState;

		// Token: 0x040041A9 RID: 16809
		private NKCUIRearmament.REARM_TYPE m_curUIType;

		// Token: 0x040041AA RID: 16810
		public Animator m_AniRearmProcess;

		// Token: 0x040041AB RID: 16811
		private const string ANI_LIST_INTRO = "LIST_INTRO";

		// Token: 0x040041AC RID: 16812
		private const string ANI_LIST_TO_PROCESS = "LIST_TO_PROCESS";

		// Token: 0x040041AD RID: 16813
		private const string ANI_PROCESS_TO_LIST = "PROCESS_TO_LIST";

		// Token: 0x040041AE RID: 16814
		private const string ANI_DIRECT_IN_PROCESS = "DIRECT_IN_PROCESS";

		// Token: 0x040041AF RID: 16815
		private const string ANI_DIRECT_IN_LIST = "DIRECT_IN_LIST";

		// Token: 0x020014BE RID: 5310
		public enum REARM_TYPE
		{
			// Token: 0x04009EF6 RID: 40694
			RT_NONE,
			// Token: 0x04009EF7 RID: 40695
			RT_LIST,
			// Token: 0x04009EF8 RID: 40696
			RT_PROCESS,
			// Token: 0x04009EF9 RID: 40697
			RT_EXTRACT
		}
	}
}
