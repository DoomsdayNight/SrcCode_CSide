using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000942 RID: 2370
	public class NKCUIComRandomVoicePlayer : MonoBehaviour
	{
		// Token: 0x06005EA8 RID: 24232 RVA: 0x001D614E File Offset: 0x001D434E
		private void OnEnable()
		{
			if (!this.m_voicePlay || !this.m_playOnEnable)
			{
				return;
			}
			this.PlayRandomVoice();
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x001D6168 File Offset: 0x001D4368
		public int PlayRandomVoice()
		{
			int result = 0;
			if (this.m_voicePlay && this.m_voice != null && this.m_voice.Length != 0)
			{
				int num = this.m_voice.Length;
				List<int> list = new List<int>();
				for (int i = 0; i < num; i++)
				{
					if (i != this.m_prevVoiceIndex)
					{
						list.Add(i);
					}
				}
				int num2 = UnityEngine.Random.Range(0, list.Count);
				int num3 = -1;
				if (num2 < list.Count)
				{
					num3 = list[num2];
				}
				if (num == 1)
				{
					num3 = 0;
				}
				if (num3 >= 0 && num3 < this.m_voice.Length)
				{
					this.m_prevVoiceIndex = num3;
					result = NKCUIVoiceManager.PlayVoice(this.m_voice[num3], this.unitID, 0, false, false);
				}
			}
			return result;
		}

		// Token: 0x04004ACB RID: 19147
		public int unitID;

		// Token: 0x04004ACC RID: 19148
		public VOICE_TYPE[] m_voice;

		// Token: 0x04004ACD RID: 19149
		public bool m_voicePlay = true;

		// Token: 0x04004ACE RID: 19150
		public bool m_playOnEnable;

		// Token: 0x04004ACF RID: 19151
		private int m_prevVoiceIndex = -1;
	}
}
