using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A50 RID: 2640
	public class NKCPopupFilterSubUIEquipStat : MonoBehaviour
	{
		// Token: 0x060073E6 RID: 29670 RVA: 0x002688B0 File Offset: 0x00266AB0
		public void Open(bool bShowSetOption, List<NKM_STAT_TYPE> lstSelectedStatType, NKCPopupFilterSubUIEquipStat.OnClickStatSlot onClickStatSlot, int selectedSlotIdx)
		{
			if (this.m_selectedIdx == selectedSlotIdx)
			{
				this.Close();
				return;
			}
			this.m_selectedIdx = selectedSlotIdx;
			this.ResetSlot();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (bShowSetOption)
			{
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_FILTER_EQUIP_TYPE_STAT_SET);
				for (int i = 0; i < NKMItemManager.m_lstItemEquipSetOptionTemplet.Count; i++)
				{
					if (NKMItemManager.m_lstItemEquipSetOptionTemplet[i].UseFilter && NKMOpenTagManager.IsOpened(NKMItemManager.m_lstItemEquipSetOptionTemplet[i].m_OpenTag))
					{
						NKCPopupFilterSubUIEquipStatSlot slot = this.GetSlot();
						slot.transform.SetParent(this.m_trContents);
						slot.GetButton().PointerClick.RemoveAllListeners();
						slot.GetButton().PointerClick.AddListener(delegate()
						{
							onClickStatSlot(slot.GetStatType(), slot.GetSetOptionID(), selectedSlotIdx);
						});
						this.m_lstVisible.Add(slot);
						NKCUtil.SetGameobjectActive(slot, true);
						slot.SetData(NKMItemManager.m_lstItemEquipSetOptionTemplet[i], false);
					}
				}
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTitle, this.GetTitle(selectedSlotIdx));
			foreach (NKCStatInfoTemplet nkcstatInfoTemplet in NKMTempletContainer<NKCStatInfoTemplet>.Values)
			{
				if (nkcstatInfoTemplet.StatType != NKM_STAT_TYPE.NST_RANDOM && nkcstatInfoTemplet.UseFilter)
				{
					NKCPopupFilterSubUIEquipStatSlot slot = this.GetSlot();
					slot.transform.SetParent(this.m_trContents);
					slot.GetButton().PointerClick.RemoveAllListeners();
					slot.GetButton().PointerClick.AddListener(delegate()
					{
						onClickStatSlot(slot.GetStatType(), slot.GetSetOptionID(), selectedSlotIdx);
					});
					this.m_lstVisible.Add(slot);
					NKCUtil.SetGameobjectActive(slot, true);
					bool flag = lstSelectedStatType.Contains(nkcstatInfoTemplet.StatType);
					slot.SetData(nkcstatInfoTemplet.StatType, flag);
					if (flag)
					{
						slot.GetButton().Lock(false);
					}
					else
					{
						slot.GetButton().UnLock(false);
					}
				}
			}
		}

		// Token: 0x060073E7 RID: 29671 RVA: 0x00268B54 File Offset: 0x00266D54
		private string GetTitle(int selectedIdx)
		{
			switch (selectedIdx)
			{
			case 0:
				return NKCUtilString.GET_STRING_FILTER_EQUIP_TYPE_STAT_MAIN;
			case 1:
				return NKCUtilString.GET_STRING_FILTER_EQUIP_TYPE_STAT_SUB1;
			case 2:
				return NKCUtilString.GET_STRING_FILTER_EQUIP_TYPE_STAT_SUB2;
			default:
				return "";
			}
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x00268B81 File Offset: 0x00266D81
		private NKCPopupFilterSubUIEquipStatSlot GetSlot()
		{
			if (this.m_stkSlot.Count > 0)
			{
				return this.m_stkSlot.Pop();
			}
			return UnityEngine.Object.Instantiate<NKCPopupFilterSubUIEquipStatSlot>(this.m_pfbSlot, this.m_trContents);
		}

		// Token: 0x060073E9 RID: 29673 RVA: 0x00268BAE File Offset: 0x00266DAE
		public void Close()
		{
			this.m_selectedIdx = -1;
			this.ResetSlot();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x00268BCC File Offset: 0x00266DCC
		private void ResetSlot()
		{
			for (int i = 0; i < this.m_lstVisible.Count; i++)
			{
				this.m_lstVisible[i].GetButton().UnLock(false);
				this.m_lstVisible[i].transform.SetParent(this.m_trObjPool);
				NKCUtil.SetGameobjectActive(this.m_lstVisible[i], false);
				this.m_stkSlot.Push(this.m_lstVisible[i]);
			}
			this.m_lstVisible.Clear();
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x00268C56 File Offset: 0x00266E56
		public void Clean()
		{
			while (this.m_stkSlot.Count > 0)
			{
				UnityEngine.Object.Destroy(this.m_stkSlot.Pop());
			}
		}

		// Token: 0x04005FD8 RID: 24536
		public NKCPopupFilterSubUIEquipStatSlot m_pfbSlot;

		// Token: 0x04005FD9 RID: 24537
		public Text m_lbTitle;

		// Token: 0x04005FDA RID: 24538
		public ScrollRect m_sr;

		// Token: 0x04005FDB RID: 24539
		public Transform m_trContents;

		// Token: 0x04005FDC RID: 24540
		public Transform m_trObjPool;

		// Token: 0x04005FDD RID: 24541
		private List<NKCPopupFilterSubUIEquipStatSlot> m_lstVisible = new List<NKCPopupFilterSubUIEquipStatSlot>();

		// Token: 0x04005FDE RID: 24542
		private Stack<NKCPopupFilterSubUIEquipStatSlot> m_stkSlot = new Stack<NKCPopupFilterSubUIEquipStatSlot>();

		// Token: 0x04005FDF RID: 24543
		private int m_selectedIdx = -1;

		// Token: 0x0200179F RID: 6047
		// (Invoke) Token: 0x0600B3CF RID: 46031
		public delegate void OnClickStatSlot(NKM_STAT_TYPE statType, int setOptionID, int selectedSlotIdx);
	}
}
