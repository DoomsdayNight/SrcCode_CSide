using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200069A RID: 1690
	public interface INKCAnimationActor
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600379F RID: 14239
		Animator Animator { get; }

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060037A0 RID: 14240
		Transform SDParent { get; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060037A1 RID: 14241
		Transform Transform { get; }

		// Token: 0x060037A2 RID: 14242
		void PlaySpineAnimation(string name, bool loop, float timeScale);

		// Token: 0x060037A3 RID: 14243
		void PlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim, bool loop, float timeScale, bool bDefaultAnim);

		// Token: 0x060037A4 RID: 14244
		bool IsSpineAnimationFinished(NKCASUIUnitIllust.eAnimation eAnim);

		// Token: 0x060037A5 RID: 14245
		bool IsSpineAnimationFinished(string name);

		// Token: 0x060037A6 RID: 14246
		Vector3 GetBonePosition(string name);

		// Token: 0x060037A7 RID: 14247
		void PlayEmotion(string animName, float speed);

		// Token: 0x060037A8 RID: 14248
		bool CanPlaySpineAnimation(string name);

		// Token: 0x060037A9 RID: 14249
		bool CanPlaySpineAnimation(NKCASUIUnitIllust.eAnimation eAnim);
	}
}
