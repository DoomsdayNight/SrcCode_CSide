using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007BD RID: 1981
	public class NKCUIDiveReadySlot : MonoBehaviour
	{
		// Token: 0x06004E80 RID: 20096 RVA: 0x0017A866 File Offset: 0x00178A66
		public int GetIndex()
		{
			return this.m_Index;
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x0017A86E File Offset: 0x00178A6E
		public NKMDiveTemplet GetNKMDiveTemplet()
		{
			return this.m_NKMDiveTemplet;
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x0017A878 File Offset: 0x00178A78
		public static NKCUIDiveReadySlot GetNewInstance(Transform parent, NKCUIDiveReadySlot.OnSelectedDiveReadySlot _OnSelectedDiveReadySlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_WORLD_MAP_DIVE", "NKM_UI_DIVE_SLOT", false, null);
			NKCUIDiveReadySlot component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIDiveReadySlot>();
			if (component == null)
			{
				Debug.LogError("NKM_UI_DIVE_SLOT Prefab null!");
				return null;
			}
			component.m_InstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.m_OnSelectedDiveReadySlot = _OnSelectedDiveReadySlot;
			component.m_NKM_UI_DIVE_SLOT.PointerClick.RemoveAllListeners();
			component.m_NKM_UI_DIVE_SLOT.PointerClick.AddListener(new UnityAction(component.OnClicked));
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x0017A933 File Offset: 0x00178B33
		public void PlayScrollArriveEffect()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SHOW_FX.gameObject, true);
			this.m_NKM_UI_DIVE_SLOT_SHOW_FX.Play("NKM_UI_DIVE_SLOT_SHOW_FX");
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x0017A956 File Offset: 0x00178B56
		private void OnDestroy()
		{
			this.DestoryInstance(true);
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x0017A95F File Offset: 0x00178B5F
		public void DestoryInstance(bool bPureDestory = false)
		{
			if (this.m_InstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			}
			this.m_InstanceData = null;
			if (!bPureDestory)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x0017A989 File Offset: 0x00178B89
		private void OnClicked()
		{
			if (this.m_OnSelectedDiveReadySlot != null)
			{
				this.m_OnSelectedDiveReadySlot(this);
			}
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x0017A9A0 File Offset: 0x00178BA0
		public void SetUI(int index, NKMDiveTemplet cNKMDiveTemplet, int cityID = -1)
		{
			if (cNKMDiveTemplet == null)
			{
				return;
			}
			this.m_cityID = cityID;
			this.m_NKMDiveTemplet = cNKMDiveTemplet;
			this.m_Index = index;
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_HURDLE, cNKMDiveTemplet.StageType == NKM_DIVE_STAGE_TYPE.NDST_HARD);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SCAVENGER, cNKMDiveTemplet.StageType == NKM_DIVE_STAGE_TYPE.NDST_SCAVENGER);
			this.SetSelected(false);
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUserData cNKMUserData = myUserData;
			UnlockInfo unlockInfo = new UnlockInfo(cNKMDiveTemplet.StageUnlockReqType, cNKMDiveTemplet.StageUnlockReqValue);
			bool flag = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false);
			this.SetLocked(!flag);
			if (myUserData.m_DiveClearData != null)
			{
				this.SetCleared(myUserData.m_DiveClearData.Contains(cNKMDiveTemplet.StageID));
			}
			else
			{
				this.SetCleared(false);
			}
			if (cNKMDiveTemplet.IsEventDive)
			{
				this.m_NKM_UI_DIVE_SLOT_LINE_PREV.enabled = false;
				this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.enabled = false;
			}
			else
			{
				if (cNKMDiveTemplet.IndexID == NKCDiveManager.BeginIndex)
				{
					this.m_NKM_UI_DIVE_SLOT_LINE_PREV.enabled = false;
				}
				else
				{
					this.m_NKM_UI_DIVE_SLOT_LINE_PREV.enabled = true;
					if (flag)
					{
						this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color = new Color(this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color.r, this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color.g, this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color.b, 1f);
					}
					else
					{
						this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color = new Color(this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color.r, this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color.g, this.m_NKM_UI_DIVE_SLOT_LINE_PREV.color.b, 0.3f);
					}
				}
				if (cNKMDiveTemplet.IndexID == NKCDiveManager.EndIndex)
				{
					this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.enabled = false;
				}
				else
				{
					this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.enabled = true;
					NKMDiveTemplet templetByUnlockData = NKCDiveManager.GetTempletByUnlockData(STAGE_UNLOCK_REQ_TYPE.SURT_CLEAR_DIVE, cNKMDiveTemplet.StageID);
					if (templetByUnlockData != null)
					{
						NKMUserData cNKMUserData2 = myUserData;
						unlockInfo = new UnlockInfo(templetByUnlockData.StageUnlockReqType, templetByUnlockData.StageUnlockReqValue);
						if (NKMContentUnlockManager.IsContentUnlocked(cNKMUserData2, unlockInfo, false))
						{
							this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color = new Color(this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color.r, this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color.g, this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color.b, 1f);
						}
						else
						{
							this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color = new Color(this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color.r, this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color.g, this.m_NKM_UI_DIVE_SLOT_LINE_NEXT.color.b, 0.3f);
						}
					}
				}
			}
			this.m_NKM_UI_DIVE_SLOT_TITLE_TEXT.text = cNKMDiveTemplet.Get_STAGE_NAME();
			this.m_NKM_UI_DIVE_SLOT_SUBTITLE_TEXT.text = cNKMDiveTemplet.Get_STAGE_NAME_SUB();
			bool bValue = false;
			if (myUserData.m_DiveGameData != null && myUserData.m_DiveGameData.Floor.Templet.StageID == this.m_NKMDiveTemplet.StageID)
			{
				if (cNKMDiveTemplet.IsEventDive)
				{
					if (NKCScenManager.CurrentUserData().m_WorldmapData.GetCityIDByEventData(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, myUserData.m_DiveGameData.DiveUid) == this.m_cityID)
					{
						bValue = true;
					}
				}
				else
				{
					bValue = true;
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_DIVE_ING, bValue);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SHOW_FX.gameObject, false);
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x0017ACB3 File Offset: 0x00178EB3
		public bool IsSelected()
		{
			return this.m_NKM_UI_DIVE_SLOT_SELECTED.activeSelf || this.m_NKM_UI_DIVE_SLOT_SELECTED_HURDLE.activeSelf || this.m_NKM_UI_DIVE_SLOT_SELECTED_SCAVENGER.activeSelf;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x0017ACE0 File Offset: 0x00178EE0
		public void SetSelected(bool bSet)
		{
			if (this.m_NKMDiveTemplet == null)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMUserData cNKMUserData = myUserData;
			UnlockInfo unlockInfo = new UnlockInfo(this.m_NKMDiveTemplet.StageUnlockReqType, this.m_NKMDiveTemplet.StageUnlockReqValue);
			bool flag = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false);
			switch (this.m_NKMDiveTemplet.StageType)
			{
			default:
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED, bSet);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_HURDLE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_SCAVENGER, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_BUT_LOCKED, !flag);
				return;
			case NKM_DIVE_STAGE_TYPE.NDST_HARD:
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_HURDLE, bSet);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_SCAVENGER, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_HURDLE_BUT_LOCKED, !flag);
				return;
			case NKM_DIVE_STAGE_TYPE.NDST_SCAVENGER:
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_HURDLE, false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_SCAVENGER, bSet);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_SELECTED_SCAVENGER_BUT_LOCKED, !flag);
				return;
			}
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x0017ADE4 File Offset: 0x00178FE4
		public void SetLocked(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_LOCKED, bSet);
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x0017ADF2 File Offset: 0x00178FF2
		public void SetCleared(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_CLEARED, bSet);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_SLOT_NEW_TEXT, !bSet);
		}

		// Token: 0x04003E1D RID: 15901
		public NKCUIComButton m_NKM_UI_DIVE_SLOT;

		// Token: 0x04003E1E RID: 15902
		public Text m_NKM_UI_DIVE_SLOT_TITLE_TEXT;

		// Token: 0x04003E1F RID: 15903
		public Text m_NKM_UI_DIVE_SLOT_SUBTITLE_TEXT;

		// Token: 0x04003E20 RID: 15904
		public GameObject m_NKM_UI_DIVE_SLOT_NEW_TEXT;

		// Token: 0x04003E21 RID: 15905
		public Image m_NKM_UI_DIVE_SLOT_LINE_PREV;

		// Token: 0x04003E22 RID: 15906
		public Image m_NKM_UI_DIVE_SLOT_LINE_NEXT;

		// Token: 0x04003E23 RID: 15907
		[Header("일반")]
		public GameObject m_NKM_UI_DIVE_SLOT_LOCKED;

		// Token: 0x04003E24 RID: 15908
		public GameObject m_NKM_UI_DIVE_SLOT_SELECTED;

		// Token: 0x04003E25 RID: 15909
		public GameObject m_NKM_UI_DIVE_SLOT_SELECTED_BUT_LOCKED;

		// Token: 0x04003E26 RID: 15910
		[Header("하드")]
		public GameObject m_NKM_UI_DIVE_SLOT_SELECTED_HURDLE;

		// Token: 0x04003E27 RID: 15911
		public GameObject m_NKM_UI_DIVE_SLOT_SELECTED_HURDLE_BUT_LOCKED;

		// Token: 0x04003E28 RID: 15912
		[Header("스케빈저")]
		public GameObject m_NKM_UI_DIVE_SLOT_SELECTED_SCAVENGER;

		// Token: 0x04003E29 RID: 15913
		public GameObject m_NKM_UI_DIVE_SLOT_SELECTED_SCAVENGER_BUT_LOCKED;

		// Token: 0x04003E2A RID: 15914
		[Header("")]
		public GameObject m_NKM_UI_DIVE_SLOT_CLEARED;

		// Token: 0x04003E2B RID: 15915
		public GameObject m_NKM_UI_DIVE_SLOT_HURDLE;

		// Token: 0x04003E2C RID: 15916
		public GameObject m_NKM_UI_DIVE_SLOT_SCAVENGER;

		// Token: 0x04003E2D RID: 15917
		public GameObject m_NKM_UI_DIVE_SLOT_DIVE_ING;

		// Token: 0x04003E2E RID: 15918
		public Animator m_NKM_UI_DIVE_SLOT_SHOW_FX;

		// Token: 0x04003E2F RID: 15919
		private NKCUIDiveReadySlot.OnSelectedDiveReadySlot m_OnSelectedDiveReadySlot;

		// Token: 0x04003E30 RID: 15920
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x04003E31 RID: 15921
		private int m_Index = -1;

		// Token: 0x04003E32 RID: 15922
		private NKMDiveTemplet m_NKMDiveTemplet;

		// Token: 0x04003E33 RID: 15923
		private int m_cityID = -1;

		// Token: 0x0200147C RID: 5244
		// (Invoke) Token: 0x0600A8F3 RID: 43251
		public delegate void OnSelectedDiveReadySlot(NKCUIDiveReadySlot cNKCUIDiveReadySlot);
	}
}
