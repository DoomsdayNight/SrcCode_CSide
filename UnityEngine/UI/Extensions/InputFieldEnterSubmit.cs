using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000336 RID: 822
	[RequireComponent(typeof(InputField))]
	[AddComponentMenu("UI/Extensions/Input Field Submit")]
	public class InputFieldEnterSubmit : MonoBehaviour
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x00048891 File Offset: 0x00046A91
		private void Awake()
		{
			this._input = base.GetComponent<InputField>();
			this._input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000488BB File Offset: 0x00046ABB
		public void OnEndEdit(string txt)
		{
			if (!UIExtensionsInputManager.GetKeyDown(KeyCode.Return) && !UIExtensionsInputManager.GetKeyDown(KeyCode.KeypadEnter))
			{
				return;
			}
			this.EnterSubmit.Invoke(txt);
			if (this.defocusInput)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		// Token: 0x04000D69 RID: 3433
		public InputFieldEnterSubmit.EnterSubmitEvent EnterSubmit;

		// Token: 0x04000D6A RID: 3434
		public bool defocusInput = true;

		// Token: 0x04000D6B RID: 3435
		private InputField _input;

		// Token: 0x02001168 RID: 4456
		[Serializable]
		public class EnterSubmitEvent : UnityEvent<string>
		{
		}
	}
}
