using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007BB RID: 1979
	public class NKCUIDangerMessage : MonoBehaviour
	{
		// Token: 0x06004E6E RID: 20078 RVA: 0x0017A3DC File Offset: 0x001785DC
		public void Play(string message)
		{
			this.Clear();
			this.m_message.text = message;
			base.gameObject.SetActive(true);
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_animator.Play("AB_FX_UI_DANGER_MESSAGE_INTRO");
			this.m_playCoroutine = base.StartCoroutine(this.Loop());
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x0017A438 File Offset: 0x00178638
		public void Stop()
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			this.ClearCoroutine();
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.m_animator.Play("AB_FX_UI_DANGER_MESSAGE_OUTRO");
			this.m_stopCoroutine = base.StartCoroutine(this.Outro());
			if (this.m_soundUID > 0)
			{
				NKCSoundManager.StopSound(this.m_soundUID);
				this.m_soundUID = 0;
			}
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x0017A4A4 File Offset: 0x001786A4
		public void Clear()
		{
			this.ClearCoroutine();
			if (this.m_soundUID > 0)
			{
				NKCSoundManager.StopSound(this.m_soundUID);
				this.m_soundUID = 0;
			}
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0017A4C7 File Offset: 0x001786C7
		private void ClearCoroutine()
		{
			if (this.m_playCoroutine != null)
			{
				base.StopCoroutine(this.m_playCoroutine);
			}
			this.m_playCoroutine = null;
			if (this.m_stopCoroutine != null)
			{
				base.StopCoroutine(this.m_stopCoroutine);
			}
			this.m_stopCoroutine = null;
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0017A4FF File Offset: 0x001786FF
		private IEnumerator Loop()
		{
			if (this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			this.m_animator.Play("AB_FX_UI_DANGER_MESSAGE_LOOP");
			this.m_soundUID = NKCSoundManager.PlaySound("FX_UI_DUNGEON_NO_UNIT_WARNING", 1f, 0f, 0f, true, 0f, false, 0f);
			yield break;
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0017A50E File Offset: 0x0017870E
		private IEnumerator Outro()
		{
			if (this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			base.gameObject.SetActive(false);
			yield break;
		}

		// Token: 0x04003E01 RID: 15873
		public Animator m_animator;

		// Token: 0x04003E02 RID: 15874
		public Text m_message;

		// Token: 0x04003E03 RID: 15875
		private const string ANI_INTRO = "AB_FX_UI_DANGER_MESSAGE_INTRO";

		// Token: 0x04003E04 RID: 15876
		private const string ANI_LOOP = "AB_FX_UI_DANGER_MESSAGE_LOOP";

		// Token: 0x04003E05 RID: 15877
		private const string ANI_OUTRO = "AB_FX_UI_DANGER_MESSAGE_OUTRO";

		// Token: 0x04003E06 RID: 15878
		private const string SOUND_NAME = "FX_UI_DUNGEON_NO_UNIT_WARNING";

		// Token: 0x04003E07 RID: 15879
		private Coroutine m_playCoroutine;

		// Token: 0x04003E08 RID: 15880
		private Coroutine m_stopCoroutine;

		// Token: 0x04003E09 RID: 15881
		private int m_soundUID;
	}
}
