using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem
{
	[RequireComponent(typeof(Animator))]
	public partial class MagicBook : MonoBehaviour
	{
		public SkinnedMeshRenderer[] pages;
		private int textureSize = 4;
		private int curPage = 1; //starts with page 2


		private Animator anim;
		private int curTextureInt = 0;
		private bool startedAnimation;
		private int openAnim;
		// Use this for initialization
		void Start()
		{
			anim = GetComponent<Animator>();
			startedAnimation = false;
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{

			}
			if (!startedAnimation)
				return;
		}


		public void NextPage()
		{
			//anim.SetTrigger()
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



		public int GetCurrentPage_Left()
		{
			return curPage;
		}
	}

}
