using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000926 RID: 2342
	public class NKCUIChangeLobbyFace : MonoBehaviour
	{
		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06005DCE RID: 24014 RVA: 0x001CF547 File Offset: 0x001CD747
		// (set) Token: 0x06005DCF RID: 24015 RVA: 0x001CF54F File Offset: 0x001CD74F
		public bool IsOpen { get; private set; }

		// Token: 0x06005DD0 RID: 24016 RVA: 0x001CF558 File Offset: 0x001CD758
		public void Init(NKCUIChangeLobbyFace.OnSelectFace onSelectFace)
		{
			if (this.m_srFace != null)
			{
				this.m_srFace.dOnGetObject += this.GetObject;
				this.m_srFace.dOnReturnObject += this.ReturnObject;
				this.m_srFace.dOnProvideData += this.ProvideData;
			}
			this.dOnSelectFace = onSelectFace;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOutsideTouch, new UnityAction(this.Close));
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x001CF5F0 File Offset: 0x001CD7F0
		public void Open(NKMUnitData unitData, int currentFaceID, NKCASUIUnitIllust targetIllust)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_currentUnitData = unitData;
			this.m_currentOperator = null;
			this.m_currentFaceID = currentFaceID;
			if (unitData != null)
			{
				int num = unitData.loyalty / 100;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			bool flag = unitTempletBase != null && unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_TRAINER);
			NKCUtil.SetGameobjectActive(this.m_UnitLoyalty, !flag);
			NKCUtil.SetGameobjectActive(this.m_objLoyaltyDesc, !flag);
			if (this.m_UnitLoyalty != null)
			{
				this.m_UnitLoyalty.SetLoyalty(unitData);
			}
			this.m_eUnitType = NKM_UNIT_TYPE.NUT_NORMAL;
			this.m_lstLobbyFace = this.MakeFaceList(targetIllust);
			if (!this.m_bSlotInit)
			{
				this.m_bSlotInit = true;
				this.m_srFace.TotalCount = this.m_lstLobbyFace.Count;
				this.m_srFace.PrepareCells(0);
				this.m_srFace.SetIndexPosition(0);
			}
			else
			{
				this.m_srFace.TotalCount = this.m_lstLobbyFace.Count;
				this.m_srFace.RefreshCells(true);
			}
			this.IsOpen = true;
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x001CF6F4 File Offset: 0x001CD8F4
		public void Open(NKMOperator operatorData, int currentFaceID, NKCASUIUnitIllust targetIllust)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_currentUnitData = null;
			this.m_currentOperator = operatorData;
			this.m_currentFaceID = currentFaceID;
			NKCUtil.SetGameobjectActive(this.m_UnitLoyalty, false);
			NKCUtil.SetGameobjectActive(this.m_objLoyaltyDesc, false);
			this.m_eUnitType = NKM_UNIT_TYPE.NUT_OPERATOR;
			this.m_lstLobbyFace = this.MakeFaceList(targetIllust);
			if (!this.m_bSlotInit)
			{
				this.m_bSlotInit = true;
				this.m_srFace.TotalCount = this.m_lstLobbyFace.Count;
				this.m_srFace.PrepareCells(0);
				this.m_srFace.SetIndexPosition(0);
			}
			else
			{
				this.m_srFace.TotalCount = this.m_lstLobbyFace.Count;
				this.m_srFace.RefreshCells(true);
			}
			this.IsOpen = true;
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x001CF7B6 File Offset: 0x001CD9B6
		public void Close()
		{
			base.gameObject.SetActive(false);
			this.IsOpen = false;
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x001CF7CC File Offset: 0x001CD9CC
		private List<NKMLobbyFaceTemplet> MakeFaceList(NKCASUIUnitIllust illust)
		{
			List<NKMLobbyFaceTemplet> list = new List<NKMLobbyFaceTemplet>();
			if (illust == null)
			{
				return list;
			}
			foreach (NKMLobbyFaceTemplet nkmlobbyFaceTemplet in NKMTempletContainer<NKMLobbyFaceTemplet>.Values)
			{
				NKCASUIUnitIllust.eAnimation value;
				if (Enum.TryParse<NKCASUIUnitIllust.eAnimation>(nkmlobbyFaceTemplet.AnimationName, out value) && illust.HasAnimation(value))
				{
					list.Add(nkmlobbyFaceTemplet);
				}
			}
			return list;
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x001CF83C File Offset: 0x001CDA3C
		private void OnSelectSlot(bool value, int data)
		{
			if (!value)
			{
				return;
			}
			this.m_currentFaceID = data;
			NKCUIChangeLobbyFace.OnSelectFace onSelectFace = this.dOnSelectFace;
			if (onSelectFace == null)
			{
				return;
			}
			onSelectFace(data);
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x001CF85A File Offset: 0x001CDA5A
		private RectTransform GetObject(int index)
		{
			NKCUIChangeLobbyFaceSlot nkcuichangeLobbyFaceSlot = UnityEngine.Object.Instantiate<NKCUIChangeLobbyFaceSlot>(this.m_pfbSlot);
			nkcuichangeLobbyFaceSlot.Init(new NKCUIComToggle.ValueChangedWithData(this.OnSelectSlot), this.m_tglgrpSlot);
			return nkcuichangeLobbyFaceSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06005DD7 RID: 24023 RVA: 0x001CF884 File Offset: 0x001CDA84
		private void ReturnObject(Transform tr)
		{
			tr.gameObject.SetActive(false);
			tr.SetParent(null);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x001CF8A4 File Offset: 0x001CDAA4
		private void ProvideData(Transform tr, int idx)
		{
			NKCUIChangeLobbyFaceSlot component = tr.GetComponent<NKCUIChangeLobbyFaceSlot>();
			if (component == null)
			{
				return;
			}
			NKM_UNIT_TYPE eUnitType = this.m_eUnitType;
			if (eUnitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (eUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					component.SetData(this.m_currentOperator, this.m_lstLobbyFace[idx]);
				}
			}
			else
			{
				component.SetData(this.m_currentUnitData, this.m_lstLobbyFace[idx]);
			}
			component.SetSelected(this.m_lstLobbyFace[idx].Key == this.m_currentFaceID);
		}

		// Token: 0x04004A0B RID: 18955
		public NKCUIChangeLobbyFaceSlot m_pfbSlot;

		// Token: 0x04004A0C RID: 18956
		public LoopScrollRect m_srFace;

		// Token: 0x04004A0D RID: 18957
		public NKCUIComToggleGroup m_tglgrpSlot;

		// Token: 0x04004A0E RID: 18958
		public NKCUIComUnitLoyalty m_UnitLoyalty;

		// Token: 0x04004A0F RID: 18959
		public GameObject m_objLoyaltyDesc;

		// Token: 0x04004A10 RID: 18960
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04004A11 RID: 18961
		public NKCUIComStateButton m_csbtnOutsideTouch;

		// Token: 0x04004A12 RID: 18962
		private NKCUIChangeLobbyFace.OnSelectFace dOnSelectFace;

		// Token: 0x04004A13 RID: 18963
		private List<NKMLobbyFaceTemplet> m_lstLobbyFace;

		// Token: 0x04004A14 RID: 18964
		private NKM_UNIT_TYPE m_eUnitType;

		// Token: 0x04004A15 RID: 18965
		private NKMUnitData m_currentUnitData;

		// Token: 0x04004A16 RID: 18966
		private NKMOperator m_currentOperator;

		// Token: 0x04004A17 RID: 18967
		private int m_currentFaceID;

		// Token: 0x04004A18 RID: 18968
		private bool m_bSlotInit;

		// Token: 0x020015B7 RID: 5559
		// (Invoke) Token: 0x0600AE02 RID: 44546
		public delegate void OnSelectFace(int id);
	}
}
