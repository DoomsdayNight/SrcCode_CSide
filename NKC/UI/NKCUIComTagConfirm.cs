using System;
using NKM;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200094B RID: 2379
	public class NKCUIComTagConfirm : MonoBehaviour
	{
		// Token: 0x06005EE3 RID: 24291 RVA: 0x001D7680 File Offset: 0x001D5880
		private void OnEnable()
		{
			bool flag = this.IsTagActivated();
			if (this.setTargetObject)
			{
				if (this.objActiveWhenConfirmed != null)
				{
					int num = this.objActiveWhenConfirmed.Length;
					for (int i = 0; i < num; i++)
					{
						NKCUtil.SetGameobjectActive(this.objActiveWhenConfirmed[i], flag);
					}
				}
				if (this.objActiveWhenDenied != null)
				{
					int num2 = this.objActiveWhenDenied.Length;
					for (int j = 0; j < num2; j++)
					{
						NKCUtil.SetGameobjectActive(this.objActiveWhenDenied[j], !flag);
					}
					return;
				}
			}
			else
			{
				base.gameObject.SetActive(flag);
			}
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x001D7708 File Offset: 0x001D5908
		private bool IsTagActivated()
		{
			if (this.tagInfo == null)
			{
				return true;
			}
			bool flag = true;
			int num = this.tagInfo.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag2 = true;
				NKCUIComTagConfirm.TagType tagType = this.tagInfo[i].tagType;
				if (tagType != NKCUIComTagConfirm.TagType.OpenTag)
				{
					if (tagType == NKCUIComTagConfirm.TagType.ContentTag)
					{
						flag2 = NKMContentsVersionManager.HasTag(this.tagInfo[i].tagName);
					}
				}
				else
				{
					flag2 = NKMOpenTagManager.IsOpened(this.tagInfo[i].tagName);
				}
				if (!this.setTargetObject && this.applyAsIgnoreTag)
				{
					flag2 = !flag2;
				}
				flag = (flag && flag2);
			}
			return flag;
		}

		// Token: 0x04004B09 RID: 19209
		public NKCUIComTagConfirm.TagInfo[] tagInfo;

		// Token: 0x04004B0A RID: 19210
		public bool applyAsIgnoreTag;

		// Token: 0x04004B0B RID: 19211
		public bool setTargetObject;

		// Token: 0x04004B0C RID: 19212
		public GameObject[] objActiveWhenConfirmed;

		// Token: 0x04004B0D RID: 19213
		public GameObject[] objActiveWhenDenied;

		// Token: 0x020015D2 RID: 5586
		public enum TagType
		{
			// Token: 0x0400A29D RID: 41629
			OpenTag,
			// Token: 0x0400A29E RID: 41630
			ContentTag
		}

		// Token: 0x020015D3 RID: 5587
		[Serializable]
		public struct TagInfo
		{
			// Token: 0x0400A29F RID: 41631
			public NKCUIComTagConfirm.TagType tagType;

			// Token: 0x0400A2A0 RID: 41632
			public string tagName;
		}
	}
}
