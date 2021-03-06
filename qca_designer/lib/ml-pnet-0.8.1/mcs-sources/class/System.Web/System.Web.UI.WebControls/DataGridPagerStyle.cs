//
// System.Web.UI.WebControls.DataGridPagerStyle.cs
//
// Authors:
//   Gaurav Vaish (gvaish@iitk.ac.in)
//   Andreas Nahr (ClassDevelopment@A-SoftTech.com)
//
// (C) Gaurav Vaish (2002)
// (C) 2003 Andreas Nahr
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;

namespace System.Web.UI.WebControls
{
	public sealed class DataGridPagerStyle : TableItemStyle
	{
		DataGrid owner;

		private static int MODE         = (0x01 << 19);
		private static int NEXT_PG_TEXT = (0x01 << 20);
		private static int PG_BTN_COUNT = (0x01 << 21);
		private static int POSITION     = (0x01 << 22);
		private static int VISIBLE      = (0x01 << 23);
		private static int PREV_PG_TEXT = (0x01 << 24);

		internal DataGridPagerStyle(DataGrid owner): base()
		{
			this.owner = owner;
		}

		internal bool IsPagerOnTop
		{
			get {
				PagerPosition p = Position;
				return (p == PagerPosition.Top || p == PagerPosition.TopAndBottom);
			}
		}
		
