﻿#pragma checksum "..\..\..\ViewWindows\Employees.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4F4656BD18573AC084BD244C537EC95D5E164A8333361BCB248449E31266D0B6"
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
    /// Employees
    /// </summary>
    public partial class Employees : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 41 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border MainBorder;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridAddEmpl;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addEmployee_id;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addEmployee_lastname;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addEmployee_firstname;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addEmployee_email;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addEmployee_username;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addEmployee_password;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\ViewWindows\Employees.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add_Employee_btn;
        
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
            System.Uri resourceLocater = new System.Uri("/StackLeader;component/viewwindows/employees.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ViewWindows\Employees.xaml"
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
            this.MainBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.dataGridAddEmpl = ((System.Windows.Controls.DataGrid)(target));
            
            #line 49 "..\..\..\ViewWindows\Employees.xaml"
            this.dataGridAddEmpl.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dataGridAddEmpl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.addEmployee_id = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.addEmployee_lastname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.addEmployee_firstname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.addEmployee_email = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.addEmployee_username = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.addEmployee_password = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 88 "..\..\..\ViewWindows\Employees.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_Back);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Add_Employee_btn = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\..\ViewWindows\Employees.xaml"
            this.Add_Employee_btn.Click += new System.Windows.RoutedEventHandler(this.Click_Add_View_Employee);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

