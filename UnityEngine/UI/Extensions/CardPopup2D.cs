using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002ED RID: 749
	[RequireComponent(typeof(Rigidbody))]
	public class CardPopup2D : MonoBehaviour
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x00038E6B File Offset: 0x0003706B
		private void Start()
		{
			this.rbody = base.GetComponent<Rigidbody>();
			this.rbody.useGravity = false;
			this.startZPos = base.transform.position.z;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00038E9C File Offset: 0x0003709C
		private void Update()
		{
			if (this.isFalling)
			{
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(this.cardFallRotation), Time.deltaTime * this.rotationSpeed);
			}
			if (this.fallToZero)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(0f, 0f, this.startZPos), Time.deltaTime * this.centeringSpeed);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * this.centeringSpeed);
				if (Vector3.Distance(base.transform.position, new Vector3(0f, 0f, this.startZPos)) < 0.0025f)
				{
					base.transform.position = new Vector3(0f, 0f, this.startZPos);
					this.fallToZero = false;
				}
			}
			if (base.transform.position.y < -4f)
			{
				this.isFalling = false;
				this.rbody.useGravity = false;
				this.rbody.velocity = Vector3.zero;
				base.transform.position = new Vector3(0f, 8f, this.startZPos);
				if (this.singleScene)
				{
					this.CardEnter();
				}
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00039016 File Offset: 0x00037216
		public void CardEnter()
		{
			this.fallToZero = true;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003901F File Offset: 0x0003721F
		public void CardFallAway(float fallRotation)
		{
			this.rbody.useGravity = true;
			this.isFalling = true;
			this.cardFallRotation = new Vector3(0f, 0f, fallRotation);
		}

		// Token: 0x04000B77 RID: 2935
		[SerializeField]
		private float rotationSpeed = 1f;

		// Token: 0x04000B78 RID: 2936
		[SerializeField]
		private float centeringSpeed = 4f;

		// Token: 0x04000B79 RID: 2937
		[SerializeField]
		private bool singleScene;

		// Token: 0x04000B7A RID: 2938
		private Rigidbody rbody;

		// Token: 0x04000B7B RID: 2939
		private bool isFalling;

		// Token: 0x04000B7C RID: 2940
		private Vector3 cardFallRotation;

		// Token: 0x04000B7D RID: 2941
		private bool fallToZero;

		// Token: 0x04000B7E RID: 2942
		private float startZPos;
	}
}