		internal bool IsPagerOnBottom
		{
			get {
				PagerPosition p = Position;
				return (p == PagerPosition.Bottom || p == PagerPosition.TopAndBottom);
			}
		}

#if !NET_2_0
		[Bindable (true)]
#endif
		[DefaultValue (PagerMode.NextPrev), WebCategory ("Misc")]
		[NotifyParentProperty (true)]
		[WebSysDescription ("The mode used for displaying multiple pages.")]
		public PagerMode Mode
		{
			get
			{
				if(IsSet(MODE))
				{
					return (PagerMode)ViewState["Mode"];
				}
				return PagerMode.NextPrev;
			}
			set
			{
				if(!Enum.IsDefined(typeof(PagerMode), value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ViewState["Mode"] = value;
				Set(MODE);
			}
		}

#if NET_2_0
		[Localizable (true)]
#else
		[Bindable (true)]
#endif
		[DefaultValue ("&gt;"), WebCategory ("Misc")]
		[NotifyParentProperty (true)]
		[WebSysDescription ("The text for the 'next page' button.")]
		public string NextPageText
		{
			get
			{
				if(IsSet(NEXT_PG_TEXT))
				{
					return (string)ViewState["NextPageText"];
				}
				return "&gt;";
			}
			set
			{
				ViewState["NextPageText"] = value;
				Set(NEXT_PG_TEXT);
			}
		}

#if NET_2_0
		[Localizable (true)]
#else
		[Bindable (true)]
#endif
		[DefaultValue ("&lt;"), WebCategory ("Misc")]
		[NotifyParentProperty (true)]
		[WebSysDescription ("The text for the 'previous page' button.")]
		public string PrevPageText
		{
			get
			{
				if(IsSet(PREV_PG_TEXT))
				{
					return (string)ViewState["PrevPageText"];
				}
				return "&lt;";
			}
			set
			{
				ViewState["PrevPageText"] = value;
				Set(PREV_PG_TEXT);
			}
		}

#if !NET_2_0
		[Bindable (true)]
#endif
		[DefaultValue (10), WebCategory ("Misc")]
		[NotifyParentProperty (true)]
		[WebSysDescription ("The maximum number of pages to show as direct links.")]
		public int PageButtonCount
		{
			get
			{
				if(IsSet(PG_BTN_COUNT))
				{
					return (int)ViewState["PageButtonCount"];
				}
				return 10;
			}
			set
			{
				if (value < 1)
					throw new ArgumentOutOfRangeException("value");
                                
				ViewState["PageButtonCount"] = value;
				Set(PG_BTN_COUNT);
			}
		}

#if !NET_2_0
		[Bindable (true)]
#endif
		[DefaultValue (PagerPosition.Bottom), WebCategory ("Misc")]
		[NotifyParentProperty (true)]
		[WebSysDescription ("The position for the page display.")]
		public PagerPosition Position
		{
			get
			{
				if(IsSet(POSITION))
				{
					return (PagerPosition)ViewState["Position"];
				}
				return PagerPosition.Bottom;
			}
			set
			{
				if(!Enum.IsDefined(typeof(PagerPosition), value))
				{
					throw new ArgumentException();
				}
				ViewState["Position"] = value;
				Set(POSITION);
			}
		}

#if NET_2_0
		[Localizable (true)]
#else
		[Bindable (true)]
#endif
		[DefaultValue (true), WebCategory ("Misc")]
		[NotifyParentProperty (true)]
		[WebSysDescription ("Determines if paging functionallity is visible to the user.")]
		public bool Visible
		{
			get
			{
				if(IsSet(VISIBLE))
				{
					return (bool)ViewState["PagerVisible"];
				}
				return true;
			}
			set
			{
				ViewState["PagerVisible"] = value;
				Set(VISIBLE);
				owner.OnPagerChanged();
			}
		}

		public override void CopyFrom(Style s)
		{
			if(s != null && !s.IsEmpty)
			{
				base.CopyFrom(s);
				if(!(s is DataGridPagerStyle)) return;

				DataGridPagerStyle from = (DataGridPagerStyle)s;
				if(from.IsSet(MODE))
				{
					Mode = from.Mode;
				}
				if(from.IsSet(NEXT_PG_TEXT))
				{
					NextPageText = from.NextPageText;
				}
				if(from.IsSet(PG_BTN_COUNT))
				{
					PageButtonCount = from.PageButtonCount;
				}
				if(from.IsSet(POSITION))
				{
					Position = from.Position;
				}
				if(from.IsSet(VISIBLE))
				{
					Visible = from.Visible;
				}
				if(from.IsSet(PREV_PG_TEXT))
				{
					PrevPageText = from.PrevPageText;
				}
			}
		}

		public override void MergeWith(Style s)
		{
			if(s != null && !s.IsEmpty)
			{
				if(IsEmpty)
				{
					CopyFrom(s);
					return;
				}

				base.MergeWith(s);

				if(!(s is DataGridPagerStyle)) return;

				DataGridPagerStyle with = (DataGridPagerStyle)s;
				if(with.IsSet(MODE) && !IsSet(MODE))
				{
					Mode = with.Mode;
				}
				if(with.IsSet(NEXT_PG_TEXT) && !IsSet(NEXT_PG_TEXT))
				{
					NextPageText = with.NextPageText;
				}
				if(with.IsSet(PG_BTN_COUNT) && !IsSet(PG_BTN_COUNT))
				{
					PageButtonCount = with.PageButtonCount;
				}
				if(with.IsSet(POSITION) && !IsSet(POSITION))
				{
					Position = with.Position;
				}
				if(with.IsSet(VISIBLE) && !IsSet(VISIBLE))
				{
					Visible = with.Visible;
				}
				if(with.IsSet(PREV_PG_TEXT) && !IsSet(PREV_PG_TEXT))
				{
					PrevPageText = with.PrevPageText;
				}
			}
		}

		public override void Reset()
		{
			if(IsSet(MODE))
			{
				ViewState.Remove("Mode");
			}
			if(IsSet(NEXT_PG_TEXT))
			{
				ViewState.Remove("NextPageText");
			}
			if(IsSet(PG_BTN_COUNT))
			{
				ViewState.Remove("PageButtonCount");
			}
			if(IsSet(POSITION))
			{
				ViewState.Remove("Position");
			}
			if(IsSet(VISIBLE))
			{
				ViewState.Remove("PagerVisible");
			}
			if(IsSet(PREV_PG_TEXT))
			{
				ViewState.Remove("PrevPageText");
			}
			base.Reset();
		}
	}
}
