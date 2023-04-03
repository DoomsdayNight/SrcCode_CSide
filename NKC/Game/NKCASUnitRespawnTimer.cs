using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.Game
{
	// Token: 0x020008A4 RID: 2212
	public class NKCASUnitRespawnTimer : MonoBehaviour
	{
		// Token: 0x06005A05 RID: 23045 RVA: 0x001B5CB5 File Offset: 0x001B3EB5
		public void SetPosition(Vector3 pos)
		{
			base.transform.localPosition = pos + this.Offset;
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x001B5CD0 File Offset: 0x001B3ED0
		public void Play(float time)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_imgTimer != null)
			{
				this.m_imgTimer.fillAmount = 0f;
				this.m_imgTimer.color = this.m_Color;
				this.m_imgTimer.DOFillAmount(1f, time).SetEase(Ease.Linear).OnComplete(new TweenCallback(this.Delay));
			}
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x001B5D41 File Offset: 0x001B3F41
		private void Delay()
		{
			this.m_imgTimer.DOFillAmount(1f, 0.1f).OnComplete(new TweenCallback(this.Overtime));
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x001B5D6A File Offset: 0x001B3F6A
		private void Overtime()
		{
			this.m_imgTimer.color = Color.red;
			this.m_imgTimer.fillAmount = 0f;
			this.m_imgTimer.DOFillAmount(1f, 1f).SetEase(Ease.Linear);
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x001B5DA8 File Offset: 0x001B3FA8
		public void Stop()
		{
			if (this.m_imgTimer != null)
			{
				this.m_imgTimer.DOKill(false);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x040045CA RID: 17866
		public Vector3 Offset = new Vector3(0f, -75f, 0f);

		// Token: 0x040045CB RID: 17867
		public Image m_imgTimer;

		// Token: 0x040045CC RID: 17868
		public Color m_Color;
	}
}
