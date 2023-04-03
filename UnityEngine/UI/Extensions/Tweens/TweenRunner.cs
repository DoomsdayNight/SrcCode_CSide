using System;
using System.Collections;

namespace UnityEngine.UI.Extensions.Tweens
{
	// Token: 0x02000361 RID: 865
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x0004DB01 File Offset: 0x0004BD01
		private static IEnumerator Start(T tweenInfo)
		{
			if (!tweenInfo.ValidTarget())
			{
				yield break;
			}
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += (tweenInfo.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
				float floatPercentage = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(floatPercentage);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
			tweenInfo.Finished();
			yield break;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0004DB10 File Offset: 0x0004BD10
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0004DB1C File Offset: 0x0004BD1C
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
			if (!this.m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			this.m_Tween = TweenRunner<T>.Start(info);
			this.m_CoroutineContainer.StartCoroutine(this.m_Tween);
		}

		// Token: 0x04000E56 RID: 3670
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x04000E57 RID: 3671
		protected IEnumerator m_Tween;
	}
}
