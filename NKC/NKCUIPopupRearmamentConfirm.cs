using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A97 RID: 2711
	public class NKCUIPopupRearmamentConfirm : NKCUIBase
	{
		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06007823 RID: 30755 RVA: 0x0027E51C File Offset: 0x0027C71C
		public static NKCUIPopupRearmamentConfirm Instance
		{
			get
			{
				if (NKCUIPopupRearmamentConfirm.m_Instance == null)
				{
					NKCUIPopupRearmamentConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupRearmamentConfirm>("ab_ui_rearm", "AB_UI_POPUP_REARM_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupRearmamentConfirm.CleanupInstance)).GetInstance<NKCUIPopupRearmamentConfirm>();
					NKCUIPopupRearmamentConfirm.m_Instance.Init();
				}
				return NKCUIPopupRearmamentConfirm.m_Instance;
			}
		}

		// Token: 0x06007824 RID: 30756 RVA: 0x0027E56B File Offset: 0x0027C76B
		private static void CleanupInstance()
		{
			NKCUIPopupRearmamentConfirm.m_Instance = null;
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06007825 RID: 30757 RVA: 0x0027E573 File Offset: 0x0027C773
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupRearmamentConfirm.m_Instance != null && NKCUIPopupRearmamentConfirm.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007826 RID: 30758 RVA: 0x0027E58E File Offset: 0x0027C78E
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupRearmamentConfirm.m_Instance != null && NKCUIPopupRearmamentConfirm.m_Instance.IsOpen)
			{
				NKCUIPopupRearmamentConfirm.m_Instance.Close();
			}
		}

		// Token: 0x06007827 RID: 30759 RVA: 0x0027E5B3 File Offset: 0x0027C7B3
		private void OnDestroy()
		{
			NKCUIPopupRearmamentConfirm.m_Instance = null;
		}

		// Token: 0x06007828 RID: 30760 RVA: 0x0027E5BB File Offset: 0x0027C7BB
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06007829 RID: 30761 RVA: 0x0027E5C9 File Offset: 0x0027C7C9
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x0600782A RID: 30762 RVA: 0x0027E5CC File Offset: 0x0027C7CC
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_REARM_CONFIRM_POPUP_TITLE;
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x0600782B RID: 30763 RVA: 0x0027E5D3 File Offset: 0x0027C7D3
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.BackButtonOnly;
			}
		}

		// Token: 0x0600782C RID: 30764 RVA: 0x0027E5D6 File Offset: 0x0027C7D6
		private void Init()
		{
			NKCUtil.SetBindFunction(this.m_stbnConfirm, new UnityAction(this.OnClickConfirm));
			NKCUtil.SetHotkey(this.m_stbnConfirm, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x0600782D RID: 30765 RVA: 0x0027E600 File Offset: 0x0027C800
		public void Open(int iTargetRearmID, long iResourceUnitUID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(iTargetRearmID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKMUnitData unitFromUID = NKCScenManager.CurrentUserData().m_ArmyData.GetUnitFromUID(iResourceUnitUID);
			if (unitFromUID == null)
			{
				return;
			}
			NKCUtil.SetImageSprite(this.m_TargetUnitFaceCard, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase), true);
			NKCUtil.SetLabelText(this.m_TargetUnitName, unitTempletBase.GetUnitTitle());
			this.m_BeforeUnitInfo.SetData(unitFromUID.m_UnitID);
			this.m_AfterUnitInfo.SetData(iTargetRearmID);
			int num = 0;
			bool bValue = false;
			for (int i = 0; i < unitTempletBase.GetSkillCount(); i++)
			{
				string skillStrID = unitTempletBase.GetSkillStrID(i);
				if (!string.IsNullOrEmpty(skillStrID))
				{
					NKMUnitSkillTemplet skillTemplet = NKMUnitSkillManager.GetSkillTemplet(skillStrID, 1);
					if (skillTemplet != null)
					{
						if (skillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
						{
							this.m_leaderSkillSlot.SetData(skillTemplet);
							bValue = true;
						}
						else
						{
							this.m_lstSkillSlot[num].SetData(skillTemplet);
							NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[num].gameObject, true);
							num++;
						}
					}
				}
			}
			for (int j = num; j < this.m_lstSkillSlot.Count; j++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[j].gameObject, false);
			}
			NKCUtil.SetGameobjectActive(this.m_leaderSkillSlot.gameObject, bValue);
			this.m_iTargetRearmID = iTargetRearmID;
			this.m_lResourceRearmUID = iResourceUnitUID;
			base.UIOpened(true);
		}

		// Token: 0x0600782E RID: 30766 RVA: 0x0027E749 File Offset: 0x0027C949
		private void OnClickConfirm()
		{
			NKCUIPopupRearmamentConfirmBox.Instance.Open(this.m_lResourceRearmUID, this.m_iTargetRearmID);
		}

		// Token: 0x040064A8 RID: 25768
		private const string ASSET_BUNDLE_NAME = "ab_ui_rearm";

		// Token: 0x040064A9 RID: 25769
		private const string UI_ASSET_NAME = "AB_UI_POPUP_REARM_CONFIRM";

		// Token: 0x040064AA RID: 25770
		private static NKCUIPopupRearmamentConfirm m_Instance;

		// Token: 0x040064AB RID: 25771
		public Image m_TargetUnitFaceCard;

		// Token: 0x040064AC RID: 25772
		public Text m_TargetUnitName;

		// Token: 0x040064AD RID: 25773
		[Header("Before&After")]
		public NKCUIPopupRearmamentConfirmSlotInfo m_BeforeUnitInfo;

		// Token: 0x040064AE RID: 25774
		public NKCUIPopupRearmamentConfirmSlotInfo m_AfterUnitInfo;

		// Token: 0x040064AF RID: 25775
		[Header("Skill")]
		public NKCUIPopupRearmamentConfirmSkillInfo m_leaderSkillSlot;

		// Token: 0x040064B0 RID: 25776
		public List<NKCUIPopupRearmamentConfirmSkillInfo> m_lstSkillSlot;

		// Token: 0x040064B1 RID: 25777
		public NKCUIComStateButton m_stbnConfirm;

		// Token: 0x040064B2 RID: 25778
		private int m_iTargetRearmID;

		// Token: 0x040064B3 RID: 25779
		private long m_lResourceRearmUID;
	}
}
