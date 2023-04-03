using System;
using System.Collections;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000954 RID: 2388
	public class NKCUIEventBarMissionGroupList : MonoBehaviour
	{
		// Token: 0x06005F38 RID: 24376 RVA: 0x001D95F0 File Offset: 0x001D77F0
		public void Init(string slotBundleName, string slotAssetName)
		{
			this.m_slotBundleName = slotBundleName;
			this.m_slotAssetName = slotAssetName;
			if (this.m_LoopScrollRect != null)
			{
				this.m_LoopScrollRect.dOnGetObject += this.GetPresetSlot;
				this.m_LoopScrollRect.dOnReturnObject += this.ReturnPresetSlot;
				this.m_LoopScrollRect.dOnProvideData += this.ProvidePresetData;
				this.m_LoopScrollRect.ContentConstraintCount = 1;
				this.m_LoopScrollRect.PrepareCells(0);
				this.m_LoopScrollRect.TotalCount = 0;
				this.m_LoopScrollRect.RefreshCells(false);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.Close));
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x001D96A8 File Offset: 0x001D78A8
		public void Open(NKCUIEventBarMissionGroupList.MissionType missionType, int missionId)
		{
			if (this.m_canvasGroup != null)
			{
				this.m_canvasGroup.blocksRaycasts = true;
			}
			if (this.m_coroutine != null)
			{
				base.StopCoroutine(this.m_coroutine);
				this.m_coroutine = null;
			}
			if (this.m_missionTempletList == null)
			{
				List<NKMMissionTemplet> list = null;
				if (missionType != NKCUIEventBarMissionGroupList.MissionType.MissionGroupId)
				{
					if (missionType == NKCUIEventBarMissionGroupList.MissionType.MissionTabId)
					{
						list = NKMMissionManager.GetMissionTempletListByTabID(missionId);
					}
				}
				else
				{
					list = NKMMissionManager.GetMissionTempletListByGroupID(missionId);
				}
				if (list == null)
				{
					return;
				}
				this.m_missionTempletList = list;
			}
			base.gameObject.SetActive(false);
			base.gameObject.SetActive(true);
			this.m_LoopScrollRect.TotalCount = this.m_missionTempletList.Count;
			this.m_LoopScrollRect.SetIndexPosition(0);
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x001D9752 File Offset: 0x001D7952
		public bool IsOpened()
		{
			return base.gameObject.activeSelf && this.m_coroutine == null;
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x001D976C File Offset: 0x001D796C
		public void Refresh()
		{
			this.m_LoopScrollRect.RefreshCells(false);
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x001D977A File Offset: 0x001D797A
		public void CloseImmediately()
		{
			if (this.m_coroutine != null)
			{
				base.StopCoroutine(this.m_coroutine);
				this.m_coroutine = null;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x001D97A3 File Offset: 0x001D79A3
		public bool IsClosed()
		{
			return this.m_coroutine != null || !base.gameObject.activeSelf;
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x001D97BD File Offset: 0x001D79BD
		public void Close()
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			if (this.m_coroutine == null)
			{
				this.m_coroutine = base.StartCoroutine(this.Outro());
			}
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x001D97E7 File Offset: 0x001D79E7
		private IEnumerator Outro()
		{
			if (this.m_canvasGroup != null)
			{
				this.m_canvasGroup.blocksRaycasts = false;
			}
			if (this.m_animator != null)
			{
				this.m_animator.Play(this.m_outroAnimation, -1, 0f);
				float startTime = this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
				while (this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime - startTime < 1f)
				{
					yield return null;
				}
			}
			base.gameObject.SetActive(false);
			this.m_coroutine = null;
			yield break;
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x001D97F6 File Offset: 0x001D79F6
		private RectTransform GetPresetSlot(int index)
		{
			NKCUIEventBarMissionGroupListSlot newInstance = NKCUIEventBarMissionGroupListSlot.GetNewInstance(null, this.m_slotBundleName, this.m_slotAssetName);
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x001D9818 File Offset: 0x001D7A18
		private void ReturnPresetSlot(Transform tr)
		{
			NKCUIEventBarMissionGroupListSlot component = tr.GetComponent<NKCUIEventBarMissionGroupListSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x001D9850 File Offset: 0x001D7A50
		private void ProvidePresetData(Transform tr, int index)
		{
			NKCUIEventBarMissionGroupListSlot component = tr.GetComponent<NKCUIEventBarMissionGroupListSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(this.m_missionTempletList[index]);
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x001D9880 File Offset: 0x001D7A80
		private void OnDestroy()
		{
			this.m_slotBundleName = null;
			this.m_slotAssetName = null;
			if (this.m_missionTempletList != null)
			{
				this.m_missionTempletList.Clear();
				this.m_missionTempletList = null;
			}
			this.m_coroutine = null;
		}

		// Token: 0x04004B4B RID: 19275
		public CanvasGroup m_canvasGroup;

		// Token: 0x04004B4C RID: 19276
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04004B4D RID: 19277
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04004B4E RID: 19278
		[Header("Outro Animation")]
		public Animator m_animator;

		// Token: 0x04004B4F RID: 19279
		public string m_outroAnimation;

		// Token: 0x04004B50 RID: 19280
		private List<NKMMissionTemplet> m_missionTempletList;

		// Token: 0x04004B51 RID: 19281
		private Coroutine m_coroutine;

		// Token: 0x04004B52 RID: 19282
		private string m_slotBundleName;

		// Token: 0x04004B53 RID: 19283
		private string m_slotAssetName;

		// Token: 0x020015D6 RID: 5590
		public enum MissionType
		{
			// Token: 0x0400A2A4 RID: 41636
			MissionGroupId,
			// Token: 0x0400A2A5 RID: 41637
			MissionTabId
		}
	}
}
