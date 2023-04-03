using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009EC RID: 2540
	[RequireComponent(typeof(NKCUISlot))]
	public class NKCUISlotEquipPreset : MonoBehaviour
	{
		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06006DBF RID: 28095 RVA: 0x0023FE89 File Offset: 0x0023E089
		// (set) Token: 0x06006DC0 RID: 28096 RVA: 0x0023FE91 File Offset: 0x0023E091
		public NKCUISlot Slot { get; private set; }

		// Token: 0x06006DC1 RID: 28097 RVA: 0x0023FE9A File Offset: 0x0023E09A
		private void Awake()
		{
			this.Slot = base.GetComponent<NKCUISlot>();
		}

		// Token: 0x06006DC2 RID: 28098 RVA: 0x0023FEA8 File Offset: 0x0023E0A8
		public void Init()
		{
			this.Slot.Init();
			NKCUtil.SetGameobjectActive(this.m_imgEquipUnit.gameObject, false);
		}

		// Token: 0x06006DC3 RID: 28099 RVA: 0x0023FEC6 File Offset: 0x0023E0C6
		public void SetEquipUnitSprite(Sprite unitSprite)
		{
			if (unitSprite == null)
			{
				NKCUtil.SetGameobjectActive(this.m_imgEquipUnit.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_imgEquipUnit.gameObject, true);
			NKCUtil.SetImageSprite(this.m_imgEquipUnit, unitSprite, false);
		}

		// Token: 0x06006DC4 RID: 28100 RVA: 0x0023FF01 File Offset: 0x0023E101
		public void SetEmpty(NKCUISlot.OnClick dOnClick = null)
		{
			this.Slot.SetEmpty(dOnClick);
			NKCUtil.SetGameobjectActive(this.m_imgEquipUnit.gameObject, false);
		}

		// Token: 0x04005957 RID: 22871
		public Image m_imgEquipUnit;
	}
}
