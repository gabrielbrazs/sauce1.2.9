using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation
{
	[HideInInspector]
	[SerializeField]
	protected int mFPS = 30;

	[HideInInspector]
	[SerializeField]
	protected string mPrefix = string.Empty;

	[HideInInspector]
	[SerializeField]
	protected bool mLoop = true;

	[SerializeField]
	[HideInInspector]
	protected bool mSnap = true;

	protected UISprite mSprite;

	protected float mDelta;

	protected int mIndex;

	protected bool mActive = true;

	protected List<string> mSpriteNames = new List<string>();

	public int frames => mSpriteNames.Count;

	public int framesPerSecond
	{
		get
		{
			return mFPS;
		}
		set
		{
			mFPS = value;
		}
	}

	public string namePrefix
	{
		get
		{
			return mPrefix;
		}
		set
		{
			if (mPrefix != value)
			{
				mPrefix = value;
				RebuildSpriteList();
			}
		}
	}

	public bool loop
	{
		get
		{
			return mLoop;
		}
		set
		{
			mLoop = value;
		}
	}

	public bool isPlaying => mActive;

	public UISpriteAnimation()
		: this()
	{
	}

	protected virtual void Start()
	{
		RebuildSpriteList();
	}

	protected virtual void Update()
	{
		if (mActive && mSpriteNames.Count > 1 && Application.get_isPlaying() && mFPS > 0)
		{
			mDelta += RealTime.deltaTime;
			float num = 1f / (float)mFPS;
			if (num < mDelta)
			{
				mDelta = ((!(num > 0f)) ? 0f : (mDelta - num));
				if (++mIndex >= mSpriteNames.Count)
				{
					mIndex = 0;
					mActive = mLoop;
				}
				if (mActive)
				{
					mSprite.spriteName = mSpriteNames[mIndex];
					if (mSnap)
					{
						mSprite.MakePixelPerfect();
					}
				}
			}
		}
	}

	public void RebuildSpriteList()
	{
		if (mSprite == null)
		{
			mSprite = this.GetComponent<UISprite>();
		}
		mSpriteNames.Clear();
		if (mSprite != null && mSprite.atlas != null)
		{
			List<UISpriteData> spriteList = mSprite.atlas.spriteList;
			int i = 0;
			for (int count = spriteList.Count; i < count; i++)
			{
				UISpriteData uISpriteData = spriteList[i];
				if (string.IsNullOrEmpty(mPrefix) || uISpriteData.name.StartsWith(mPrefix))
				{
					mSpriteNames.Add(uISpriteData.name);
				}
			}
			mSpriteNames.Sort();
		}
	}

	public void Play()
	{
		mActive = true;
	}

	public void Pause()
	{
		mActive = false;
	}

	public void ResetToBeginning()
	{
		mActive = true;
		mIndex = 0;
		if (mSprite != null && mSpriteNames.Count > 0)
		{
			mSprite.spriteName = mSpriteNames[mIndex];
			if (mSnap)
			{
				mSprite.MakePixelPerfect();
			}
		}
	}
}
