using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Office;
using UnityEngine;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C69 RID: 3177
	public class NKCUIComOfficeInteriorDetail : MonoBehaviour
	{
		// Token: 0x060093C1 RID: 37825 RVA: 0x003273A0 File Offset: 0x003255A0
		public void SetData(int itemID)
		{
			NKMOfficeInteriorTemplet data = NKMItemMiscTemplet.FindInterior(itemID);
			this.SetData(data);
		}

		// Token: 0x060093C2 RID: 37826 RVA: 0x003273BC File Offset: 0x003255BC
		public void SetData(NKMItemMiscTemplet miscItemTemplet)
		{
			if (miscItemTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			if (miscItemTemplet.m_ItemMiscType != NKM_ITEM_MISC_TYPE.IMT_INTERIOR)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKMOfficeInteriorTemplet data = NKMItemMiscTemplet.FindInterior(miscItemTemplet.m_ItemMiscID);
			this.SetData(data);
		}

		// Token: 0x060093C3 RID: 37827 RVA: 0x00327404 File Offset: 0x00325604
		public void SetData(NKMOfficeInteriorTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			bool flag = false;
			NKCUtil.SetGameobjectActive(this.m_objInteraction, templet.HasInteraction);
			flag = (flag || templet.HasInteraction);
			NKCUtil.SetGameobjectActive(this.m_objAnimationDot, flag);
			NKCUtil.SetGameobjectActive(this.m_objAnimation, templet.Animation);
			flag = (flag || templet.Animation);
			NKCUtil.SetGameobjectActive(this.m_objSoundDot, flag);
			NKCUtil.SetGameobjectActive(this.m_objSound, templet.HasSound);
			flag = (flag || templet.HasSound);
			NKCUtil.SetGameobjectActive(this.m_objEffectDot, flag);
			NKCUtil.SetGameobjectActive(this.m_objEffect, templet.Effect);
			flag = (flag || templet.Effect);
			NKCUtil.SetGameobjectActive(this.m_objBGMDot, flag);
			NKCUtil.SetGameobjectActive(this.m_objBGM, templet.HasBGM);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x060093C4 RID: 37828 RVA: 0x003274E8 File Offset: 0x003256E8
		public void SetData(IEnumerable<int> lstInteriorID)
		{
			List<NKMOfficeInteriorTemplet> list = new List<NKMOfficeInteriorTemplet>();
			foreach (int key in lstInteriorID)
			{
				NKMOfficeInteriorTemplet nkmofficeInteriorTemplet = NKMOfficeInteriorTemplet.Find(key);
				if (nkmofficeInteriorTemplet != null)
				{
					list.Add(nkmofficeInteriorTemplet);
				}
			}
			this.SetData(list);
		}

		// Token: 0x060093C5 RID: 37829 RVA: 0x00327548 File Offset: 0x00325748
		public void SetData(IEnumerable<NKMOfficeInteriorTemplet> lstTemplet)
		{
			if (lstTemplet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			foreach (NKMOfficeInteriorTemplet nkmofficeInteriorTemplet in lstTemplet)
			{
				if (nkmofficeInteriorTemplet != null)
				{
					flag |= nkmofficeInteriorTemplet.HasInteraction;
					flag2 |= nkmofficeInteriorTemplet.Animation;
					flag3 |= nkmofficeInteriorTemplet.HasSound;
					flag4 |= nkmofficeInteriorTemplet.Effect;
					flag5 |= nkmofficeInteriorTemplet.HasBGM;
				}
			}
			bool flag6 = false;
			NKCUtil.SetGameobjectActive(this.m_objInteraction, flag);
			flag6 = (flag6 || flag);
			NKCUtil.SetGameobjectActive(this.m_objAnimationDot, flag6);
			NKCUtil.SetGameobjectActive(this.m_objAnimation, flag2);
			flag6 = (flag6 || flag2);
			NKCUtil.SetGameobjectActive(this.m_objSoundDot, flag6);
			NKCUtil.SetGameobjectActive(this.m_objSound, flag3);
			flag6 = (flag6 || flag3);
			NKCUtil.SetGameobjectActive(this.m_objEffectDot, flag6);
			NKCUtil.SetGameobjectActive(this.m_objEffect, flag4);
			flag6 = (flag6 || flag4);
			NKCUtil.SetGameobjectActive(this.m_objBGMDot, flag6);
			NKCUtil.SetGameobjectActive(this.m_objBGM, flag5);
			NKCUtil.SetGameobjectActive(base.gameObject, flag || flag2 || flag3 || flag4 || flag5);
		}

		// Token: 0x040080B9 RID: 32953
		public GameObject m_objInteraction;

		// Token: 0x040080BA RID: 32954
		public GameObject m_objAnimation;

		// Token: 0x040080BB RID: 32955
		public GameObject m_objAnimationDot;

		// Token: 0x040080BC RID: 32956
		public GameObject m_objSound;

		// Token: 0x040080BD RID: 32957
		public GameObject m_objSoundDot;

		// Token: 0x040080BE RID: 32958
		public GameObject m_objEffect;

		// Token: 0x040080BF RID: 32959
		public GameObject m_objEffectDot;

		// Token: 0x040080C0 RID: 32960
		public GameObject m_objBGM;

		// Token: 0x040080C1 RID: 32961
		public GameObject m_objBGMDot;
	}
}
