using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000986 RID: 2438
	public class NKCUIEpisodeActSlotCC : MonoBehaviour
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06006400 RID: 25600 RVA: 0x001FB66A File Offset: 0x001F986A
		public int UnitID
		{
			get
			{
				return this.m_UnitID;
			}
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x001FB672 File Offset: 0x001F9872
		private void OnDestroy()
		{
			NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceData);
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x001FB680 File Offset: 0x001F9880
		public static NKCUIEpisodeActSlotCC GetNewInstance(Transform parent, NKCUIEpisodeActSlotCC.OnSelectedItemSlot selectedSlot = null)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_COUNTER_CASE", "NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT", false, null);
			NKCUIEpisodeActSlotCC component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIEpisodeActSlotCC>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIEpisodeActSlotCC Prefab null!");
				return null;
			}
			component.m_NKCAssetInstanceData = nkcassetInstanceData;
			component.SetOnSelectedItemSlot(selectedSlot);
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.m_comBtn.PointerClick.RemoveAllListeners();
			component.m_comBtn.PointerClick.AddListener(new UnityAction(component.OnSelectedItemSlotImpl));
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x001FB757 File Offset: 0x001F9957
		public void SetOnSelectedItemSlot(NKCUIEpisodeActSlotCC.OnSelectedItemSlot selectedSlot)
		{
			if (selectedSlot != null)
			{
				this.m_OnSelectedSlot = selectedSlot;
			}
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x001FB763 File Offset: 0x001F9963
		private void OnSelectedItemSlotImpl()
		{
			if (this.m_OnSelectedSlot != null)
			{
				this.m_OnSelectedSlot(this.m_ActID);
				NKCContentManager.RemoveUnlockedCounterCaseKey(this.m_ActID);
			}
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x001FB78C File Offset: 0x001F998C
		private bool IsComplete(NKMEpisodeTempletV2 cNKMEpisodeTemplet, int actID)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return false;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return false;
			}
			if (cNKMEpisodeTemplet.m_DicStage.ContainsKey(actID))
			{
				for (int i = 0; i < cNKMEpisodeTemplet.m_DicStage[actID].Count; i++)
				{
					if (!NKMEpisodeMgr.CheckClear(myUserData, cNKMEpisodeTemplet.m_DicStage[actID][i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x001FB7F8 File Offset: 0x001F99F8
		private void UpdateProgressBarUI(NKMEpisodeTempletV2 cNKMEpisodeTemplet, int actID)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			if (cNKMEpisodeTemplet.m_DicStage.ContainsKey(actID))
			{
				for (int i = 0; i < this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS_BAR.Count; i++)
				{
					if (i < cNKMEpisodeTemplet.m_DicStage[actID].Count)
					{
						NKCUtil.SetImageColor(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS_BAR[i].GetComponent<Image>(), Color.white);
						if (NKMEpisodeMgr.CheckClear(myUserData, cNKMEpisodeTemplet.m_DicStage[actID][i]))
						{
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS_BAR[i], true);
						}
						else
						{
							if (PlayerPrefs.HasKey(NKCContentManager.GetCounterCaseNormalKey(actID)))
							{
								NKCUtil.SetGameobjectActive(this.m_objRedDot, true);
							}
							NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS_BAR[i], false);
						}
					}
					else
					{
						NKCUtil.SetImageColor(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS_BAR[i].GetComponent<Image>(), Color.black);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS_BAR[i], true);
					}
				}
			}
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x001FB8FC File Offset: 0x001F9AFC
		public void SetData(NKMEpisodeTempletV2 cNKMEpisodeTemplet, int actID)
		{
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			this.m_ActID = actID;
			this.m_UnitID = NKMEpisodeMgr.GetUnitID(cNKMEpisodeTemplet, actID);
			bool flag = NKMEpisodeMgr.CheckLockCounterCase(NKCScenManager.GetScenManager().GetMyUserData(), cNKMEpisodeTemplet, actID);
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS, !flag);
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
			this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_UNIT.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UNIT_FACE_CARD", unitTempletBase.m_FaceCardName, false);
			if (!flag)
			{
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NICKNAME.text = unitTempletBase.GetUnitTitle();
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NAME.text = unitTempletBase.GetUnitName();
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_UNIT.color = new Color(1f, 1f, 1f, 1f);
				bool flag2 = this.IsComplete(cNKMEpisodeTemplet, actID);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_COMPLETE, flag2);
				if (flag2)
				{
					this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_BG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COUNTER_CASE_SPRITE", "AB_UI_NKM_UI_COUNTER_CASE_UNIT_SLOT_BG_COMPLETE", false);
				}
				else
				{
					this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_BG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COUNTER_CASE_SPRITE", "AB_UI_NKM_UI_COUNTER_CASE_UNIT_SLOT_BG", false);
				}
				this.UpdateProgressBarUI(cNKMEpisodeTemplet, actID);
			}
			else
			{
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_BG.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_COUNTER_CASE_SPRITE", "AB_UI_NKM_UI_COUNTER_CASE_UNIT_SLOT_BG", false);
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NICKNAME.text = "";
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NAME.text = "";
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_UNIT.color = new Color(0f, 0f, 0f, 1f);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_COMPLETE, false);
			}
			this.m_comBtn.enabled = !flag;
			if (this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NODATA.activeSelf == !flag)
			{
				this.m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NODATA.SetActive(flag);
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x001FBAC3 File Offset: 0x001F9CC3
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf == !bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x001FBAE2 File Offset: 0x001F9CE2
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x04004F9F RID: 20383
		public NKCUIComButton m_comBtn;

		// Token: 0x04004FA0 RID: 20384
		public Image m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_BG;

		// Token: 0x04004FA1 RID: 20385
		public Image m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_UNIT;

		// Token: 0x04004FA2 RID: 20386
		public GameObject m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NODATA;

		// Token: 0x04004FA3 RID: 20387
		public GameObject m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_COMPLETE;

		// Token: 0x04004FA4 RID: 20388
		public GameObject m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS;

		// Token: 0x04004FA5 RID: 20389
		public Text m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NICKNAME;

		// Token: 0x04004FA6 RID: 20390
		public Text m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_NAME;

		// Token: 0x04004FA7 RID: 20391
		public GameObject m_objRedDot;

		// Token: 0x04004FA8 RID: 20392
		public List<GameObject> m_NKM_UI_COUNTER_CASE_UNIT_LIST_SLOT_PROGRESS_BAR;

		// Token: 0x04004FA9 RID: 20393
		private NKCUIEpisodeActSlotCC.OnSelectedItemSlot m_OnSelectedSlot;

		// Token: 0x04004FAA RID: 20394
		private int m_ActID;

		// Token: 0x04004FAB RID: 20395
		private int m_UnitID;

		// Token: 0x04004FAC RID: 20396
		private NKCAssetInstanceData m_NKCAssetInstanceData;

		// Token: 0x02001639 RID: 5689
		// (Invoke) Token: 0x0600AF89 RID: 44937
		public delegate void OnSelectedItemSlot(int actID);
	}
}
