using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C22 RID: 3106
	public class NKCUICollectionIllustSlot : MonoBehaviour
	{
		// Token: 0x06008FCC RID: 36812 RVA: 0x0030E234 File Offset: 0x0030C434
		public void Init()
		{
		}

		// Token: 0x06008FCD RID: 36813 RVA: 0x0030E236 File Offset: 0x0030C436
		public List<RectTransform> GetRentalSlot()
		{
			return this.m_lstRentalSlot;
		}

		// Token: 0x06008FCE RID: 36814 RVA: 0x0030E23E File Offset: 0x0030C43E
		public void ClearRentalList()
		{
			this.m_lstRentalSlot.Clear();
		}

		// Token: 0x06008FCF RID: 36815 RVA: 0x0030E24C File Offset: 0x0030C44C
		public void SetData(int CategoryID, List<RectTransform> lstSlot, NKCUICollectionIllust.OnIllustView CallBack = null)
		{
			NKCCollectionIllustTemplet illustTemplet = NKCCollectionManager.GetIllustTemplet(CategoryID);
			if (illustTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_ALBUM_TITLE_TEXT_EP, illustTemplet.GetCategoryTitle());
				NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_ALBUM_TITLE_TEXT, illustTemplet.GetCategorySubTitle());
				int num = 0;
				foreach (KeyValuePair<int, NKCCollectionIllustData> keyValuePair in illustTemplet.m_dicIllustData)
				{
					if (!(lstSlot[num] == null))
					{
						NKCUIIllustSlot component = lstSlot[num].GetComponent<NKCUIIllustSlot>();
						component.transform.SetParent(this.m_rt_NKM_UI_COLLECTION_ALBUM_SLOT_CONTENT);
						component.Init(CategoryID, keyValuePair.Key, CallBack);
						component.SetData(keyValuePair.Value);
						this.m_lstRentalSlot.Add(lstSlot[num]);
						num++;
					}
				}
			}
		}

		// Token: 0x04007CC3 RID: 31939
		[Header("타이블")]
		public Text m_NKM_UI_COLLECTION_ALBUM_TITLE_TEXT_EP;

		// Token: 0x04007CC4 RID: 31940
		public Text m_NKM_UI_COLLECTION_ALBUM_TITLE_TEXT;

		// Token: 0x04007CC5 RID: 31941
		public RectTransform m_rt_NKM_UI_COLLECTION_ALBUM_SLOT_CONTENT;

		// Token: 0x04007CC6 RID: 31942
		private List<RectTransform> m_lstRentalSlot = new List<RectTransform>();
	}
}
