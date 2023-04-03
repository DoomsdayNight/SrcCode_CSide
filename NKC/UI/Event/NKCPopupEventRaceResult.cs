using System;
using System.Collections.Generic;
using ClientPacket.Event;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BCC RID: 3020
	public class NKCPopupEventRaceResult : NKCUIBase
	{
		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06008BDC RID: 35804 RVA: 0x002F9660 File Offset: 0x002F7860
		public static NKCPopupEventRaceResult Instance
		{
			get
			{
				if (NKCPopupEventRaceResult.m_Instance == null)
				{
					NKCPopupEventRaceResult.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventRaceResult>("AB_UI_NKM_UI_EVENT_PF_RACE", "NKM_UI_EVENT_RACE_RESULT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventRaceResult.CleanupInstance)).GetInstance<NKCPopupEventRaceResult>();
					NKCPopupEventRaceResult.m_Instance.InitUI();
				}
				return NKCPopupEventRaceResult.m_Instance;
			}
		}

		// Token: 0x06008BDD RID: 35805 RVA: 0x002F96AF File Offset: 0x002F78AF
		private static void CleanupInstance()
		{
			NKCPopupEventRaceResult.m_Instance = null;
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06008BDE RID: 35806 RVA: 0x002F96B7 File Offset: 0x002F78B7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventRaceResult.m_Instance != null && NKCPopupEventRaceResult.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008BDF RID: 35807 RVA: 0x002F96D2 File Offset: 0x002F78D2
		private void OnDestroy()
		{
			NKCPopupEventRaceResult.m_Instance = null;
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06008BE0 RID: 35808 RVA: 0x002F96DA File Offset: 0x002F78DA
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06008BE1 RID: 35809 RVA: 0x002F96DD File Offset: 0x002F78DD
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008BE2 RID: 35810 RVA: 0x002F96E4 File Offset: 0x002F78E4
		private void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
		}

		// Token: 0x06008BE3 RID: 35811 RVA: 0x002F9712 File Offset: 0x002F7912
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			if (NKCPopupEventRace.IsInstanceOpen)
			{
				NKCPopupEventRace.Instance.Close();
			}
		}

		// Token: 0x06008BE4 RID: 35812 RVA: 0x002F9734 File Offset: 0x002F7934
		public void Open(NKMPACKET_RACE_START_ACK sPacket)
		{
			List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(sPacket.rewardData, false, false);
			for (int i = 0; i < list.Count; i++)
			{
				this.m_slot.SetData(list[i], true, null);
			}
			NKCUtil.SetGameobjectActive(this.m_objWin, sPacket.isWin);
			NKCUtil.SetGameobjectActive(this.m_objLose, !sPacket.isWin);
			if (sPacket.racePrivate.SelectTeam == RaceTeam.TeamA)
			{
				NKCUtil.SetLabelText(this.m_lbTeamName, NKCUtilString.GET_STRING_EVENT_RACE_RESULT_TEAM_RED);
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbTeamName, NKCUtilString.GET_STRING_EVENT_RACE_RESULT_TEAM_BLUE);
			}
			NKMEventRaceTemplet nkmeventRaceTemplet = NKMEventRaceTemplet.Find(sPacket.racePrivate.RaceId);
			if (nkmeventRaceTemplet != null)
			{
				long racePoint = nkmeventRaceTemplet.GetRacePoint(sPacket.isWin);
				NKCUtil.SetImageSprite(this.m_imgPoint, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_EVENT_RACE_SPRITE", this.GetIconName(sPacket.racePrivate.SelectTeam), false), false);
				NKCUtil.SetLabelText(this.m_lbEarnPoint, string.Format("+{0}", racePoint));
			}
			if (this.m_UnitIllust != null)
			{
				this.m_UnitIllust.Unload();
				this.m_UnitIllust = null;
			}
			if (sPacket.racePrivate.SelectTeam == RaceTeam.TeamA)
			{
				string text = (nkmeventRaceTemplet != null) ? this.GetUnitIllustName(nkmeventRaceTemplet.TeamAUnitImageType, nkmeventRaceTemplet.TeamAUnitId) : "";
				this.m_UnitIllust = NKCResourceUtility.OpenSpineIllust(text, text, false);
			}
			else
			{
				string text2 = (nkmeventRaceTemplet != null) ? this.GetUnitIllustName(nkmeventRaceTemplet.TeamBUnitImageType, nkmeventRaceTemplet.TeamBUnitId) : "";
				this.m_UnitIllust = NKCResourceUtility.OpenSpineIllust(text2, text2, false);
			}
			this.m_UnitIllust.SetIllustBackgroundEnable(false);
			this.m_UnitIllust.SetParent(this.m_trSpineRoot, false);
			this.m_UnitIllust.GetRectTransform().localPosition = Vector3.zero;
			this.m_UnitIllust.GetRectTransform().localScale = Vector3.one;
			if (sPacket.isWin)
			{
				this.m_UnitIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.UNIT_LAUGH, true, false, 0f);
			}
			else
			{
				this.m_UnitIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.UNIT_DESPAIR, true, false, 0f);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (sPacket.isWin)
			{
				this.m_AniResult.Play("NKM_UI_EVENT_RACE_RESULT_WIN");
			}
			else
			{
				this.m_AniResult.Play("NKM_UI_EVENT_RACE_RESULT_LOSE");
			}
			base.UIOpened(true);
		}

		// Token: 0x06008BE5 RID: 35813 RVA: 0x002F9967 File Offset: 0x002F7B67
		private string GetIconName(RaceTeam team)
		{
			if (team != RaceTeam.TeamA)
			{
				return "NKM_UI_EVENT_RACE_ICON_TEAM_BLUE";
			}
			return "NKM_UI_EVENT_RACE_ICON_TEAM_RED";
		}

		// Token: 0x06008BE6 RID: 35814 RVA: 0x002F9978 File Offset: 0x002F7B78
		private string GetUnitIllustName(string type, int id)
		{
			if (type != null && type == "SKIN")
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(id);
				if (skinTemplet == null)
				{
					return "";
				}
				return skinTemplet.m_SpineIllustName;
			}
			else
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(id);
				if (unitTempletBase == null)
				{
					return "";
				}
				return unitTempletBase.m_SpineIllustName;
			}
		}

		// Token: 0x040078BB RID: 30907
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_PF_RACE";

		// Token: 0x040078BC RID: 30908
		private const string UI_ASSET_NAME = "NKM_UI_EVENT_RACE_RESULT";

		// Token: 0x040078BD RID: 30909
		private static NKCPopupEventRaceResult m_Instance;

		// Token: 0x040078BE RID: 30910
		public NKCUIComStateButton m_btnClose;

		// Token: 0x040078BF RID: 30911
		public Animator m_AniResult;

		// Token: 0x040078C0 RID: 30912
		public GameObject m_objWin;

		// Token: 0x040078C1 RID: 30913
		public GameObject m_objLose;

		// Token: 0x040078C2 RID: 30914
		public Transform m_trSpineRoot;

		// Token: 0x040078C3 RID: 30915
		public Text m_lbTeamName;

		// Token: 0x040078C4 RID: 30916
		public Image m_imgPoint;

		// Token: 0x040078C5 RID: 30917
		public Text m_lbEarnPoint;

		// Token: 0x040078C6 RID: 30918
		private const string POINT_ICON_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT_RACE_SPRITE";

		// Token: 0x040078C7 RID: 30919
		private const string POINT_ICON_RED = "NKM_UI_EVENT_RACE_ICON_TEAM_RED";

		// Token: 0x040078C8 RID: 30920
		private const string POINT_ICON_BLUE = "NKM_UI_EVENT_RACE_ICON_TEAM_BLUE";

		// Token: 0x040078C9 RID: 30921
		public NKCUISlot m_slot;

		// Token: 0x040078CA RID: 30922
		private NKCASUIUnitIllust m_UnitIllust;
	}
}
