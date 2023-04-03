using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000941 RID: 2369
	public class NKCUIComRandomSoundPlayer : MonoBehaviour
	{
		// Token: 0x06005EA5 RID: 24229 RVA: 0x001D6080 File Offset: 0x001D4280
		private void OnEnable()
		{
			if (!string.IsNullOrEmpty(this.m_PlaySoundAtEnable))
			{
				this.m_iCurPlayedSoundUId = NKCSoundManager.PlaySound(this.m_PlaySoundAtEnable, 1f, 0f, 0f, false, 0f, false, 0f);
			}
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x001D60C8 File Offset: 0x001D42C8
		public void OnRandomSoundPlay()
		{
			if (this.m_lstRandomSound.Count <= 0)
			{
				return;
			}
			if (this.m_iCurPlayedSoundUId != 0)
			{
				NKCSoundManager.StopSound(this.m_iCurPlayedSoundUId);
			}
			int index = UnityEngine.Random.Range(0, this.m_lstRandomSound.Count);
			this.m_iCurPlayedSoundUId = NKCSoundManager.PlaySound(this.m_lstRandomSound[index], 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x04004AC8 RID: 19144
		public string m_PlaySoundAtEnable;

		// Token: 0x04004AC9 RID: 19145
		public List<string> m_lstRandomSound = new List<string>();

		// Token: 0x04004ACA RID: 19146
		private int m_iCurPlayedSoundUId;
	}
}
