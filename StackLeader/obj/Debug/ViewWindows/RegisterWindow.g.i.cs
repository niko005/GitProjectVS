﻿#pragma checksum "..\..\..\ViewWindows\RegisterWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E9C675219F76B67D891479B9C9B5A1D2145FFEA8FA22F9266C44F4ADF9B42441"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using StackLeader.ViewWindows;
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


namespace StackLeader.ViewWindows {
    
    
    /// <summary>
    /// RegisterWindow
    /// </summary>
    public partial class RegisterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 60 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimize;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox signup_username;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox signup_password;
        
        #line default
        #line hidden
        
        
        #line 202 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox signup_email;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox signup_lastname;
        
        #line default
        #line hidden
        
        
        #line 254 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox signup_firstname;
        
        #line default
        #line hidden
        
        
        #line 281 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox signup_isadmin;
        
        #line default
        #line hidden
        
        
        #line 292 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox signup_adminKey;
        
        #line default
        #line hidden
        
        
        #line 310 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button signup_btn;
        
        #line default
        #line hidden
        
        
        #line 359 "..\..\..\ViewWindows\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock signup_loginBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/StackLeader;component/viewwindows/registerwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ViewWindows\RegisterWindow.xaml"
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
            
            #line 9 "..\..\..\ViewWindows\RegisterWindow.xaml"
            ((StackLeader.ViewWindows.RegisterWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnMinimize = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\ViewWindows\RegisterWindow.xaml"
            this.btnMinimize.Click += new System.Windows.RoutedEventHandler(this.btnMinimize_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\..\ViewWindows\RegisterWindow.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.signup_username = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.signup_password = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.signup_email = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.signup_lastname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.signup_firstname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.signup_isadmin = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.signup_adminKey = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 11:
            this.signup_btn = ((System.Windows.Controls.Button)(target));
            
            #line 318 "..\..\..\ViewWindows\RegisterWindow.xaml"
            this.signup_btn.Click += new System.Windows.RoutedEventHandler(this.signup_btn_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.signup_loginBtn = ((System.Windows.Controls.TextBlock)(target));
            
            #line 360 "..\..\..\ViewWindows\RegisterWindow.xaml"
            this.signup_loginBtn.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.signup_loginBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

