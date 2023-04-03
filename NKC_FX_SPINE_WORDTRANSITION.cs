using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000004 RID: 4
public class NKC_FX_SPINE_WORDTRANSITION : MonoBehaviour
{
	// Token: 0x0600000A RID: 10 RVA: 0x000021E0 File Offset: 0x000003E0
	private void Start()
	{
		this._skeleton = this.uiGraphic.Skeleton;
		this.targetBone = this._skeleton.FindBone(this.boneName);
		if (this.wordMat == null)
		{
			this.tempMat = this.wordImage.material;
			return;
		}
		this.tempMat = new Material(this.wordMat);
		this.wordImage.material = this.tempMat;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002258 File Offset: 0x00000458
	private void Update()
	{
		if (this.tempMat == null)
		{
			return;
		}
		this.targetRot = this.targetBone.Rotation;
		if (this.targetRot < 360f && this.targetRot > this.rotStandard)
		{
			this.wordRot = -1f;
		}
		else
		{
			this.wordRot = this.targetRot;
		}
		this.tempMat.SetFloat("_subIntense", this.wordRot / this.rotStandard);
	}

	// Token: 0x04000006 RID: 6
	public Material wordMat;

	// Token: 0x04000007 RID: 7
	public SkeletonGraphic uiGraphic;

	// Token: 0x04000008 RID: 8
	public string boneName;

	// Token: 0x04000009 RID: 9
	public Image wordImage;

	// Token: 0x0400000A RID: 10
	public float wordRot = -1f;

	// Token: 0x0400000B RID: 11
	private Skeleton _skeleton;

	// Token: 0x0400000C RID: 12
	private Bone targetBone;

	// Token: 0x0400000D RID: 13
	private float targetRot;

	// Token: 0x0400000E RID: 14
	private Material tempMat;

	// Token: 0x0400000F RID: 15
	public float rotStandard = 270f;
}
