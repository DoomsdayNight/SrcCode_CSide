using System;
using BehaviorDesigner.Runtime;
using NKC.UI.Component;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000818 RID: 2072
	public class BTOfficeEmotion : BTOfficeActionBase
	{
		// Token: 0x060051EE RID: 20974 RVA: 0x0018DFF0 File Offset: 0x0018C1F0
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			if (UnityEngine.Random.Range(0, 100) < this.Probability)
			{
				if (string.IsNullOrEmpty(this.AnimName.Value))
				{
					this.m_Character.PlayEmotion(this.eEmotion, this.m_fSpeed);
					return;
				}
				this.m_Character.PlayEmotion(this.AnimName.Value, this.m_fSpeed);
			}
		}

		// Token: 0x04004228 RID: 16936
		[Header("플레이할 애니 이름. 비어있으면 아래 eEmotion 사용")]
		public SharedString AnimName;

		// Token: 0x04004229 RID: 16937
		[Header("확률. 100에서 100%")]
		public int Probability = 30;

		// Token: 0x0400422A RID: 16938
		[Header("AnimName 비어있는 경우 이쪽 사용")]
		public NKCUIComCharacterEmotion.Type eEmotion;

		// Token: 0x0400422B RID: 16939
		[Header("애니 속도")]
		public float m_fSpeed = 1f;
	}
}
