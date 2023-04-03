using System;
using NKM.EventPass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200098D RID: 2445
	public class NKCUIEventPassRewardSlot : MonoBehaviour
	{
		// Token: 0x06006503 RID: 25859 RVA: 0x00201D84 File Offset: 0x001FFF84
		private void OnDestroy()
		{
			this.m_lbPassLevel = null;
			this.m_objComplete = null;
			this.m_objProceeding = null;
			this.m_objLock = null;
			this.m_objProcedingLine = null;
			this.m_objCompleteFull = null;
			this.m_normalRewardGroup.Release();
			this.m_coreRewardGroup.Release();
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x00201DDC File Offset: 0x001FFFDC
		public static NKCUIEventPassRewardSlot GetNewInstance(Transform parent, bool bMentoringSlot = false)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_EVENT_PASS", "NKM_UI_EVENT_PASS_SLOT", false, null);
			NKCUIEventPassRewardSlot nkcuieventPassRewardSlot = (nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant.GetComponent<NKCUIEventPassRewardSlot>() : null;
			if (nkcuieventPassRewardSlot == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCUIEventPassRewardSlot Prefab null!");
				return null;
			}
			nkcuieventPassRewardSlot.m_InstanceData = nkcassetInstanceData;
			nkcuieventPassRewardSlot.Init();
			if (parent != null)
			{
				nkcuieventPassRewardSlot.transform.SetParent(parent);
			}
			nkcuieventPassRewardSlot.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			nkcuieventPassRewardSlot.gameObject.SetActive(false);
			return nkcuieventPassRewardSlot;
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x00201E76 File Offset: 0x00200076
		public void DestoryInstance()
		{
			NKCAssetResourceManager.CloseInstance(this.m_InstanceData);
			this.m_InstanceData = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x00201E98 File Offset: 0x00200098
		public void Init()
		{
			NKCUISlot rewardIconSlot = this.m_normalRewardGroup.rewardIconSlot;
			if (rewardIconSlot != null)
			{
				rewardIconSlot.Init();
			}
			NKCUISlot rewardIconSlot2 = this.m_coreRewardGroup.rewardIconSlot;
			if (rewardIconSlot2 != null)
			{
				rewardIconSlot2.Init();
			}
			NKCUtil.SetButtonClickDelegate(this.m_normalRewardGroup.csbtnGetReward, new UnityAction(this.OnClickGetReward));
			NKCUtil.SetButtonClickDelegate(this.m_coreRewardGroup.csbtnGetReward, new UnityAction(this.OnClickGetReward));
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00201F0C File Offset: 0x0020010C
		public void SetData(NKMEventPassRewardTemplet passRewardTemplet, int userPassLevel, int maxPassLevel, bool corePassPurchased, int normalRewardLevel, int coreRewardLevel, NKCUIEventPassRewardSlot.dOnClickGetReward onClickGetReward)
		{
			if (passRewardTemplet == null)
			{
				this.m_dOnClickGetReward = null;
				return;
			}
			NKCUtil.SetLabelText(this.m_lbPassLevel, string.Format("{0:00}", passRewardTemplet.PassLevel));
			bool flag = passRewardTemplet.PassLevel <= userPassLevel;
			bool flag2 = passRewardTemplet.PassLevel <= normalRewardLevel;
			this.SetNormalRewardGroup(this.m_normalRewardGroup, passRewardTemplet, flag, flag2);
			bool flag3 = passRewardTemplet.PassLevel <= coreRewardLevel;
			this.SetCoreRewardGroup(this.m_coreRewardGroup, passRewardTemplet, flag, flag3, corePassPurchased);
			bool flag4 = (flag2 && !corePassPurchased) || (flag2 && corePassPurchased && flag3);
			NKCUtil.SetGameobjectActive(this.m_objCompleteFull, flag4);
			NKCUtil.SetGameobjectActive(this.m_objProcedingLine, flag && !flag4);
			if (flag)
			{
				NKCUtil.SetGameobjectActive(this.m_objProceeding, !flag4);
				NKCUtil.SetGameobjectActive(this.m_objComplete, flag4);
				NKCUtil.SetGameobjectActive(this.m_objLock, false);
				NKCUtil.SetLabelTextColor(this.m_lbPassLevel, flag4 ? this.m_colCompleteText : this.m_colProceedText);
				if (userPassLevel == passRewardTemplet.PassLevel || userPassLevel == maxPassLevel)
				{
					NKCUtil.SetImageFillAmount(this.m_imgCenterGauge, 0.5f);
				}
				else
				{
					NKCUtil.SetImageFillAmount(this.m_imgCenterGauge, 1f);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objLock, true);
				NKCUtil.SetImageFillAmount(this.m_imgCenterGauge, 0f);
				NKCUtil.SetLabelTextColor(this.m_lbPassLevel, this.m_colLockText);
			}
			this.m_dOnClickGetReward = onClickGetReward;
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0020206C File Offset: 0x0020026C
		private void SetNormalRewardGroup(NKCUIEventPassRewardSlot.EventPassRewardGroup normalRewardGroup, NKMEventPassRewardTemplet passRewardTemplet, bool isUnderUserPassLevel, bool normalRewardReceiced)
		{
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(passRewardTemplet.NormalRewardItemType, passRewardTemplet.NormalRewardItemId, passRewardTemplet.NormalRewardItemCount, 0);
			if (normalRewardGroup.rewardIconSlot != null)
			{
				if (slotData.eType == NKCUISlot.eSlotMode.ItemMisc)
				{
					normalRewardGroup.rewardIconSlot.SetData(slotData, true, new NKCUISlot.OnClick(this.OnClickRewardIcon));
				}
				else
				{
					normalRewardGroup.rewardIconSlot.SetData(slotData, true, null);
				}
				normalRewardGroup.rewardIconSlot.SetCompleteMark(normalRewardReceiced);
			}
			NKCUIComStateButton csbtnGetReward = normalRewardGroup.csbtnGetReward;
			NKCUtil.SetGameobjectActive((csbtnGetReward != null) ? csbtnGetReward.gameObject : null, isUnderUserPassLevel);
			NKCUIComStateButton csbtnGetReward2 = normalRewardGroup.csbtnGetReward;
			if (csbtnGetReward2 == null)
			{
				return;
			}
			csbtnGetReward2.SetLock(normalRewardReceiced, false);
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x0020210C File Offset: 0x0020030C
		private void SetCoreRewardGroup(NKCUIEventPassRewardSlot.EventPassRewardGroup coreRewardGroup, NKMEventPassRewardTemplet passRewardTemplet, bool isUnderUserPassLevel, bool coreRewardRecieved, bool corePassPurchased)
		{
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(passRewardTemplet.CoreRewardItemType, passRewardTemplet.CoreRewardItemId, passRewardTemplet.CoreRewardItemCount, 0);
			if (coreRewardGroup.rewardIconSlot != null)
			{
				if (slotData.eType == NKCUISlot.eSlotMode.ItemMisc)
				{
					coreRewardGroup.rewardIconSlot.SetData(slotData, true, new NKCUISlot.OnClick(this.OnClickRewardIcon));
				}
				else
				{
					coreRewardGroup.rewardIconSlot.SetData(slotData, true, null);
				}
				coreRewardGroup.rewardIconSlot.SetCompleteMark(coreRewardRecieved);
				coreRewardGroup.rewardIconSlot.SetDisable(!corePassPurchased, "");
			}
			if (!corePassPurchased)
			{
				NKCUtil.SetGameobjectActive(coreRewardGroup.objRewardLocked, true);
				NKCUIComStateButton csbtnGetReward = coreRewardGroup.csbtnGetReward;
				NKCUtil.SetGameobjectActive((csbtnGetReward != null) ? csbtnGetReward.gameObject : null, false);
				return;
			}
			NKCUtil.SetGameobjectActive(coreRewardGroup.objRewardLocked, false);
			NKCUIComStateButton csbtnGetReward2 = coreRewardGroup.csbtnGetReward;
			NKCUtil.SetGameobjectActive((csbtnGetReward2 != null) ? csbtnGetReward2.gameObject : null, isUnderUserPassLevel);
			NKCUIComStateButton csbtnGetReward3 = coreRewardGroup.csbtnGetReward;
			if (csbtnGetReward3 == null)
			{
				return;
			}
			csbtnGetReward3.SetLock(coreRewardRecieved, false);
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x002021F4 File Offset: 0x002003F4
		private void OnClickRewardIcon(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, false);
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x00202206 File Offset: 0x00200406
		private void OnClickGetReward()
		{
			if (this.m_dOnClickGetReward != null)
			{
				this.m_dOnClickGetReward();
			}
		}

		// Token: 0x040050AE RID: 20654
		public Text m_lbPassLevel;

		// Token: 0x040050AF RID: 20655
		public Color m_colCompleteText;

		// Token: 0x040050B0 RID: 20656
		public Color m_colProceedText;

		// Token: 0x040050B1 RID: 20657
		public Color m_colLockText;

		// Token: 0x040050B2 RID: 20658
		public Image m_imgCenterGauge;

		// Token: 0x040050B3 RID: 20659
		public GameObject m_objComplete;

		// Token: 0x040050B4 RID: 20660
		public GameObject m_objProceeding;

		// Token: 0x040050B5 RID: 20661
		public GameObject m_objLock;

		// Token: 0x040050B6 RID: 20662
		public GameObject m_objProcedingLine;

		// Token: 0x040050B7 RID: 20663
		public GameObject m_objCompleteFull;

		// Token: 0x040050B8 RID: 20664
		public NKCUIEventPassRewardSlot.EventPassRewardGroup m_coreRewardGroup;

		// Token: 0x040050B9 RID: 20665
		public NKCUIEventPassRewardSlot.EventPassRewardGroup m_normalRewardGroup;

		// Token: 0x040050BA RID: 20666
		private NKCAssetInstanceData m_InstanceData;

		// Token: 0x040050BB RID: 20667
		private NKCUIEventPassRewardSlot.dOnClickGetReward m_dOnClickGetReward;

		// Token: 0x02001652 RID: 5714
		[Serializable]
		public struct EventPassRewardGroup
		{
			// Token: 0x0600AFF2 RID: 45042 RVA: 0x0035E60B File Offset: 0x0035C80B
			public void Release()
			{
				this.rewardIconSlot = null;
				this.csbtnGetReward = null;
				this.objRewardLocked = null;
			}

			// Token: 0x0400A40E RID: 41998
			public NKCUISlot rewardIconSlot;

			// Token: 0x0400A40F RID: 41999
			public NKCUIComStateButton csbtnGetReward;

			// Token: 0x0400A410 RID: 42000
			public GameObject objRewardLocked;
		}

		// Token: 0x02001653 RID: 5715
		// (Invoke) Token: 0x0600AFF4 RID: 45044
		public delegate void dOnClickGetReward();
	}
}
