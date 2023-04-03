using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000618 RID: 1560
	public class NKCComAudioPlayer : MonoBehaviour
	{
		// Token: 0x0600303F RID: 12351 RVA: 0x000ED880 File Offset: 0x000EBA80
		private void OnEnable()
		{
			AudioSource audioSource = base.GetComponent<AudioSource>();
			if (audioSource == null)
			{
				audioSource = base.gameObject.AddComponent<AudioSource>();
				audioSource.clip = this.AudioClip;
			}
			audioSource.volume = NKCSoundManager.GetFinalVol(0f, 0f, this.Volume);
			audioSource.Play();
		}

		// Token: 0x04002FA1 RID: 12193
		[Header("Only Play On Enable")]
		public AudioClip AudioClip;

		// Token: 0x04002FA2 RID: 12194
		[Range(0f, 1f)]
		public float Volume = 1f;
	}
}
