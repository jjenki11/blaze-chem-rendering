
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
/**
 * Namespace: System.Web.UI.WebControls
 * Class:     TableItemStyle
 *
 * Author:  Gaurav Vaish
 * Maintainer: gvaish@iitk.ac.in
 * Contact: <my_scripts2001@yahoo.com>, <gvaish@iitk.ac.in>
 * Implementation: yes
 * Status:  100%
 *
 * (C) Gaurav Vaish (2002)
 */

using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;

namespace System.Web.UI.WebControls
{
	public class TableItemStyle: Style
	{
		private static int H_ALIGN = (0x01 << 16);
		private static int V_ALIGN = (0x01 << 17);
		private static int WRAP    = (0x01 << 18);

		public TableItemStyle(): base()
		{
		}

		public TableItemStyle(StateBag bag): base(bag)
		{
		}

#if !NET_2_0
		[Bindable (true)]
#endif
		[DefaultValue(HorizontalAlign.NotSet)]
		[NotifyParentProperty(true)]
		[WebCategory("Layout")]
		[WebSysDescription("TableItemStyle_HorizontalAlign")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if(IsSet(H_ALIGN))
					return (HorizontalAlign)ViewState["HorizontalAlign"];
				return HorizontalAlign.NotSet;
			}
			set
			{
				if(!Enum.IsDefined(typeof(HorizontalAlign), value))
				{
					throw new ArgumentException();
				}
				ViewState["HorizontalAlign"] = value;
				Set(H_ALIGN);
			}
		}

#if !NET_2_0
		[Bindable (true)]
#endif
		[DefaultValue(VerticalAlign.NotSet)]
		[NotifyParentProperty(true)]
		[WebCategory("Layout")]
		[WebSysDescription("TableItemStyle_VerticalAlign")]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if(IsSet(V_ALIGN))
					return (VerticalAlign)ViewState["VerticalAlign"];
				return VerticalAlign.NotSet;
			}
			set
			{
				if(!Enum.IsDefined(typeof(VerticalAlign), value))
				{
					throw new ArgumentException();
				}
				ViewState["VerticalAlign"] = value;
				Set(V_ALIGN);
			}
		}

#if !NET_2_0
		[Bindable (true)]
#endif
		[DefaultValue(VerticalAlign.NotSet)]
		[NotifyParentProperty(true)]
		[WebCategory("Layout")]
		[WebSysDescription("TableItemStyle_Wrap")]
		public virtual bool Wrap
		{
			get
			{
				if(IsSet(WRAP))
					return (bool)ViewState["Wrap"];
				return true;
			}
			set
			{
				ViewState["Wrap"] = value;
				Set(WRAP);
			}
		}

		public override void CopyFrom(Style s)
		{
			if(s!=null && !s.IsEmpty)
			{
				base.CopyFrom(s);

				if (!(s is TableItemStyle))
					return;
                                
				TableItemStyle from = (TableItemStyle)s;
				if(from.IsSet(H_ALIGN))
				{
					HorizontalAlign = from.HorizontalAlign;
				}
				if(from.IsSet(V_ALIGN))
				{
					VerticalAlign = from.VerticalAlign;
				}
				if(from.IsSet(WRAP))
				{
					Wrap = from.Wrap;
				}
			}
		}

		public override void MergeWith(Style s)
		{
			if(s!=null && !s.IsEmpty)
			{
				if (IsEmpty) {
					CopyFrom (s);
					return;
				}
				base.MergeWith(s);

				if (!(s is TableItemStyle))
					return;
				
				TableItemStyle with = (TableItemStyle)s;
				if(with.IsSet(H_ALIGN) && !IsSet(H_ALIGN))
				{
					HorizontalAlign = with.HorizontalAlign;
				}
				if(with.IsSet(V_ALIGN) && !IsSet(V_ALIGN))
				{
					VerticalAlign = with.VerticalAlign;
				}
				if(with.IsSet(WRAP) && !IsSet(WRAP))
				{
					Wrap = with.Wrap;
				}
			}
		}

		public override void Reset()
		{
			if(IsSet(H_ALIGN))
				ViewState.Remove("HorizontalAlign");
			if(IsSet(V_ALIGN))
				ViewState.Remove("VerticalAlign");
			if(IsSet(WRAP))
				ViewState.Remove("Wrap");
			base.Reset();
		}

		public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			base.AddAttributesToRender(writer, owner);
			if(!Wrap)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap");
			}
			if(HorizontalAlign != HorizontalAlign.NotSet)
			{
				// Temporarily commented out. I'm having problems in cygwin.
				//writer.AddAttribute(HtmlTextWriterAttribute.Align, TypeDescriptor.GetConverter(typeof(HorizontalAlign)).ConvertToString(HorizontalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Align, HorizontalAlign.ToString ());
			}
			if(VerticalAlign != VerticalAlign.NotSet)
			{
				// Temporarily commented out. I'm having problems in cygwin.
				//writer.AddAttribute(HtmlTextWriterAttribute.Valign, TypeDescriptor.GetConverter(typeof(VerticalAlign)).ConvertToString(VerticalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, VerticalAlign.ToString ());
			}
		}
	}
}
