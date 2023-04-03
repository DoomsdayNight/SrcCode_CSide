using System;
using ClientPacket.Mode;
using Cs.Logging;
using NKC.UI.Trim;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA3 RID: 2723
	public class NKCPopupTrimScoreResult : NKCUIBase
	{
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x060078AC RID: 30892 RVA: 0x002810B8 File Offset: 0x0027F2B8
		public static NKCPopupTrimScoreResult Instance
		{
			get
			{
				if (NKCPopupTrimScoreResult.m_Instance == null)
				{
					NKCPopupTrimScoreResult.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupTrimScoreResult>("ab_ui_trim", "AB_UI_TRIM_RECORD_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupTrimScoreResult.CleanupInstance)).GetInstance<NKCPopupTrimScoreResult>();
					NKCPopupTrimScoreResult.m_Instance.Init();
				}
				return NKCPopupTrimScoreResult.m_Instance;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x060078AD RID: 30893 RVA: 0x00281107 File Offset: 0x0027F307
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupTrimScoreResult.m_Instance != null && NKCPopupTrimScoreResult.m_Instance.IsOpen;
			}
		}

		// Token: 0x060078AE RID: 30894 RVA: 0x00281122 File Offset: 0x0027F322
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupTrimScoreResult.m_Instance != null && NKCPopupTrimScoreResult.m_Instance.IsOpen)
			{
				NKCPopupTrimScoreResult.m_Instance.Close();
			}
		}

		// Token: 0x060078AF RID: 30895 RVA: 0x00281147 File Offset: 0x0027F347
		private static void CleanupInstance()
		{
			NKCPopupTrimScoreResult.m_Instance = null;
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x060078B0 RID: 30896 RVA: 0x0028114F File Offset: 0x0027F34F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x060078B1 RID: 30897 RVA: 0x00281152 File Offset: 0x0027F352
		public override string MenuName
		{
			get
			{
				return "TRIM SCORE RESULT";
			}
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x0028115C File Offset: 0x0027F35C
		private void Init()
		{
			if (this.m_eventTrigger != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnTouchBG));
				this.m_eventTrigger.triggers.Clear();
				this.m_eventTrigger.triggers.Add(entry);
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060078B3 RID: 30899 RVA: 0x002811C8 File Offset: 0x0027F3C8
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			if (this.m_dOnClose != null)
			{
				this.m_dOnClose();
			}
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x002811EC File Offset: 0x0027F3EC
		public void Open(TrimModeState trimModeState, int totalScore, int bestScore, NKCPopupTrimScoreResult.OnClose onClose = null)
		{
			if (trimModeState == null)
			{
				return;
			}
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimModeState.trimId);
			string msg;
			if (nkmtrimTemplet != null)
			{
				msg = NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false);
			}
			else
			{
				Log.Error(string.Format("PopupTrimScoreResult - Wrong TrimId (TrimModeState.trimId: {0})", trimModeState.trimId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Trim/NKCPopupTrimScoreResult.cs", 98);
				msg = " - ";
			}
			NKCUtil.SetLabelText(this.m_lbDungeonName, msg);
			NKCUtil.SetLabelText(this.m_lbTrimLevel, trimModeState.trimLevel.ToString());
			NKCUtil.SetLabelText(this.m_lbTotalScore, string.Format("{0:#,0}", totalScore.ToString()));
			NKCUtil.SetLabelText(this.m_lbBestScore, string.Format("{0:#,0}", bestScore.ToString()));
			NKCUtil.SetGameobjectActive(this.m_objNewTag, totalScore >= bestScore);
			if (this.m_trimScoreSlot != null)
			{
				int num = 0;
				int num2 = this.m_trimScoreSlot.Length;
				int count = trimModeState.stageList.Count;
				for (int i = 0; i < num2; i++)
				{
					if (i < count)
					{
						this.m_trimScoreSlot[i].SetActive(true);
						this.m_trimScoreSlot[i].SetData(trimModeState.stageList[i]);
						num++;
					}
				}
				if (num < num2)
				{
					this.m_trimScoreSlot[num].SetActive(true);
					this.m_trimScoreSlot[num].SetData(trimModeState.lastClearStage);
					num++;
				}
				for (int j = num; j < num2; j++)
				{
					this.m_trimScoreSlot[j].SetActive(false);
				}
			}
			this.m_dOnClose = onClose;
			base.gameObject.SetActive(true);
			base.UIOpened(true);
		}

		// Token: 0x060078B5 RID: 30901 RVA: 0x0028137A File Offset: 0x0027F57A
		private void OnTouchBG(BaseEventData eventData)
		{
			base.Close();
		}

		// Token: 0x04006536 RID: 25910
		private const string BUNDLE_NAME = "ab_ui_trim";

		// Token: 0x04006537 RID: 25911
		private const string ASSET_NAME = "AB_UI_TRIM_RECORD_POPUP";

		// Token: 0x04006538 RID: 25912
		private static NKCPopupTrimScoreResult m_Instance;

		// Token: 0x04006539 RID: 25913
		public Text m_lbDungeonName;

		// Token: 0x0400653A RID: 25914
		public Text m_lbTrimLevel;

		// Token: 0x0400653B RID: 25915
		public Text m_lbTotalScore;

		// Token: 0x0400653C RID: 25916
		public Text m_lbBestScore;

		// Token: 0x0400653D RID: 25917
		public EventTrigger m_eventTrigger;

		// Token: 0x0400653E RID: 25918
		public NKCUITrimScoreSlot[] m_trimScoreSlot;

		// Token: 0x0400653F RID: 25919
		public GameObject m_objNewTag;

		// Token: 0x04006540 RID: 25920
		private NKCPopupTrimScoreResult.OnClose m_dOnClose;

		// Token: 0x020017F8 RID: 6136
		// (Invoke) Token: 0x0600B4CC RID: 46284
		public delegate void OnClose();
	}
}
