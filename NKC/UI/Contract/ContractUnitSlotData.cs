using System;
using NKM.Templet;

namespace NKC.UI.Contract
{
	// Token: 0x02000BEB RID: 3051
	public class ContractUnitSlotData
	{
		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06008D73 RID: 36211 RVA: 0x00301A52 File Offset: 0x002FFC52
		public NKM_UNIT_GRADE grade { get; }

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06008D74 RID: 36212 RVA: 0x00301A5A File Offset: 0x002FFC5A
		public NKM_UNIT_STYLE_TYPE type { get; }

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06008D75 RID: 36213 RVA: 0x00301A62 File Offset: 0x002FFC62
		public NKM_UNIT_ROLE_TYPE role { get; }

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06008D76 RID: 36214 RVA: 0x00301A6A File Offset: 0x002FFC6A
		public string Name { get; }

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06008D77 RID: 36215 RVA: 0x00301A72 File Offset: 0x002FFC72
		public float Percent { get; }

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06008D78 RID: 36216 RVA: 0x00301A7A File Offset: 0x002FFC7A
		public bool RatioUp { get; }

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06008D79 RID: 36217 RVA: 0x00301A82 File Offset: 0x002FFC82
		public bool Awaken { get; }

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06008D7A RID: 36218 RVA: 0x00301A8A File Offset: 0x002FFC8A
		public bool Rearm { get; }

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06008D7B RID: 36219 RVA: 0x00301A92 File Offset: 0x002FFC92
		public bool Pickup { get; }

		// Token: 0x06008D7C RID: 36220 RVA: 0x00301A9C File Offset: 0x002FFC9C
		public ContractUnitSlotData(int _unitID, NKM_UNIT_GRADE _grade, NKM_UNIT_STYLE_TYPE _type, NKM_UNIT_ROLE_TYPE _role, bool _Awaken, bool _Rearm, string _name, float _percent, bool _ratioUp, bool _pickup)
		{
			this.UnitID = _unitID;
			this.grade = _grade;
			this.type = _type;
			this.role = _role;
			this.Name = _name;
			this.Percent = _percent;
			this.Awaken = _Awaken;
			this.Rearm = _Rearm;
			this.RatioUp = _ratioUp;
			this.Pickup = _pickup;
		}

		// Token: 0x04007A4F RID: 31311
		public int UnitID;
	}
}
