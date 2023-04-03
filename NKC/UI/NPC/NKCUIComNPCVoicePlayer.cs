using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC.UI.NPC
{
	// Token: 0x02000BF3 RID: 3059
	public class NKCUIComNPCVoicePlayer : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06008E0A RID: 36362 RVA: 0x003066F8 File Offset: 0x003048F8
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.m_voiceFileName == null)
			{
				return;
			}
			int num = this.m_voiceFileName.Length;
			int num2 = UnityEngine.Random.Range(0, this.m_voiceFileName.Length);
			if (num2 < 0 || num2 >= num)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.m_voiceFileName[num2]))
			{
				return;
			}
			NKCUINPCBase.PlayVoice(this.m_npcType, this.m_voiceFileName[num2], true, true);
		}

		// Token: 0x04007B1F RID: 31519
		public NPC_TYPE m_npcType;

		// Token: 0x04007B20 RID: 31520
		public string[] m_voiceFileName;
	}
}
