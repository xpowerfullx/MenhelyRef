﻿#pragma checksum "..\..\OrokbefogadasAblak.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CE3BB29B8539B4A17F6B7ADD2F441430"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Menhely {
    
    
    /// <summary>
    /// OrokbefogadasAblak
    /// </summary>
    public partial class OrokbefogadasAblak : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\OrokbefogadasAblak.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid allatokDG;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Menhely;component/orokbefogadasablak.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\OrokbefogadasAblak.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.allatokDG = ((System.Windows.Controls.DataGrid)(target));
            
            #line 6 "..\..\OrokbefogadasAblak.xaml"
            this.allatokDG.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.allatokDG_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 16 "..\..\OrokbefogadasAblak.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btElfogadas);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 17 "..\..\OrokbefogadasAblak.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btVisszautasitas);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 18 "..\..\OrokbefogadasAblak.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btMegse);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

