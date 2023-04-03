using System;
using System.Collections;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200061A RID: 1562
	public class NKCComSoundPlayer : MonoBehaviour
	{
		// Token: 0x0600304A RID: 12362 RVA: 0x000EDD93 File Offset: 0x000EBF93
		private void OnEnable()
		{
			if (this.PlayOnEnable)
			{
				if (this.Delay > 0f)
				{
					base.StartCoroutine(this.PlayCoroutine());
					return;
				}
				this.Play();
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000EDDBE File Offset: 0x000EBFBE
		public void Preload(bool bAsync)
		{
			if (this.AssetName.Contains("@"))
			{
				NKCResourceUtility.LoadAssetResourceTemp<AudioClip>(NKMAssetName.ParseBundleName("AB_SOUND", this.AssetName), bAsync);
				return;
			}
			NKCResourceUtility.LoadAssetResourceTemp<AudioClip>(this.AssetName, bAsync);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000EDDF8 File Offset: 0x000EBFF8
		public void Play()
		{
			if (NKCSoundManager.IsInit() && !string.IsNullOrWhiteSpace(this.AssetName))
			{
				if (this.AssetName.Contains("@"))
				{
					NKCSoundManager.PlaySound(NKMAssetName.ParseBundleName("AB_SOUND", this.AssetName), this.Volume, 0f, 0f, false, 0f, false, this.PlayStartPos);
					return;
				}
				NKCSoundManager.PlaySound(this.AssetName, this.Volume, 0f, 0f, false, 0f, false, this.PlayStartPos);
			}
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000EDE88 File Offset: 0x000EC088
		private IEnumerator PlayCoroutine()
		{
			yield return new WaitForSeconds(this.Delay);
			this.Play();
			yield break;
		}

		// Token: 0x04002FAC RID: 12204
		public string AssetName;

		// Token: 0x04002FAD RID: 12205
		[Range(0f, 1f)]
		public float Volume = 1f;

		// Token: 0x04002FAE RID: 12206
		public bool PlayOnEnable = true;

		// Token: 0x04002FAF RID: 12207
		public float Delay;

		// Token: 0x04002FB0 RID: 12208
		public float PlayStartPos;
	}
}
