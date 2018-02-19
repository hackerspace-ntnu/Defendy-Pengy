using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	[RequireComponent(typeof(Animator))]
	public class MagicBook_Animation : MonoBehaviour
	{
		public SkinnedMeshRenderer[] pages;
		private int textureSize = 4;
		//private int curPage = 1; //starts with page 2
		

		private Animator anim;
		//private int curTextureInt = 0;
		private bool startedAnimation = false;

		public MagicBook_SpellController spellController; //the container of the spell

		#region SteamVR touchPad gestures
		bool prevIsTouched = false;
		bool isSwiped = false;
		SteamVR_Controller.Device gestureController;
		Vector2 prevTouchPos = Vector2.zero;
		Vector2 gestureStartPoint = Vector2.zero;
		float gestureTimeSpent = 0f;
		#endregion

		void Start()
		{
			anim = GetComponent<Animator>();
		}

		void Update()
		{
			#region SteamVR Swipe recognition
			foreach(var hand in Player.instance.hands)
			{
				if (!hand.currentAttachedObject)
					continue;
				if (hand.currentAttachedObject.GetComponent<MagicBook>()) // FIXME: if the hand is the MagicBook hand, CODE CAN BE IMPROVED!!!!!!!!
				{
					var ctrl = hand.controller;
					if (ctrl == null)
						continue;
					SteamVR_Controller.Input((int)ctrl.index);
					if (ctrl.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) // if finger is on touchpad
					{
						if (isSwiped) //if the player already has a recognized swipe, skip it until the user releases that touch.
							continue;
						gestureTimeSpent += Time.deltaTime;
						var touchPos = ctrl.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad); // get the touched position of the pad
						if (prevIsTouched == false) //if the touchpad previously was not touched
						{
							gestureController = ctrl;
							gestureStartPoint = touchPos;
							gestureTimeSpent = 0f;
						} else
						{
							var diff = touchPos - prevTouchPos;
							var gestureTotalMoved = touchPos - gestureStartPoint;
							if (Mathf.Sign(gestureTotalMoved.x) != Mathf.Sign(diff.x)) //if the touchpad did NOT kept moving in the same direction
							{
								//restart observing the gesture
								gestureStartPoint = touchPos;
								gestureTimeSpent = 0f;
								gestureTotalMoved = Vector2.zero;
							}
							gestureTotalMoved += diff;
							if (gestureTotalMoved.magnitude > 0.5f && gestureTimeSpent < 0.33f) // if the gesture is so fast and so long.
							{
								var dir = Mathf.Sign(gestureTotalMoved.x) == 1 ? "right" : "left";
								isSwiped = true;
								gestureStartPoint = touchPos;
								gestureTimeSpent = 0f;
								OnSwipe(dir);
							}
						}
						prevTouchPos = touchPos;
						prevIsTouched = true;
					}
					else
					{
						if (ctrl == gestureController)
						{
							isSwiped = false;
							prevIsTouched = false;
						}

					}
				}
			}
			#endregion

			if(Input.GetKeyDown(KeyCode.LeftArrow))
				NextPage();
			if(Input.GetKeyDown(KeyCode.RightArrow))
				PrevPage();

			if (!startedAnimation)
				return;
		}

		private void OnSwipe(string dir)
		{
			print("Swiped " + dir);
			if (dir == "right")
			{
				PrevPage();
			}
			else //dir == right
			{
				NextPage();
			}
		}

		public void NextPage()
		{
			anim.SetTrigger("NextPage");
			anim.ResetTrigger("PrevPage");
		}
		public void PrevPage()
		{
			anim.SetTrigger("PrevPage");
			anim.ResetTrigger("NextPage");
		}

		public void PageFlippingTo(int page) {
			//curPage = page;
			spellController.ChangeSpell(page);
		}

		public void PlayGlowEffect() //plays on the two current pages
		{
		}


		public void StopGlowEffect()
		{

		}

		public void OffsetMaterial(SkinnedMeshRenderer rend, int i)
		{
			i += 1;
			var mat = rend.material;
			int sumOfXandY = Mathf.CeilToInt((Mathf.Sqrt(1 + 8 * i) - 1) / 2f) - 1;
			int x = 0, y = 0;
			if (i > (textureSize * (textureSize + 1)) / 2)
			{
				//number larger than the triangleNumbers, invert matrix and find again
				int invertedI = textureSize * textureSize - i + 1;
				sumOfXandY = Mathf.CeilToInt((Mathf.Sqrt(1 + 8 * invertedI) - 1) / 2f) - 1;
				var level = sumOfXandY + 1; //levelInTriangleNumbers
				var triangleNumber = (level * (level + 1)) / 2;
				var startX = sumOfXandY;
				var invertedY = triangleNumber - invertedI;
				var invertedX = startX - invertedY;
				y = textureSize - 1 - invertedY;
				x = textureSize - 1 - invertedX;
			} else
			{
				var level = sumOfXandY + 1; //levelInTriangleNumbers
				var triangleNumber = (level * (level + 1)) / 2;
				var startX = sumOfXandY;
				y = triangleNumber - i;
				x = startX - y;
			}


			var matX = 0.25f * y;
			var matY = -0.25f * x + 0.75f;


			mat.mainTextureOffset = new Vector2(matX, matY);
			//int(mat.mainTextureOffset.ToString("F3"));
		}
	}
}
