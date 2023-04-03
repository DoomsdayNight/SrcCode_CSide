using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007B4 RID: 1972
	public class NKCUICCSecretSlot : MonoBehaviour
	{
		// Token: 0x06004E0A RID: 19978 RVA: 0x00177DAA File Offset: 0x00175FAA
		public void SetOnSelectedItemSlot(NKCUICCSecretSlot.OnSelectedCCSSlot _OnSelectedSlot)
		{
			this.m_OnSelectedSlot = _OnSelectedSlot;
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x00177DB3 File Offset: 0x00175FB3
		public int GetActID()
		{
			return this.m_ActID;
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x00177DBB File Offset: 0x00175FBB
		public bool IsBookOpen()
		{
			return this.m_bBookOpen;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00177DC3 File Offset: 0x00175FC3
		public void SetBookOpen(bool bSet)
		{
			this.m_bBookOpen = bSet;
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x00177DCC File Offset: 0x00175FCC
		public void ResetPos()
		{
			this.m_RTButton.anchoredPosition = new Vector2(660f + (float)this.m_Index * 572f, 0f);
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x00177DF8 File Offset: 0x00175FF8
		public static NKCUICCSecretSlot GetNewInstance(int index, Transform parent, NKCUICCSecretSlot.OnSelectedCCSSlot dOnSelectedItemSlot)
		{
			NKCUICCSecretSlot component = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_COUNTER_CASE", "NKM_UI_COUNTER_CASE_SECRET_SLOT", false, null).m_Instant.GetComponent<NKCUICCSecretSlot>();
			if (component == null)
			{
				Debug.LogError("NKCUICCSecretSlot Prefab null!");
				return null;
			}
			component.m_Index = index;
			component.SetOnSelectedItemSlot(dOnSelectedItemSlot);
			component.m_comBtn.PointerClick.RemoveAllListeners();
			component.m_comBtn.PointerClick.AddListener(new UnityAction(component.OnSelectedSlotImpl));
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.ResetPos();
			Vector3 localScale = new Vector3(0.9f, 0.9f, 1f);
			component.m_RTButton.localScale = localScale;
			component.m_fOrgHalfWidth = component.m_RTButton.sizeDelta.x / 2f;
			component.ResetPos();
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x00177EDC File Offset: 0x001760DC
		public float GetHalfOfWidth()
		{
			return this.m_fOrgHalfWidth;
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x00177EE4 File Offset: 0x001760E4
		public float GetCenterX()
		{
			return this.m_RTButton.anchoredPosition.x + this.m_RTButton.sizeDelta.x / 2f;
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x00177F0D File Offset: 0x0017610D
		public void SetData(NKMEpisodeTempletV2 cNKMEpisodeTemplet, int actID, bool bLock = false)
		{
			this.m_ActID = actID;
			if (cNKMEpisodeTemplet == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COUNTER_CASE_SECRET_SLOT_LOCK, bLock);
			this.SetActive(true);
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x00177F2D File Offset: 0x0017612D
		public void OnSelectedSlotImpl()
		{
			if (this.m_OnSelectedSlot != null)
			{
				this.m_OnSelectedSlot(this.m_ActID);
			}
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x00177F48 File Offset: 0x00176148
		public void SetActive(bool bSet)
		{
			if (base.gameObject.activeSelf == !bSet)
			{
				base.gameObject.SetActive(bSet);
			}
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x00177F67 File Offset: 0x00176167
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x04003D9B RID: 15771
		public Image m_AB_UI_NKM_UI_COUNTER_CASE_SECRET_SLOT_UNIT_IMAGE;

		// Token: 0x04003D9C RID: 15772
		public GameObject m_NKM_UI_COUNTER_CASE_SECRET_SLOT_LOCK;

		// Token: 0x04003D9D RID: 15773
		public NKCUIComButton m_comBtn;

		// Token: 0x04003D9E RID: 15774
		public RectTransform m_RTButton;

		// Token: 0x04003D9F RID: 15775
		public Animator m_Animator;

		// Token: 0x04003DA0 RID: 15776
		private float m_fOrgHalfWidth;

		// Token: 0x04003DA1 RID: 15777
		private NKCUICCSecretSlot.OnSelectedCCSSlot m_OnSelectedSlot;

		// Token: 0x04003DA2 RID: 15778
		private int m_ActID;

		// Token: 0x04003DA3 RID: 15779
		private bool m_bBookOpen;

		// Token: 0x04003DA4 RID: 15780
		public const float SIZE_X = 642f;

		// Token: 0x04003DA5 RID: 15781
		public const float SIZE_OFFSET_X = 70f;

		// Token: 0x04003DA6 RID: 15782
		public const float POS_OFFSET_X = 960f;

		// Token: 0x04003DA7 RID: 15783
		public const float POS_OFFSET_X_BY_VIEWPORT = -300f;

		// Token: 0x04003DA8 RID: 15784
		private int m_Index = -1;

		// Token: 0x02001475 RID: 5237
		// (Invoke) Token: 0x0600A8D8 RID: 43224
		public delegate void OnSelectedCCSSlot(int actID);
	}
}
