using System;
using NKC.UI;
using NKM.Templet;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x02000832 RID: 2098
	public class NKCOfficeFacilityTerrabrain : NKCOfficeFacility
	{
		// Token: 0x06005366 RID: 21350 RVA: 0x00196C44 File Offset: 0x00194E44
		public override void Init()
		{
			base.Init();
			if (this.m_fnTerrabrain != null)
			{
				this.m_fnTerrabrain.SetLock(false);
				this.m_fnTerrabrain.dOnClickFuniture = new NKCOfficeFuniture.OnClickFuniture(this.OnClickTerrabrainDesk);
			}
			if (this.m_NPCTerraBrainGap != null)
			{
				this.m_NPCTerraBrainGap.SetOnClick(new NKCOfficeCharacter.OnClick(this.OnTouchTerrabrainGap));
			}
			NKCUtil.SetGameobjectActive(this.m_objFloorEffect, false);
			NKCUtil.SetGameobjectActive(this.m_objFloorTouchEffect, false);
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00196CC5 File Offset: 0x00194EC5
		private void OnClickTerrabrainDesk(int id, long uid)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.TERRA_BRAIN, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.TERRA_BRAIN, 0);
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCStringTable.GetString("SI_CONTENTS_READY_TERRA_BRAIN", false), null, "");
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x00196CF7 File Offset: 0x00194EF7
		protected override void Update()
		{
			base.Update();
			if (this.m_NPCTerraBrainGap != null && this.m_NPCTerraBrainGap.GetBTValue<bool>("IdleBegun", false))
			{
				NKCUtil.SetGameobjectActive(this.m_objFloorEffect, true);
			}
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x00196D2C File Offset: 0x00194F2C
		private bool OnTouchTerrabrainGap()
		{
			if (this.m_NPCTerraBrainGap != null && this.m_NPCTerraBrainGap.GetBTValue<bool>("IdleBegun", false))
			{
				NKCUtil.SetGameobjectActive(this.m_objFloorTouchEffect, false);
				NKCUtil.SetGameobjectActive(this.m_objFloorTouchEffect, true);
			}
			return false;
		}

		// Token: 0x040042D5 RID: 17109
		public NKCOfficeFacilityFuniture m_fnTerrabrain;

		// Token: 0x040042D6 RID: 17110
		public GameObject m_objFloorEffect;

		// Token: 0x040042D7 RID: 17111
		public GameObject m_objFloorTouchEffect;

		// Token: 0x040042D8 RID: 17112
		public NKCOfficeCharacterNPC m_NPCTerraBrainGap;
	}
}
