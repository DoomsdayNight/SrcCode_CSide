using System;

namespace NKC.BT.Office
{
	// Token: 0x0200081C RID: 2076
	public class BTOfficeInteractionCancel : BTOfficeActionBase
	{
		// Token: 0x060051F9 RID: 20985 RVA: 0x0018E62D File Offset: 0x0018C82D
		public override void OnStart()
		{
			this.bActionSuccessFlag = true;
			if (this.m_Character != null)
			{
				this.m_Character.UnregisterInteraction();
			}
		}
	}
}
