using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002BA RID: 698
	[Serializable]
	public class DropDownListItem
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0002C9F4 File Offset: 0x0002ABF4
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		public string Caption
		{
			get
			{
				return this._caption;
			}
			set
			{
				this._caption = value;
				if (this.OnUpdate != null)
				{
					this.OnUpdate();
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0002CA18 File Offset: 0x0002AC18
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0002CA20 File Offset: 0x0002AC20
		public Sprite Image
		{
			get
			{
				return this._image;
			}
			set
			{
				this._image = value;
				if (this.OnUpdate != null)
				{
					this.OnUpdate();
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0002CA3C File Offset: 0x0002AC3C
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x0002CA44 File Offset: 0x0002AC44
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				this._isDisabled = value;
				if (this.OnUpdate != null)
				{
					this.OnUpdate();
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0002CA60 File Offset: 0x0002AC60
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0002CA68 File Offset: 0x0002AC68
		public string ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002CA71 File Offset: 0x0002AC71
		public DropDownListItem(string caption = "", string inId = "", Sprite image = null, bool disabled = false, Action onSelect = null)
		{
			this._caption = caption;
			this._image = image;
			this._id = inId;
			this._isDisabled = disabled;
			this.OnSelect = onSelect;
		}

		// Token: 0x04000A2A RID: 2602
		[SerializeField]
		private string _caption;

		// Token: 0x04000A2B RID: 2603
		[SerializeField]
		private Sprite _image;

		// Token: 0x04000A2C RID: 2604
		[SerializeField]
		private bool _isDisabled;

		// Token: 0x04000A2D RID: 2605
		[SerializeField]
		private string _id;

		// Token: 0x04000A2E RID: 2606
		public Action OnSelect;

		// Token: 0x04000A2F RID: 2607
		internal Action OnUpdate;
	}
}
