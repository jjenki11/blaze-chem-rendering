//
// System.Web.UI.WebControls.SiteMapPath.cs
//
// Authors:
//	Lluis Sanchez Gual (lluis@novell.com)
//
// (C) 2005 Novell, Inc (http://www.novell.com)
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
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//

#if NET_2_0

using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	public class SiteMapPath: CompositeControl
	{
		SiteMapProvider provider;
		
		Style currentNodeStyle;
		Style nodeStyle;
		Style pathSeparatorStyle;
		Style rootNodeStyle;
		
		ITemplate currentNodeTemplate;
		ITemplate nodeTemplate;
		ITemplate pathSeparatorTemplate;
		ITemplate rootNodeTemplate;

		private static readonly object ItemCreatedEvent = new object();
		private static readonly object ItemDataBoundEvent = new object();
		
		public event SiteMapNodeItemEventHandler ItemCreated {
			add { Events.AddHandler (ItemCreatedEvent, value); }
			remove { Events.RemoveHandler (ItemCreatedEvent, value); }
		}
		
		public event SiteMapNodeItemEventHandler ItemDataBound {
			add { Events.AddHandler (ItemDataBoundEvent, value); }
			remove { Events.RemoveHandler (ItemDataBoundEvent, value); }
		}
		
		protected virtual void OnItemCreated (SiteMapNodeItemEventArgs e)
		{
			if (Events != null) {
				SiteMapNodeItemEventHandler eh = (SiteMapNodeItemEventHandler) Events [ItemCreatedEvent];
				if (eh != null) eh (this, e);
			}
		}
		
		protected virtual void OnItemDataBound (SiteMapNodeItemEventArgs e)
		{
			if (Events != null) {
				SiteMapNodeItemEventHandler eh = (SiteMapNodeItemEventHandler) Events [ItemDataBoundEvent];
				if (eh != null) eh (this, e);
			}
		}
		
	    [DefaultValueAttribute (null)]
	    [DesignerSerializationVisibilityAttribute (DesignerSerializationVisibility.Content)]
	    [NotifyParentPropertyAttribute (true)]
	    [PersistenceModeAttribute (PersistenceMode.InnerProperty)]
		public Style CurrentNodeStyle {
			get {
				if (currentNodeStyle == null) {
					currentNodeStyle = new Style ();
					if (IsTrackingViewState)
						((IStateManager)currentNodeStyle).TrackViewState ();
				}
				return currentNodeStyle;
			}
		}
	
		[DefaultValue (null)]
		[TemplateContainer (typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
	    [Browsable (false)]
		public virtual ITemplate CurrentNodeTemplate {
			get { return currentNodeTemplate; }
			set { currentNodeTemplate = value; UpdateControls (); }
		}
		
	    [DefaultValueAttribute (null)]
	    [DesignerSerializationVisibilityAttribute (DesignerSerializationVisibility.Content)]
	    [NotifyParentPropertyAttribute (true)]
	    [PersistenceModeAttribute (PersistenceMode.InnerProperty)]
		public Style NodeStyle {
			get {
				if (nodeStyle == null) {
					nodeStyle = new Style ();
					if (IsTrackingViewState)
						((IStateManager)nodeStyle).TrackViewState ();
				}
				return nodeStyle;
			}
		}
	
		[DefaultValue (null)]
		[TemplateContainer (typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
	    [Browsable (false)]
		public virtual ITemplate NodeTemplate {
			get { return nodeTemplate; }
			set { nodeTemplate = value; UpdateControls (); }
		}
		
	    [DefaultValueAttribute (-1)]
	    [ThemeableAttribute (false)]
		public virtual int ParentLevelsDisplayed {
			get {
				object ob = ViewState ["ParentLevelsDisplayed"];
				if (ob != null) return (int) ob;
				else return -1;
			}
			set {
				if (value < -1) throw new ArgumentOutOfRangeException ("value");
				ViewState ["ParentLevelsDisplayed"] = value;
				UpdateControls ();
			}
		}
		
	    [DefaultValueAttribute (PathDirection.RootToCurrent)]
		public virtual PathDirection PathDirection {
			get {
				object ob = ViewState ["PathDirection"];
				if (ob != null) return (PathDirection) ob;
				else return PathDirection.RootToCurrent;
			}
			set {
				if (value != PathDirection.RootToCurrent && value != PathDirection.CurrentToRoot)
					throw new ArgumentOutOfRangeException ("value");
				ViewState ["PathDirection"] = value;
				UpdateControls ();
			}
		}
		
	    [DefaultValueAttribute (" > ")]
	    [LocalizableAttribute (true)]
		public virtual string PathSeparator {
			get {
				object ob = ViewState ["PathSeparator"];
				if (ob != null) return (string) ob;
				else return " > ";
			}
			set {
				ViewState ["PathSeparator"] = value;
				UpdateControls ();
			}
		}
		
	    [DefaultValueAttribute (null)]
	    [DesignerSerializationVisibilityAttribute (DesignerSerializationVisibility.Content)]
	    [NotifyParentPropertyAttribute (true)]
	    [PersistenceModeAttribute (PersistenceMode.InnerProperty)]
		public Style PathSeparatorStyle {
			get {
				if (pathSeparatorStyle == null) {
					pathSeparatorStyle = new Style ();
					if (IsTrackingViewState)
						((IStateManager)pathSeparatorStyle).TrackViewState ();
				}
				return pathSeparatorStyle;
			}
		}
	
		[DefaultValue (null)]
		[TemplateContainer (typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
	    [Browsable (false)]
		public virtual ITemplate PathSeparatorTemplate {
			get { return pathSeparatorTemplate; }
			set { pathSeparatorTemplate = value; UpdateControls (); }
		}
		
	    [BrowsableAttribute (false)]
	    [DesignerSerializationVisibilityAttribute (DesignerSerializationVisibility.Hidden)]
		public SiteMapProvider Provider {
			get {
				if (provider == null) {
					if (this.SiteMapProvider.Length == 0) {
						provider = SiteMap.Provider;
						if (provider == null)
							throw new HttpException ("There is no default provider configured for the site.");
					} else {
						provider = SiteMap.Providers [this.SiteMapProvider];
						if (provider == null)
							throw new HttpException ("SiteMap provider '" + this.SiteMapProvider + "' not found.");
					}
				}
				return provider;
			}
			set {
				provider = value;
				UpdateControls ();
			}
		}
		
	    [DefaultValueAttribute (false)]
		public virtual bool RenderCurrentNodeAsLink {
			get {
				object o = ViewState ["RenderCurrentNodeAsLink"];
				if (o != null) return (bool) o;
				else return false;
			}
			set {
				ViewState ["RenderCurrentNodeAsLink"] = value;
				UpdateControls ();
			}
		}
		
	    [DefaultValueAttribute (null)]
	    [DesignerSerializationVisibilityAttribute (DesignerSerializationVisibility.Content)]
	    [NotifyParentPropertyAttribute (true)]
	    [PersistenceModeAttribute (PersistenceMode.InnerProperty)]
		public Style RootNodeStyle {
			get {
				if (rootNodeStyle == null) {
					rootNodeStyle = new Style ();
					if (IsTrackingViewState)
						((IStateManager)rootNodeStyle).TrackViewState ();
				}
				return rootNodeStyle;
			}
		}
	
		[DefaultValue (null)]
		[TemplateContainer (typeof(SiteMapNodeItem), BindingDirection.OneWay)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
	    [Browsable (false)]
		public virtual ITemplate RootNodeTemplate {
			get { return rootNodeTemplate; }
			set { rootNodeTemplate = value; UpdateControls (); }
		}
		
	    [DefaultValueAttribute (true)]
	    [ThemeableAttribute (false)]
		public virtual bool ShowToolTips {
			get {
				object o = ViewState ["ShowToolTips"];
				if (o != null) return (bool) o;
				else return true;
			}
			set {
				ViewState ["ShowToolTips"] = value;
				UpdateControls ();
			}
		}
		
	    [DefaultValueAttribute ("")]
	    [ThemeableAttribute (false)]
		public string SiteMapProvider {
			get {
				object o = ViewState ["SiteMapProvider"];
				if (o != null) return (string) o;
				else return string.Empty;
			}
			set {
				ViewState ["SiteMapProvider"] = value;
				UpdateControls ();
			}
		}
		
		void UpdateControls ()
		{
			ChildControlsCreated = false;
		}
		
		protected override void CreateChildControls ()
		{
			Controls.Clear ();
			CreateControlHierarchy ();
		}
		
		protected virtual void CreateControlHierarchy ()
		{
			ArrayList nodes = new ArrayList ();
			SiteMapNode node = Provider.CurrentNode;
			if (node == null) return;
			
			int levels = ParentLevelsDisplayed != -1 ? ParentLevelsDisplayed + 1 : int.MaxValue;
			
			while (node != null && levels > 0) {
				if (nodes.Count > 0) {
					SiteMapNodeItem sep = new SiteMapNodeItem (nodes.Count, SiteMapNodeItemType.PathSeparator);
					InitializeItem (sep);
					SiteMapNodeItemEventArgs sargs = new SiteMapNodeItemEventArgs (sep);
					OnItemCreated (sargs);
					nodes.Add (sep);
				}

				SiteMapNodeItemType nt;
				if (nodes.Count == 0)
					nt = SiteMapNodeItemType.Current;
				else if (node.ParentNode == null)
					nt = SiteMapNodeItemType.Root;
				else
					nt = SiteMapNodeItemType.Parent;
					
				SiteMapNodeItem it = new SiteMapNodeItem (nodes.Count, nt);
				it.SiteMapNode = node;
				InitializeItem (it);
				
				SiteMapNodeItemEventArgs args = new SiteMapNodeItemEventArgs (it);
				OnItemCreated (args);
				
				it.DataBind ();
				OnItemDataBound (args);
				
				nodes.Add (it);
				node = node.ParentNode;
				levels--;
			}
			
			if (PathDirection == PathDirection.RootToCurrent) {
				for (int n=nodes.Count - 1; n>=0; n--)
					Controls.Add ((Control)nodes[n]);
			} else {
				for (int n=0; n<nodes.Count; n++)
					Controls.Add ((Control)nodes[n]);
			}
		}
		
		protected virtual void InitializeItem (SiteMapNodeItem item)
		{
			switch (item.ItemType) {
				case SiteMapNodeItemType.Root:
					if (RootNodeTemplate != null) {
						item.ApplyStyle (NodeStyle);
						item.ApplyStyle (RootNodeStyle);
						RootNodeTemplate.InstantiateIn (item);
					}
					else {
						WebControl c = CreateNodeControl (true, item);
						c.ApplyStyle (NodeStyle);
						c.ApplyStyle (RootNodeStyle);
						item.Controls.Add (c);
					}
					break;

				case SiteMapNodeItemType.Current:
					if (CurrentNodeTemplate != null) {
						item.ApplyStyle (NodeStyle);
						item.ApplyStyle (CurrentNodeStyle);
						CurrentNodeTemplate.InstantiateIn (item);
					}
					else {
						WebControl c = CreateNodeControl (RenderCurrentNodeAsLink, item);
						c.ApplyStyle (NodeStyle);
						c.ApplyStyle (CurrentNodeStyle);
						item.Controls.Add (c);
					}
					break;
					
				case SiteMapNodeItemType.Parent:
					if (NodeTemplate != null) {
						item.ApplyStyle (NodeStyle);
						NodeTemplate.InstantiateIn (item);
					}
					else {
						WebControl c = CreateNodeControl (true, item);
						c.ApplyStyle (NodeStyle);
						item.Controls.Add (c);
					}
					break;
					
				case SiteMapNodeItemType.PathSeparator:
					if (PathSeparatorTemplate != null) {
						item.ApplyStyle (PathSeparatorStyle);
						PathSeparatorTemplate.InstantiateIn (item);
					}
					else {
						Label h = new Label ();
						h.Text = PathSeparator;
						h.ApplyStyle (PathSeparatorStyle);
						item.Controls.Add (h);
					}
					break;
			}
		}
		
		WebControl CreateNodeControl (bool link, SiteMapNodeItem item)
		{
			if (link) {
				HyperLink h = new HyperLink ();
				h.Text = item.SiteMapNode.Title;
				h.NavigateUrl = item.SiteMapNode.Url;
				if (ShowToolTips)
					h.ToolTip = item.SiteMapNode.Description;
				return h;
			}
			else {
				Label h = new Label ();
				h.Text = item.SiteMapNode.Title;
				if (ShowToolTips)
					h.ToolTip = item.SiteMapNode.Description;
				return h;
			}
		}
		
		protected override void LoadViewState (object savedState)
		{
			if (savedState == null) {
				base.LoadViewState (null);
				return;
			}
			
			object[] states = (object[]) savedState;
			base.LoadViewState (states [0]);
			
			if (states[1] != null) ((IStateManager)CurrentNodeStyle).LoadViewState (states[1]);
			if (states[2] != null) ((IStateManager)NodeStyle).LoadViewState (states[2]);
			if (states[3] != null) ((IStateManager)PathSeparatorStyle).LoadViewState (states[3]);
			if (states[4] != null) ((IStateManager)RootNodeStyle).LoadViewState (states[4]);
		}
		
		protected override object SaveViewState ()
		{
			object[] state = new object [5];
			state [0] = base.SaveViewState ();
			
			if (currentNodeStyle != null) state [1] = ((IStateManager)currentNodeStyle).SaveViewState ();
			if (nodeStyle != null) state [2] = ((IStateManager)nodeStyle).SaveViewState ();
			if (pathSeparatorStyle != null) state [3] = ((IStateManager)pathSeparatorStyle).SaveViewState ();
			if (rootNodeStyle != null) state [4] = ((IStateManager)rootNodeStyle).SaveViewState ();
			
			for (int n=0; n<state.Length; n++)
				if (state [n] != null) return state;
			return null;
		}
		
		protected override void TrackViewState ()
		{
			base.TrackViewState();
			if (currentNodeStyle != null) ((IStateManager)currentNodeStyle).TrackViewState();
			if (nodeStyle != null) ((IStateManager)nodeStyle).TrackViewState();
			if (pathSeparatorStyle != null) ((IStateManager)pathSeparatorStyle).TrackViewState();
			if (rootNodeStyle != null) ((IStateManager)rootNodeStyle).TrackViewState();
		}
	}
}

#endif
