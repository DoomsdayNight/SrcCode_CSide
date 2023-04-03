using System;
using System.Collections;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002EE RID: 750
	public class CardStack2D : MonoBehaviour
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x00039068 File Offset: 0x00037268
		private void Start()
		{
			this.xPowerDifference = 9 - this.cards.Length;
			if (this.useDefaultUsedXPos)
			{
				int num = (int)this.cards[0].GetComponent<RectTransform>().rect.width;
				this.usedCardXPos = (int)((float)Screen.width * 0.5f + (float)num);
			}
			this.cardPositions = new Vector3[this.cards.Length * 2 - 1];
			for (int i = this.cards.Length; i > -1; i--)
			{
				if (i < this.cards.Length - 1)
				{
					this.cardPositions[i] = new Vector3(-Mathf.Pow(2f, (float)(i + this.xPowerDifference)) + this.cardPositions[i + 1].x, 0f, (float)(this.cardZMultiplier * Mathf.Abs(i + 1 - this.cards.Length)));
				}
				else
				{
					this.cardPositions[i] = Vector3.zero;
				}
			}
			for (int j = this.cards.Length; j < this.cardPositions.Length; j++)
			{
				this.cardPositions[j] = new Vector3((float)(this.usedCardXPos + 4 * (j - this.cards.Length)), 0f, (float)(-2 + -2 * (j - this.cards.Length)));
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000391B8 File Offset: 0x000373B8
		private void Update()
		{
			if (CardStack2D.canUseHorizontalAxis)
			{
				if ((UIExtensionsInputManager.GetAxisRaw("Horizontal") < 0f || UIExtensionsInputManager.GetKey(this.leftButton)) && this.cardArrayOffset > 0)
				{
					this.cardArrayOffset--;
					base.StartCoroutine(this.ButtonCooldown());
				}
				else if ((UIExtensionsInputManager.GetAxisRaw("Horizontal") > 0f || UIExtensionsInputManager.GetKey(this.rightButton)) && this.cardArrayOffset < this.cards.Length - 1)
				{
					this.cardArrayOffset++;
					base.StartCoroutine(this.ButtonCooldown());
				}
			}
			for (int i = 0; i < this.cards.Length; i++)
			{
				this.cards[i].localPosition = Vector3.Lerp(this.cards[i].localPosition, this.cardPositions[i + this.cardArrayOffset], Time.deltaTime * this.cardMoveSpeed);
				if (Mathf.Abs(this.cards[i].localPosition.x - this.cardPositions[i + this.cardArrayOffset].x) < 0.01f)
				{
					this.cards[i].localPosition = this.cardPositions[i + this.cardArrayOffset];
					if (this.cards[i].localPosition.x == 0f)
					{
						this.cards[i].gameObject.GetComponent<CanvasGroup>().interactable = true;
					}
					else
					{
						this.cards[i].gameObject.GetComponent<CanvasGroup>().interactable = false;
					}
				}
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00039357 File Offset: 0x00037557
		private IEnumerator ButtonCooldown()
		{
			CardStack2D.canUseHorizontalAxis = false;
			yield return new WaitForSeconds(this.buttonCooldownTime);
			CardStack2D.canUseHorizontalAxis = true;
			yield break;
		}

		// Token: 0x04000B7F RID: 2943
		[SerializeField]
		private float cardMoveSpeed = 8f;

		// Token: 0x04000B80 RID: 2944
		[SerializeField]
		private float buttonCooldownTime = 0.125f;

		// Token: 0x04000B81 RID: 2945
		[SerializeField]
		private int cardZMultiplier = 32;

		// Token: 0x04000B82 RID: 2946
		[SerializeField]
		private bool useDefaultUsedXPos = true;

		// Token: 0x04000B83 RID: 2947
		[SerializeField]
		private int usedCardXPos = 1280;

		// Token: 0x04000B84 RID: 2948
		[SerializeField]
		private KeyCode leftButton = KeyCode.LeftArrow;

		// Token: 0x04000B85 RID: 2949
		[SerializeField]
		private KeyCode rightButton = KeyCode.RightArrow;

		// Token: 0x04000B86 RID: 2950
		[SerializeField]
		private Transform[] cards;

		// Token: 0x04000B87 RID: 2951
		private int cardArrayOffset;

		// Token: 0x04000B88 RID: 2952
		private Vector3[] cardPositions;

		// Token: 0x04000B89 RID: 2953
		private int xPowerDifference;

		// Token: 0x04000B8A RID: 2954
		public static bool canUseHorizontalAxis = true;
	}
}
