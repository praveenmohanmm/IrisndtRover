package crc64db62d61d9af52c56;


public abstract class MvxDialogFragment_1
	extends mvvmcross.platforms.android.views.fragments.MvxDialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MvvmCross.Platforms.Android.Views.Fragments.MvxDialogFragment`1, MvvmCross", MvxDialogFragment_1.class, __md_methods);
	}


	public MvxDialogFragment_1 ()
	{
		super ();
		if (getClass () == MvxDialogFragment_1.class)
			mono.android.TypeManager.Activate ("MvvmCross.Platforms.Android.Views.Fragments.MvxDialogFragment`1, MvvmCross", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
