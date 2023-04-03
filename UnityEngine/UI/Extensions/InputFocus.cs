using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002BC RID: 700
	[RequireComponent(typeof(InputField))]
	[AddComponentMenu("UI/Extensions/InputFocus")]
	public class InputFocus : MonoBehaviour
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002CD15 File Offset: 0x0002AF15
		private void Start()
		{
			this._inputField = base.GetComponent<InputField>();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002CD23 File Offset: 0x0002AF23
		private void Update()
		{
			if (UIExtensionsInputManager.GetKeyUp(KeyCode.Return) && !this._inputField.isFocused)
			{
				if (this._ignoreNextActivation)
				{
					this._ignoreNextActivation = false;
					return;
				}
				this._inputField.Select();
				this._inputField.ActivateInputField();
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002CD61 File Offset: 0x0002AF61
		public void buttonPressed()
		{
			bool flag = this._inputField.text == "";
			this._inputField.text = "";
			if (!flag)
			{
				this._inputField.Select();
				this._inputField.ActivateInputField();
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		public void OnEndEdit(string textString)
		{
			if (!UIExtensionsInputManager.GetKeyDown(KeyCode.Return))
			{
				return;
			}
			bool flag = this._inputField.text == "";
			this._inputField.text = "";
			if (flag)
			{
				this._ignoreNextActivation = true;
			}
		}

		// Token: 0x04000A3C RID: 2620
		protected InputField _inputField;

		// Token: 0x04000A3D RID: 2621
		public bool _ignoreNextActivation;
	}
}
