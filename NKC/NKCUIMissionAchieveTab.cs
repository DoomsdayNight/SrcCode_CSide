using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009BC RID: 2492
	public class NKCUIMissionAchieveTab : MonoBehaviour
	{
		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x060069A5 RID: 27045 RVA: 0x00223228 File Offset: 0x00221428
		private Color TITLE_COLOR_TAB_ON
		{
			get
			{
				if (this.m_NKMMissionTabTemplet != null && this.m_NKMMissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
				{
					return NKCUtil.GetColor("#222222");
				}
				return new Color(0.003921569f, 0.105882354f, 0.23137255f);
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x060069A6 RID: 27046 RVA: 0x00223260 File Offset: 0x00221460
		private Color TITLE_COLOR_TAB_OFF
		{
			get
			{
				if (this.m_NKMMissionTabTemplet != null && this.m_NKMMissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
				{
					return NKCUtil.GetColor("#E5E5E5");
				}
				return Color.white;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x060069A7 RID: 27047 RVA: 0x00223289 File Offset: 0x00221489
		private Color TITLE_COLOR_TAB_LOCK
		{
			get
			{
				if (this.m_NKMMissionTabTemplet != null && this.m_NKMMissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
				{
					return NKCUtil.GetColor("#747474");
				}
				return Color.white;
			}
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x002232B4 File Offset: 0x002214B4
		public static NKCUIMissionAchieveTab GetNewInstance(Transform parent, string bundleName, string assetName)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(bundleName, assetName, false, null);
			NKCUIMissionAchieveTab component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIMissionAchieveTab>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIMissionAchieveTab Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			component.m_Tgl.m_bGetCallbackWhileLocked = true;
			component.m_Tgl.OnValueChanged.RemoveAllListeners();
			component.m_Tgl.OnValueChanged.AddListener(new UnityAction<bool>(component.OnValueChanged));
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x00223372 File Offset: 0x00221572
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x0022337F File Offset: 0x0022157F
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x002233A0 File Offset: 0x002215A0
		public void SetData(NKMMissionTabTemplet tabTemplet, NKCUIComToggleGroup toggleGroup, NKCUIMissionAchieveTab.OnClickTab onClickTab)
		{
			this.dOnClickTab = onClickTab;
			NKCUtil.SetGameobjectActive(this.m_objOffComplete, false);
			NKCUtil.SetGameobjectActive(this.m_objOnComplete, false);
			NKCUtil.SetGameobjectActive(this.m_objNew, false);
			this.m_NKMMissionTabTemplet = tabTemplet;
			this.m_MissionTabID = tabTemplet.m_tabID;
			this.m_Tgl.SetToggleGroup(toggleGroup);
			NKCUtil.SetLabelText(this.m_lbTitle, tabTemplet.GetDesc());
			NKCUtil.SetImageSprite(this.m_imgOnIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(this.GetSpriteBundleName(), tabTemplet.m_MissionTabIconName, false), false);
			NKCUtil.SetImageSprite(this.m_imgOffIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(this.GetSpriteBundleName(), tabTemplet.m_MissionTabIconName, false), false);
			NKCUtil.SetImageSprite(this.m_imgLockIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_mission_sprite", "AB_UI_NKM_UI_MISSION_ICON_LOCK", false), false);
			this.CheckTabState(tabTemplet, out this.m_bLocked, out this.m_bCompleted);
			this.SetLabelColor(this.m_Tgl.m_bSelect);
			this.m_bLimited = NKMMissionManager.TryGetMissionTabExpireUtcTime(tabTemplet, NKCScenManager.CurrentUserData(), out this.m_tEndTime);
			if (this.m_bLimited)
			{
				NKCUtil.SetLabelText(this.m_lbFestaTime, string.Format(NKCUtilString.GET_STRING_REMAIN_TIME_LEFT_ONE_PARAM, NKCUtilString.GetRemainTimeString(this.m_tEndTime, 1)));
			}
			NKCUtil.SetGameobjectActive(this.m_objFesta, this.m_bLimited);
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x002234D5 File Offset: 0x002216D5
		private string GetSpriteBundleName()
		{
			if (this.m_NKMMissionTabTemplet != null && this.m_NKMMissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
			{
				return "ui_mission_guide_sprite";
			}
			return "ab_ui_nkm_ui_mission_sprite";
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x002234FC File Offset: 0x002216FC
		private void SetLabelColor(bool bSelected)
		{
			if (this.m_bLocked)
			{
				NKCUtil.SetLabelTextColor(this.m_lbTitle, this.TITLE_COLOR_TAB_LOCK);
				return;
			}
			if (bSelected)
			{
				NKCUtil.SetLabelTextColor(this.m_lbTitle, this.TITLE_COLOR_TAB_ON);
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_lbTitle, this.TITLE_COLOR_TAB_OFF);
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x00223549 File Offset: 0x00221749
		public void SetCompleteObject(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objOffComplete, bSet);
			NKCUtil.SetGameobjectActive(this.m_objOnComplete, bSet);
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x00223563 File Offset: 0x00221763
		public void SetNewObject(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objNew, bSet);
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x00223574 File Offset: 0x00221774
		public void SetLockObject(bool bSkipCompletLock = false)
		{
			NKCUtil.SetGameobjectActive(this.m_objLock, this.m_bLocked);
			if ((this.m_bLocked && !this.m_NKMMissionTabTemplet.m_VisibleWhenLocked) || (this.m_bCompleted && !bSkipCompletLock))
			{
				this.m_Tgl.Lock(false);
				return;
			}
			this.m_Tgl.UnLock(false);
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x002235CB File Offset: 0x002217CB
		public NKCUIComToggle GetToggle()
		{
			return this.m_Tgl;
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x002235D3 File Offset: 0x002217D3
		public bool GetLocked()
		{
			return this.m_bLocked;
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x002235DB File Offset: 0x002217DB
		public bool GetCompleted()
		{
			return this.m_bCompleted;
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x002235E3 File Offset: 0x002217E3
		public void RefreshTab()
		{
			this.CheckTabState(this.m_NKMMissionTabTemplet, out this.m_bLocked, out this.m_bCompleted);
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x00223600 File Offset: 0x00221800
		public void OnValueChanged(bool bSet)
		{
			if (this.m_Tgl.m_bLock)
			{
				if (this.m_NKMMissionTabTemplet != null && this.m_NKMMissionTabTemplet.m_MissionType == NKM_MISSION_TYPE.COMBINE_GUIDE_MISSION)
				{
					string missionTabUnlockCondition = NKMMissionManager.GetMissionTabUnlockCondition(this.m_MissionTabID, NKCScenManager.CurrentUserData());
					if (!string.IsNullOrEmpty(missionTabUnlockCondition))
					{
						NKCPopupMessageManager.AddPopupMessage(missionTabUnlockCondition, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
				}
				else
				{
					if (this.m_objOffComplete.activeInHierarchy)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_MISSION_COMPLETE_GROWTH_TAB, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_MISSION_LOCK_GROWTH_TAB, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				}
				return;
			}
			this.SetLabelColor(bSet);
			if (bSet)
			{
				if (this.m_bLocked)
				{
					string missionTabUnlockCondition2 = NKMMissionManager.GetMissionTabUnlockCondition(this.m_MissionTabID, NKCScenManager.CurrentUserData());
					if (!string.IsNullOrEmpty(missionTabUnlockCondition2))
					{
						NKCPopupMessageManager.AddPopupMessage(missionTabUnlockCondition2, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					}
				}
				if (!this.m_bLocked || this.m_NKMMissionTabTemplet.m_VisibleWhenLocked)
				{
					NKCUIMissionAchieveTab.OnClickTab onClickTab = this.dOnClickTab;
					if (onClickTab == null)
					{
						return;
					}
					onClickTab(this.m_MissionTabID, this.m_Tgl.m_bChecked);
				}
			}
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x00223700 File Offset: 0x00221900
		private void CheckTabState(NKMMissionTabTemplet tabTemplet, out bool bLocked, out bool bCompleted)
		{
			bCompleted = false;
			bLocked = false;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMMissionTemplet missionTemplet = NKMMissionManager.GetMissionTemplet(tabTemplet.m_completeMissionID);
			if (missionTemplet != null)
			{
				NKMMissionData missionData = nkmuserData.m_MissionData.GetMissionData(missionTemplet);
				if (missionData != null && missionData.isComplete)
				{
					bCompleted = true;
				}
			}
			NKMMissionTemplet missionTemplet2 = NKMMissionManager.GetMissionTemplet(tabTemplet.m_firstMissionID);
			if (missionTemplet2 != null && missionTemplet2.m_MissionRequire > 0 && (nkmuserData.m_MissionData.GetMissionDataByMissionId(missionTemplet2.m_MissionRequire) == null || !nkmuserData.m_MissionData.GetMissionDataByMissionId(missionTemplet2.m_MissionRequire).isComplete))
			{
				bLocked = true;
			}
			if (!NKMContentUnlockManager.IsContentUnlocked(nkmuserData, tabTemplet.m_UnlockInfo, false))
			{
				bLocked = true;
			}
			NKCUtil.SetGameobjectActive(this.m_objLock, bLocked);
			if (bLocked | bCompleted)
			{
				this.m_Tgl.Lock(false);
				this.SetLabelColor(false);
			}
			else
			{
				this.m_Tgl.UnLock(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objOffComplete, bCompleted);
			NKCUtil.SetGameobjectActive(this.m_objOnComplete, bCompleted);
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x002237F0 File Offset: 0x002219F0
		private void Update()
		{
			if (this.m_bLimited && !this.m_bLocked && !this.m_bCompleted)
			{
				this.m_tDeltaTime += Time.deltaTime;
				if (this.m_tDeltaTime > 1f)
				{
					this.m_tDeltaTime -= 1f;
					NKCUtil.SetLabelText(this.m_lbFestaTime, string.Format(NKCUtilString.GET_STRING_REMAIN_TIME_LEFT_ONE_PARAM, NKCUtilString.GetRemainTimeString(this.m_tEndTime, 1)));
				}
			}
		}

		// Token: 0x0400556A RID: 21866
		private const string SPRITE_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_mission_sprite";

		// Token: 0x0400556B RID: 21867
		private const string LOCK_ICON_IMAGE_NAME = "AB_UI_NKM_UI_MISSION_ICON_LOCK";

		// Token: 0x0400556C RID: 21868
		private const string SPRITE_ASSET_BUNDLE_NAME_GUIDE = "ui_mission_guide_sprite";

		// Token: 0x0400556D RID: 21869
		public NKCUIComToggle m_Tgl;

		// Token: 0x0400556E RID: 21870
		public Text m_lbTitle;

		// Token: 0x0400556F RID: 21871
		public Image m_imgOnIcon;

		// Token: 0x04005570 RID: 21872
		public Image m_imgOffIcon;

		// Token: 0x04005571 RID: 21873
		public Image m_imgLockIcon;

		// Token: 0x04005572 RID: 21874
		public GameObject m_objLock;

		// Token: 0x04005573 RID: 21875
		public GameObject m_objOffComplete;

		// Token: 0x04005574 RID: 21876
		public GameObject m_objOnComplete;

		// Token: 0x04005575 RID: 21877
		public GameObject m_objNew;

		// Token: 0x04005576 RID: 21878
		public GameObject m_objFesta;

		// Token: 0x04005577 RID: 21879
		public Text m_lbFestaTime;

		// Token: 0x04005578 RID: 21880
		private NKCUIMissionAchieveTab.OnClickTab dOnClickTab;

		// Token: 0x04005579 RID: 21881
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x0400557A RID: 21882
		private int m_MissionTabID;

		// Token: 0x0400557B RID: 21883
		private bool m_bLimited;

		// Token: 0x0400557C RID: 21884
		private bool m_bLocked;

		// Token: 0x0400557D RID: 21885
		private bool m_bCompleted;

		// Token: 0x0400557E RID: 21886
		private DateTime m_tEndTime;

		// Token: 0x0400557F RID: 21887
		private NKMMissionTabTemplet m_NKMMissionTabTemplet;

		// Token: 0x04005580 RID: 21888
		private float m_tDeltaTime;

		// Token: 0x020016BA RID: 5818
		// (Invoke) Token: 0x0600B124 RID: 45348
		public delegate void OnClickTab(int tabID, bool bSet);
	}
}
