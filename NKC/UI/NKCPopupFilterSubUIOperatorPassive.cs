using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A54 RID: 2644
	public class NKCPopupFilterSubUIOperatorPassive : MonoBehaviour
	{
		// Token: 0x0600740F RID: 29711 RVA: 0x00269940 File Offset: 0x00267B40
		public void Open(int selectedSkillID, NKCPopupFilterSubUIOperatorPassive.OnClickSkillSlot onClickStatSlot, OperatorSkillType openType)
		{
			this.ResetSlot();
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			foreach (NKMOperatorSkillTemplet nkmoperatorSkillTemplet in NKMTempletContainer<NKMOperatorSkillTemplet>.Values)
			{
				if (nkmoperatorSkillTemplet.m_OperSkillType == openType && nkmoperatorSkillTemplet.UseFilter)
				{
					NKCPopupFilterSubUIOperatorPassiveSlot slot = this.GetSlot();
					slot.transform.SetParent(this.m_trContents);
					slot.GetButton().PointerClick.RemoveAllListeners();
					slot.GetButton().PointerClick.AddListener(delegate()
					{
						this.Close();
						onClickStatSlot(slot.GetPassiveTemplet().m_OperSkillID);
					});
					this.m_lstVisible.Add(slot);
					NKCUtil.SetGameobjectActive(slot, true);
					bool flag = selectedSkillID == nkmoperatorSkillTemplet.m_OperSkillID;
					slot.SetData(nkmoperatorSkillTemplet, flag);
					if (flag)
					{
						slot.GetButton().Select(true, true, true);
					}
					else
					{
						slot.GetButton().Select(false, true, true);
					}
				}
			}
		}

		// Token: 0x06007410 RID: 29712 RVA: 0x00269A94 File Offset: 0x00267C94
		private NKCPopupFilterSubUIOperatorPassiveSlot GetSlot()
		{
			if (this.m_stkSlot.Count > 0)
			{
				return this.m_stkSlot.Pop();
			}
			return UnityEngine.Object.Instantiate<NKCPopupFilterSubUIOperatorPassiveSlot>(this.m_pfbSlot, this.m_trContents);
		}

		// Token: 0x06007411 RID: 29713 RVA: 0x00269AC1 File Offset: 0x00267CC1
		public void Close()
		{
			this.ResetSlot();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007412 RID: 29714 RVA: 0x00269AD8 File Offset: 0x00267CD8
		private void ResetSlot()
		{
			for (int i = 0; i < this.m_lstVisible.Count; i++)
			{
				this.m_lstVisible[i].transform.SetParent(this.m_trObjPool);
				NKCUtil.SetGameobjectActive(this.m_lstVisible[i], false);
				this.m_stkSlot.Push(this.m_lstVisible[i]);
			}
			this.m_lstVisible.Clear();
		}

		// Token: 0x06007413 RID: 29715 RVA: 0x00269B4B File Offset: 0x00267D4B
		public void Clean()
		{
			while (this.m_stkSlot.Count > 0)
			{
				UnityEngine.Object.Destroy(this.m_stkSlot.Pop());
			}
		}

		// Token: 0x04006024 RID: 24612
		public NKCPopupFilterSubUIOperatorPassiveSlot m_pfbSlot;

		// Token: 0x04006025 RID: 24613
		public ScrollRect m_sr;

		// Token: 0x04006026 RID: 24614
		public Transform m_trContents;

		// Token: 0x04006027 RID: 24615
		public Transform m_trObjPool;

		// Token: 0x04006028 RID: 24616
		private List<NKCPopupFilterSubUIOperatorPassiveSlot> m_lstVisible = new List<NKCPopupFilterSubUIOperatorPassiveSlot>();

		// Token: 0x04006029 RID: 24617
		private Stack<NKCPopupFilterSubUIOperatorPassiveSlot> m_stkSlot = new Stack<NKCPopupFilterSubUIOperatorPassiveSlot>();

		// Token: 0x020017A7 RID: 6055
		// (Invoke) Token: 0x0600B3E4 RID: 46052
		public delegate void OnClickSkillSlot(int skillID);
	}
}
