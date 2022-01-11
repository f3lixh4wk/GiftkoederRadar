﻿#pragma checksum "..\..\..\MapView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D54F48078BAEB82BE2EA0799100153C292A55880"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using GMap.NET.WindowsPresentation;
using GiftkoederRadar;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GiftkoederRadar {
    
    
    /// <summary>
    /// MapView
    /// </summary>
    public partial class MapView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemOpenURL;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemAbout;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemove;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearchInMap;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tboxSearch;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExpandMap;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GMap.NET.WindowsPresentation.GMapControl mapView;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\MapView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lboxReportList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GiftkoederRadar;component/mapview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MapView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.menuItemOpenURL = ((System.Windows.Controls.MenuItem)(target));
            
            #line 40 "..\..\..\MapView.xaml"
            this.menuItemOpenURL.Click += new System.Windows.RoutedEventHandler(this.menuItemClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.menuItemAbout = ((System.Windows.Controls.MenuItem)(target));
            
            #line 41 "..\..\..\MapView.xaml"
            this.menuItemAbout.Click += new System.Windows.RoutedEventHandler(this.menuItemClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\MapView.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\MapView.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnRemove = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\MapView.xaml"
            this.btnRemove.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnSearchInMap = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\MapView.xaml"
            this.btnSearchInMap.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tboxSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnExpandMap = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\MapView.xaml"
            this.btnExpandMap.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.mapView = ((GMap.NET.WindowsPresentation.GMapControl)(target));
            
            #line 77 "..\..\..\MapView.xaml"
            this.mapView.Loaded += new System.Windows.RoutedEventHandler(this.mapViewLoaded);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lboxReportList = ((System.Windows.Controls.ListBox)(target));
            
            #line 78 "..\..\..\MapView.xaml"
            this.lboxReportList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lboxReportList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

