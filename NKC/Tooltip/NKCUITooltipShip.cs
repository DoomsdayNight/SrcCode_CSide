using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B07 RID: 2823
	public class NKCUITooltipShip : NKCUITooltipBase
	{
		// Token: 0x06008054 RID: 32852 RVA: 0x002B47B7 File Offset: 0x002B29B7
		public override void Init()
		{
			this.m_slot.Init();
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x002B47C4 File Offset: 0x002B29C4
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.UnitData unitData = data as NKCUITooltip.UnitData;
			if (unitData == null)
			{
				Debug.LogError("Tooltip Unit Data is null");
				return;
			}
			NKMUnitTempletBase unitTempletBase = unitData.UnitTempletBase;
			this.m_slot.SetData(unitData.Slot, false, false, false, null);
			this.m_name.text = NKCUISlot.GetName(unitData.Slot.eType, unitData.Slot.ID);
			this.m_counter.text = NKCUtilString.GetUnitStyleName(unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
			NKCUtil.SetImageSprite(this.m_grade, this.GetGradeSprite(unitTempletBase.m_NKM_UNIT_GRADE), true);
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x002B4856 File Offset: 0x002B2A56
		private Sprite GetGradeSprite(NKM_UNIT_GRADE unitGrade)
		{
			switch (unitGrade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				return this.m_spN;
			case NKM_UNIT_GRADE.NUG_R:
				return this.m_spR;
			case NKM_UNIT_GRADE.NUG_SR:
				return this.m_spSR;
			case NKM_UNIT_GRADE.NUG_SSR:
				return this.m_spSSR;
			default:
				return null;
			}
		}

		// Token: 0x04006C8E RID: 27790
		public NKCUISlot m_slot;

		// Token: 0x04006C8F RID: 27791
		public Text m_name;

		// Token: 0x04006C90 RID: 27792
		public Text m_counter;

		// Token: 0x04006C91 RID: 27793
		public Image m_grade;

		// Token: 0x04006C92 RID: 27794
		[Header("등급")]
		public Sprite m_spSSR;

		// Token: 0x04006C93 RID: 27795
		public Sprite m_spSR;

		// Token: 0x04006C94 RID: 27796
		public Sprite m_spR;

		// Token: 0x04006C95 RID: 27797
		public Sprite m_spN;
	}
}
