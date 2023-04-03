using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A0E RID: 2574
	public class NKCUIOperationSubSummary : MonoBehaviour
	{
		// Token: 0x06007057 RID: 28759 RVA: 0x002537C0 File Offset: 0x002519C0
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_LoopPVE_01.dOnGetObject += this.GetObjectPVE_01;
			this.m_LoopPVE_01.dOnReturnObject += this.ReturnObjectPVE_01;
			this.m_LoopPVE_01.dOnProvideData += this.ProvideDataPVE_01;
			this.m_LoopPVE_01.PrepareCells(0);
			this.m_LoopPVE_02.dOnGetObject += this.GetObjectPVE_02;
			this.m_LoopPVE_02.dOnReturnObject += this.ReturnObjectPVE_02;
			this.m_LoopPVE_02.dOnProvideData += this.ProvideDataPVE_02;
			this.m_LoopPVE_02.PrepareCells(0);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007058 RID: 28760 RVA: 0x00253888 File Offset: 0x00251A88
		private void CloseBigSlot()
		{
			for (int i = 0; i < NKCUIOperationSubSummary.m_listNKCAssetResourceData.Count; i++)
			{
				NKCAssetResourceManager.CloseInstance(NKCUIOperationSubSummary.m_listNKCAssetResourceData[i]);
			}
			NKCUIOperationSubSummary.m_listNKCAssetResourceData.Clear();
		}

		// Token: 0x06007059 RID: 28761 RVA: 0x002538C4 File Offset: 0x00251AC4
		private RectTransform GetObjectPVE_01(int index)
		{
			NKCUIOperationSubSummarySlot nkcuioperationSubSummarySlot;
			if (this.m_stkPVE_01.Count > 0)
			{
				nkcuioperationSubSummarySlot = this.m_stkPVE_01.Pop();
			}
			else
			{
				nkcuioperationSubSummarySlot = UnityEngine.Object.Instantiate<NKCUIOperationSubSummarySlot>(this.m_pfbPVE_01, this.m_LoopPVE_01.content);
			}
			return nkcuioperationSubSummarySlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x0025390C File Offset: 0x00251B0C
		private void ReturnObjectPVE_01(Transform tr)
		{
			NKCUIOperationSubSummarySlot component = tr.GetComponent<NKCUIOperationSubSummarySlot>();
			if (component == null)
			{
				return;
			}
			this.m_stkPVE_01.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x00253940 File Offset: 0x00251B40
		private void ProvideDataPVE_01(Transform tr, int idx)
		{
			NKCUIOperationSubSummarySlot component = tr.GetComponent<NKCUIOperationSubSummarySlot>();
			if (component == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_lstEpTempletForPVE_01[idx]);
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x00253978 File Offset: 0x00251B78
		private RectTransform GetObjectPVE_02(int index)
		{
			NKCUIOperationSubSummarySlot nkcuioperationSubSummarySlot;
			if (this.m_stkPVE_02.Count > 0)
			{
				nkcuioperationSubSummarySlot = this.m_stkPVE_02.Pop();
			}
			else
			{
				nkcuioperationSubSummarySlot = UnityEngine.Object.Instantiate<NKCUIOperationSubSummarySlot>(this.m_pfbPVE_02, this.m_LoopPVE_02.content);
			}
			return nkcuioperationSubSummarySlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x002539C0 File Offset: 0x00251BC0
		private void ReturnObjectPVE_02(Transform tr)
		{
			NKCUIOperationSubSummarySlot component = tr.GetComponent<NKCUIOperationSubSummarySlot>();
			if (component == null)
			{
				return;
			}
			this.m_stkPVE_02.Push(component);
			NKCUtil.SetGameobjectActive(component, false);
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x002539F4 File Offset: 0x00251BF4
		private void ProvideDataPVE_02(Transform tr, int idx)
		{
			NKCUIOperationSubSummarySlot component = tr.GetComponent<NKCUIOperationSubSummarySlot>();
			if (component == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_lstEpTempletForPVE_02[idx]);
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x00253A2C File Offset: 0x00251C2C
		public void Open()
		{
			this.BuildTempletList();
			if (NKCUIOperationSubSummary.m_listNKCAssetResourceData.Count > 0)
			{
				this.CloseBigSlot();
			}
			if (this.m_BigBannerTemplet != null)
			{
				NKCUIOperationSubSummarySlot nkcuioperationSubSummarySlot = NKCUIOperationSubSummary.OpenInstanceByAssetName<NKCUIOperationSubSummarySlot>(this.m_BigBannerTemplet.m_BigResourceID, this.m_BigBannerTemplet.m_BigResourceID, this.m_trBigRoot);
				if (nkcuioperationSubSummarySlot != null)
				{
					nkcuioperationSubSummarySlot.transform.SetParent(this.m_trBigRoot);
					nkcuioperationSubSummarySlot.SetData(this.m_BigBannerTemplet);
					NKCUtil.SetGameobjectActive(this.m_objBigNone, false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_objBigNone, true);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objBigNone, true);
			}
			this.m_LoopPVE_01.TotalCount = this.m_lstEpTempletForPVE_01.Count;
			NKCUtil.SetGameobjectActive(this.m_objPVE_01_None, this.m_LoopPVE_01.TotalCount == 0);
			if (this.m_LoopPVE_01.TotalCount > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_LoopPVE_01, true);
				this.m_LoopPVE_01.SetIndexPosition(0);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_LoopPVE_01, false);
			}
			this.m_LoopPVE_02.TotalCount = this.m_lstEpTempletForPVE_02.Count;
			NKCUtil.SetGameobjectActive(this.m_objPVE_02_None, this.m_LoopPVE_02.TotalCount == 0);
			if (this.m_LoopPVE_02.TotalCount > 0)
			{
				NKCUtil.SetGameobjectActive(this.m_LoopPVE_02, true);
				this.m_LoopPVE_02.SetIndexPosition(0);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_LoopPVE_02, false);
			}
			if (this.m_slotProgress != null)
			{
				bool flag = this.m_slotProgress.SetMainStreamProgress();
				NKCUtil.SetGameobjectActive(this.m_slotProgress, flag);
				NKCUtil.SetGameobjectActive(this.m_objProgressNone, !flag);
			}
			if (this.m_slotLastPlay != null)
			{
				bool flag2 = this.m_slotLastPlay.SetLastPlayInfo(NKCScenManager.CurrentUserData().m_LastPlayInfo);
				NKCUtil.SetGameobjectActive(this.m_slotLastPlay, flag2);
				NKCUtil.SetGameobjectActive(this.m_objLastPlayNone, !flag2);
			}
			this.TutorialCheck();
		}

		// Token: 0x06007060 RID: 28768 RVA: 0x00253C0B File Offset: 0x00251E0B
		private void BuildTempletList()
		{
			NKMEpisodeMgr.BuildSummaryTemplet(out this.m_BigBannerTemplet, out this.m_lstEpTempletForPVE_01, out this.m_lstEpTempletForPVE_02);
		}

		// Token: 0x06007061 RID: 28769 RVA: 0x00253C24 File Offset: 0x00251E24
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
			NKCUIOperationSubSummary.m_listNKCAssetResourceData.Add(nkcassetInstanceData);
			return component;
		}

		// Token: 0x06007062 RID: 28770 RVA: 0x00253CAA File Offset: 0x00251EAA
		private void TutorialCheck()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.Operation_Summary, true);
		}

		// Token: 0x04005C0B RID: 23563
		[Header("좌측 큰 이벤트배너 부모오브젝트")]
		public Transform m_trBigRoot;

		// Token: 0x04005C0C RID: 23564
		public GameObject m_objBigNone;

		// Token: 0x04005C0D RID: 23565
		public GameObject m_objPVE_01_None;

		// Token: 0x04005C0E RID: 23566
		public NKCUIOperationSubSummarySlot m_pfbPVE_01;

		// Token: 0x04005C0F RID: 23567
		public LoopScrollRect m_LoopPVE_01;

		// Token: 0x04005C10 RID: 23568
		public GameObject m_objPVE_02_None;

		// Token: 0x04005C11 RID: 23569
		public NKCUIOperationSubSummarySlot m_pfbPVE_02;

		// Token: 0x04005C12 RID: 23570
		public LoopScrollRect m_LoopPVE_02;

		// Token: 0x04005C13 RID: 23571
		public GameObject m_objProgressNone;

		// Token: 0x04005C14 RID: 23572
		public NKCUIOperationSubSummarySlot m_slotProgress;

		// Token: 0x04005C15 RID: 23573
		public GameObject m_objLastPlayNone;

		// Token: 0x04005C16 RID: 23574
		public NKCUIOperationSubSummarySlot m_slotLastPlay;

		// Token: 0x04005C17 RID: 23575
		private Stack<NKCUIOperationSubSummarySlot> m_stkPVE_01 = new Stack<NKCUIOperationSubSummarySlot>();

		// Token: 0x04005C18 RID: 23576
		private Stack<NKCUIOperationSubSummarySlot> m_stkPVE_02 = new Stack<NKCUIOperationSubSummarySlot>();

		// Token: 0x04005C19 RID: 23577
		private NKCEpisodeSummaryTemplet m_BigBannerTemplet;

		// Token: 0x04005C1A RID: 23578
		private List<NKCEpisodeSummaryTemplet> m_lstEpTempletForPVE_01 = new List<NKCEpisodeSummaryTemplet>();

		// Token: 0x04005C1B RID: 23579
		private List<NKCEpisodeSummaryTemplet> m_lstEpTempletForPVE_02 = new List<NKCEpisodeSummaryTemplet>();

		// Token: 0x04005C1C RID: 23580
		private static List<NKCAssetInstanceData> m_listNKCAssetResourceData = new List<NKCAssetInstanceData>();
	}
}
