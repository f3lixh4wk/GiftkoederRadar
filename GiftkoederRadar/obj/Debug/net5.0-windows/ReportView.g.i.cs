#pragma checksum "..\..\..\ReportView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1A61B2277174F43CC191174FE4712817305F37B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// ReportView
    /// </summary>
    public partial class ReportView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label titleLabel;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboxCountry;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPostCode;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tboxPostCode;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTown;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tboxTown;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblStreet;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tboxStreet;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblBaitTitle;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tboxBaitTitle;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteSketch;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOpenSketch;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvasSketch;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblDescription;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tboxDescription;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\ReportView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateReport;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GiftkoederRadar;V1.0.0.0;component/reportview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ReportView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\ReportView.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.titleLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.cboxCountry = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.lblPostCode = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.tboxPostCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.lblTown = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.tboxTown = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.lblStreet = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.tboxStreet = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.lblBaitTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.tboxBaitTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.btnDeleteSketch = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\ReportView.xaml"
            this.btnDeleteSketch.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnOpenSketch = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\ReportView.xaml"
            this.btnOpenSketch.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            case 14:
            this.canvasSketch = ((System.Windows.Controls.Canvas)(target));
            return;
            case 15:
            this.lblDescription = ((System.Windows.Controls.Label)(target));
            return;
            case 16:
            this.tboxDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 17:
            this.btnCreateReport = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\..\ReportView.xaml"
            this.btnCreateReport.Click += new System.Windows.RoutedEventHandler(this.btnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

